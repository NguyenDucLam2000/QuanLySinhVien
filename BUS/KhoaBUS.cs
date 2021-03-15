using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIP_Project.BUS
{
    class KhoaBUS
    {
        public static Khoa GetKhoabyMaKhoa(string makhoa, dbQuanLyDataContext db)
        {
            return db.Khoas.Where(p => p.MaKhoa == makhoa).SingleOrDefault();
        }

        public static void ThemKhoa(string makhoa, string tenkhoa, dbQuanLyDataContext db)
        {

            Khoa kh = new Khoa
            {
                MaKhoa = makhoa,
                TenKhoa = tenkhoa
            };
            db.Khoas.InsertOnSubmit(kh);
            db.SubmitChanges();
        }

        public static void XoaKhoa(Khoa khoa, dbQuanLyDataContext db)
        {
            db.Khoas.DeleteOnSubmit(khoa);
            db.SubmitChanges();
        }

        public static void SuaKhoa(Khoa khoa, string tenkhoa, dbQuanLyDataContext db)
        {
            khoa.TenKhoa = tenkhoa;
            db.SubmitChanges();
        }

        public static List<Khoa> GetDSKhoa(dbQuanLyDataContext db)
        {
            return db.Khoas.Select(p => p).ToList();
        }
    }
}
