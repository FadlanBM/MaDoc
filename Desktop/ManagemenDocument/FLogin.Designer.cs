namespace ManagemenDocument
{
    partial class FLogin
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.lb_chapca = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_chapta = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome Admin \r\n";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(82, 190);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(228, 20);
            this.tb_username.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password";
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(82, 240);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(228, 20);
            this.tb_password.TabIndex = 5;
            // 
            // lb_chapca
            // 
            this.lb_chapca.Font = new System.Drawing.Font("MV Boli", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_chapca.Image = global::ManagemenDocument.Properties.Resources.Background_with_randomly_drawn_lines_and_special_effects_in_Step_9_Q320;
            this.lb_chapca.Location = new System.Drawing.Point(126, 279);
            this.lb_chapca.Name = "lb_chapca";
            this.lb_chapca.Size = new System.Drawing.Size(114, 45);
            this.lb_chapca.TabIndex = 6;
            this.lb_chapca.Text = "Cahpta";
            // 
            // label2
            // 
            this.label2.Image = global::ManagemenDocument.Properties.Resources.Group_3;
            this.label2.Location = new System.Drawing.Point(69, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 117);
            this.label2.TabIndex = 1;
            // 
            // tb_chapta
            // 
            this.tb_chapta.Location = new System.Drawing.Point(97, 340);
            this.tb_chapta.Name = "tb_chapta";
            this.tb_chapta.Size = new System.Drawing.Size(168, 20);
            this.tb_chapta.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 391);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(286, 35);
            this.button1.TabIndex = 8;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 432);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(286, 35);
            this.button2.TabIndex = 9;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 474);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_chapta);
            this.Controls.Add(this.lb_chapca);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_username);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FLogin";
            this.Text = "Login Admin";
            this.Load += new System.EventHandler(this.FLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label lb_chapca;
        private System.Windows.Forms.TextBox tb_chapta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}