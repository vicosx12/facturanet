using System;
using Migrator.Framework;
using System.Data;
using Facturanet.Util;

namespace Facturanet.DbMigration.Migrations
{
    [Migration(4)]
    public class AddInvoiceItemTable : Migration
    {
        public override void Up()
        {
            Database.AddTable("InvoiceItem", new Column[]
            {
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("Cuantity", DbType.Decimal, ColumnProperty.NotNull),
                new Column("BasePrice", DbType.Decimal, ColumnProperty.NotNull),
                new Column("IdProduct", DbType.Guid),
                new Column("IdInvoice", DbType.Guid)
            });

            Database.AddForeignKey("fk_InvoiceItem_Product", "InvoiceItem", "IdProduct", "Product", "Id");
            Database.AddForeignKey("fk_InvoiceItem_Invoice", "InvoiceItem", "IdInvoice", "Invoice", "Id");
        }

        public override void Down()
        {
            //
            Database.RemoveTable("InvoiceItem");
        }
    }
}