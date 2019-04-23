using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Transfer8Pro.Client.Controls
{
    public partial class LoaderForm : Form
    {
        public LoaderForm()
        {
            InitializeComponent();
            this.FormClosing += LoaderForm_FormClosing;
        }

        public void SetLoadingMsg(string msg)
        {
            lblLoadingMsg.Text = !string.IsNullOrEmpty(msg) ? msg.Trim() : "加载中...";
        }

        /// <summary>
        /// 关闭命令
        /// </summary>
        public void closeOrder()
        {
            if (this.InvokeRequired)
            {              
                this.Invoke(new MethodInvoker(()=>
                {
                    while (!this.IsHandleCreated)
                    {

                    }
                    if (this.IsDisposed)
                        return;

                    if (!this.IsDisposed)
                    {
                        this.Dispose();
                    }
                }));
            }
            else
            {
                if (this.IsDisposed)
                    return;
                if (!this.IsDisposed)
                {
                    this.Dispose();
                }
            }
        }

        private void LoaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsDisposed)
            {
                this.Dispose(true);
            }
        }
    }
}
