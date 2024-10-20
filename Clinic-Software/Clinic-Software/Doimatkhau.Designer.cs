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
            this.ckbhienthi = new System.Windows.Forms.CheckBox();
            this.btnXacnhan = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblmatkhaumoi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ckbhienthi
            // 
            this.ckbhienthi.AutoSize = true;
            this.ckbhienthi.Location = new System.Drawing.Point(387, 210);
            this.ckbhienthi.Name = "ckbhienthi";
            this.ckbhienthi.Size = new System.Drawing.Size(130, 20);
            this.ckbhienthi.TabIndex = 11;
            this.ckbhienthi.Text = "Hiển thị mật khẩu";
            this.ckbhienthi.UseVisualStyleBackColor = true;
            // 
            // btnXacnhan
            // 
            this.btnXacnhan.BackColor = System.Drawing.Color.SkyBlue;
            this.btnXacnhan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXacnhan.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnXacnhan.Location = new System.Drawing.Point(207, 248);
            this.btnXacnhan.Name = "btnXacnhan";
            this.btnXacnhan.Size = new System.Drawing.Size(160, 45);
            this.btnXacnhan.TabIndex = 10;
            this.btnXacnhan.Text = "Xác nhận";
            this.btnXacnhan.UseVisualStyleBackColor = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(285, 160);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(232, 31);
            this.textBox2.TabIndex = 9;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(285, 79);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(232, 31);
            this.textBox1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(52, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Xác nhận mật khẩu mới";
            // 
            // lblmatkhaumoi
            // 
            this.lblmatkhaumoi.AutoSize = true;
            this.lblmatkhaumoi.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblmatkhaumoi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblmatkhaumoi.Location = new System.Drawing.Point(52, 79);
            this.lblmatkhaumoi.Name = "lblmatkhaumoi";
            this.lblmatkhaumoi.Size = new System.Drawing.Size(162, 19);
            this.lblmatkhaumoi.TabIndex = 6;
            this.lblmatkhaumoi.Text = "Nhập mật khẩu mới";
            // 
            // Doimatkhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(589, 323);
            this.Controls.Add(this.ckbhienthi);
            this.Controls.Add(this.btnXacnhan);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblmatkhaumoi);
            this.Name = "Doimatkhau";
            this.Text = "Doimatkhau";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbhienthi;
        private System.Windows.Forms.Button btnXacnhan;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblmatkhaumoi;
    }
}