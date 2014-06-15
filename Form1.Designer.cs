namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.外出 = new System.Windows.Forms.Button();
            this.教会 = new System.Windows.Forms.Button();
            this.休む = new System.Windows.Forms.Button();
            this.読書 = new System.Windows.Forms.Button();
            this.ステータス = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // 外出
            // 
            this.外出.Image = ((System.Drawing.Image)(resources.GetObject("外出.Image")));
            this.外出.Location = new System.Drawing.Point(124, 414);
            this.外出.Name = "外出";
            this.外出.Size = new System.Drawing.Size(100, 127);
            this.外出.TabIndex = 0;
            this.外出.UseVisualStyleBackColor = true;
            this.外出.MouseDown += new System.Windows.Forms.MouseEventHandler(this.外出_MouseDown);
            this.外出.MouseEnter += new System.EventHandler(this.外出_MouseEnter);
            this.外出.MouseLeave += new System.EventHandler(this.外出_MouseLeave);
            this.外出.MouseMove += new System.Windows.Forms.MouseEventHandler(this.外出_MouseMove);
            this.外出.MouseUp += new System.Windows.Forms.MouseEventHandler(this.外出_MouseUp);
            // 
            // 教会
            // 
            this.教会.Image = ((System.Drawing.Image)(resources.GetObject("教会.Image")));
            this.教会.Location = new System.Drawing.Point(230, 414);
            this.教会.Name = "教会";
            this.教会.Size = new System.Drawing.Size(100, 127);
            this.教会.TabIndex = 1;
            this.教会.UseVisualStyleBackColor = true;
            this.教会.MouseDown += new System.Windows.Forms.MouseEventHandler(this.教会_MouseDown);
            this.教会.MouseEnter += new System.EventHandler(this.教会_MouseEnter);
            this.教会.MouseLeave += new System.EventHandler(this.教会_MouseLeave);
            this.教会.MouseMove += new System.Windows.Forms.MouseEventHandler(this.教会_MouseMove);
            this.教会.MouseUp += new System.Windows.Forms.MouseEventHandler(this.教会_MouseUp);
            // 
            // 休む
            // 
            this.休む.Image = ((System.Drawing.Image)(resources.GetObject("休む.Image")));
            this.休む.Location = new System.Drawing.Point(336, 414);
            this.休む.Name = "休む";
            this.休む.Size = new System.Drawing.Size(100, 127);
            this.休む.TabIndex = 2;
            this.休む.UseVisualStyleBackColor = true;
            this.休む.MouseDown += new System.Windows.Forms.MouseEventHandler(this.休む_MouseDown);
            this.休む.MouseEnter += new System.EventHandler(this.休む_MouseEnter);
            this.休む.MouseLeave += new System.EventHandler(this.休む_MouseLeave);
            this.休む.MouseMove += new System.Windows.Forms.MouseEventHandler(this.休む_MouseMove);
            this.休む.MouseUp += new System.Windows.Forms.MouseEventHandler(this.休む_MouseUp);
            // 
            // 読書
            // 
            this.読書.Image = ((System.Drawing.Image)(resources.GetObject("読書.Image")));
            this.読書.Location = new System.Drawing.Point(442, 414);
            this.読書.Name = "読書";
            this.読書.Size = new System.Drawing.Size(100, 127);
            this.読書.TabIndex = 3;
            this.読書.UseVisualStyleBackColor = true;
            this.読書.MouseDown += new System.Windows.Forms.MouseEventHandler(this.読書_MouseDown);
            this.読書.MouseEnter += new System.EventHandler(this.読書_MouseEnter);
            this.読書.MouseLeave += new System.EventHandler(this.読書_MouseLeave);
            this.読書.MouseMove += new System.Windows.Forms.MouseEventHandler(this.読書_MouseMove);
            this.読書.MouseUp += new System.Windows.Forms.MouseEventHandler(this.読書_MouseUp);
            // 
            // ステータス
            // 
            this.ステータス.Image = ((System.Drawing.Image)(resources.GetObject("ステータス.Image")));
            this.ステータス.Location = new System.Drawing.Point(548, 414);
            this.ステータス.Name = "ステータス";
            this.ステータス.Size = new System.Drawing.Size(100, 127);
            this.ステータス.TabIndex = 4;
            this.ステータス.UseVisualStyleBackColor = true;
            this.ステータス.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ステータス_MouseDown);
            this.ステータス.MouseEnter += new System.EventHandler(this.ステータス_MouseEnter);
            this.ステータス.MouseLeave += new System.EventHandler(this.ステータス_MouseLeave);
            this.ステータス.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ステータス_MouseMove);
            this.ステータス.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ステータス_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(655, 414);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(655, 459);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(655, 503);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 38);
            this.button3.TabIndex = 7;
            this.button3.Text = "Option";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(43, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 400);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ステータス);
            this.Controls.Add(this.読書);
            this.Controls.Add(this.休む);
            this.Controls.Add(this.教会);
            this.Controls.Add(this.外出);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button 外出;
        private System.Windows.Forms.Button 教会;
        private System.Windows.Forms.Button 休む;
        private System.Windows.Forms.Button 読書;
        private System.Windows.Forms.Button ステータス;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

