//using Oracle.ManagedDataAccess.Client;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace XUniversity.Forms
//{
//    partial class LoginForm
//    {
//        private System.ComponentModel.IContainer components = null;
//        private Label lblUsername;
//        private Label lblPassword;
//        private TextBox txtUsername;
//        private TextBox txtPassword;
//        private Button btnLogin;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null)) components.Dispose();
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            this.lblUsername = new Label();
//            this.lblPassword = new Label();
//            this.txtUsername = new TextBox();
//            this.txtPassword = new TextBox();
//            this.btnLogin = new Button();
//            this.SuspendLayout();

//            // lblUsername
//            this.lblUsername.AutoSize = true;
//            this.lblUsername.Location = new System.Drawing.Point(40, 30);
//            this.lblUsername.Name = "lblUsername";
//            this.lblUsername.Size = new System.Drawing.Size(68, 13);
//            this.lblUsername.Text = "Username:";

//            // txtUsername
//            this.txtUsername.Location = new System.Drawing.Point(130, 27);
//            this.txtUsername.Name = "txtUsername";
//            this.txtUsername.Size = new System.Drawing.Size(180, 20);

//            // lblPassword
//            this.lblPassword.AutoSize = true;
//            this.lblPassword.Location = new System.Drawing.Point(40, 70);
//            this.lblPassword.Name = "lblPassword";
//            this.lblPassword.Size = new System.Drawing.Size(66, 13);
//            this.lblPassword.Text = "Password:";

//            // txtPassword
//            this.txtPassword.Location = new System.Drawing.Point(130, 67);
//            this.txtPassword.Name = "txtPassword";
//            this.txtPassword.PasswordChar = '*';
//            this.txtPassword.Size = new System.Drawing.Size(180, 20);

//            // btnLogin
//            this.btnLogin.Location = new System.Drawing.Point(130, 110);
//            this.btnLogin.Name = "btnLogin";
//            this.btnLogin.Size = new System.Drawing.Size(90, 28);
//            this.btnLogin.Text = "Login";
//            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

//            // LoginForm
//            this.ClientSize = new System.Drawing.Size(380, 170);
//            this.Controls.Add(this.lblUsername);
//            this.Controls.Add(this.lblPassword);
//            this.Controls.Add(this.txtUsername);
//            this.Controls.Add(this.txtPassword);
//            this.Controls.Add(this.btnLogin);
//            this.Name = "LoginForm";
//            this.Text = "User Login";
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }
//    }
//}

// LoginForm.cs (Giao diện giống ảnh mẫu, giữ nguyên phần logic xử lý)
using System;
using System.Drawing;
using System.Windows.Forms;

namespace XUniversity.Forms
{
    public partial class LoginForm : Form
    {
        private Label lblTitle, lblEmail, lblPassword;
        private TextBox txtUsername, txtPassword;
        private Button btnLogin;
        private PictureBox pictureLeft;

        private void InitializeComponent()
        {
            this.Text = "Đăng nhập - X University";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Hình ảnh bên trái
            pictureLeft = new PictureBox();
            pictureLeft.Location = new Point(0, 0);
            pictureLeft.Size = new Size(400, 500);
            pictureLeft.Image = Image.FromFile("BG.png"); // tên ảnh mới
            pictureLeft.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLeft.BorderStyle = BorderStyle.None;
            this.Controls.Add(pictureLeft);

            // Tiêu đề
            lblTitle = new Label();
            lblTitle.Text = "Đăng nhập";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(520, 60);
            this.Controls.Add(lblTitle);

            // Label Email
            lblEmail = new Label();
            lblEmail.Text = "Username";
            lblEmail.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(520, 130);
            this.Controls.Add(lblEmail);

            // TextBox Email
            txtUsername = new TextBox();
            txtUsername.Font = new Font("Segoe UI", 10);
            txtUsername.Size = new Size(230, 25);
            txtUsername.Location = new Point(520, 155);
            this.Controls.Add(txtUsername);

            // Label Password
            lblPassword = new Label();
            lblPassword.Text = "Password";
            lblPassword.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(520, 200);
            this.Controls.Add(lblPassword);

            // TextBox Password
            txtPassword = new TextBox();
            txtPassword.Font = new Font("Segoe UI", 10);
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(230, 25);
            txtPassword.Location = new Point(520, 225);
            this.Controls.Add(txtPassword);

            // Button Login
            btnLogin = new Button();
            btnLogin.Text = "Đăng nhập";
            btnLogin.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogin.Size = new Size(230, 35);
            btnLogin.BackColor = Color.FromArgb(74, 124, 158);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Location = new Point(520, 270);
            btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.Controls.Add(btnLogin);
        }
    }
}
