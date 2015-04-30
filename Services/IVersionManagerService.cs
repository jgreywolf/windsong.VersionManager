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
        IEnumerable<ContentTypeVersion> GetContentTypeVersionList(string id);

        int BuildNewContentItemVersion(ContentItem item, bool asPublished);
        void SaveContentTypeVersion(ContentTypeDefinition item);

        ContentTypeVersionRecord GetContentTypeVersion(string name, int version = 0);
        int GetMaxContentTypeVersionNumber(string name);
    }
}