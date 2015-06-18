using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace Windsong.VersionManager.Models
{
    public class ReadOnlySettings : ContentPart
    {
        public bool ReadOnly
        {
            get { return this.Retrieve(p => p.ReadOnly); }
            set { this.Store(p => p.ReadOnly, value); }
        }

        public string ModifiedBy
        {
            get { return this.Retrieve(p => p.ModifiedBy); }
            set { this.Store(p => p.ModifiedBy, value); }
        }
        public string ModifiedDate
        {
            get { return this.Retrieve(p => p.ModifiedDate); }
            set { this.Store(p => p.ModifiedDate, value); }
        }
    }
}