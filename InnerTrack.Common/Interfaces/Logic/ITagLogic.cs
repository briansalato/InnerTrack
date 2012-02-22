using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Objs;

namespace InnerTrack.Common.Interfaces.Logic
{
    public interface ITagLogic
    {
        IList<TagObj> GetForProject(int projectId);
    }
}
