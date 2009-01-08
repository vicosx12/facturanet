using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet;
using Facturanet.Entities;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Facturanet.Server
{
    internal static class NHibernateHelper
    {
        public static readonly ISessionFactory SessionFactory;
        
        internal static readonly System.Reflection.Assembly Assembly = typeof(NHibernateHelper).Assembly;
        internal static readonly string ConfigNH = Assembly.CodeBase + ".xml"; 

        static NHibernateHelper()
        {
            Console.WriteLine("CREANDO FACTORY");
            Configuration cfg = new Configuration();
            cfg.Configure(ConfigNH);
            cfg.AddAssembly(Assembly);
            //CrearEstructuraDB(cfg);
            SessionFactory = cfg.BuildSessionFactory();
            //CrearDatosDB();
        }

        public static void ForceInit()
        {
            Console.WriteLine(SessionFactory);
        }

        /*
        private static void CrearEstructuraDB(Configuration config)
        {
            SchemaExport exporter = new SchemaExport(config);
            exporter.Create(true, true); //borra y crea nuevamente las tablas en la base.
        }

        private static void CrearDatosDB()
        {
            ISession session = SessionFactory.OpenSession();

            Customer c1 = new Customer() { Name = "Andrés Moschini" };
            session.Save(c1);
            Customer c2 = new Customer() { Name = "Juan Perez" };
            session.Save(c2);

            Product a1 = new Product() { Name = "Arroz", Taxes = 0.2M };
            session.Save(a1);
            Product a2 = new Product() { Name = "Queso", Taxes = 0.3M };
            session.Save(a2);


            Invoice f1 = new Invoice()
            {
                Customer = c1,
                Number = "Factura001"
                
                //,Items = 
                //{
                //    new ItemFactura() { Articulo = a1, Cantidad=5, PrecioBase=10 },
                //    new ItemFactura() { Articulo = a1, Cantidad=1, PrecioBase=20 },
                //    new ItemFactura() { Articulo = a2, Cantidad=5, PrecioBase=10 }
                //}
                
            };

            f1.Items.Add(new InvoiceItem() { Product = a1, Quantity = 5, BasePrice = 10, Invoice = f1 });
            f1.Items.Add(new InvoiceItem() { Product = a1, Quantity = 1, BasePrice = 20, Invoice = f1 });
            f1.Items.Add(new InvoiceItem() { Product = a2, Quantity = 5, BasePrice = 10, Invoice = f1 });

            session.Save(f1);

            session.Close();
        }
         * */
    }
}
