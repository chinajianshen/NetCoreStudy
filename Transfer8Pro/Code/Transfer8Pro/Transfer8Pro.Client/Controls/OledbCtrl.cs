using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transfer8Pro.Client.Core;

namespace Transfer8Pro.Client.Controls
{
    public partial class OledbCtrl : UserControl, IDbConnBase
    {
        public OledbCtrl()
        {
            InitializeComponent();
        }

        public OledbCtrl(string connString)
        {

        }

        public string GetConnString()
        {
            throw new NotImplementedException();
        }

        public bool ValidateData()
        {
            throw new NotImplementedException();
        }
    }
}
