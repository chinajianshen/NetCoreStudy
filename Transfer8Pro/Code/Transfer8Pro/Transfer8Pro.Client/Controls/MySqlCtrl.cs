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
    public partial class MySqlCtrl : UserControl, IDbConnBase
    {
        public MySqlCtrl()
        {
            InitializeComponent();
        }

        public MySqlCtrl(string connString) : this()
        {
            if (RegexExpressionUtil.MySqlConnReg.IsMatch(connString))
            {
                Match match = RegexExpressionUtil.MySqlConnReg.Match(connString);
                txtServer.Text = match.Groups["server"].ToString();
                txtDatabase.Text = match.Groups["database"].ToString();
                txtUserName.Text = match.Groups["uid"].ToString();
                txtPassword.Text = match.Groups["pwd"].ToString();
                txtPort.Text = match.Groups["port"].ToString();
            }
        }

        public string GetConnString()
        {
            string connString = "";
            if (ValidateData())
            {
                connString = $"server={txtServer.Text.Trim()};database={txtDatabase.Text.Trim()};userid={txtUserName.Text.Trim()};password={txtPassword.Text.Trim()};port={txtPort.Text.Trim()}; Charset = utf8; Allow User Variables = True";               
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

            if (string.IsNullOrEmpty(txtPort.Text))
            {
                MessageBox.Show("端口号为必填项");
                txtPort.Focus();
                return false;
            }

            return true;
        }
    }
}
