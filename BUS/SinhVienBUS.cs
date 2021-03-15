using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIP_Project.BUS
{
    class SinhVienBUS
    {
        public static IQueryable<SinhVien> GetDSSinhVienbyMaKhoa(string makhoa, dbQuanLyDataContext db)
        {
            return db.SinhViens.Where(p => p.MaKhoa == makhoa);
        }

        public static SinhVien GetSinhVienbyMaSV(string masv, dbQuanLyDataContext db)
        {
            return db.SinhViens.SingleOrDefault(p => p.MaSV == masv);
        }

        public static void ThemSinhVien(string masv, string tensv, DateTime ngaysinh, string diachi, string makhoa, dbQuanLyDataContext db)
        {
            SinhVien sinhvien = new SinhVien {
                MaSV = masv,
                TenSV = tensv,
                NgaySinh = ngaysinh,
                DiaChi = diachi,
                MaKhoa = makhoa,
            };
            db.SinhViens.InsertOnSubmit(sinhvien);
            db.SubmitChanges();
        }
        public static void XoaSinhVien(SinhVien sv, dbQuanLyDataContext db)
        {
            db.SinhViens.DeleteOnSubmit(sv);
            db.SubmitChanges();
        }
        public static void SuaSinhVien(SinhVien sinhvien, string tensv, string diachi, DateTime ngaysinh, dbQuanLyDataContext db)
        {
            sinhvien.TenSV = tensv;
            sinhvien.DiaChi = diachi;
            sinhvien.NgaySinh = ngaysinh;
                db.SubmitChanges();
        }
    }
}
