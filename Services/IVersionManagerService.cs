using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windsong.VersionManager.Models;

namespace Windsong.VersionManager.Services
{
    public interface IVersionManagerService : IDependency
    {
        IEnumerable<ContentItemVersionInfo> GetVersionList(int id);

        int BuildNewContentItemVersion(ContentItem item, bool asPublished);
        int BuildNewContentTypeVersion(ContentTypeVersion item);
    }
}