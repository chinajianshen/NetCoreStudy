using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Transfer8Pro.Client.Controls;
using Transfer8Pro.Client.Core;
using Transfer8Pro.Core;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Entity;

namespace Transfer8Pro.Client
{
    public partial class BaseFrm : Form
    {      
        protected static SystemConfigService systemConfigService;
        protected static FtpService ftpService;      

        private static TaskMainFrm _ParentTaskForm;
        protected TaskMainFrm ParentTaskForm
        {
            get
            {
                return _ParentTaskForm;
            }
        }

        static BaseFrm()
        {
            //注册IOC依赖
            AutoFacConfiguration.RegisterDependencies();
            //启动Quartz作业任务
            TaskManagerService.Start();
            systemConfigService = new SystemConfigService();
            ftpService = new FtpService();
        }

        public BaseFrm()
        {
            InitializeComponent();
            //loadingctrl = new LoadingCtrl();
            //loadingctrl.Visible = false;
            //loadingctrl.Left = -1 * loadingctrl.Width;
            //this.Controls.Add(loadingctrl);
        }

        protected void SetMdiParent(TaskMainFrm frm)
        {
            _ParentTaskForm = frm;
        }

        public void UIAction(MethodInvoker md)
        {
            if (IsDisposed) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(md));
            }
            else
            {
                md();
            }           
        }

        public void UIAction(Form frm,MethodInvoker md)
        {
            if (frm == null) return;
            if (IsDisposed) return;

            if (frm.InvokeRequired)
            {
                frm.Invoke(new MethodInvoker(md));
            }
            else
            {
                md();
            }
        }

        public void UIAction(Control ctrl, MethodInvoker md)
        {
            if (ctrl == null) return;
            if (IsDisposed) return;

            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(new MethodInvoker(md));
            }
            else
            {
                md();
            }
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #region 加载/显示Loading
        public void ShowLoading()
        {
            UIAction(() =>
            {
                //this.Enabled = false;
                LoadingProHelper.ShowLoadingScreen();
            });
        }

        public void HideLoading()
        {          
            UIAction(() =>
            {
                //this.Enabled = true;
                LoadingProHelper.CloseForm();
            });
        }

        public void ShowLoading(string msg)
        {
            UIAction(() =>
            {
                //this.Enabled = false;
                LoadingProHelper.ShowLoadingScreen(msg);
            });
        }     
        #endregion

        public void MyAsync(ThreadStart func)
        {
            new System.Threading.Thread(func).Start();
        }

        /// <summary>
        /// 判断窗体是否打开
        /// </summary>
        /// <param name="frmTypeName"></param>
        protected bool OpenFormByMdiParent(string frmTypeName)
        {
            if (this.ParentTaskForm != null && !string.IsNullOrEmpty(frmTypeName))
            {
                foreach (Form frm in this.ParentTaskForm.MdiChildren)
                {
                    if (frm.GetType().Name == frmTypeName)
                    {
                        frm.BringToFront();
                        frm.WindowState = FormWindowState.Maximized;

                        if (frm is BaseFrm)
                        {
                            BaseFrm baseFrm = (BaseFrm)frm;
                            baseFrm.loadGridData(1);
                        }                     
                        return true;
                    }
                }
            }
            return false;
        }

        protected void CloseMdiChildForm()
        {
            Form[] frmList = this.MdiChildren;
            foreach (Form frm in frmList)
            {
                frm.Close();
            }
        }

        /// <summary>
        /// 检测系统配置
        /// </summary>
        protected bool CheckSystemConfiguration()
        {
            if (!Common.IsExistSQLiteDB())
            {
                ShowErrorMessage("系统未检测到数据库，请联系开卷客服人员");
                return false;
            }

            List<SystemConfigEntity> systemConfigList = systemConfigService.GetSystemConfigList();
            if (systemConfigList.Count == 0)
            {
                ShowErrorMessage("系统配置表不存在，请联系开卷客服人员");
                return false;
            }

            //1检测客户密钥           
            SystemConfigEntity systemConfig = systemConfigList.Find(item => item.SysConfigID == (int)SystemConfigs.EncryptKey);
            if (systemConfig == null)
            {
                ShowErrorMessage("系统配置表不存在客户密钥数据记录，请联系开卷客服人员");
                return false;
            }
            if (string.IsNullOrEmpty(systemConfig.ExSetting01))
            {
                ShowErrorMessage("未配置客户密钥，请配置客户密钥");

                EncryptKeyConfigFrm frm = new EncryptKeyConfigFrm();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
           

            //2检测FTP配置
            FtpConfigEntity ftpConfig = ftpService.GetFirstFtpInfo();
            if (ftpConfig == null)
            {
                ShowErrorMessage("未配置FTP信息，请配置FTP数据");
                FtpConfigFrm frm = new FtpConfigFrm();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
           
            return true;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="pageIndex"></param>
        public virtual void loadGridData(int pageIndex)
        {

        }
    }

    //public partial class BaseFrm<T>:BaseFrm where T : class, new()
    //{
    //    #region 页面控件绑定
    //    public void SetDefaultControl(Button bt, DataGridView grid, PagerCtrl pager, Control disabledctrl)
    //    {
    //        __InitConditionControls();
    //        LoadingDisabledControl = disabledctrl;

    //        if (bt != null)
    //        {
    //            this.SearchButton = bt;
    //            this.SearchButton.Click += SearchButton_Click;
    //        }

    //        this.Grid = grid;

    //        this.Grid.CellClick += Grid_CellClick;

    //        if (pager != null)
    //        {
    //            this.Pager = pager;
    //            this.Pager.OnNextPageClicked += Pager_OnNextPageClicked;
    //            this.Pager.OnPreviousPageClicked += Pager_OnPreviousPageClicked;
    //        }

    //    }

    //    void Pager_OnPreviousPageClicked(object sender, MyEventArgs<int> e)
    //    {
    //        pageno = e.Value1 + 1;
    //        __LoadData(false);
    //    }

    //    void Pager_OnNextPageClicked(object sender, MyEventArgs<int> e)
    //    {
    //        pageno = e.Value1 + 1;
    //        __LoadData(false);
    //    }

    //    protected PagerCtrl Pager { get; set; }

    //    void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
    //    {
    //        if (e.RowIndex < 0)
    //            return;
    //        __RowClicked(sender, e);
    //    }

    //    public virtual void __RowClicked(object sender, DataGridViewCellEventArgs e)
    //    {

    //    }

    //    protected int pageno = 1;

    //    /// <summary>
    //    /// 初始化控件
    //    /// </summary>
    //    public virtual void __InitConditionControls()
    //    { }

    //    protected Control LoadingDisabledControl { get; set; }

    //    protected Button SearchButton { get; set; }

    //    void SearchButton_Click(object sender, EventArgs e)
    //    {
    //        if (__CheckSearch())
    //        {
    //            Prms = __FetchCurrentPrms();
    //            pageno = 1;
    //            __LoadData(false);
    //        }

    //    }

    //    /// <summary>
    //    /// 查询检测
    //    /// </summary>
    //    /// <returns></returns>
    //    public virtual bool __CheckSearch()
    //    {
    //        return true;
    //    }

    //    protected ParamtersForDBPageEntity<T> Prms { get; set; }

    //    public virtual ParamtersForDBPageEntity<T> __FetchCurrentPrms()
    //    {
    //        return null;
    //    }

    //    protected DataGridView Grid { get; set; }

    //    public void ShowLoading(Control disabledcontrol, string msg = "正在努力加载中..请稍后..")
    //    {
    //        //loadingtip.Text = msg;
    //        //loading_pannel.Left = (disabledcontrol.Width - loading_pannel.Width) / 2;
    //        //loading_pannel.Top = (disabledcontrol.Height - loading_pannel.Height) / 2;
    //        //loading_pannel.Visible = true;
    //        //loading_pannel.BringToFront();
    //        //disabledcontrol.Enabled = false;


    //    }

    //    protected void __LoadData(bool reloadcondition = true)
    //    {
    //        if (this.DesignMode)
    //        {
    //            return;
    //        }
    //        this.Grid.ScrollBars = ScrollBars.None;
    //        ShowLoading(this.LoadingDisabledControl);
    //        MyAsync(() =>
    //        {

    //            if (reloadcondition)
    //            {
    //                pageno = 1;
    //                UIAction(() =>
    //                {
    //                    __InitConditionControlValues();
    //                    Prms = __FetchCurrentPrms();
    //                });

    //            }

    //            if (Prms != null)
    //            {
    //                Prms.PageIndex = pageno;
    //            }
    //            //RtValueEntity<DataTable> jm = __QueryData(Prms);
    //            //if (jm.Status == 1)
    //            //{
    //            //    UIAction(() =>
    //            //    {
    //            //        __BindData(jm.Value);

    //            //        if (this.Pager != null)
    //            //        {
    //            //            if (pageno == 1)
    //            //            {
    //            //                int totalcnt = int.Parse(jm.Tag);
    //            //                if (totalcnt >= 0)
    //            //                {
    //            //                    this.Pager.TotalPages = totalcnt;
    //            //                }
    //            //            }
    //            //        }

    //            //        HideLoading(this.LoadingDisabledControl);
    //            //    });
    //            //}
    //            //else
    //            //{
    //            //    ShowMessageBackground(jm.Msg);
    //            //}
    //        });
    //    }

    //    public virtual void __InitConditionControlValues()
    //    { }
    //    #endregion
    //}
}
