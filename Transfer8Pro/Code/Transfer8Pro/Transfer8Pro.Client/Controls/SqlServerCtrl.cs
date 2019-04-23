using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transfer8Pro.Client.Core;
using Transfer8Pro.Utils;
using System.Text.RegularExpressions;

namespace Transfer8Pro.Client.Controls
{
    public partial class SqlServerCtrl : UserControl, IDbConnBase
    {
        public SqlServerCtrl()
        {
            InitializeComponent();
        }

        public SqlServerCtrl(string connString) : this()
        {
            if (RegexExpressionUtil.SqlServerConnReg.IsMatch(connString))
            {
                Match match = RegexExpressionUtil.SqlServerConnReg.Match(connString);
                txtServer.Text = match.Groups["server"].ToString();
                txtDatabase.Text = match.Groups["database"].ToString();
                txtUserName.Text = match.Groups["uid"].ToString();
                txtPassword.Text = match.Groups["pwd"].ToString();
            }
        }

        public string GetConnString()
        {
            string connString = "";
            if (ValidateData())
            {
                connString = $"server={txtServer.Text.Trim()};database={txtDatabase.Text.Trim()};uid={txtUserName.Text.Trim()};pwd={txtPassword.Text.Trim()};min pool size=10;max pool size=300;Connection Timeout=10;";
            }
            return connString;
        }

        public bool ValidateData()
        {
            if (string.IsNullOrEmpty(txtServer.Text))
            {
                MessageBox.Show("数据库服务器名为必填项");
                txtServer.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("用户名为必填项");
                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("密码为必填项");
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDatabase.Text))
            {
                MessageBox.Show("数据库为必填项");
                txtDatabase.Focus();
                return false;
            }

            return true;
        }
    }
}
