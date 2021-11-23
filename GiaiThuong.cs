using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCK_CTDL
{
    public class GiaiThuong
    {
        public string GT;
        public int tienThuong;
        public GiaiThuong(string GT, int tienThuong)
        {
            this.GT = GT;
            this.tienThuong = tienThuong;
        }
    }
}
