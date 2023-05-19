using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThiThuCK2.Models;

namespace ThiThuCK2.DTO
{
    public class SinhVienGRV
    {
        public int STT { get; set; }
        public string TenSV { get; set; }
        public string  LopSH { get; set; }
        public string  TenHP { get; set; }
        public double DiemBT { get; set; }
        public double DiemGK { get; set; }
        public double DiemCK { get; set; }
        public double DiemTK{ get; set; }
        public DateTime NgayThi { get; set; }
        public bool GioiTinh { get; set; }  

        public Tuple<string,string> ID { get;set; } // luu mssv vs mahp

    }
}
