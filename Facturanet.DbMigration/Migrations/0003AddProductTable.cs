using System;
using Migrator.Framework;
using System.Data;
using Facturanet.Util;

namespace Facturanet.DbMigration.Migrations
{
    [Migration(3)]
    public class AddProductTable : Migration
    {
        public override void Up()
        {
            Database.AddTable("Product", new Column[]
            {
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("Name", DbType.String, 100, ColumnProperty.NotNull),
                new Column("Taxes", DbType.Decimal, ColumnProperty.NotNull)
            });
        }

        public override void Down()
        {
            Database.RemoveTable("Product");
        }
    }
}