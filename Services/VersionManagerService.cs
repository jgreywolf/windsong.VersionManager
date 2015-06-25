using System.Globalization;
//using MoreLinq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.Records;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Windsong.VersionManager.Models;

namespace Windsong.VersionManager.Services
{
    public class VersionManagerService : IVersionManagerService
    {
        private readonly IContentManager _contentManager;
        private readonly IRepository<ContentItemVersionRecord> _contentItemVersionRepository;
        private readonly IVersionUtilities _versionUtilities;

        public VersionManagerService(
            IContentManager contentManager,
            IRepository<ContentItemVersionRecord> contentItemVersionRepository,
            IVersionUtilities versionUtilities)
        {
            _contentManager = contentManager;
            _contentItemVersionRepository = contentItemVersionRepository;
            _versionUtilities = versionUtilities;
        }

        public IEnumerable<ContentItemVersion> GetContentItemVersionList(int id)
        {
            var versions = _contentManager.GetAllVersions(id).OrderByDescending(x=>x.Version);
            var list = (from version in versions
                        let modifiedBy = version.Has<VersionInfoSettings>() ? (String.IsNullOrWhiteSpace(version.As<VersionInfoSettings>().ModifiedBy) ? "Unknown" : version.As<VersionInfoSettings>().ModifiedBy) : (String.IsNullOrWhiteSpace(version.As<CommonPart>().Owner.UserName) ? "Unknown" : version.As<CommonPart>().Owner.UserName)
                        let commonPart = version.As<CommonPart>()
                        let title = version.Has<TitlePart>() ? version.As<TitlePart>().Title : String.Empty
                        let identifier = version.Has<IdentityPart>() ? version.As<IdentityPart>().Identifier : String.Empty
                        select new ContentItemVersion
                        {
                            Version = version.Version.ToString(CultureInfo.InvariantCulture),
                            ModifiedDate = commonPart.VersionModifiedUtc.ToString(),
                            PublishedDate = commonPart.VersionPublishedUtc.ToString(),
                            ModifiedBy = modifiedBy,
                            Title = title,
                            Identifier = identifier,
                            IsPublished = version.VersionRecord.Published,
                        }).ToList();

            return list;
        }

        public int BuildNewContentItemVersion(ContentItem versionToPromote)
        {
            var readOnlySettings = versionToPromote.Has<ReadOnlySettings>()
                ? versionToPromote.As<ReadOnlySettings>()
                : new ReadOnlySettings(){ReadOnly = false};

            if (readOnlySettings.ReadOnly)
            {
                return 0;
            }

            var contentItemRecord = versionToPromote.Record;

            var newItemVersionRecord = new ContentItemVersionRecord
            {
                ContentItemRecord = contentItemRecord,
                Latest = true,
                Published = false,
                Data = contentItemRecord.Data,
            };

            var latestVersion = contentItemRecord.Versions.SingleOrDefault(x => x.Latest);

            if (latestVersion != null)
            {
                latestVersion.Latest = false;
                newItemVersionRecord.Number = latestVersion.Number + 1;
            }
            else
            {
                newItemVersionRecord.Number = contentItemRecord.Versions.Max(x => x.Number) + 1;
            }

            contentItemRecord.Versions.Add(newItemVersionRecord);
            _contentItemVersionRepository.Create(newItemVersionRecord);

            var newContentItem = _contentManager.New(versionToPromote.ContentType);
            newContentItem.VersionRecord = newItemVersionRecord;

            return newContentItem.Version;
        }
    }
}