namespace Transfer8Pro.Client.Controls
{
    partial class LoaderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLoadingMsg = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLoadingMsg
            // 
            this.lblLoadingMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblLoadingMsg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoadingMsg.ForeColor = System.Drawing.Color.Transparent;
            this.lblLoadingMsg.Location = new System.Drawing.Point(113, 53);
            this.lblLoadingMsg.Name = "lblLoadingMsg";
            this.lblLoadingMsg.Size = new System.Drawing.Size(140, 23);
            this.lblLoadingMsg.TabIndex = 0;
            this.lblLoadingMsg.Text = "加载中...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Transfer8Pro.Client.Properties.Resources.loading;
            this.pictureBox1.Location = new System.Drawing.Point(63, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 39);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // LoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(275, 124);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblLoadingMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoaderForm";
            this.ShowInTaskbar = false;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLoadingMsg;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}