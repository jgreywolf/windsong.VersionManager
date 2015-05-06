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
        private readonly IContentDefinitionWriter _contentDefinitionWriter;
        private readonly IContentDefinitionReader _contentDefinitionReader;
        private readonly IRepository<ContentItemVersionRecord> _contentItemVersionRepository;
        private readonly IRepository<ContentTypeVersionRecord> _contentTypeVersionRepository;
        private readonly IVersionUtilities _versionUtilities;

        public VersionManagerService(
            IContentManager contentManager,
            IContentDefinitionWriter contentDefinitionWriter,
            IContentDefinitionReader contentDefinitionReader,
            IRepository<ContentItemVersionRecord> contentItemVersionRepository,
            IRepository<ContentTypeVersionRecord> contentTypeVersionRepository,
            IVersionUtilities versionUtilities)
        {
            _contentManager = contentManager;
            _contentDefinitionWriter = contentDefinitionWriter;
            _contentDefinitionReader = contentDefinitionReader;
            _contentItemVersionRepository = contentItemVersionRepository;
            _contentTypeVersionRepository = contentTypeVersionRepository;
            _versionUtilities = versionUtilities;
        }

        public IEnumerable<ContentItemVersion> GetContentItemVersionList(int id)
        {
            var versions = _contentManager.GetAllVersions(id);
            var list = (from version in versions
                        let modifiedBy = version.Has<VersionInfoSettings>() ? version.As<VersionInfoSettings>().ModifiedBy : String.Empty
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
                            IsPublished = version.VersionRecord.Published
                        }).ToList();

            return list.OrderByDescending(x => x.Version);
        }

        public IEnumerable<ContentTypeVersion> GetContentTypeVersionList(string id)
        {
            var versions = _contentTypeVersionRepository.Table.Where(x => x.Name == id);
            var list = new List<ContentTypeVersion>();

            foreach (var version in versions) {
                var typeXml = XElement.Parse(version.Data);
                var builder = new ContentTypeDefinitionBuilder();
                _contentDefinitionReader.Merge(typeXml, builder);

                var type = new ContentTypeVersion {
                    Version = version.Version,
                    DisplayName = version.Name,
                    TypeDefinition = builder.Current
                };
                list.Add(type);
            }

            return list.OrderByDescending(x => x.Version);
        }

        public int BuildNewContentItemVersion(ContentItem item, bool asPublished)
        {
            var contentItemRecord = item.Record;

            var newItemVersionRecord = new ContentItemVersionRecord
            {
                ContentItemRecord = contentItemRecord,
                Latest = true,
                Published = asPublished,
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

            var newContentItem = _contentManager.New(item.ContentType);
            newContentItem.VersionRecord = newItemVersionRecord;

            return newContentItem.Version;
        }

        public void SaveContentTypeVersion(ContentTypeDefinition type)
        {
            var exportedType = _contentDefinitionWriter.Export(type);
            var newRecord = new ContentTypeVersionRecord
            {
                Name = type.DisplayName ?? String.Empty,
                Version = GetMaxContentTypeVersionNumber(type.Name) + 1,
                ModifiedBy = _versionUtilities.GetUser(),
                Data = exportedType.ToString(SaveOptions.DisableFormatting)
            };
            _contentTypeVersionRepository.Create(newRecord);
        }

        public ContentTypeVersionRecord GetContentTypeVersion(string name, int version)
        {
            return _contentTypeVersionRepository.Table.SingleOrDefault(x => x.Name == name && x.Version == version);
        }

        public int GetMaxContentTypeVersionNumber(string name)
        {
            return 0;// _contentTypeVersionRepository.Table.MaxBy(x => x.Name == name).Version;
        }
    }
}