﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using System.Reflection;

namespace Facturanet.Server
{
    internal class PersistenceProcessorFactory : IProcessorFactory
    {
        public IProcessor CreateProcessor(Type requestType)
        {
            IProcessor processor;

            //TODO: Esto lo tendria que hacer con atributos y reflection
            if (requestType == typeof(Server.CompositeRequest))
                processor = new Server.CompositeProcessor();
            else if (requestType == typeof(Business.ListProductsRequest))
                processor = new Business.ListProductsProcessor();
            else if (requestType == typeof(Infrastructure.SystemInfoRequest))
                processor = new Infrastructure.SystemInfoProcessor();
            else if (requestType == typeof(Test.GenerateTestDataRequest))
                processor = new Test.GenerateTestDataProcessor();
            else if (requestType == typeof(Business.ListCustomersRequest))
                processor = new Business.ListCustomersProcessor();
            else if (requestType == typeof(Business.ListInvoicesRequest))
                processor = new Business.ListInvoicesProcessor();
            else if (requestType == typeof(Business.ListAccountTreesRequest))
                processor = new Business.ListAccountTreesProcessor();
            else if (requestType == typeof(Business.CreateAccountTreeRequest))
                processor = new Business.CreateAccountTreeProcessor();
            else processor = null;

            return processor;
        }

        public void ForceInit()
        {
            Console.WriteLine("ForzarInicializacion del Factory DB");
             NHibernateHelper.ForceInit();
        }
    }
}
