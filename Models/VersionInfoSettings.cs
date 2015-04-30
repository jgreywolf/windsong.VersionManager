using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace Windsong.VersionManager.Models
{
    public class VersionInfoSettings : ContentPart
    {
        public string ModifiedBy
        {
            get { return this.Retrieve(p => p.ModifiedBy); }
            set { this.Store(p => p.ModifiedBy, value); }
        }
    }
}