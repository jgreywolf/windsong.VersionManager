using Orchard.Data.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windsong.VersionManager.Models
{
    public class ContentTypeVersionRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Version { get; set; }
        public virtual string ModifiedBy { get; set; }
        [StringLengthMax]
        public virtual string Data { get; set; }
    }
}