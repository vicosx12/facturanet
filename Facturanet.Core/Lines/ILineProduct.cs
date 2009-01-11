using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Facturanet.Lines
{
    public interface ILineProduct
    {
        Guid Id { get; set; }
        string Code { get; set; }
        bool Active { get; set; }
        string Name { get; set; }
        double Taxes { get; set; }
    }
}




