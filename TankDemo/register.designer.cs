namespace TankDemo
{
    partial class Register
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
            this.text_username = new System.Windows.Forms.TextBox();
            this.text_password = new System.Windows.Forms.TextBox();
            this.text_repassword = new System.Windows.Forms.TextBox();
            this.text_email = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_confrim = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text_username
            // 
            this.text_username.Location = new System.Drawing.Point(203, 29);
            this.text_username.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.text_username.Name = "text_username";
            this.text_username.Size = new System.Drawing.Size(132, 25);
            this.text_username.TabIndex = 0;
            this.text_username.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // text_password
            // 
            this.text_password.Location = new System.Drawing.Point(203, 106);
            this.text_password.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.text_password.Name = "text_password";
            this.text_password.PasswordChar = '*';
            this.text_password.Size = new System.Drawing.Size(132, 25);
            this.text_password.TabIndex = 1;
            this.text_password.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // text_repassword
            // 
            this.text_repassword.Location = new System.Drawing.Point(203, 189);
            this.text_repassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.text_repassword.Name = "text_repassword";
            this.text_repassword.PasswordChar = '*';
            this.text_repassword.Size = new System.Drawing.Size(132, 25);
            this.text_repassword.TabIndex = 2;
            // 
            // text_email
            // 
            this.text_email.Location = new System.Drawing.Point(203, 266);
            this.text_email.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.text_email.Name = "text_email";
            this.text_email.Size = new System.Drawing.Size(132, 25);
            this.text_email.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "用户名";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 106);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 189);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "重复密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 266);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "邮箱";
            // 
            // button_confrim
            // 
            this.button_confrim.Location = new System.Drawing.Point(203, 330);
            this.button_confrim.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_confrim.Name = "button_confrim";
            this.button_confrim.Size = new System.Drawing.Size(100, 29);
            this.button_confrim.TabIndex = 8;
            this.button_confrim.Text = "注册";
            this.button_confrim.UseVisualStyleBackColor = true;
            this.button_confrim.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 374);
            this.Controls.Add(this.button_confrim);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_email);
            this.Controls.Add(this.text_repassword);
            this.Controls.Add(this.text_password);
            this.Controls.Add(this.text_username);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "register";
            this.Text = "register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_username;
        private System.Windows.Forms.TextBox text_password;
        private System.Windows.Forms.TextBox text_repassword;
        private System.Windows.Forms.TextBox text_email;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_confrim;
    }
}