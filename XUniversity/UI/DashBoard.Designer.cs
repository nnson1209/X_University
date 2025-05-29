using System;
using System.Drawing;
using System.Windows.Forms;

namespace XUniversity.UI
{
    partial class DashboardForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuThongTin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSinhVien = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDonVi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuanLy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHocPhan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMoMon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDangKy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemThongBao = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNhatKy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuThongTin,
            this.menuQuanLy,
            this.menuHeThong});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(1476, 35);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuThongTin
            // 
            this.menuThongTin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNhanVien,
            this.menuItemSinhVien,
            this.menuItemDonVi});
            this.menuThongTin.Name = "menuThongTin";
            this.menuThongTin.Size = new System.Drawing.Size(104, 29);
            this.menuThongTin.Text = "Thông tin";
            // 
            // menuItemNhanVien
            // 
            this.menuItemNhanVien.Name = "menuItemNhanVien";
            this.menuItemNhanVien.Size = new System.Drawing.Size(270, 34);
            this.menuItemNhanVien.Text = "Nhân viên";
            this.menuItemNhanVien.Click += new System.EventHandler(this.menuItemNhanVien_Click);
            // 
            // menuItemSinhVien
            // 
            this.menuItemSinhVien.Name = "menuItemSinhVien";
            this.menuItemSinhVien.Size = new System.Drawing.Size(270, 34);
            this.menuItemSinhVien.Text = "Sinh viên";
            this.menuItemSinhVien.Click += new System.EventHandler(this.menuItemSinhVien_Click);
            // 
            // menuItemDonVi
            // 
            this.menuItemDonVi.Name = "menuItemDonVi";
            this.menuItemDonVi.Size = new System.Drawing.Size(270, 34);
            this.menuItemDonVi.Text = "Đơn vị";
            this.menuItemDonVi.Click += new System.EventHandler(this.menuItemDonVi_Click);
            // 
            // menuQuanLy
            // 
            this.menuQuanLy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHocPhan,
            this.menuItemMoMon,
            this.menuItemDangKy});
            this.menuQuanLy.Name = "menuQuanLy";
            this.menuQuanLy.Size = new System.Drawing.Size(89, 29);
            this.menuQuanLy.Text = "Quản lý";
            // 
            // menuItemHocPhan
            // 
            this.menuItemHocPhan.Name = "menuItemHocPhan";
            this.menuItemHocPhan.Size = new System.Drawing.Size(270, 34);
            this.menuItemHocPhan.Text = "Học phần";
            this.menuItemHocPhan.Click += new System.EventHandler(this.menuItemHocPhan_Click);
            // 
            // menuItemMoMon
            // 
            this.menuItemMoMon.Name = "menuItemMoMon";
            this.menuItemMoMon.Size = new System.Drawing.Size(270, 34);
            this.menuItemMoMon.Text = "Mở lớp học phần";
            this.menuItemMoMon.Click += new System.EventHandler(this.menuItemMoMon_Click);
            // 
            // menuItemDangKy
            // 
            this.menuItemDangKy.Name = "menuItemDangKy";
            this.menuItemDangKy.Size = new System.Drawing.Size(270, 34);
            this.menuItemDangKy.Text = "Đăng ký học phần";
            this.menuItemDangKy.Click += new System.EventHandler(this.menuItemDangKy_Click);
            // 
            // menuHeThong
            // 
            this.menuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemThongBao,
            this.menuItemNhatKy,
            this.menuItemBackup,
            this.menuItemLogout});
            this.menuHeThong.Name = "menuHeThong";
            this.menuHeThong.Size = new System.Drawing.Size(103, 29);
            this.menuHeThong.Text = "Hệ thống";
            // 
            // menuItemThongBao
            // 
            this.menuItemThongBao.Name = "menuItemThongBao";
            this.menuItemThongBao.Size = new System.Drawing.Size(271, 34);
            this.menuItemThongBao.Text = "Thông báo";
            this.menuItemThongBao.Click += new System.EventHandler(this.menuItemThongBao_Click);
            // 
            // menuItemNhatKy
            // 
            this.menuItemNhatKy.Name = "menuItemNhatKy";
            this.menuItemNhatKy.Size = new System.Drawing.Size(271, 34);
            this.menuItemNhatKy.Text = "Nhật ký hệ thống";
            this.menuItemNhatKy.Click += new System.EventHandler(this.menuItemNhatKy_Click);
            // 
            // menuItemBackup
            // 
            this.menuItemBackup.Name = "menuItemBackup";
            this.menuItemBackup.Size = new System.Drawing.Size(271, 34);
            this.menuItemBackup.Text = "Sao lưu và phục hồi";
            this.menuItemBackup.Click += new System.EventHandler(this.menuItemBackup_Click);
            // 
            // menuItemLogout
            // 
            this.menuItemLogout.Name = "menuItemLogout";
            this.menuItemLogout.Size = new System.Drawing.Size(271, 34);
            this.menuItemLogout.Text = "Đăng xuất";
            this.menuItemLogout.Click += new System.EventHandler(this.menuItemLogout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 831);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(1476, 32);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(93, 25);
            this.lblStatus.Text = "Trạng thái:";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblWelcome);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 35);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1476, 796);
            this.pnlMain.TabIndex = 2;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(303, 255);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(568, 32);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Chào mừng đến với hệ thống quản lý dữ liệu";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1476, 863);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống quản lý dữ liệu nội bộ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DashboardForm_FormClosed);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuThongTin;
        private System.Windows.Forms.ToolStripMenuItem menuItemNhanVien;
        private System.Windows.Forms.ToolStripMenuItem menuItemSinhVien;
        private System.Windows.Forms.ToolStripMenuItem menuItemDonVi;
        private System.Windows.Forms.ToolStripMenuItem menuQuanLy;
        private System.Windows.Forms.ToolStripMenuItem menuItemHocPhan;
        private System.Windows.Forms.ToolStripMenuItem menuItemMoMon;
        private System.Windows.Forms.ToolStripMenuItem menuItemDangKy;
        private System.Windows.Forms.ToolStripMenuItem menuHeThong;
        private System.Windows.Forms.ToolStripMenuItem menuItemThongBao;
        private System.Windows.Forms.ToolStripMenuItem menuItemNhatKy;
        private System.Windows.Forms.ToolStripMenuItem menuItemBackup;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogout;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblWelcome;
    }
} 