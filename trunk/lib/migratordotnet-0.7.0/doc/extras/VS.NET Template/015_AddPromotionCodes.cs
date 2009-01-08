using System.Data;
using Migrator.Framework;

namespace $rootnamespace$
{
    [$rootnamespace$(0)]
    public class $fileinputname$ : Migrator.Framework.$rootnamespace$
    {
        private const string TBL = "TableName";

        public override void Up()
        {
            Database.AddTable(TBL, new Column[]
                                       {
                                           new Column("Id", DbType.Int32,ColumnProperty.PrimaryKeyWithIdentity),
                                       });
        }

        public override void Down()
        {
            Database.RemoveTable(TBL);
        }
    }
}