namespace Clinic_Software
{
    partial class Dangnhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dangnhap));
            this.btnDangnhap = new System.Windows.Forms.Button();
            this.ckbHienmatkhau = new System.Windows.Forms.CheckBox();
            this.txtmatkhau = new System.Windows.Forms.TextBox();
            this.lblMatkhau = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDangnhap
            // 
            this.btnDangnhap.BackColor = System.Drawing.Color.SkyBlue;
            this.btnDangnhap.FlatAppearance.BorderSize = 0;
            this.btnDangnhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangnhap.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDangnhap.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDangnhap.Location = new System.Drawing.Point(31, 380);
            this.btnDangnhap.Name = "btnDangnhap";
            this.btnDangnhap.Size = new System.Drawing.Size(372, 47);
            this.btnDangnhap.TabIndex = 11;
            this.btnDangnhap.Text = "Đăng nhập";
            this.btnDangnhap.UseVisualStyleBackColor = false;
            // 
            // ckbHienmatkhau
            // 
            this.ckbHienmatkhau.AutoSize = true;
            this.ckbHienmatkhau.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ckbHienmatkhau.Location = new System.Drawing.Point(289, 336);
            this.ckbHienmatkhau.Name = "ckbHienmatkhau";
            this.ckbHienmatkhau.Size = new System.Drawing.Size(114, 20);
            this.ckbHienmatkhau.TabIndex = 10;
            this.ckbHienmatkhau.Text = "Hiện mật khẩu";
            this.ckbHienmatkhau.UseVisualStyleBackColor = true;
            // 
            // txtmatkhau
            // 
            this.txtmatkhau.BackColor = System.Drawing.SystemColors.Control;
            this.txtmatkhau.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtmatkhau.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtmatkhau.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtmatkhau.Location = new System.Drawing.Point(136, 267);
            this.txtmatkhau.Multiline = true;
            this.txtmatkhau.Name = "txtmatkhau";
            this.txtmatkhau.PasswordChar = '*';
            this.txtmatkhau.Size = new System.Drawing.Size(267, 36);
            this.txtmatkhau.TabIndex = 9;
            // 
            // lblMatkhau
            // 
            this.lblMatkhau.AutoSize = true;
            this.lblMatkhau.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMatkhau.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblMatkhau.Location = new System.Drawing.Point(27, 276);
            this.lblMatkhau.Name = "lblMatkhau";
            this.lblMatkhau.Size = new System.Drawing.Size(88, 21);
            this.lblMatkhau.TabIndex = 8;
            this.lblMatkhau.Text = "Mật khẩu";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.panel1.Location = new System.Drawing.Point(31, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 3);
            this.panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(106, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(222, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Dangnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 463);
            this.Controls.Add(this.btnDangnhap);
            this.Controls.Add(this.ckbHienmatkhau);
            this.Controls.Add(this.txtmatkhau);
            this.Controls.Add(this.lblMatkhau);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Dangnhap";
            this.Text = "Dangnhap";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDangnhap;
        private System.Windows.Forms.CheckBox ckbHienmatkhau;
        private System.Windows.Forms.TextBox txtmatkhau;
        private System.Windows.Forms.Label lblMatkhau;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}