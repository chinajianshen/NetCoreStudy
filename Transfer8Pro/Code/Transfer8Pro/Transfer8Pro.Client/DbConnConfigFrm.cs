using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transfer8Pro.Client.Controls;
using Transfer8Pro.Client.Core;

namespace Transfer8Pro.Client
{
    public partial class DbConnConfigFrm : Form
    {
        public DbConnConfigFrm()
        {
            InitializeComponent();
        }

        public string GetConnString
        {
            get;private set;
        }

        private DbTypes _DbType { get; set; }


        private string _ConnString { get; set; }

        public DbConnConfigFrm(DbTypes dbType,string connString) : this()
        {

            _DbType = dbType;
            _ConnString = connString;          
        }

        public bool CheckIsUseable()
        {
            if (_DbType == DbTypes.Oledb)
            {
                return false;
            }

            return true;
        }

        private void DbConnConfigFrm_Load(object sender, EventArgs e)
        {
            pnlConnSource.Controls.Clear();
            UserControl userCtrl = null;
            switch (_DbType)
            {
                case DbTypes.Sqlserver:
                    userCtrl = new SqlServerCtrl(_ConnString);
                    break;
                case DbTypes.Oracle:
                    userCtrl = new OracleCtrl(_ConnString);
                    break;
                case DbTypes.MySql:
                    userCtrl = new MySqlCtrl(_ConnString);
                    break;
                case DbTypes.Oledb:
                    userCtrl = new OledbCtrl(_ConnString);
                    break;
                default:
                    MessageBox.Show("未实现该类型数据源配置");
                    return;
            }

            userCtrl.Name = "DbSourceCtrl";
            pnlConnSource.Controls.Add(userCtrl);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            IDbConnBase connBase = GetDbConnCtrl();
            if (connBase == null)
            {
                MessageBox.Show("用户控件异常");
                return;
            }
            if (!connBase.ValidateData())
            {
                return;
            }

            GetConnString = connBase.GetConnString();
            this.DialogResult = DialogResult.OK;
            this.Hide();
        } 
        
        private IDbConnBase GetDbConnCtrl()
        {
            Control[] ctrls = pnlConnSource.Controls.Find("DbSourceCtrl",false);
            if (ctrls.Length > 0)
            {
                return ctrls[0] as IDbConnBase;
            }
            return null;
        }
    }
}
