using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiThuCK2.BLL;
using ThiThuCK2.DTO;
using ThiThuCK2.Models;

namespace ThiThuCK2
{
    public partial class DetailForm : Form
    {
        public delegate void Mydel(string hpid, string search, int option);
        public Mydel d  { get; set; }
        private Tuple<string,string> _id;
        public DetailForm(Tuple<string, string> id = null)
        {
            _id = id;
            InitializeComponent();
            LoadData();
        }
        public void LoadCbbHocPhan()
        {
            cbbHocPhan.Items.AddRange(QLSV_BLL.Instance.GetCBBItems().ToArray());
            cbbHocPhan.SelectedIndex = 0;
        }
        public void LoadCbbLSH()
        {
            cbbLSH.DataSource = QLSV_BLL.Instance.GetLSHs();
        }
        public void LoadData()
        {
            LoadCbbLSH();
            LoadCbbHocPhan();
            if (_id != null) // edit
            {
                var hpsv = QLSV_BLL.Instance.GetHpSvById(_id);
                txtMSSV.Enabled = false;

                // load data
                cbbLSH.SelectedIndex = cbbLSH.Items.IndexOf(hpsv.SinhVien.LopSh);
                foreach (var item in cbbHocPhan.Items)
                {
                    var tmp = item as CBBItem;
                    if (tmp.Value == hpsv.HocPhan.MaHP)
                    {
                        cbbHocPhan.SelectedItem = item; break;
                    }
                }

                txtMSSV.Text = hpsv.SinhVien.MaSV.ToString();
                txtName.Text = hpsv.SinhVien.TenSV.ToString();

                if (hpsv.SinhVien.GioiTinh) rbtnMale.Checked = true;
                else rbtnFemale.Checked = true;

                dtpNgayThi.Value = (DateTime)hpsv.NgayThi;
                txtDiemBT.Text = hpsv.DiemBT.ToString();
                txtDiemGK.Text = hpsv.DiemGK.ToString();
                txtDiemCK.Text = hpsv.DiemCK.ToString();
                txtTongKet.Text = (hpsv.DiemBT * 0.2 + hpsv.DiemGK * 0.2
                                + hpsv.DiemCK  * 0.6).ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMSSV.Text) || string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Điền đủ thông tin!");
                    return;
                }
                var newRecord = new SinhVienGRV()
                {
                    TenSV = txtName.Text,
                    LopSH = cbbLSH.SelectedItem.ToString(),
                    GioiTinh = rbtnMale.Checked,
                    DiemBT = Convert.ToDouble(txtDiemBT.Text.ToString()),
                    DiemGK = Convert.ToDouble(txtDiemGK.Text.ToString()),
                    DiemCK = Convert.ToDouble(txtDiemCK.Text.ToString()),
                    NgayThi = dtpNgayThi.Value,
                    ID = new Tuple<string, string>(txtMSSV.Text, (cbbHocPhan.SelectedItem as CBBItem).Value)
                };
                if (txtMSSV.Enabled) // add
                {
                    if (QLSV_BLL.Instance.AddRecord(newRecord))
                    {
                        MessageBox.Show("Them thanh cong");
                        d.Invoke("All","",-1);
                        this.Dispose();
                    }
                    else
                        MessageBox.Show("Trung MSSV");
                }
                else // edit
                {
                    QLSV_BLL.Instance.UpdateRecord(newRecord);
                    MessageBox.Show("Cap nhat thanh cong");
                    d.Invoke("All", "", -1);
                    this.Dispose();
                }
            }
            catch(FormatException) 
            {
                MessageBox.Show("Diem khong hop le");
            }
        }
        
    }
}
