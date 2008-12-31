using System;
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

            if (requestType == typeof(Server.CompositeRequest))
                processor = new Server.CompositeProcessor();
            else if (requestType == typeof(Business.GetProductsRequest))
                processor = new Business.GetProductsProcessor();
            else if (requestType == typeof(Infrastructure.SystemInfoRequest))
                processor = new Infrastructure.SystemInfoProcessor();
            else if (requestType == typeof(Business.RemoveProductRequest))
                processor = new Business.RemoveProductProcessor();
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
