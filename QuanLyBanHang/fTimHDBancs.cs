using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyBanHang.Class;

namespace QuanLyBanHang
{
    public partial class fTimHDBancs : Form
    {
        private DataTable tblHDB; //Hoá đơn bán
        public fTimHDBancs()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void fTimHDBancs_Load(object sender, EventArgs e)
        {
            ResetValues();
            dgvTKHoaDon.DataSource = null;
            btnTimLai.Enabled = false;
            panel2.BackColor = Color.Transparent;
            panel1.BackColor = Color.Transparent;
            panel3.BackColor = Color.Transparent;
        }

        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txbMaHDBan.Text = "";
            txbMaNhanVien.Text = "";
            txbNam.Text = "";
            txbThang.Text = "";
            txbMaKhach.Text = "";
            txbTongTien.Text = "";
            txbMaHDBan.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            btnTimLai.Enabled = true;
            string sql;
            if ((txbMaHDBan.Text == "") && (txbThang.Text == "") && (txbNam.Text == "") &&
               (txbMaNhanVien.Text == "") && (txbMaKhach.Text == "") &&
               (txbTongTien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM HoaDonBan WHERE 1=1";
            if (txbMaHDBan.Text != "")
                sql = sql + " AND MaHDBan Like N'%" + txbMaHDBan.Text + "%'";
            if (txbThang.Text != "")
                sql = sql + " AND MONTH(NgayBan) =" + txbThang.Text;
            if (txbNam.Text != "")
                sql = sql + " AND YEAR(NgayBan) =" + txbNam.Text;
            if (txbMaNhanVien.Text != "")
                sql = sql + " AND MaNhanVien Like N'%" + txbMaNhanVien.Text + "%'";
            if (txbMaKhach.Text != "")
                sql = sql + " AND MaKhach Like N'%" + txbMaKhach.Text + "%'";
            if (txbTongTien.Text != "")
                sql = sql + " AND TongTien <=" + txbTongTien.Text;
            tblHDB = Functions.GetDataToTable(sql);
            if (tblHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tblHDB.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTKHoaDon.DataSource = tblHDB;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            dgvTKHoaDon.Columns[0].HeaderText = "Mã hóa đơn bán";
            dgvTKHoaDon.Columns[1].HeaderText = "Mã nhân viên";
            dgvTKHoaDon.Columns[2].HeaderText = "Ngày bán";
            dgvTKHoaDon.Columns[3].HeaderText = "Mã khách";
            dgvTKHoaDon.Columns[4].HeaderText = "Tổng tiền";
            dgvTKHoaDon.Columns[0].Width = 150;
            dgvTKHoaDon.Columns[1].Width = 100;
            dgvTKHoaDon.Columns[2].Width = 120;
            dgvTKHoaDon.Columns[3].Width = 80;
            dgvTKHoaDon.Columns[4].Width = 100;
            dgvTKHoaDon.AllowUserToAddRows = false;
            dgvTKHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void txbTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void dgvTKHoaDon_DoubleClick(object sender, EventArgs e)
        {
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = dgvTKHoaDon.CurrentRow.Cells["MaHDBan"].Value.ToString();
                fHoaDonBan frm = new fHoaDonBan();
                frm.txbMaHDBan.Text = mahd;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dgvTKHoaDon.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbThang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txbNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }


    }
}
