using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        QuanLySinhVien qlsv = new QuanLySinhVien();
        private void ThemSV(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MSSV);
            lvitem.SubItems.Add(sv.HoTenLot);
            lvitem.SubItems.Add(sv.Ten);
            lvitem.SubItems.Add(sv.NgaySinh.ToLongDateString());
            lvitem.SubItems.Add(sv.DiaChi);
            lvitem.SubItems.Add(sv.Lop);
            string gt = "Nữ";
            if (sv.GioiTinh)
                gt = "Nam";
            lvitem.SubItems.Add(gt);
            lvitem.SubItems.Add(sv.CMND);
            lvitem.SubItems.Add(sv.SDT);
            string mh = string.Join(",", sv.MonHoc);
            lvitem.SubItems.Add(mh);
            this.lvSinhVien.Items.Add(lvitem);
        }
        private void LoadListView()
        {
            this.lvSinhVien.Items.Clear();
            foreach (SinhVien sv in qlsv.dsSinhVien)
                ThemSV(sv);
        }
        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            bool gt = true;
            List<string> mh = new List<string>();
            sv.MSSV = this.mtxMaSo.Text;
            sv.HoTenLot = this.txtHoTenLot.Text;
            sv.Ten = this.txtTen.Text;
            sv.NgaySinh = this.dtpNgaySinh.Value;
            sv.DiaChi = this.txtDiaChi.Text;
            sv.Lop = this.cboLop.Text;
            if (rdNu.Checked)
                gt = false;
            sv.GioiTinh = gt;
            sv.CMND = this.mtxCMND.Text;
            sv.SDT = this.mtxSDT.Text;
            for (int i = 0; i < this.clbMonHoc.Items.Count; i++)
                if (clbMonHoc.GetItemChecked(i))
                    mh.Add(clbMonHoc.Items.ToString());
            sv.MonHoc = mh;
            return sv;
        }
        private SinhVien GetSinhVienLV(ListViewItem lvitem)
        {
            SinhVien sv = new SinhVien();
            sv.MSSV = lvitem.SubItems[0].Text;
            sv.HoTenLot = lvitem.SubItems[1].Text;
            sv.Ten = lvitem.SubItems[2].Text;
            sv.NgaySinh = DateTime.Parse(lvitem.SubItems[3].Text);
            sv.DiaChi = lvitem.SubItems[4].Text;
            sv.Lop = lvitem.SubItems[5].Text;
            sv.GioiTinh = false;
            if (lvitem.SubItems[6].Text == "Nam")
                sv.GioiTinh = true;
            sv.CMND = lvitem.SubItems[7].Text;
            sv.SDT = lvitem.SubItems[8].Text;
            List<string> mh = new List<string>();
            string[] s = lvitem.SubItems[9].Text.Split(',');
            foreach (string t in s)
                mh.Add(t.Trim());
            sv.MonHoc = mh;
            return sv;
        }
        private void ThietLapThongTin(SinhVien sv)
        {
            this.mtxMaSo.Text = sv.MSSV;
            this.txtHoTenLot.Text = sv.HoTenLot;
            this.txtTen.Text = sv.Ten;
            this.dtpNgaySinh.Value = sv.NgaySinh;
            this.txtDiaChi.Text = sv.DiaChi;
            this.cboLop.Text = sv.Lop;
            if (sv.GioiTinh)
                this.rdNam.Checked = true;
            else
                this.rdNu.Checked = true;
            this.mtxCMND.Text = sv.CMND;
            this.mtxSDT.Text = sv.SDT;
            for (int i = 0; i < this.clbMonHoc.Items.Count; i++)
                this.clbMonHoc.SetItemChecked(i, false);
            foreach (string s in sv.MonHoc)
            {
                for (int i = 0; i < this.clbMonHoc.Items.Count; i++)
                    if (s.CompareTo(this.clbMonHoc.Items[i]) == 0)
                        this.clbMonHoc.SetItemChecked(i, true);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private bool KiemTraTT()
        {
            if (string.IsNullOrWhiteSpace(mtxMaSo.Text) ||
               string.IsNullOrWhiteSpace(txtHoTenLot.Text) ||
               string.IsNullOrWhiteSpace(txtTen.Text) ||
               string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
               string.IsNullOrWhiteSpace(cboLop.Text) ||
               string.IsNullOrWhiteSpace(mtxCMND.Text) ||
               string.IsNullOrWhiteSpace(mtxSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu thông tin!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (mtxMaSo.Text.Length != 7)
            {
                MessageBox.Show("MSSV phải gồm 7 số!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (mtxSDT.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải gòm 10 số.", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!rdNam.Checked && !rdNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Thiếu thông tin!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (clbMonHoc.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 môn học.", "Thiếu thông tin!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            DateTime ngaySinh = dtpNgaySinh.Value.Date;
            if (ngaySinh >= DateTime.Now.Date)
            {
                MessageBox.Show("Ngày sinh không hợp lệ.", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraTT()) return;
            var sv = GetSinhVien();
            qlsv.dsSinhVien.Add(sv);
            ThemSV(sv);
            MessageBox.Show("Thêm sinh viên thành công!");
        }
        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.lvSinhVien.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem lvitem = this.lvSinhVien.SelectedItems[0];
                SinhVien sv = GetSinhVienLV(lvitem);
                ThietLapThongTin(sv);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            SinhVien svMoi = GetSinhVien();
            if (!KiemTraTT()) return;
            string mssv = mtxMaSo.Text.Trim();
            var sv = qlsv.dsSinhVien.FirstOrDefault(x => x.MSSV == mssv);
            if (sv != null)
            {
                sv.HoTenLot = svMoi.HoTenLot;
                sv.Ten = svMoi.Ten;
                sv.NgaySinh = svMoi.NgaySinh;
                sv.DiaChi = svMoi.DiaChi;
                sv.Lop = svMoi.Lop;
                sv.GioiTinh = svMoi.GioiTinh;
                sv.CMND = svMoi.CMND;
                sv.SDT = svMoi.SDT;
                sv.MonHoc = svMoi.MonHoc;
                ThemSV(svMoi);
                MessageBox.Show("Cập nhật sinh viên thành công!");
            }
            else
                MessageBox.Show("Không tìm thấy MSSV để cập nhật!");
        }
    }
}
