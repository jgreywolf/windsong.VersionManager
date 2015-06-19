using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData.Models;
using System.Collections.Generic;
using Windsong.VersionManager.Models;

namespace Windsong.VersionManager.Services
{
    public interface IVersionManagerService : IDependency
    {
        IEnumerable<ContentItemVersion> GetContentItemVersionList(int id);
        int BuildNewContentItemVersion(ContentItem item, bool asPublished);
    }
}