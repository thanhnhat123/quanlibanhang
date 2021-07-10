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
    public partial class fDMNhanVien : Form
    {
        private DataTable tblNV;
        public fDMNhanVien()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void fDMNhanVien_Load(object sender, EventArgs e)
        {
            txbMaNhanVien.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            panel2.BackColor = Color.Transparent;
            panel1.BackColor = Color.Transparent;
        }
        public void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM NhanVien";
            tblNV = Class.Functions.GetDataToTable(sql); //lấy dữ liệu
            dgvNhanVien.DataSource = tblNV;
            dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns[2].HeaderText = "Giới tính";
            dgvNhanVien.Columns[3].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[4].HeaderText = "Điện thoại";
            dgvNhanVien.Columns[5].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns[0].Width = 100;
            dgvNhanVien.Columns[1].Width = 150;
            dgvNhanVien.Columns[2].Width = 100;
            dgvNhanVien.Columns[3].Width = 200;
            dgvNhanVien.Columns[4].Width = 100;
            dgvNhanVien.Columns[5].Width = 100;
            dgvNhanVien.AllowUserToAddRows = false;//ngăn chặn ng dùng thêm thêm dữ liệu trực tiếp
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;//Không cho sửa dữ liệu trực tiếp
        }

        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbMaNhanVien.Focus();
                return;
            }
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txbMaNhanVien.Text = dgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value.ToString();
            txbTenNhanVien.Text = dgvNhanVien.CurrentRow.Cells["TenNhanVien"].Value.ToString();
            if (dgvNhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") chkGioiTinh.Checked = true;
            else chkGioiTinh.Checked = false;
            txbDiaChi.Text = dgvNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
            mtbDienThoai.Text = dgvNhanVien.CurrentRow.Cells["DienThoai"].Value.ToString();
            dtpNgaySinh.Value = (DateTime)dgvNhanVien.CurrentRow.Cells["NgaySinh"].Value;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txbMaNhanVien.Enabled = false;
            txbTenNhanVien.Focus();
            //tạo mã tự động
            string ktn, s;
            if (tblNV.Rows.Count <= 0) // Kiểm tra xem trong csdl co dữ liệu chưa
            // nếu chưa có
            {
                int n = tblNV.Rows.Count + 1;
                string s3 = n.ToString();
                if (n < 9)
                {
                    txbMaNhanVien.Text = "NV" + "0" + s3;
                }
                else
                {
                    txbMaNhanVien.Text = "NV" + s3;
                }
            }

            else
            {
                // có rồi
                for (int i = 0; i <= tblNV.Rows.Count; i++)
                {
                    s = (i + 1).ToString();
                    if (i == tblNV.Rows.Count)
                    {
                        if (i < 9)
                        {
                            ktn = "NV" + "0" + (i + 1).ToString();
                        }
                        else
                        {
                            ktn = "NV" + (i + 1).ToString(); ;
                        }
                        txbMaNhanVien.Text = ktn;
                    }
                    else
                    {
                        if (i < 9)
                        {
                            ktn = "NV" + "0" + s;
                        }
                        else
                        {
                            ktn = "NV" + s;
                        }
                        try
                        {
                            if (ktn != (tblNV.Rows[i][0].ToString()))
                            {
                                txbMaNhanVien.Text = ktn;
                                break;
                            }
                        } // có dữ liệu là MTD001, MTD002, MTD003, MTDu008. thì nó sẽ tăng là MTDu004 tiếp cho đến 
                        //MTDu007 rồi nó tăng lên MTDu009 cứ thế.
                        catch { }
                    }
                }
            }
        }

        private void ResetValues()//khởi tạo giá trị về rỗng, mặc định ban đầu
        {
            txbMaNhanVien.Text = "";
            txbTenNhanVien.Text = "";
            chkGioiTinh.Checked = false;
            txbDiaChi.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            mtbDienThoai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txbTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbTenNhanVien.Focus();
                return;
            }
            if (txbDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(   )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbDienThoai.Focus();
                return;
            }
            if (chkGioiTinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            sql = "INSERT INTO NhanVien(MaNhanVien,TenNhanVien,GioiTinh, DiaChi,DienThoai, NgaySinh) VALUES (N'" + txbMaNhanVien.Text.Trim() + "',N'" + txbTenNhanVien.Text.Trim() + "',N'" + gt + "',N'" + txbDiaChi.Text.Trim() + "','" + mtbDienThoai.Text + "','" + dtpNgaySinh.Value + "')";
            Functions.RunSql(sql);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txbMaNhanVien.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbMaNhanVien.Text.Trim() == "")//trim(): tránh trường hợp ng dùng đánh những khoảng trống
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbTenNhanVien.Focus();
                return;
            }
            if (txbDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(   )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbDienThoai.Focus();
                return;
            }
            if (chkGioiTinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            sql = "UPDATE NhanVien SET  TenNhanVien=N'" + txbTenNhanVien.Text.Trim().ToString() +
                    "',DiaChi=N'" + txbDiaChi.Text.Trim().ToString() +
                    "',DienThoai='" + mtbDienThoai.Text.ToString() + "',GioiTinh=N'" + gt +
                    "',NgaySinh='" + dtpNgaySinh.Value +
                    "' WHERE MaNhanVien=N'" + txbMaNhanVien.Text + "'";
            //nếu ko có where sẽ xảy ra trường hợp tất cả các bản ghi của csdl sẽ đc cập nhật vs giá trị giống nhau
            Functions.RunSql(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE NhanVien WHERE MaNhanVien=N'" + txbMaNhanVien.Text + "'";
                Functions.RunSql(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txbMaNhanVien.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbMaNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txbTenNhanVien_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbTenNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txbDiaChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mtbDienThoai_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtbDienThoai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void dtpNgaySinh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
