using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCK_CTDL
{
    public class MonHoc
    {
        public string MH;
        public int soTinChi;
        public MonHoc(string MH, int soTinChi)
        {
            this.MH = MH;
            this.soTinChi = soTinChi;
        }
    }
}
