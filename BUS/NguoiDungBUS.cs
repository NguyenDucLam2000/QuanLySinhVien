using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace UIP_Project.BUS
{
    class NguoiDungBUS
    {
        public static NguoiDung GetNguoiDung(string tendangnhap, dbQuanLyDataContext db)
        {
            return db.NguoiDungs.SingleOrDefault(p => p.TenDangNhap == tendangnhap);
        }

        public static void ThemNguoiDung(string tendangnhap, string matkhau, dbQuanLyDataContext db)
        {
            string encodedMatkhau = GetMD5(matkhau);
            NguoiDung user = new NguoiDung
            {
                TenDangNhap = tendangnhap,
                MatKhau = encodedMatkhau,
                Moderator = 0
            };
            db.NguoiDungs.InsertOnSubmit(user);
            db.SubmitChanges();
        }

        public static void SuaNguoiDung(NguoiDung nguoidung, string matkhau, dbQuanLyDataContext db)
        {
            string encodedMatkhau = GetMD5(matkhau);
            nguoidung.MatKhau = encodedMatkhau;
            db.SubmitChanges();
        }

        public static void XoaNguoiDung(NguoiDung nguoidung, dbQuanLyDataContext db)
        {
            db.NguoiDungs.DeleteOnSubmit(nguoidung);
            db.SubmitChanges();
        }

        public static string GetMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] originalText = ASCIIEncoding.Default.GetBytes(str);
            byte[] encodedText = md5.ComputeHash(originalText);

            return BitConverter.ToString(encodedText).Replace("-", "");
        }
    }
}
