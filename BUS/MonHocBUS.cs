using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIP_Project.BUS
{
    class MonHocBUS
    {

        public static IQueryable<MonHoc> GetDSMonHocbyMaKhoa(string makhoa, dbQuanLyDataContext db)
        {
            return db.MonHocs.Where(p => p.MaKhoa == makhoa);
            
        }

        public static MonHoc GetMonHocbyMaMH(string mamh, dbQuanLyDataContext db)
        {
            return db.MonHocs.SingleOrDefault(p => p.MaMH == mamh);
        }

        public static void ThemMonHoc(string mamh, string tenmh, string makhoa, int sotinchi, string gv, dbQuanLyDataContext db)
        {
            MonHoc mh = new MonHoc
            {
                MaMH = mamh,
                TenMH = tenmh,
                GV = gv,
                SoTinChi = sotinchi,
                MaKhoa = makhoa
            };
            db.MonHocs.InsertOnSubmit(mh);
            db.SubmitChanges();
        }

        public static void XoaMonHoc(MonHoc monhoc, dbQuanLyDataContext db)
        {
            db.MonHocs.DeleteOnSubmit(monhoc);
            db.SubmitChanges();
        }

        public static void SuaMonHoc(MonHoc monhoc, string tenmh, string gv, int sotinchi, dbQuanLyDataContext db)
        {
            monhoc.TenMH = tenmh;
            monhoc.GV = gv;
            monhoc.SoTinChi = sotinchi;

            db.SubmitChanges();
        }
    }
}
