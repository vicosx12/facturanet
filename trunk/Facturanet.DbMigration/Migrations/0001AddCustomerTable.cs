using System;
using Migrator.Framework;
using System.Data;
using Facturanet.Util;

namespace Facturanet.DbMigration.Migrations
{
    [Migration(1)]
    public class AddCustomerTable : Migration
    {
        public override void Up()
        {
            Database.AddTable("Customer", new Column[]
            {
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("Name", DbType.String, 100, ColumnProperty.NotNull)
            });
        }

        public override void Down()
        {
            Database.RemoveTable("Customer");
        }
    }
}
