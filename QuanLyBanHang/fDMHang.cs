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
    public partial class fDMHang : Form
    {
        private DataTable tblH;
        public fDMHang()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txbMaHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fDMHang_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * from ChatLieu";
            txbMaHang.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            Functions.FillCombo(sql, cboMaChatLieu, "MaChatLieu", "TenChatLieu");
            cboMaChatLieu.SelectedIndex = -1;
            ResetValues();
            panel2.BackColor = Color.Transparent;
            panel1.BackColor = Color.Transparent;
        }
        //Khởi tạo lại giá trị
        private void ResetValues()
        {
            txbMaHang.Text = "";
            txbTenHang.Text = "";
            cboMaChatLieu.SelectedIndex = -1;
            txbSoLuong.Text = "0";
            txbDonGiaNhap.Text = "0";
            txbDonGiaBan.Text = "0";
            txbSoLuong.Enabled = true;
            txbDonGiaNhap.Enabled = false;
            txbDonGiaBan.Enabled = false;
            txbAnh.Text = "";
            picAnh.Image = null;
            txbGhiChu.Text = "";
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * from Hang";
            tblH = Functions.GetDataToTable(sql);
            dgvHang.DataSource = tblH;
            dgvHang.Columns[0].HeaderText = "Mã hàng";
            dgvHang.Columns[1].HeaderText = "Tên hàng";
            dgvHang.Columns[2].HeaderText = "Mã chất liệu";
            dgvHang.Columns[3].HeaderText = "Số lượng";
            dgvHang.Columns[4].HeaderText = "Đơn giá nhập";
            dgvHang.Columns[5].HeaderText = "Đơn giá bán";
            dgvHang.Columns[6].HeaderText = "Ảnh";
            dgvHang.Columns[7].HeaderText = "Ghi chú";
            dgvHang.Columns[0].Width = 80;
            dgvHang.Columns[1].Width = 140;
            dgvHang.Columns[2].Width = 100;
            dgvHang.Columns[3].Width = 80;
            dgvHang.Columns[4].Width = 100;
            dgvHang.Columns[5].Width = 100;
            dgvHang.Columns[6].Width = 200;
            dgvHang.Columns[7].Width = 300;
            dgvHang.AllowUserToAddRows = false;
            dgvHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaChatLieu;
            string sql;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbMaHang.Focus();
                return;
            }
            if (tblH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txbMaHang.Text = dgvHang.CurrentRow.Cells["MaHang"].Value.ToString();
            txbTenHang.Text = dgvHang.CurrentRow.Cells["TenHang"].Value.ToString();
            MaChatLieu = dgvHang.CurrentRow.Cells["MaChatLieu"].Value.ToString();
            sql = "SELECT TenChatLieu FROM ChatLieu WHERE MaChatLieu=N'" + MaChatLieu + "'";
            cboMaChatLieu.Text = Functions.GetFieldValues(sql);
            txbSoLuong.Text = dgvHang.CurrentRow.Cells["SoLuong"].Value.ToString();
            txbDonGiaNhap.Text = dgvHang.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
            txbDonGiaBan.Text = dgvHang.CurrentRow.Cells["DonGiaBan"].Value.ToString();
            sql = "SELECT Anh FROM Hang WHERE MaHang=N'" + txbMaHang.Text + "'";
            txbAnh.Text = Functions.GetFieldValues(sql);
            picAnh.Image = Image.FromFile(txbAnh.Text);
            sql = "SELECT Ghichu FROM Hang WHERE MaHang = N'" + txbMaHang.Text + "'";
            txbGhiChu.Text = Functions.GetFieldValues(sql);
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
            txbMaHang.Enabled = false;
            txbTenHang.Focus();
            txbSoLuong.Enabled = true;
            txbDonGiaNhap.Enabled = true;
            txbDonGiaBan.Enabled = true;
            //tạo mã tự động
            string ktn, s;
            if (tblH.Rows.Count <= 0) // Kiểm tra xem trong csdl co dữ liệu chưa
            // nếu chưa có
            {
                int n = tblH.Rows.Count + 1;
                string s3 = n.ToString();
                if (n < 9)
                {
                    txbMaHang.Text = "HH" + "0" + s3;
                }
                else
                {
                    txbMaHang.Text = "HH" + s3;
                }
            }

            else
            {
                // có rồi
                for (int i = 0; i <= tblH.Rows.Count; i++)
                {
                    s = (i + 1).ToString();
                    if (i == tblH.Rows.Count)
                    {
                        if (i < 9)
                        {
                            ktn = "HH" + "0" + (i + 1).ToString();
                        }
                        else
                        {
                            ktn = "HH" + (i + 1).ToString(); ;
                        }
                        txbMaHang.Text = ktn;
                    }
                    else
                    {
                        if (i < 9)
                        {
                            ktn = "HH" + "0" + s;
                        }
                        else
                        {
                            ktn = "HH" + s;
                        }
                        try
                        {
                            if (ktn != (tblH.Rows[i][0].ToString()))
                            {
                                txbMaHang.Text = ktn;
                                break;
                            }
                        } // có dữ liệu là MTD001, MTD002, MTD003, MTDu008. thì nó sẽ tăng là MTDu004 tiếp cho đến 
                        //MTDu007 rồi nó tăng lên MTDu009 cứ thế.
                        catch { }
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txbTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbTenHang.Focus();
                return;
            }
            if (cboMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaChatLieu.Focus();
                return;
            }
            if (txbSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbSoLuong.Focus();
                return;
            }
            if (txbDonGiaNhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbDonGiaNhap.Focus();
                return;
            }
            if (txbDonGiaBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbDonGiaBan.Focus();
                return;
            }
            if (txbAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpen.Focus();
                return;
            }
            sql = "INSERT INTO Hang(MaHang,TenHang,MaChatLieu,SoLuong,DonGiaNhap, DonGiaBan,Anh,Ghichu) VALUES(N'"
                + txbMaHang.Text.Trim() + "',N'" + txbTenHang.Text.Trim() +
                "',N'" + cboMaChatLieu.SelectedValue.ToString() +
                "'," + txbSoLuong.Text.Trim() + "," + txbDonGiaNhap.Text +
                "," + txbDonGiaBan.Text + ",'" + txbAnh.Text + "',N'" + txbGhiChu.Text.Trim() + "')";

            Functions.RunSql(sql);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txbMaHang.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbMaHang.Focus();
                return;
            }
            if (txbTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbTenHang.Focus();
                return;
            }
            if (cboMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaChatLieu.Focus();
                return;
            }
            if (txbAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải thêm ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbAnh.Focus();
                return;
            }
            sql = "UPDATE Hang SET TenHang=N'" + txbTenHang.Text.Trim().ToString() +
                "',MaChatLieu=N'" + cboMaChatLieu.SelectedValue.ToString() +
                "',SoLuong=" + txbSoLuong.Text +
                ",Anh='" + txbAnh.Text + "',Ghichu=N'" + txbGhiChu.Text + "' WHERE MaHang=N'" + txbMaHang.Text + "'";
            Functions.RunSql(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE Hang WHERE MaHang=N'" + txbMaHang.Text + "'";
                Functions.RunSql(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txbMaHang.Enabled = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txbAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txbMaHang.Text == "") && (txbTenHang.Text == "") && (cboMaChatLieu.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from Hang WHERE 1=1";
            if (txbMaHang.Text != "")
                sql += " AND MaHang LIKE N'%" + txbMaHang.Text + "%'";
            if (txbTenHang.Text != "")
                sql += " AND TenHang LIKE N'%" + txbTenHang.Text + "%'";
            if (cboMaChatLieu.Text != "")
                sql += " AND MaChatLieu LIKE N'%" + cboMaChatLieu.SelectedValue + "%'";
            tblH = Functions.GetDataToTable(sql);
            if (tblH.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblH.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvHang.DataSource = tblH;
            ResetValues();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaHang,TenHang,MaChatLieu,SoLuong,DonGiaNhap,DonGiaBan,Anh,Ghichu FROM Hang";
            tblH = Functions.GetDataToTable(sql);
            dgvHang.DataSource = tblH;
            ResetValues();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txbDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txbDonGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txbTenHang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txbSoLuong_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txbDonGiaNhap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txbDonGiaBan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void cboMaChatLieu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txbAnh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnOpen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }



    }
}
