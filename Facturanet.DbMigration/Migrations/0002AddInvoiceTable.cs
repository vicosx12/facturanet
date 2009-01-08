using System;
using Migrator.Framework;
using System.Data;
using Facturanet.Util;

namespace Facturanet.DbMigration.Migrations
{
    [Migration(2)]
    public class AddInvoiceTable : Migration
    {
        public override void Up()
        {
            Database.AddTable("Invoice", new Column[]
            {
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("Number", DbType.String, 30, ColumnProperty.NotNull),
                new Column("IdCustomer", DbType.Guid)
            });

            Database.AddForeignKey("fk_Invoice_Customer", "Invoice", "IdCustomer", "Customer", "Id");
        }

        public override void Down()
        {
            //Database.RemoveForeignKey("Invoice", "fk_Invoice_Customer");
            Database.RemoveTable("Invoice");
        }
    }
}