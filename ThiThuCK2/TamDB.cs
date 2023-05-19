using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Design;
using System.Linq;
using ThiThuCK2.Models;

namespace ThiThuCK2
{
    public class TamDB : DbContext
    {
        public TamDB()
            : base("name=TamDB")
        {
            Database.SetInitializer<TamDB>(new CreateDB());
        }

        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<HocPhan> HocPhans { get; set; }
        public virtual DbSet<HP_SV> HP_SVs { get; set; }

        
    }

}