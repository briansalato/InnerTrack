
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using InnerTrack.Common.Interfaces.Data;
using InnerTrack.Common.Objs.Data;
using System.Transactions;

namespace InnerTrack.Processes
{
    /// <summary>
    /// This will read through our project sources and run the ones that are set to run
    /// TODO - This currently go through each project source one at a time, but it would be better to bulk read by ProjectSourceType
    /// </summary>
    public class ProcessProjectSources 
    {
        public static void Main(string[] args)
        {
            new ProcessProjectSources();
        }

        public ProcessProjectSources()
        {
            //We need to go through each type to see what needs to run. We open and close the connection since there are some long
            //calls before we hit up the DB again
            var configsToProcess = GetTypesThatNeedToRun();
            foreach (var config in configsToProcess)
            {
                var domain = AppDomain.CurrentDomain.Load(config.AssemblyName);
                Type type = domain.GetType(config.FullClassName);

                //we will only use only instance of the project source adapter
                IProjectSource projectSource = Activator.CreateInstance(type) as IProjectSource;
                if (projectSource == null)
                {
                    //if we couldnt initialize the type then the string is probably wrong so throw an error
                    throw new Exception();
                }

                //go through each instance for that adapter and call to get data and save it
                //pull back all of the data before writing any to the db so we dont have to keep a transaction open and
                //we dont risk writing part of the data but not all of it
                //a dictionary is kept so we can associate our projectSourceId to its entries
                var entriesFromSource = new Dictionary<int, IEnumerable<SourceEntryObj>>();
                foreach (var sourceInstance in config.ProjectSources)
                {
                    projectSource.Configure(sourceInstance.Configuration);
                    entriesFromSource.Add(sourceInstance.Id, projectSource.GetEntriesSince(config.LastRun ?? DateTime.MinValue));
                }

                using (var db = GetDb())
                {
                    using (var transaction = new TransactionScope())
                    {
                        //go through each source
                        foreach (var source in entriesFromSource)
                        {
                            //go through each entry for that source
                            foreach (var entry in source.Value)
                            {
                                //check and see if an entry already exists for that Id. If it does then update it, otherwise create a new one
                                ProjectSourceEntry dbEntry = db.ProjectSourceEntries.SingleOrDefault(e => e.SourceEntryId == entry.Id);
                                if (dbEntry == null)
                                {
                                    dbEntry = new ProjectSourceEntry
                                    {
                                        SourceEntryId = entry.Id.Value,
                                        ProjectSourceId = source.Key,
                                        CreatedBy = System.Environment.UserName,
                                        CreatedOn = DateTime.Now
                                    };

                                    db.ProjectSourceEntries.AddObject(dbEntry);
                                }
                                else
                                {
                                    dbEntry.UpdatedBy = System.Environment.UserName;
                                    dbEntry.UpdatedOn = DateTime.Now;
                                }

                                dbEntry.Title = entry.Title;
                                dbEntry.Message = entry.Message;
                                db.SaveChanges();
                            }
                        }

                        //regrab the config since we already closed the connection that grabbed the config
                        var configToUpdate = db.ProjectSourceTypes.Single(p => p.Id == config.Id);
                        configToUpdate.LastRun = DateTime.Now;
                        configToUpdate.NextRun = configToUpdate.LastRun.Value.AddMinutes(configToUpdate.Interval);
                        db.SaveChanges();

                        transaction.Complete();
                    }
                }
            }
        }

        private IList<ProjectSourceType> GetTypesThatNeedToRun()
        {
            using (var db = GetDb())
            {
                return db.ProjectSourceTypes.Include("ProjectSources").Where(p => p.Enabled && p.NextRun < DateTime.Now).ToList();
            }
        }

        private ProjectSourceDbContainer GetDb()
        {
            return new ProjectSourceDbContainer(ConfigurationManager.ConnectionStrings["ProjectSourceDbContainer"].ConnectionString);
        }
    }
}
