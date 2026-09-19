namespace MiniSupermarket.WinForms
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            label1 = new Label();
            label2 = new Label();
            txtUser = new TextBox();
            txtPass = new TextBox();
            groupBox1 = new GroupBox();
            label3 = new Label();
            panel1 = new Panel();
            btnLogin = new Button();

            groupBox1.SuspendLayout();
            SuspendLayout();

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(57, 121);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 0;
            label1.Text = "Mật khẩu";

            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(57, 71);
            label2.Name = "label2";
            label2.Size = new Size(71, 20);
            label2.TabIndex = 1;
            label2.Text = "Tài khoản";

            // 
            // txtUser
            // 
            txtUser.Location = new Point(155, 71);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(125, 27);
            txtUser.TabIndex = 2;

            // 
            // txtPass
            // 
            txtPass.Location = new Point(155, 121);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(125, 27);
            txtPass.TabIndex = 3;
            txtPass.UseSystemPasswordChar = true;
            txtPass.TextChanged += txtPass_TextChanged;

            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(panel1);
            groupBox1.Controls.Add(btnLogin);
            groupBox1.Controls.Add(txtUser);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtPass);
            groupBox1.Location = new Point(74, 39);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(371, 348);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Đăng nhập";
            groupBox1.Enter += groupBox1_Enter;

            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(217, 325);
            label3.Name = "label3";
            label3.Size = new Size(154, 20);
            label3.TabIndex = 6;
            label3.Text = "Kết nối chưa xác thực.";
            label3.Click += label3_Click;

            // 
            // panel1
            // 
            panel1.Location = new Point(57, 225);
            panel1.Name = "panel1";
            panel1.Size = new Size(223, 31);
            panel1.TabIndex = 5;
            panel1.Paint += panel1_Paint;

            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(57, 177);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(223, 29);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Đăng nhập hệ thống";
            btnLogin.UseVisualStyleBackColor = true;

            // QUAN TRỌNG: Gắn sự kiện Click cho nút đăng nhập
            btnLogin.Click += btnLogin_Click;

            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Name = "FormLogin";
            Text = "Đăng nhập hệ thống";
            Load += FormLogin_Load;

            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtUser;
        private TextBox txtPass;
        private GroupBox groupBox1;
        private Button btnLogin;
        private Panel panel1;
        private Label label3;
    }
}