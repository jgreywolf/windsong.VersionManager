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

        public string User
        {
            get { return this.Retrieve(p => p.User); }
            set { this.Store(p => p.User, value); }
        }
    }
}