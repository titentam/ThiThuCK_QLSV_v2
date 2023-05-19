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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadCBBHocPhan();
            LoadData();
            LoadCBBSort();

        }

        public void LoadCBBSort()
        {
            List<string> list = new List<string>();
            list.Add("Tên sinh viên");
            list.Add("Điểm tổng kết");
            cbbSort.DataSource = list;
        }
        public void LoadCBBHocPhan()
        {
            cbbHocPhan.Items.Add(new CBBItem { Value = "All", Text = "All" });
            cbbHocPhan.Items.AddRange(QLSV_BLL.Instance.GetCBBItems().ToArray());
            cbbHocPhan.SelectedIndex = 0;
        }
        public void LoadData(string hpId = "All", string search ="", int optionSort = -1 )
        {
            dataGridView1.DataSource = QLSV_BLL.Instance.GetSVs(hpId, search, optionSort);
            dataGridView1.Columns["TenSV"].HeaderText = "Tên SV";
            dataGridView1.Columns["LopSH"].HeaderText = "Lớp SH";
            dataGridView1.Columns["TenHP"].HeaderText = "Tên học phần";
            dataGridView1.Columns["DiemBT"].HeaderText = "Điểm BT";
            dataGridView1.Columns["DiemGK"].HeaderText = "Điểm GK";
            dataGridView1.Columns["DiemCK"].HeaderText = "Điểm CK";
            dataGridView1.Columns["DiemTK"].HeaderText = "Tổng kết";
            dataGridView1.Columns["NgayThi"].HeaderText = "Ngày thi";

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["GioiTinh"].Visible = false;
            
        }

        private void cbbHocPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hpId = (cbbHocPhan.SelectedItem as CBBItem).Value;
            LoadData(hpId);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var search = txtSearch.Text;
            var hpId = (cbbHocPhan.SelectedItem as CBBItem).Value;
            LoadData(hpId, search);

        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            var search = txtSearch.Text;
            var hpId = (cbbHocPhan.SelectedItem as CBBItem).Value;
            var optionSort = cbbSort.SelectedIndex;
            LoadData(hpId, search, optionSort);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var listSelectedRow = dataGridView1.SelectedRows;
            if(listSelectedRow.Count > 0)
            {
                var records = new List<Tuple<string,string>>();
                foreach(DataGridViewRow row in listSelectedRow)
                {
                    records.Add(row.Cells["ID"].Value as Tuple<string, string>);
                }
                QLSV_BLL.Instance.RemoveRecords(records);
                LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DetailForm detailForm = new DetailForm();
            detailForm.d += new DetailForm.Mydel(LoadData);
            detailForm.ShowDialog();
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 1)
            {
                var id = dataGridView1.SelectedRows[0].Cells["ID"].Value as Tuple<string,string>;
                DetailForm detailForm = new DetailForm(id);
                detailForm.d += new DetailForm.Mydel(LoadData);
                detailForm.ShowDialog();
                
            }
        }
    }
}
