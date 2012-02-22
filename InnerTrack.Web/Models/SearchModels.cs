using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace InnerTrack.Web.Models
{
    public class BasicSearchModel
    {
        [Description("Search")]
        public string Query { get; set; }
        public IList<BasicSearchResultModel> Results { get; set; }
        public string ModelName { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPriorResults { get; set; }
        public bool HasMoreResults { get; set; }
    }

    public class BasicSearchResultModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}