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
    public partial class fDMKhachHang : Form
    {
        private DataTable tblKH; //Bảng khách hàng
        public fDMKhachHang()
        {
            InitializeComponent();
        }

        private void fDMKhachHang_Load(object sender, EventArgs e)
        {
            txbMaKhach.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            panel2.BackColor = Color.Transparent;
            panel1.BackColor = Color.Transparent;
        }
        //Phương thức nạp dữ liệu lên lưới
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * from Khach";
            tblKH = Functions.GetDataToTable(sql); //Lấy dữ liệu từ bảng
            dgvKhachHang.DataSource = tblKH; //Hiển thị vào dataGridView
            dgvKhachHang.Columns[0].HeaderText = "Mã khách";
            dgvKhachHang.Columns[1].HeaderText = "Tên khách";
            dgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns[3].HeaderText = "Điện thoại";
            dgvKhachHang.Columns[0].Width = 100;
            dgvKhachHang.Columns[1].Width = 150;
            dgvKhachHang.Columns[2].Width = 250;
            dgvKhachHang.Columns[3].Width = 150;
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvKhachHang_Click(object sender, EventArgs e)
        {

        }


        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fDMKhachHang_Click(object sender, EventArgs e)
        {

        }


        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbMaKhach.Focus();
                return;
            }
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txbMaKhach.Text = dgvKhachHang.CurrentRow.Cells["MaKhach"].Value.ToString();
            txbTenKhach.Text = dgvKhachHang.CurrentRow.Cells["TenKhach"].Value.ToString();
            txbDiaChi.Text = dgvKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
            mtbDienThoai.Text = dgvKhachHang.CurrentRow.Cells["DienThoai"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            //kiểm tra dữ liệu có hợp lệ hay ko
            if (txbTenKhach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbTenKhach.Focus();
                return;
            }
            if (txbDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(   )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoai.Focus();
                return;
            }
            //Chèn thêm
            sql = "INSERT INTO Khach VALUES (N'" + txbMaKhach.Text.Trim() +
                "',N'" + txbTenKhach.Text.Trim() + "',N'" + txbDiaChi.Text.Trim() + "','" + mtbDienThoai.Text + "')";
            Functions.RunSql(sql);
            LoadDataGridView();
            ResetValues();

            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txbMaKhach.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txbMaKhach.Enabled = false;
            txbTenKhach.Focus();
            //
            //tạo mã tự động
            string ktn, s;
            if (tblKH.Rows.Count <= 0) // Kiểm tra xem trong csdl co dữ liệu chưa
            // nếu chưa có
            {
                int n = tblKH.Rows.Count + 1;
                string s3 = n.ToString();
                if (n < 9)
                {
                    txbMaKhach.Text = "KH" + "0" + s3;
                }
                else
                {
                    txbMaKhach.Text = "KH" + s3;
                }
            }

            else
            {
                // có rồi
                for (int i = 0; i <= tblKH.Rows.Count; i++)
                {
                    s = (i + 1).ToString();
                    if (i == tblKH.Rows.Count)
                    {
                        if (i < 9)
                        {
                            ktn = "KH" + "0" + (i + 1).ToString();
                        }
                        else
                        {
                            ktn = "KH" + (i + 1).ToString(); ;
                        }
                        txbMaKhach.Text = ktn;
                    }
                    else
                    {
                        if (i < 9)
                        {
                            ktn = "KH" + "0" + s;
                        }
                        else
                        {
                            ktn = "KH" + s;
                        }
                        try
                        {
                            if (ktn != (tblKH.Rows[i][0].ToString()))
                            {
                                txbMaKhach.Text = ktn;
                                break;
                            }
                        } // có dữ liệu là MTD001, MTD002, MTD003, MTDu008. thì nó sẽ tăng là MTDu004 tiếp cho đến 
                        //MTDu007 rồi nó tăng lên MTDu009 cứ thế.
                        catch { }
                    }
                }
            }
        }

        //Khởi tạo lại giá trị trên form
        private void ResetValues()
        {
            txbMaKhach.Text = "";
            txbTenKhach.Text = "";
            txbDiaChi.Text = "";
            mtbDienThoai.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbMaKhach.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbTenKhach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbTenKhach.Focus();
                return;
            }
            if (txbDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(   )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoai.Focus();
                return;
            }
            sql = "UPDATE Khach SET TenKhach=N'" + txbTenKhach.Text.Trim().ToString() + "',DiaChi=N'" +
                txbDiaChi.Text.Trim().ToString() + "',DienThoai='" + mtbDienThoai.Text.ToString() +
                "' WHERE MaKhach=N'" + txbMaKhach.Text + "'";
            Functions.RunSql(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbMaKhach.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE Khach WHERE MaKhach=N'" + txbMaKhach.Text + "'";
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
            txbMaKhach.Enabled = false;
        }

        private void txbMaKhach_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txbTenKhach_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txbDiaChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mtbDienThoai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txbTenKhach_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbDiaChi_TextChanged(object sender, EventArgs e)
        {

        }






    }
}
