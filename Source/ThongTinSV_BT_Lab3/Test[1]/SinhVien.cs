using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_1_
{
    internal class SinhVien
    {
        public string MSSV { get; set; }
        public string HoTenLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string Lop { get; set; }
        public bool GioiTinh { get; set; }
        public string CMND { get; set; }
        public string SDT { get; set; }
        public List<string> MonHoc { get; set; }
        public SinhVien()
        {
            MonHoc = new List<string>();
        }

        public SinhVien(string mSSV, string hoTenLot, string ten, DateTime ngaySinh, string diaChi, string lop, bool gioiTinh, string cMND, string sDT, List<string> monHoc)
        {
            MSSV = mSSV;
            HoTenLot = hoTenLot;
            Ten = ten;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            Lop = lop;
            GioiTinh = gioiTinh;
            CMND = cMND;
            SDT = sDT;
            MonHoc = monHoc;
        }
    }
}
