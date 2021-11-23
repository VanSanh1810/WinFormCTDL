using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCK_CTDL
{
    public class SinhVien
    {
        public string MSSV;
        public string name;
        public int namsinh;
        public LinkedList<MonHoc> MN;
        public LinkedList<GiaiThuong> GT;
        public DataTable dt_MH;
        public DataTable dt_GT;
        public SinhVien(string MSSV, string name, int namsinh)
        {
            this.name = name;
            this.MSSV = MSSV;
            this.namsinh = namsinh;
            this.MN = new LinkedList<MonHoc>();
            this.GT = new LinkedList<GiaiThuong>();
            dt_MH = new DataTable(); //Bảng dữ liệu của môn học
            dt_GT = new DataTable(); //Bảng dữ liệu của sinh viên
            Init_DTGT();
            Init_DTMN();
        }
        private void Init_DTMN()
        {
            dt_MH.Columns.Add("Tên môn");
            dt_MH.Columns.Add("Số tín chỉ");
        }
        private void Init_DTGT()
        {
            dt_GT.Columns.Add("Tên giải thưởng");
            dt_GT.Columns.Add("Tiền thưởng");
        }
        public void Update_dtMH()
        {
            dt_MH.Rows.Clear();
            for (LinkedListNode<MonHoc> K = MN.First; K != null; K = K.Next)
            {
                DataRow dr = dt_MH.NewRow();
                dr[0] = K.Value.MH;
                dr[1] = K.Value.soTinChi;
                dt_MH.Rows.Add(dr);
            }
        }

        public void Update_dtGT()
        {
            dt_GT.Rows.Clear();
            for (LinkedListNode<GiaiThuong> K = GT.First; K != null; K = K.Next)
            {
                DataRow dr = dt_GT.NewRow();
                dr[0] = K.Value.GT;
                dr[1] = K.Value.tienThuong;
                dt_GT.Rows.Add(dr);
            }
        }
    }
}
