
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.Class;

namespace QuanLyBanHang
{
    public partial class fTableManager : Form
    {
        
        public fTableManager()
        {
            InitializeComponent();
        }




        private void mnuChatLieu_Click(object sender, EventArgs e)
        {
            fDMChatLieu f=new fDMChatLieu();
            f.ShowDialog();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            fDMNhanVien f = new fDMNhanVien();
            f.ShowDialog();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            fDMKhachHang f = new fDMKhachHang();
            f.ShowDialog();
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            fDMHang f = new fDMHang();
            f.ShowDialog();
        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            fHoaDonBan f = new fHoaDonBan();
            f.ShowDialog();
        }

        private void mnuFindHoaDon_Click(object sender, EventArgs e)
        {
            fTimHDBancs f = new fTimHDBancs();
            f.ShowDialog();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect(); //Đóng kết nối
            Application.Exit();
        }

        private void fTableManager_Load(object sender, EventArgs e)
        {
            Functions.Connect(); //Mở kết nối
        }

        private void fTableManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel)!=System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;

            }
        }


    }
}
