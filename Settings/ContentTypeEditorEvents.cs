using System.Collections.Generic;
using System.Globalization;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using Orchard.ContentTypes.Services;
using Orchard.Data;
using Windsong.VersionManager.Models;

namespace Windsong.VersionManager.Settings
{
    public class ContentTypeVersionEditorEvents : ContentDefinitionEditorEventsBase
    {
        //private readonly IContentDefinitionService _contentDefinitionService;
        //private readonly IRepository<ContentTypeVersionRecord> _repository;

        //public ContentTypeVersionEditorEvents(
        //    IContentDefinitionService contentDefinitionService,
        //    IRepository<ContentTypeVersionRecord> repository)
        //{
        //    _contentDefinitionService = contentDefinitionService;
        //    _repository = repository;
        //}

        //public override void TypeEditorUpdating(ContentTypeDefinitionBuilder definition)
        //{
        //    var type = definition.Current;
        //    var versionRecord = new ContentTypeVersionRecord();
        //    _repository.Create(versionRecord);
        //}
    }
}