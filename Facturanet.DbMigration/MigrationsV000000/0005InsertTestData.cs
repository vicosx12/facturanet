﻿using System;
using Migrator.Framework;
using System.Data;
using Facturanet.Util;

namespace Facturanet.DbMigration.Migrations
{
    [Migration(5)]
    public class InsertTestData : Migration
    {
        public override void  Up()
        {
            Database.Insert(
                "Product",
                new string[] 
                { 
                    "Id", 
                    "Code", 
                    "Name", 
                    "Taxes" 
                },
                new string[] 
                { 
                    IdentifierHelper.GenerateComb().ToString(), 
                    "CODIGOPRODUCTO",
                    "Prueba", 
                    "15"
                }
            );
        }

        public override void Down()
        {
        }
    }
}