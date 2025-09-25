using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_1_
{
    public delegate int SoSanh(object sv1, object sv2);
    internal class QuanLySinhVien
    {
        public List<SinhVien> dsSinhVien;
        public QuanLySinhVien()
        {
            dsSinhVien = new List<SinhVien>();
        }
        public SinhVien this[int index]
        {
            get { return this.dsSinhVien[index]; }
            set { this.dsSinhVien[index] = value; }
        }
        public void Them(SinhVien sv)
        {
            dsSinhVien.Add(sv);
        }
        public SinhVien Tim(object obj, SoSanh ss)
        {
            SinhVien svresult = null;
            foreach (SinhVien sv in dsSinhVien)
            {
                svresult = sv;
                break;
            }
            return svresult;
        }
        public bool Sua(SinhVien svsua, object obj, SoSanh ss)
        {
            int count;
            bool kq = false;
            count = this.dsSinhVien.Count - 1;
            for (int i = 0; i < count; i++)
            {
                if (ss(obj, this[i]) == 0)
                {
                    this[i] = svsua;
                    kq = true;
                    break;
                }
            }
            return kq;
        }
        public void Xoa(object obj, SoSanh ss)
        {
            int i = dsSinhVien.Count - 1;
            for (; i >= 0; i--)
            {
                if (ss(obj, this[i]) == 0)
                    this.dsSinhVien.RemoveAt(i);
            }
        }
        //Đọc file txt
        public void DocTuFile(string filename)
        {
            string t;
            string[] s;
            SinhVien sv;
            using (StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                while ((t = sr.ReadLine()) != null)
                {
                    s = t.Split('\t');
                    sv = new SinhVien();
                    sv.MSSV = s[0];
                    sv.HoTenLot = s[1];
                    sv.Ten = s[2];
                    sv.NgaySinh = DateTime.Parse(s[3]);
                    sv.DiaChi = s[4];
                    sv.Lop = s[5];
                    sv.GioiTinh = false;
                    if (s[6] == "1")
                        sv.GioiTinh = true;
                    sv.CMND = s[7];
                    sv.SDT = s[8];
                    string[] mh = s[9].Split(',');
                    foreach (string m in mh)
                        sv.MonHoc.Add(m);
                    this.Them(sv);
                }
            }
        }
    }
}
