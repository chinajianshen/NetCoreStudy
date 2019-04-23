using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Transfer8Pro.Core.Infrastructure;

namespace Transfer8Pro.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //bool noRun = false;
            //Mutex mutex = new Mutex(true, "Transfer8Pro_Client", out noRun);
            //if (noRun)
            //{                
            //    Application.Run(new TaskMainFrm());
            //}
            //else
            //{
            //    MessageBox.Show("该客户端正在运行中");
            //}
            Process instance = RunningInstance();
            if (instance == null)
            {
                Application.Run(new TaskMainFrm());
            }
            else
            {
                MessageBox.Show("该客户端正在运行中");
            }
        }

      /// <summary>
        /// 获取正在运行的实例，没有运行的实例返回null;
        /// </summary>
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (process.MainModule.FileName == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        ///// <summary>
        ///// 显示已运行的程序。
        ///// </summary>
        //public static void HandleRunningInstance(Process instance)
        //{
        //    if (CONSTANTDEFINE.LOGIN == 1)  //笔者这里根据实际情况，进行了控制
        //        ShowWindowAsync(instance.MainWindowHandle, WS_SHOWMAXIMIZE); //显示，通过后面的值可以对窗口大小进行控制
        //    else
        //        ShowWindowAsync(instance.MainWindowHandle, WS_SHOW);

        //    SetForegroundWindow(instance.MainWindowHandle);            //放到前端
        //}
    }
}
