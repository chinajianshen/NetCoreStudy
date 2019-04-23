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
    public partial class OracleCtrl : UserControl, IDbConnBase
    {
        public OracleCtrl()
        {
            InitializeComponent();
        }

        public OracleCtrl(string connString) : this()
        {
            if (RegexExpressionUtil.OracleConnReg.IsMatch(connString))
            {
                Match match = RegexExpressionUtil.OracleConnReg.Match(connString);
                txtUserName.Text = match.Groups["uid"].ToString();
                txtPassword.Text = match.Groups["pwd"].ToString();
                txtHostAddress.Text = match.Groups["host"].ToString();
                txtPort.Text = match.Groups["port"].ToString();
                txtServer.Text = match.Groups["servicename"].ToString();
            }
        }

        public string GetConnString()
        {
            string connString = "";
            if (ValidateData())
            {
                connString = $"User ID={txtUserName.Text.Trim()};Password={txtPassword.Text.Trim()};Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={txtHostAddress.Text.Trim()})(PORT={txtPort.Text.Trim()})))(CONNECT_DATA=(SERVICE_NAME={txtServer.Text.Trim()})))";
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

            if (string.IsNullOrEmpty(txtHostAddress.Text))
            {
                MessageBox.Show("Host地址为必填项");
                txtHostAddress.Focus();
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
