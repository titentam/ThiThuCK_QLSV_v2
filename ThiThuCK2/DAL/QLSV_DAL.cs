using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiThuCK2.Models;

namespace ThiThuCK2.DAL
{
    public class QLSV_DAL
    {
        private static QLSV_DAL instance;

        private QLSV_DAL() { }

        public static QLSV_DAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QLSV_DAL();
                }
                return instance;
            }
        }

        public List<HocPhan> GetAllHocPhan()
        {
            using(TamDB db = new TamDB())
            {
                return db.HocPhans.ToList();
            }
        }
        public List<SinhVien> GetAllSV()
        {
            using (TamDB db = new TamDB())
            {
                return db.SinhViens.Include("HocPhans").ToList();
            }
        }
        public HocPhan GetHocPhanById(string id)
        {
            using (TamDB db = new TamDB())
            {
                return db.HocPhans.Find(id);
            }
        }

        public void RemoveHpSvs(List<Tuple<string,string>> listID )
        {
            using (TamDB db = new TamDB())
            {
                foreach (var id in listID)
                {
                    var itemRemove = db.HP_SVs.Where(x=>x.MaSV == id.Item1&& x.MaHP==id.Item2).SingleOrDefault();
                    db.HP_SVs.Remove(itemRemove);
                }
                db.SaveChanges();
            }
        }

        public HP_SV GetHpSvById(Tuple<string, string>  id)
        {
            using (TamDB db = new TamDB())
            {

                return db.HP_SVs.
                    Include("HocPhan").
                    Include("SinhVien").
                    Where(x => x.MaSV == id.Item1 && x.MaHP == id.Item2).SingleOrDefault();
            }
        }

        public SinhVien GetSvById(string id)
        {
            using (TamDB db = new TamDB())
            {
                return db.SinhViens.SingleOrDefault(x => x.MaSV == id);
            }
        }
        public void AddOrUpdateSv(SinhVien sv)
        {
            using (TamDB db = new TamDB())
            {
                db.SinhViens.AddOrUpdate(sv);
                db.SaveChanges();
            }
        }
        public void AddOrUpdateHpSv(HP_SV hpsv)
        {
            using (TamDB db = new TamDB())
            {
                db.HP_SVs.AddOrUpdate(hpsv);
                db.SaveChanges();
            }
        }
    }
}
