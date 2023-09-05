namespace ManagemenDocument
{
    partial class FResetPassword
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
            this.label9 = new System.Windows.Forms.Label();
            this.tb_passwordNew = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_confrimPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_passwordOld = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 90;
            this.label9.Text = "Password New";
            // 
            // tb_passwordNew
            // 
            this.tb_passwordNew.Location = new System.Drawing.Point(24, 150);
            this.tb_passwordNew.Name = "tb_passwordNew";
            this.tb_passwordNew.Size = new System.Drawing.Size(408, 20);
            this.tb_passwordNew.TabIndex = 89;
            this.tb_passwordNew.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Comfirm Password";
            // 
            // tb_confrimPass
            // 
            this.tb_confrimPass.Location = new System.Drawing.Point(26, 197);
            this.tb_confrimPass.Name = "tb_confrimPass";
            this.tb_confrimPass.Size = new System.Drawing.Size(408, 20);
            this.tb_confrimPass.TabIndex = 87;
            this.tb_confrimPass.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "Password Old";
            // 
            // tb_passwordOld
            // 
            this.tb_passwordOld.Location = new System.Drawing.Point(26, 104);
            this.tb_passwordOld.Name = "tb_passwordOld";
            this.tb_passwordOld.Size = new System.Drawing.Size(408, 20);
            this.tb_passwordOld.TabIndex = 85;
            this.tb_passwordOld.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(245, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 32);
            this.button1.TabIndex = 91;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 25);
            this.label1.TabIndex = 92;
            this.label1.Text = "Reset Password";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(115, 241);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 32);
            this.button2.TabIndex = 93;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 285);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tb_passwordNew);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_confrimPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_passwordOld);
            this.Name = "FResetPassword";
            this.Text = "FResetPassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_passwordNew;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_confrimPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_passwordOld;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}