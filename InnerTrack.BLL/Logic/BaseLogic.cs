using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Interfaces.Database;
using System.Diagnostics.CodeAnalysis;

namespace InnerTrack.BLL.Logic
{
    public abstract class BaseLogic
    {
        [ExcludeFromCodeCoverage]
        public BaseLogic(IInnerTrackRepository repository)
        {
            Repository = repository;
        }

        protected IInnerTrackRepository Repository { get; set; }
    }
}
