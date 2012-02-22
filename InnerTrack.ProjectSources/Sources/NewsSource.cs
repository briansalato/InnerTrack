using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Interfaces.Database;
using InnerTrack.Common.Objs;
using InnerTrack.Common.Interfaces.Data;
using InnerTrack.ProjectSources.Repository;
using System.Configuration;
using InnerTrack.Common.Objs.Data;

namespace InnerTrack.ProjectSources.Sources
{
    public class NewsSource : IProjectSource
    {    
        public IList<SourceEntryObj>  GetAllEntries()
        {
            return GetEntriesSince(DateTime.MinValue);
        }

        public IList<SourceEntryObj>  GetEntriesSince(DateTime lastUpdate)
        {
            using (var db = GetNewsFeedEntities())
            {
                var entries =
                    (from nfe in db.NewsFeedEntries
                     where nfe.NewsFeedId == NewsFeedId
                           && (nfe.UpdatedOn ?? nfe.CreatedOn) > lastUpdate
                     select new SourceEntryObj()
                     {
                         Id = nfe.Id,
                         Title = nfe.Title,
                         Message = nfe.Message
                     }
                ).ToList();

                return entries;
            }
        }

        public SourceEntryObj  GetEntry(int localId)
        {
            using (var db = GetNewsFeedEntities())
            {
                var entry =
                    (from nfe in db.NewsFeedEntries
                     where nfe.NewsFeedId == NewsFeedId
                            && nfe.Id == localId
                     select new SourceEntryObj()
                     {
                         Id = nfe.Id,
                         Title = nfe.Title,
                         Message = nfe.Message
                     }
                ).FirstOrDefault();

                return entry;
            }
        }

        public int AddEntry(SourceEntryObj e, string username)
        {
            using (var db = GetNewsFeedEntities())
            {
                var dbEntry = new NewsFeedEntry
                {
                    Title = e.Title,
                    Message = e.Message,
                    CreatedOn = DateTime.Now,
                    CreatedBy = username
                };

                db.NewsFeedEntries.AddObject(dbEntry);

                db.SaveChanges();

                return dbEntry.Id;
            }
        }

        public bool UpdateEntry(SourceEntryObj e, string username)
        {
            using (var db = GetNewsFeedEntities())
            {
                var dbEntry = db.NewsFeedEntries.SingleOrDefault(nfe => nfe.Id == e.Id);
                if (dbEntry == null)
                {
                    return false;
                }

                dbEntry.Title = e.Title;
                dbEntry.Message = e.Message;
                dbEntry.UpdatedOn = DateTime.Now;
                dbEntry.UpdatedBy = username;

                db.SaveChanges();

                return true;
            }
        }

        public void Configure(string configString)
        {
            NewsFeedId = int.Parse(configString);
        }

        #region -Private Methods
        private NewsFeedEntities GetNewsFeedEntities()
        {
            return new NewsFeedEntities(ConfigurationManager.ConnectionStrings["NewsFeedEntities"].ConnectionString);
        }
        #endregion

        #region -Properties
        private int? NewsFeedId { get; set; }
        #endregion
    }
}
