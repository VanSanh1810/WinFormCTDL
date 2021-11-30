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
                for (int i = 0; i < sv.Length; i = i +1)
                {
                    string[] temp = sv[i].Split(' ');
                    //SV.AddFirst(new SinhVien(temp[0], temp[1], int.Parse(temp[2])));
                    if (SV.Count != 0) //Nếu danh sách không rỗng
                    {
                        if (int.Parse(temp[0]) < int.Parse(SV.First.Value.MSSV)) //Nếu mssv nhỏ nhất
                        {
                            SV.AddFirst(new SinhVien(temp[0], temp[1], int.Parse(temp[2])));
                        }
                        else
                        {
                            for (LinkedListNode<SinhVien> K = SV.First; K.Next != null; K = K.Next) //Vòng lặp tìm vị trí trung gian
                            {
                                if (int.Parse(K.Value.MSSV) < int.Parse(temp[0]) && int.Parse(K.Next.Value.MSSV) > int.Parse(temp[0]))
                                {
                                    SV.AddAfter(K, new SinhVien(temp[0], temp[1], int.Parse(temp[2])));
                                    goto NEXT;
                                }
                            }
                            SV.AddLast(new SinhVien(temp[0], temp[1], int.Parse(temp[2]))); //Nếu mssv lớn nhất
                        }
                    }
                    else //Nếu danh sách rỗng
                    {
                        SV.AddLast(new SinhVien(temp[0], temp[1], int.Parse(temp[2])));
                    }
                    NEXT:;
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
            catch
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

            if (SV.Count > 0)
            {
                Stack<string> lsv = new Stack<string>();
                for (int i = 0; i < SV.Count; i++)
                {
                    lsv.Push(SV.ElementAt<SinhVien>(i).MSSV.ToString() + " " + SV.ElementAt<SinhVien>(i).name.ToString() + " " + SV.ElementAt<SinhVien>(i).namsinh.ToString());
                }
                File.WriteAllLines("ListSV.txt", lsv);
            }
            else
            {
                try
                {
                    File.Delete("ListSV.txt");
                }
                catch
                {

                }
            }
            foreach(var i in SV)
            {
                if(i.MN.Count > 0)
                {
                    Stack<string> temp1 = new Stack<string>();
                    for(int j=0; j< i.MN.Count; j++)
                    {
                        temp1.Push(i.MN.ElementAt<MonHoc>(j).MH.ToString() + " " + i.MN.ElementAt<MonHoc>(j).soTinChi.ToString());
                    }
                    File.WriteAllLines(i.MSSV.ToString()+"MH.txt", temp1);
                }
                else
                {
                    try
                    {
                        File.Delete(i.MSSV.ToString() + "MH.txt");
                    }
                    catch
                    {

                    }
                }
                if (i.GT.Count > 0)
                {
                    Stack<string> temp2 = new Stack<string>();
                    for (int j = 0; j < i.MN.Count; j++)
                    {
                        temp2.Push(i.GT.ElementAt<GiaiThuong>(j).GT.ToString() + " " + i.GT.ElementAt<GiaiThuong>(j).tienThuong.ToString());
                    }
                    File.WriteAllLines(i.MSSV.ToString() + "GT.txt", temp2);

                }
                else
                {
                    try
                    {
                        File.Delete(i.MSSV.ToString() + "GT.txt");
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
