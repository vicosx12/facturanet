using System;
using Migrator.Framework;
using System.Data;
using Facturanet.Util;

namespace Facturanet.DbMigration.Migrations
{
    [Migration(20010101010140)]
    public class AddInvoiceTable : Migration
    {
        public override void Up()
        {
            Database.AddTable("Invoice", new Column[]
            {
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("IdEnterprise", DbType.Guid),
                new Column("FiscalType", DbType.String, 30, ColumnProperty.NotNull),
                new Column("Number", DbType.String, 30, ColumnProperty.NotNull),
                new Column("Date", DbType.DateTime, ColumnProperty.NotNull),
                new Column("IdCustomer", DbType.Guid)
            });

            //TODO: agregar la restriccion de unique para IdEnterprise junto con Number
            Database.AddForeignKey("fk_Invoice_Customer", "Invoice", "IdCustomer", "Customer", "Id");
            Database.AddForeignKey("fk_Invoice_Enterprise", "Invoice", "IdEnterprise", "Enterprise", "Id");

            Database.AddTable("InvoiceItem", new Column[]
            {
                new Column("IdInvoice", DbType.Guid),
                new Column("InvoiceLine", DbType.Int32, ColumnProperty.NotNull),
                new Column("IdProduct", DbType.Guid),
                new Column("Quantity", DbType.Decimal, ColumnProperty.NotNull),
                new Column("Price", DbType.Decimal, ColumnProperty.NotNull),
            });
            //TODO: hacer el primary key con IdInvoice e Id
            Database.AddForeignKey("fk_InvoiceItem_Product", "InvoiceItem", "IdProduct", "Product", "Id");
            Database.AddForeignKey("fk_InvoiceItem_Invoice", "InvoiceItem", "IdInvoice", "Invoice", "Id");
        }

        public override void Down()
        {
            Database.RemoveTable("InvoiceItem");
            Database.RemoveTable("Invoice");
        }
    }
}