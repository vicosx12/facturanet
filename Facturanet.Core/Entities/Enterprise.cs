﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Facturanet.Entities
{
    
    public class Enterprise : Entity
    {
        public virtual string Code { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Name { get; set; }
    }
}