using System;
using Migrator.Framework;
using System.Data;
using Facturanet.Util;

namespace Facturanet.DbMigration.Migrations
{
    /*
    [Migration(20090128181800)]
    public class InsertAccountTreeTestData : Migration
    {
        public override void  Up()
        {
            int IdContableConfigurationSample = IdentifierHelper.GenerateComb().ToString();

            Database.Insert(
                "ContableConfiguration",
                new string[] 
                { 
                    "Id", 
                    "Code", 
                    "Name", 
                    "Description",
                    "IdAccountTree",
                    "IdDefaultAccount",
                        
                },
                new string[] 
                { 
                    IdContableConfigurationSample, 
                    "CODIGOPRODUCTO",
                    "Prueba", 
                    "Configuración contable de prueba"
                }
            );

        }

        public override void Down()
        {
        }
    }
     * */
}
