using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Transfer8Pro.Client.Controls;

namespace Transfer8Pro.Client.Core
{
    public class LoadingHelper
    {
        /// <summary>
        /// 开始加载
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ownerForm">父窗体</param>
        /// <param name="work">待执行工作</param>
        /// <param name="workArg">工作参数</param>
        public static void ShowLoading(Form ownerForm, ParameterizedThreadStart work, string message = "加载中", object workArg = null)
        {
            var loadingForm = new FrmLoading(message);
            dynamic expandoObject = new ExpandoObject();
            expandoObject.Form = loadingForm;
            expandoObject.WorkArg = workArg;
            expandoObject.OwnerForm = ownerForm;
            loadingForm.SetWorkAction(work, expandoObject);
            loadingForm.ShowDialog(ownerForm);
            if (loadingForm.WorkException != null)
            {
                throw loadingForm.WorkException;
            }
        }
    }


    public class LoadingProHelper
    {
        #region 相关变量定义
        /// <summary>
        /// 定义委托进行窗口关闭
        /// </summary>
        private delegate void CloseDelegate();
        private static LoaderForm loadingForm;
        private static readonly Object syncLock = new Object();  //加锁使用


        #endregion

        private LoadingProHelper()
        {

        }

        public static void ShowLoadingScreen()
        {
            ShowLoadingScreen(null);
        }

        /// <summary>
        /// 显示loading框
        /// </summary>
        public static void ShowLoadingScreen(string loadingMsg)
        {
            // Make sure it is only launched once.
            if (loadingForm != null)
                return;
            
            Thread thread = new Thread(new ParameterizedThreadStart(LoadingProHelper.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(loadingMsg);
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public static void CloseForm()
        {
            Thread.Sleep(50); //可能到这里线程还未起来，所以进行延时，可以确保线程起来，彻底关闭窗口
            if (loadingForm != null)
            {
                lock (syncLock)
                {
                    Thread.Sleep(50);
                    if (loadingForm != null)
                    {
                        Thread.Sleep(50);  //通过三次延时，确保可以彻底关闭窗口
                        loadingForm.Invoke(new CloseDelegate(LoadingProHelper.CloseFormInternal));
                    }
                }
            }
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        private static void ShowForm(object loadingMsg)
        {
            if (loadingForm != null)
            {
                loadingForm.closeOrder();
                loadingForm = null;
            }

            loadingForm = new LoaderForm();
            loadingForm.TopMost = true;
            loadingForm.StartPosition = FormStartPosition.Manual;      

            if (loadingMsg != null)
            {
                loadingForm.SetLoadingMsg(loadingMsg.ToString());
            }
            var screenRect = Screen.PrimaryScreen.WorkingArea;
            loadingForm.Location = new Point((screenRect.Width - loadingForm.Width) / 2, (screenRect.Height - loadingForm.Height) / 2);

            loadingForm.ShowDialog();
        }

        private static Point LocationOnClient(Form c)
        {
            Point retval = new Point(0, 0);
            do
            {
                retval.Offset(c.Location);
                c = c.MdiParent;
            }
            while (c != null);
            return retval;
        }

        /// <summary>
        /// 关闭窗口，委托中使用
        /// </summary>
        private static void CloseFormInternal()
        {

            loadingForm.closeOrder();
            loadingForm = null;

        }

    }
}
