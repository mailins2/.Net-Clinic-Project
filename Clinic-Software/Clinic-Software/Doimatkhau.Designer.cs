namespace Clinic_Software
{
    partial class Doimatkhau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Doimatkhau));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblmkmoi = new System.Windows.Forms.Label();
            this.lblxacnhanmk = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ckbHTmatkhau = new System.Windows.Forms.CheckBox();
            this.btnxacnhan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(49, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.panel1.Location = new System.Drawing.Point(232, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 3);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SkyBlue;
            this.panel2.Location = new System.Drawing.Point(232, 166);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(413, 3);
            this.panel2.TabIndex = 8;
            // 
            // lblmkmoi
            // 
            this.lblmkmoi.AutoSize = true;
            this.lblmkmoi.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblmkmoi.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblmkmoi.Location = new System.Drawing.Point(228, 56);
            this.lblmkmoi.Name = "lblmkmoi";
            this.lblmkmoi.Size = new System.Drawing.Size(115, 19);
            this.lblmkmoi.TabIndex = 9;
            this.lblmkmoi.Text = "Mật khẩu mới";
            this.lblmkmoi.Click += new System.EventHandler(this.lblmkmoi_Click);
            // 
            // lblxacnhanmk
            // 
            this.lblxacnhanmk.AutoSize = true;
            this.lblxacnhanmk.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblxacnhanmk.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblxacnhanmk.Location = new System.Drawing.Point(231, 138);
            this.lblxacnhanmk.Name = "lblxacnhanmk";
            this.lblxacnhanmk.Size = new System.Drawing.Size(159, 19);
            this.lblxacnhanmk.TabIndex = 10;
            this.lblxacnhanmk.Text = "Xác nhận mật khẩu";
            this.lblxacnhanmk.Click += new System.EventHandler(this.lblxacnhanmk_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox1.ForeColor = System.Drawing.Color.SteelBlue;
            this.textBox1.Location = new System.Drawing.Point(418, 44);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(227, 31);
            this.textBox1.TabIndex = 11;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox2.ForeColor = System.Drawing.Color.SteelBlue;
            this.textBox2.Location = new System.Drawing.Point(418, 126);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(227, 31);
            this.textBox2.TabIndex = 12;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // ckbHTmatkhau
            // 
            this.ckbHTmatkhau.AutoSize = true;
            this.ckbHTmatkhau.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ckbHTmatkhau.Location = new System.Drawing.Point(515, 186);
            this.ckbHTmatkhau.Name = "ckbHTmatkhau";
            this.ckbHTmatkhau.Size = new System.Drawing.Size(130, 20);
            this.ckbHTmatkhau.TabIndex = 13;
            this.ckbHTmatkhau.Text = "Hiện thị mật khẩu";
            this.ckbHTmatkhau.UseVisualStyleBackColor = true;
            // 
            // btnxacnhan
            // 
            this.btnxacnhan.BackColor = System.Drawing.Color.SkyBlue;
            this.btnxacnhan.FlatAppearance.BorderSize = 0;
            this.btnxacnhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnxacnhan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnxacnhan.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnxacnhan.Location = new System.Drawing.Point(333, 225);
            this.btnxacnhan.Name = "btnxacnhan";
            this.btnxacnhan.Size = new System.Drawing.Size(230, 48);
            this.btnxacnhan.TabIndex = 14;
            this.btnxacnhan.Text = "Xác nhận";
            this.btnxacnhan.UseVisualStyleBackColor = false;
            // 
            // Doimatkhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 306);
            this.Controls.Add(this.btnxacnhan);
            this.Controls.Add(this.ckbHTmatkhau);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblxacnhanmk);
            this.Controls.Add(this.lblmkmoi);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Doimatkhau";
            this.Text = "Doimatkhau";
            this.Load += new System.EventHandler(this.Doimatkhau_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblmkmoi;
        private System.Windows.Forms.Label lblxacnhanmk;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox ckbHTmatkhau;
        private System.Windows.Forms.Button btnxacnhan;
    }
}