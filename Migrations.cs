using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windsong.VersionManager
{
    public class Migrations : DataMigrationImpl {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create() {
			// Creating table ContentDefinitionVersionRecord
            SchemaBuilder.CreateTable("ContentTypeVersionRecord", table => table
				.Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<string>("Name")
                .Column<int>("Number")
                .Column<string>("Parts", c => c.Unlimited())
                .Column<string>("Settings", c => c.Unlimited())
                .Column<int>("ContentTypeRecord_id", c => c.NotNull())
			);

            SchemaBuilder.AlterTable("ContentTypeVersionRecord",
                table => table
                    .CreateIndex("IDX_ContentTypeRecord_id", "ContentTypeRecord_id")
                );


            foreach (var typeDefinition in _contentDefinitionManager.ListTypeDefinitions())
            {
                
            }

            return 1;
        }
    }
}
