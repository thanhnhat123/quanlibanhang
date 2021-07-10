using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //Sử dụng thư viện để làm việc SQL server
using QuanLyBanHang.Class; //Sử dụng class Functions.cs

namespace QuanLyBanHang
{
    public partial class fDMChatLieu : Form
    {
        DataTable tblCL; //Chứa dữ liệu bảng Chất liệu
        public fDMChatLieu()
        {
            InitializeComponent();
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fDMChatLieu_Load(object sender, EventArgs e)
        {
            txbMaChatLieu.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView(); //Hiển thị bảng tblChatLieu
            panel2.BackColor = Color.Transparent;
            panel1.BackColor = Color.Transparent;
  
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaChatLieu, TenChatLieu FROM ChatLieu";
            tblCL = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvChatLieu.DataSource = tblCL; //Nguồn dữ liệu            
            dgvChatLieu.Columns[0].HeaderText = "Mã chất liệu";
            dgvChatLieu.Columns[1].HeaderText = "Tên chất liệu";
            dgvChatLieu.Columns[0].Width = 100;
            dgvChatLieu.Columns[1].Width = 300;
            dgvChatLieu.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvChatLieu.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); //Xoá trắng các textbox
            txbMaChatLieu.Enabled = false;
            txbTenChatLieu.Focus();
            //tạo mã tự động
            string ktn, s;
            if (tblCL.Rows.Count <= 0) // Kiểm tra xem trong csdl co dữ liệu chưa
            // nếu chưa có
            {
                int n = tblCL.Rows.Count + 1;
                string s3 = n.ToString();
                if (n < 9)
                {
                    txbMaChatLieu.Text = "CL" + "0" + s3;
                }
                else
                {
                    txbMaChatLieu.Text = "CL" + s3;
                }
            }

            else
            {
                // có rồi
                for (int i = 0; i <= tblCL.Rows.Count; i++)
                {
                    s = (i + 1).ToString();
                    if (i == tblCL.Rows.Count)
                    {
                        if (i < 9)
                        {
                            ktn = "CL" + "0" + (i + 1).ToString();
                        }
                        else
                        {
                            ktn = "CL" + (i + 1).ToString(); ;
                        }
                        txbMaChatLieu.Text = ktn;
                    }
                    else
                    {
                        if (i < 9)
                        {
                            ktn = "CL" + "0" + s;
                        }
                        else
                        {
                            ktn = "CL" + s;
                        }
                        try
                        {
                            if (ktn != (tblCL.Rows[i][0].ToString()))
                            {
                                txbMaChatLieu.Text = ktn;
                                break;
                            }
                        } //ví dụ: có dữ liệu là MTD001, MTD002, MTD003, MTD008. thì nó sẽ tăng là MTD004 tiếp cho đến 
                        //MTDu007 rồi nó tăng lên MTDu009 cứ thế.
                        catch { }
                    }
                }
            }
       

        }

        private void ResetValue()
        {
            txbMaChatLieu.Text = "";
            txbTenChatLieu.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbMaChatLieu.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE ChatLieu WHERE MaChatLieu=N'" + txbMaChatLieu.Text + "'";
                Class.Functions.RunSql(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbMaChatLieu.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txbTenChatLieu.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE ChatLieu SET TenChatLieu=N'" +
                txbTenChatLieu.Text.ToString() +
                "' WHERE MaChatLieu=N'" + txbMaChatLieu.Text + "'";
            Class.Functions.RunSql(sql);
            LoadDataGridView();
            ResetValue();

            btnBoQua.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txbTenChatLieu.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbTenChatLieu.Focus();
                return;
            }
            sql = "INSERT INTO ChatLieu VALUES(N'" +
                txbMaChatLieu.Text + "',N'" + txbTenChatLieu.Text + "')";
            Class.Functions.RunSql(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txbMaChatLieu.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txbMaChatLieu.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fDMChatLieu_Click(object sender, EventArgs e)
        {

        }

        private void dgvChatLieu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbMaChatLieu.Focus();
                return;
            }
            if (tblCL.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txbMaChatLieu.Text = dgvChatLieu.CurrentRow.Cells["MaChatLieu"].Value.ToString();
            txbTenChatLieu.Text = dgvChatLieu.CurrentRow.Cells["TenChatLieu"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void txbMaChatLieu_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void txbTenChatLieu_KeyUp(object sender, KeyEventArgs e)
        {
        }

    }
}
