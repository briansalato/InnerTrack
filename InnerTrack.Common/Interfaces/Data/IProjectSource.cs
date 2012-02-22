using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Objs.Data;

namespace InnerTrack.Common.Interfaces.Data
{
    public interface IProjectSource
    {
        IList<SourceEntryObj> GetAllEntries();
        IList<SourceEntryObj> GetEntriesSince(DateTime lastUpdate);
        SourceEntryObj GetEntry(int localId);
        int AddEntry(SourceEntryObj e, string username);
        bool UpdateEntry(SourceEntryObj e, string username);
        void Configure(string configString);
    }
}
