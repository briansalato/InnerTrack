using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Objs;

namespace InnerTrack.Common.Interfaces.Database
{
    public interface IFeedIO
    {
        IList<EventObj> GetAllEvents();
        IList<EventObj> GetEventsSince(DateTime lastUpdate);
        int AddEvent(EventObj e, string username);
        bool UpdateEvent(EventObj e, string username);
    }
}