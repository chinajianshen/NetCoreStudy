using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transfer8Pro.Core;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Core.QuartzJobs;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Test
{
    public partial class MainFrm : Form
    {
        //TaskManagerService service = null; //new TaskManagerService();
        public MainFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //List<SystemConfigEntity> list = new List<SystemConfigEntity>();
            //list.Add(new SystemConfigEntity {  FtpUserName= "t8bjzhxshd" , SysConfigID=10, SysConfigName= "数据导出服务", Status=1,Cron= "0 0/5 * * * ? * " });
            //list.Add(new SystemConfigEntity { FtpUserName = "t8bjzhxshd", SysConfigID = 20, SysConfigName = "FTP上传服务", Status = 1, Cron = "0 0/5 * * * ? * " });
            //list.Add(new SystemConfigEntity { FtpUserName = "t8bjzhxshd", SysConfigID = 30, SysConfigName = "心跳服务", Status = 1, Cron = "0 0/2 * * * ? * " });
            //list.Add(new SystemConfigEntity { FtpUserName = "t8bjzhxshd", SysConfigID = 40, SysConfigName = "配置同步状态", Status = 1, Cron = "0 0/10 * * * ? * " });
            //list.Add(new SystemConfigEntity { FtpUserName = "t8bjzhxshd", SysConfigID = 50, SysConfigName = "系统版本", Status = 1, Cron = "" ,ExSetting01="1.0.0"});
            //list.Add(new SystemConfigEntity { FtpUserName = "t8bjzhxshd", SysConfigID = 60, SysConfigName = "客户密钥", Status = 1, Cron = "",ExSetting01= "08491090B30349FB89C543A477946459" });
            //bool isTrue = new ClientUploadService().SaveSysConfig(list);
            
            #region FTPHeart相关操作
            //FtpInfoService ftpInfoBll = new FtpInfoService();

            //FtpInfoEntity ftpInfoEntity = new FtpInfoEntity();
            //ftpInfoEntity.FtpUserName = "t8jyqssd2";
            //ftpInfoEntity.FtpPassword = "t8jyqssd2";
            //ftpInfoEntity.FtpFolderName = "华北_内蒙_鄂尔多斯2";
            //ftpInfoEntity.FtpEncryptKey = "jjj";
            // int result = ftpInfoBll.Insert(ftpInfoEntity);

            //ftpInfoEntity.FtpUserName = "t8jyqssd3";
            //ftpInfoEntity.FtpPassword = "t8jyqssd3";
            //ftpInfoEntity.FtpFolderName = "华北_内蒙_鄂尔多斯3";
            //result = ftpInfoBll.Insert(ftpInfoEntity);

            //ftpInfoEntity.FtpUserName = "t8jyqssd4";
            //ftpInfoEntity.FtpPassword = "t8jyqssd4";
            //ftpInfoEntity.FtpFolderName = "华北_内蒙_鄂尔多斯4";
            //result = ftpInfoBll.Insert(ftpInfoEntity);

            //ftpInfoEntity.FtpUserName = "t8jyqssd5";
            //ftpInfoEntity.FtpPassword = "t8jyqssd5";
            //ftpInfoEntity.FtpFolderName = "华北_内蒙_鄂尔多斯5";
            //result = ftpInfoBll.Insert(ftpInfoEntity);

            //FtpHeartbeatViewEntity ftpHeartbeat = new FtpHeartbeatViewEntity();          

            //ParamtersForDBPageEntity<FtpHeartbeatViewEntity> entity = ftpInfoBll.GetList(ftpHeartbeat, 1, 2);
            #endregion

            //Core.Service.TaskService taskBll = new Core.Service.TaskService();

            ////新加
            //TaskEntity taskEntity = InsertTask();

            //int result = taskBll.Insert(taskEntity);
            //if (result == 1)
            //{
            //    MessageBox.Show(string.Format("添加数据成功任务ID[{0}],任务名[{1}]",taskEntity.TaskID,taskEntity.TaskName));
            //}
            //else if (result == 2)
            //{
            //    MessageBox.Show("系统中存在相同的任务名称");
            //}
            //else
            //{
            //    MessageBox.Show("任务添加失败");
            //}

        }

        private TaskEntity InsertTask()
        {
            TaskEntity taskEntity = new TaskEntity();
            taskEntity.TaskID = Guid.NewGuid().ToString("N");
            taskEntity.DataType = DataTypes.Sale;
            taskEntity.Cron = "0 3 * * * ? *";
            taskEntity.DataHandler = "Transfer8Pro.DAO.DataHandlers.SqlServer_DataHandler";
            string connStr = @"server=192.168.0.14;database=Smart_NewBookDB;uid=sa;pwd=sa.;min pool size=10;max pool size=300;Connection Timeout=10;";
            string encryptKey = Common.GetEncryptKey();
            taskEntity.DBConnectString_Hashed = RijndaelCrypt.Encrypt(connStr, encryptKey);
            taskEntity.SQL = "SELECT * FROM dbo.T8_BookInfo WHERE SalesDateTime>=@StartTime AND SalesDateTime<=@EndTime";
            taskEntity.TaskName = "天销售数据" + DateTime.Now.ToLongTimeString();
            //taskEntity.Enabled = true;
            taskEntity.IsDelete = false;
            taskEntity.TaskStatus = TaskStatus.RUN;
            taskEntity.CreateTime = DateTime.Now;
            return taskEntity;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EncryptFrm frm = new EncryptFrm();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string taskID = txtTaskID.Text.Trim();
            if (string.IsNullOrEmpty(taskID))
            {
                MessageBox.Show("请在右边输入TaskID");
                return;
            }

            Core.Service.TaskService taskBll = new Core.Service.TaskService();

            //查找一条数据
            //TaskEntity taskEntity = taskBll.Find(taskID);
            //if (taskEntity == null)
            //{
            //    MessageBox.Show(string.Format("TaskID[{0}]数据库不存在",taskID));
            //    return;
            //}            


            //更新           
            //taskEntity.ModifyTime = DateTime.Now;
            //taskEntity.Remark = DateTime.Now.ToString();
            //bool isSuccess2 = taskBll.Update(taskEntity);
            //if (isSuccess2)
            //{

            //}

            //获取任务列表
            //TaskEntity taskEntity2 = new TaskEntity();
            ParamtersForDBPageEntity<TaskEntity> pageTask = taskBll.GetTaskList(null, 1, 2);
            ParamtersForDBPageEntity<TaskEntity> pageTask2 = taskBll.GetTaskList(null, 2, 2);
            //List<TaskEntity> taskList = taskBll.GetAllTaskList();

            //更新上次运行时间
            //bool isResult1 = taskBll.UpdateRecentRunTime(taskID, DateTime.Parse("2018-12-04 02:25:25"));

            //更新下次运行时间
            //bool isResult3 = taskBll.UpdateNextFireTime(taskID, DateTime.Now.AddSeconds(1));

            //更新任务状态
            //bool isResult2 = taskBll.UpdateTaskStatus(taskID, TaskStatus.STOP);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GenerateFileNameFrm frm = new GenerateFileNameFrm();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            JobFrm frm = new JobFrm();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Core.Service.FtpService bll = new Core.Service.FtpService();
            FtpConfigEntity ftpConfig = new FtpConfigEntity();

            MessageBox.Show("先设置FTP文件夹目录");
            FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
            saveFileDialog.Description = "设置FTP文件夹目录";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                ftpConfig.ServerAddress = "ftp://openbook.cn";
                ftpConfig.UserName = "t8jyqssd";
                ftpConfig.UserPassword = Common.EncryptData("t8jyqssd");
                ftpConfig.ExportFileDirectory = saveFileDialog.SelectedPath;
            }

            bool state = bll.InsertOrUpdate(ftpConfig);
            if (state)
            {
                MessageBox.Show("添加成功");
                ;
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {          
            Button btn = (Button)sender;
            int status = btn.Tag.ToString().ToInt();
            if (status == 0)
            {
                btn.Tag = 1;
                btn.Text = "结束Quartz任务调度";
                TaskManagerService.Start();
            }
            else
            {
                TaskManagerService.Stop();
                btn.Text = "开始Quartz任务调度";
                btn.Tag = 0;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtZipFilePath.Text))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtZipFilePath.Text = openFileDialog.FileName;
                }
            }

            string destPath = Path.Combine(AppPath.DataFolder, "TestZipFile");
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            string destFile = Path.Combine(destPath, FileHelper.GetFileName(txtZipFilePath.Text));

            FileHelper.CopyFile(txtZipFilePath.Text, destFile);
            //FileHelper.CopyFilePlus(txtZipFilePath.Text, destFile);


            //加解密
            //destFile = destFile + ".zip";
            //FileHelper.ZipFile(txtZipFilePath.Text, destFile,Common.GetEncryptKey());
            //解密
            //FileHelper.UnZip(destFile, destPath, Common.GetEncryptKey());

            MessageBox.Show("文件复制成功");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUploadPath.Text))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtUploadPath.Text = openFileDialog.FileName;
                }
            }
            FtpConfigEntity ftpConfig = new FtpService().GetFirstFtpInfo();

            try
            {
                FtpHelper.UploadFile(ftpConfig, txtUploadPath.Text);
                MessageBox.Show("上传成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传失败");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Environment.Exit(0);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtHeartbeat.Text))
            //{
            //    MessageBox.Show("请输入地址");
            //    txtHeartbeat.Focus();
            //    return;
            //}

            //string url = txtHeartbeat.Text.Trim();

            //HttpHelper httpHelper = new HttpHelper();
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("signature", ApiHelper.GenerateApiSignature());
            //string ftpUserName = HttpHelper.UrlEncodeUnicode(RijndaelCrypt.Encrypt("t8jyqssd"));
            //string systemtype = HttpHelper.UrlEncodeUnicode(RijndaelCrypt.Encrypt("2"));
            //string encryptstr = HttpHelper.UrlEncodeUnicode(Common.EncryptData($"{DateTime.Now.AddSeconds(-2)}_{DateTime.Now.AddSeconds(2)}"));
            //string content = $"fname={ftpUserName}&encryptstr={encryptstr}&systemtype={systemtype}";
            //string result = httpHelper.Post(url, content, url, Encoding.UTF8, dic);
            //dynamic response = JsonObj<dynamic>.FromJson(result);
            //int a1 = response.Status;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Application.ExitThread();
            //Application.Exit();
            Application.Restart();
            //Process.GetCurrentProcess().Kill();

            //System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            txtGuid.Text = Guid.NewGuid().ToString("N").ToUpper();
        }
    }
}
