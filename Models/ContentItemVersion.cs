using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windsong.VersionManager.Models
{
    public class ContentItemVersion
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Identifier { get; set; }
        public string ModifiedDate { get; set; }
        public string PublishedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsPublished { get; set; }   
    }
}