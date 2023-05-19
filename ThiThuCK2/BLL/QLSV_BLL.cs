using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThiThuCK2.DAL;
using ThiThuCK2.DTO;
using ThiThuCK2.Models;

namespace ThiThuCK2.BLL
{
    public class QLSV_BLL
    {
        private static QLSV_BLL instance;

        private QLSV_BLL() { }

        public static QLSV_BLL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QLSV_BLL();
                }
                return instance;
            }
        }

        public List<CBBItem> GetCBBItems()
        {
            List<CBBItem> items = new List<CBBItem>();
            var hps = QLSV_DAL.Instance.GetAllHocPhan();
            foreach (var hp in hps)
            {
                items.Add(new CBBItem { Text = hp.TenHP, Value = hp.MaHP });
            }
            return items;   
            
        }
        public List<SinhVienGRV> GetSVs(string hpId, string search, int optionSort)
        {
            List<SinhVienGRV> result = new List<SinhVienGRV>();
            var svs = QLSV_DAL.Instance.GetAllSV();

            // filter tensv
            if(!string.IsNullOrEmpty(search))
            {
                svs = svs.Where(x=>x.TenSV.Contains(search)).ToList();
            }
            int stt = 1;
            foreach(var sv in svs)
            {
                var hp_svs = sv.HocPhans.ToList();

                // filter HocPhan
                if (hpId != "All")
                {
                    hp_svs = hp_svs.Where(x=>x.MaHP==hpId).ToList();
                }
                foreach(var hp_sv in hp_svs)
                {
                    var hpCur = QLSV_DAL.Instance.GetHocPhanById(hp_sv.MaHP);
                    result.Add(new SinhVienGRV
                    {
                        ID = new Tuple<string,string>(hp_sv.MaSV,hp_sv.MaHP),

                        STT = stt++,
                        TenSV = sv.TenSV,
                        LopSH = sv.LopSh,
                        TenHP = hpCur.TenHP,
                        DiemBT = hp_sv.DiemBT,
                        DiemGK = hp_sv.DiemGK,
                        DiemCK = hp_sv.DiemCK,
                        DiemTK = hp_sv.DiemBT*0.2 + hp_sv.DiemGK*0.2 + hp_sv.DiemCK*0.6,
                        NgayThi = hp_sv.NgayThi
                    });
                }
            }

            // sort
            switch (optionSort)
            {
                case 0: // sort ten
                    {
                        result = result.OrderBy(x => x.TenSV).ToList();
                        break;
                    }
                case 1: // sort diem tong ket
                    {
                        result = result.OrderBy(x => x.DiemTK).ToList();
                        break;
                    }
                default: { break; }
            }

            return result;
        }

        public void RemoveRecords(List<Tuple<string,string>> listId)
        {
            QLSV_DAL.Instance.RemoveHpSvs(listId);
        }

        public HP_SV GetHpSvById(Tuple<string, string> id)
        {
            return QLSV_DAL.Instance.GetHpSvById(id);
        }

        public List<string> GetLSHs() 
        {
            List<string> result = new List<string>();
            var svs = QLSV_DAL.Instance.GetAllSV();
            foreach (var s in svs )
            {
                result.Add(s.LopSh);
            }
            return result;
        }

        public bool AddRecord(SinhVienGRV record)
        {
            var sv = QLSV_DAL.Instance.GetSvById(record.ID.Item1);
            if (sv != null) return false; // trung sv
            sv = new SinhVien
            {
                MaSV = record.ID.Item1,
                TenSV = record.TenSV,
                LopSh = record.LopSH,
                GioiTinh = record.GioiTinh,
            };
            var hpsv = new HP_SV
            {
                MaSV = record.ID.Item1,
                MaHP = record.ID.Item2,
                DiemBT = record.DiemBT,
                DiemGK = record.DiemGK,
                DiemCK = record.DiemCK,
                NgayThi = record.NgayThi,
            };
            QLSV_DAL.Instance.AddOrUpdateSv(sv);
            QLSV_DAL.Instance.AddOrUpdateHpSv(hpsv);
            return true;
            
        }
        public void UpdateRecord(SinhVienGRV record)
        {
            var sv = QLSV_DAL.Instance.GetSvById(record.ID.Item1);
            sv.TenSV = record.TenSV;
            sv.LopSh = record.LopSH;
            sv.GioiTinh = record.GioiTinh;
            QLSV_DAL.Instance.AddOrUpdateSv(sv);

            var hpsv = QLSV_DAL.Instance.GetHpSvById(record.ID);
            if(hpsv == null) // chưa có 
            {
                hpsv = new HP_SV
                {
                    MaSV = record.ID.Item1,
                    MaHP = record.ID.Item2,
                };
            }
            hpsv.DiemBT = record.DiemBT;
            hpsv.DiemGK = record.DiemGK;
            hpsv.DiemCK = record.DiemCK;
            hpsv.NgayThi = record.NgayThi;
            QLSV_DAL.Instance.AddOrUpdateSv(sv);
            QLSV_DAL.Instance.AddOrUpdateHpSv(hpsv);
        }
    }
}
