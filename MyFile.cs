using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BTCK_CTDL
{
    public class MyFile
    {
        public static void SetupSV(LinkedList<SinhVien> SV)
        {
            try
            {
                string[] sv = File.ReadAllLines("ListSV.txt");
                for (int i = 0; i < sv.Length; i++)
                {
                    string[] temp = sv[i].Split(' ');
                    SV.AddFirst(new SinhVien(temp[0], temp[1], int.Parse(temp[2])));
                }
            }
            catch
            {

            }
        }
        public static void SetupGT(LinkedList<GiaiThuong> GT, string filename)
        {
            try
            {
                string[] gt = File.ReadAllLines(filename + "GT.txt");
                for (int i = 0; i < gt.Length; i++)
                {
                    string[] temp = gt[i].Split(' ');
                    GT.AddFirst(new GiaiThuong(temp[0], int.Parse(temp[1])));
                }
            }
            catch (Exception e)
            {
                
            }
        }
        public static void SetupMH(LinkedList<MonHoc> MH, string filename)
        {
            try
            {
                string[] mh = File.ReadAllLines(filename + "MH.txt");
                for (int i = 0; i < mh.Length; i++)
                {
                    string[] temp = mh[i].Split(' ');
                    MH.AddFirst(new MonHoc(temp[0], int.Parse(temp[1])));
                }
            }
            catch
            {

            }
        }

        public static void MakeFile(LinkedList<SinhVien> SV)
        {
            
            Stack<string> lsv = new Stack<string>();
            for(int i=0; i< SV.Count; i++)
            {
                lsv.Push(SV.ElementAt<SinhVien>(i).MSSV.ToString() + " " + SV.ElementAt<SinhVien>(i).name.ToString() + " " + SV.ElementAt<SinhVien>(i).namsinh.ToString());
            }
            File.WriteAllLines("ListSV.txt", lsv);
            foreach(var i in SV)
            {
                if(i.MN.Count > 0)
                {
                    string[] temp1 = { };
                    for(int j=0; j< i.MN.Count; j++)
                    {
                        temp1[j] = i.MN.ElementAt<MonHoc>(j).MH.ToString() + " " + i.MN.ElementAt<MonHoc>(j).soTinChi.ToString();
                    }
                    File.WriteAllLines(i.MSSV.ToString()+"MH.txt", temp1);
                }
                if (i.GT.Count > 0)
                {
                    string[] temp2 = { };
                    for (int j = 0; j < i.MN.Count; j++)
                    {
                        temp2[j] = i.GT.ElementAt<GiaiThuong>(j).GT.ToString() + " " + i.GT.ElementAt<GiaiThuong>(j).tienThuong.ToString();
                    }
                    File.WriteAllLines(i.MSSV.ToString() + "GT.txt", temp2);
                }
            }
        }
    }
}
