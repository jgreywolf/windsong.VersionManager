using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windsong.VersionManager.Models;

namespace Windsong.VersionManager
{
    public class Migrations : DataMigrationImpl {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create() {

            // Creating table BodyPartVersionRecord
            SchemaBuilder.CreateTable("BodyPartVersionRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<int>("ContentItemRecord_id", c => c.NotNull())
                .Column<int>("ContentItemVersionRecord_id", c => c.NotNull())
                .Column<string>("Text")
                .Column<string>("Format")
            );

            SchemaBuilder.AlterTable("BodyPartVersionRecord",
                table => table
                    .CreateIndex("IDX_ContentItemRecord_id", "ContentItemRecord_id")
                );

            return 1;
        }
    }
}
