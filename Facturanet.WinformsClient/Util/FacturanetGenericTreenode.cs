﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facturanet.Util;
using Facturanet.UI;

namespace Facturanet.WinformsClient.Util
{
    public class FacturanetGenericTreenode<T> : Util.FacturanetBaseTreenode<T>
        where T : class
    {
        public FacturanetGenericTreenode(T asociatedObject)
            : base(asociatedObject)
        {
        }

        public Func<T, string> GetNodeNameFunc { private get; set; }
        public Func<T, string> GetNodeTextFunc { private get; set; }
        public Func<T, string> GetNodeToolTipTextFunc { private get; set; }

        protected override string GetNodeName()
        {
            if (GetNodeNameFunc == null)
                return string.Empty;
            else
                return GetNodeNameFunc(TypedAsociatedObject);
        }

        protected override string GetNodeText()
        {
            if (GetNodeTextFunc == null)
                return string.Empty;
            else
                return GetNodeTextFunc(TypedAsociatedObject);
        }

        protected override string GetNodeToolTipText()
        {
            if (GetNodeToolTipTextFunc == null)
                return string.Empty;
            else
                return GetNodeToolTipTextFunc(TypedAsociatedObject);
        }
    }
}
