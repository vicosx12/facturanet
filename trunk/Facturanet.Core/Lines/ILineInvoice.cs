using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Facturanet.Lines
{
    public interface ILineInvoice 
    {
        Guid Id { get; set; }
        string EnterpriseCode { get; }
        string FiscalType { get; set; }
        string Number { get; set; }
        DateTime Date { get; set; }
        string CustomerCode { get; }
        string CustomerName { get; }
        double Total { get; }
    }
}




