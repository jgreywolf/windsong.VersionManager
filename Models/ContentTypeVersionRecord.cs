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
        public virtual int Number { get; set; }
        [StringLengthMax]
        public virtual string Parts { get; set; }
        [StringLengthMax]
        public virtual string Settings { get; set; }
        public virtual int ContentTypeRecord_id { get; set; }

    }
}