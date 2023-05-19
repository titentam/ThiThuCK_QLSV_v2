using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiThuCK2.Models;

namespace ThiThuCK2
{
    public class CreateDB:CreateDatabaseIfNotExists<TamDB>
    {
        protected override void Seed(TamDB context)
        {
            context.HocPhans.AddRange(new HocPhan[]
            {
                new HocPhan{MaHP = "HP001", TenHP = "DHMT"},
                new HocPhan{MaHP = "HP002", TenHP = "DotNet"},
                new HocPhan{MaHP = "HP003", TenHP = "Java"},
            });
            context.SinhViens.AddRange(new SinhVien[]
            {
                new SinhVien{MaSV = "SV001", TenSV="NVA", LopSh="LSH001",
                    GioiTinh = true},
                new SinhVien{MaSV = "SV002", TenSV="NVB", LopSh="LSH001",
                    GioiTinh = true},
                new SinhVien{MaSV = "SV003", TenSV="NVC", LopSh="LSH002",
                    GioiTinh = true},
                new SinhVien{MaSV = "SV004", TenSV = "NVD", LopSh = "LSH002",
                    GioiTinh = true},
                new SinhVien{MaSV = "SV005", TenSV="NVE", LopSh="LSH003",
                    GioiTinh = true},

            });
            context.HP_SVs.AddRange(new HP_SV[]
            {
                new HP_SV{MaHP="HP001", MaSV="SV001"
                , DiemBT = 9, DiemGK = 9, DiemCK = 9.5,NgayThi = DateTime.Now},
                new HP_SV{MaHP="HP001", MaSV="SV002"
                , DiemBT = 9, DiemGK = 8, DiemCK = 9.5,NgayThi = DateTime.Now},
                new HP_SV{MaHP="HP001", MaSV="SV003"
                , DiemBT = 7, DiemGK = 9, DiemCK = 9.5,NgayThi = DateTime.Now},
                new HP_SV{MaHP="HP001", MaSV="SV005"
                , DiemBT = 9, DiemGK = 6, DiemCK = 9.5,NgayThi = DateTime.Now},

                new HP_SV{MaHP="HP002", MaSV="SV001"
                , DiemBT = 7, DiemGK = 10, DiemCK = 9.5,NgayThi = DateTime.Now},
                new HP_SV{MaHP="HP002", MaSV="SV004"
                , DiemBT = 9, DiemGK = 9, DiemCK = 9,NgayThi = DateTime.Now},

                new HP_SV{MaHP="HP003", MaSV="SV001"
                , DiemBT = 9, DiemGK = 9, DiemCK = 10,NgayThi = DateTime.Now},
                new HP_SV{MaHP="HP003", MaSV="SV002"
                , DiemBT = 9, DiemGK = 9, DiemCK = 8,NgayThi = DateTime.Now},
                new HP_SV{MaHP="HP003", MaSV="SV003"
                , DiemBT = 10, DiemGK = 9, DiemCK = 9.5,NgayThi = DateTime.Now},
                new HP_SV{MaHP="HP003", MaSV="SV004"
                , DiemBT = 8, DiemGK = 8, DiemCK = 9.5,NgayThi = DateTime.Now},

            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
