using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTCK_CTDL
{
    public partial class Form1 : Form
    {
        #region Declaration
        public string _MSSV;
        public string _name;
        public int _namsinh;
        public string _MH;
        public int _soTinChi;
        public string _GT;
        public int _tienThuong;

        public DataTable dt_SV = new DataTable(); //Bảng dữ liệu của sinh viên
        public LinkedList<SinhVien> SV = new LinkedList<SinhVien>();

        Exception TrungDuLieu_SV = new Exception("This student already exists in the list !");
        Exception KhongCoDuLieu_SV = new Exception("This student dont exists in the list !");

        Exception TrungDuLieu_MH = new Exception("This subject already exists in the list !");
        Exception KhongCoDuLieu_MH = new Exception("This subject dont exists in the list !");

        Exception TrungDuLieu_GT = new Exception("This awards already exists in the list !");
        Exception KhongCoDuLieu_GT = new Exception("This awards dont exists in the list !");
        #endregion

        public Form1()
        {
            InitializeComponent();
            Init_DTSV();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        //////////////////////////////////////////////////////////////////////////////////////////

        #region Function
        private void Update_dtSV()
        {
            dt_SV.Rows.Clear();
            for (LinkedListNode<SinhVien> K = SV.First; K != null; K = K.Next)
            {
                DataRow dr = dt_SV.NewRow();
                dr[0] = K.Value.MSSV;
                dr[1] = K.Value.name;
                dr[2] = K.Value.namsinh;
                dt_SV.Rows.Add(dr);
            }
            dataGridView_SV.DataSource = dt_SV;
            dt_SV.AcceptChanges();
        }

        private void Init_DTSV()
        {
            dt_SV.Columns.Add("MSSV");
            dt_SV.Columns.Add("Tên");
            dt_SV.Columns.Add("Năm sinh");
        }

        private bool checkSV(string _MSSV) //Kiểm tra xem có trùng dữ liệu, không trùng thì false ngược lại thì true
        {
            for (LinkedListNode<SinhVien> K = SV.First; K != null; K = K.Next)
            {
                if(_MSSV == K.Value.MSSV)
                {
                    return true;
                }
            }
            return false;
        }
        private bool checkMH(string MH, SinhVien a) //Kiểm tra xem có trùng dữ liệu, không trùng thì false ngược lại thì true
        {
            for (LinkedListNode<MonHoc> K = a.MN.First; K != null; K = K.Next)
            {
                if (MH == K.Value.MH)
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkGT(string GT, SinhVien a) //Kiểm tra xem có trùng dữ liệu, không trùng thì false ngược lại thì true
        {
            for (LinkedListNode<GiaiThuong> K = a.GT.First; K != null; K = K.Next)
            {
                if (GT == K.Value.GT)
                {
                    return true;
                }
            }
            return false;
        }

        private void SetCheckList_GT(LinkedList<GiaiThuong> a)
        {
            checkedListBox_delGT.Items.Clear();
            for(LinkedListNode<GiaiThuong> K = a.First; K != null; K = K.Next)
            {
                string temp = K.Value.GT;
                checkedListBox_delGT.Items.Add(temp);
            }
        }
        #endregion



        ///////////////////////////////////////////////////////////////////////////////////////////


        #region EventHandler
        ////////////////////SV
        private void btn_addSV_Click(object sender, EventArgs e) //Thêm sinh viên
        {
            try
            {
                _MSSV = null;
                _name = null;
                _namsinh = 0;
                _MSSV = txbMSSV_addSV.Text;
                _name = txbTENSV_addSV.Text;
                _namsinh = int.Parse(txbNAMSINH_addSV.Text);
                if (!checkSV(_MSSV)) //Không có sv
                {
                    SV.AddLast(new SinhVien(_MSSV, _name, _namsinh));
                    Update_dtSV();
                    //
                    txbMSSV_addSV.Text = "";
                    txbTENSV_addSV.Text = "";
                    txbNAMSINH_addSV.Text = "";
                }
                else
                {
                    throw TrungDuLieu_SV;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }
        }
        private void txbTENSV_addSV_KeyPress(object sender, KeyPressEventArgs e) //txbTENSV_addSV chỉ được dùng ký tự chữ (Letter only))
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
        private void txbNAMSINH_addSV_KeyPress(object sender, KeyPressEventArgs e) //number only
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void btn_delSV_Click(object sender, EventArgs e) // Xóa sinh viên
        {
            try
            {
                _MSSV = null;
                _MSSV = txbMSSV_delSV.Text;
                if (checkSV(_MSSV)) //Có sinh viên
                {
                    var K = SV.SingleOrDefault(r => r.MSSV == _MSSV);
                    SV.Remove(K);
                    Update_dtSV();
                    //
                    txbMSSV_delSV.Text = "";
                }
                else
                {
                    throw KhongCoDuLieu_SV;
                }
            }
            catch (Exception eror)
            {
                MessageBox.Show(eror.Message);
            }
        }


        ///////////////////////////////////////MH
        
        private void btn_addMH_Click(object sender, EventArgs e)
        {
            try
            {
                _MSSV = null;
                _MSSV = txbMSSV_addMH.Text;
                if (checkSV(_MSSV))
                {
                    lb_MHofSV_addMH.Text = "(" + _MSSV.ToString() + ")";
                    var K = SV.SingleOrDefault(r => r.MSSV == _MSSV);
                    _MH = txbTENMH_addMH.Text;
                    if (!checkMH(_MH, K))
                    {
                        _soTinChi = int.Parse(txbSOTC_addMH.Text);
                        K.MN.AddLast(new MonHoc(_MH, _soTinChi));
                        K.Update_dtMH();
                        dataGridView_MH.DataSource = K.dt_MH;
                        K.dt_MH.AcceptChanges();
                        //
                        txbTENMH_addMH.Text = "";
                        txbSOTC_addMH.Text = "";
                    }
                    else
                    {
                        throw TrungDuLieu_MH;
                    }
                }
                else
                {
                    throw KhongCoDuLieu_SV;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void txbSOTC_addMH_KeyPress(object sender, KeyPressEventArgs e) //Number only
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
        private void btn_delMH_Click(object sender, EventArgs e)
        {
            try
            {
                _MSSV = null;
                _MSSV = txbMSSV_delMH.Text;
                if (checkSV(_MSSV))
                {
                    lb_MHofVS_delMH.Text = "(" + _MSSV.ToString() + ")";
                    var K = SV.SingleOrDefault(r => r.MSSV == _MSSV);
                    _MH = txbTENMH_delMH.Text;
                    if (checkMH(_MH, K))
                    {
                        var I = K.MN.SingleOrDefault(r => r.MH == _MH);
                        K.MN.Remove(I);
                        K.Update_dtMH();
                        dataGridView_MH.DataSource = K.dt_MH;
                        K.dt_MH.AcceptChanges();
                        //
                        txbMSSV_delMH.Text = "";
                        txbTENMH_delMH.Text = "";
                    }
                    else
                    {
                        throw KhongCoDuLieu_MH;
                    }
                }
                else
                {
                    throw KhongCoDuLieu_SV;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }


        /////////////////////////////////////////////////GT
        private void btn_addGT_Click(object sender, EventArgs e)
        {
            try
            {
                _MSSV = null;
                _MSSV = txbMSSV_addGT.Text;
                if (checkSV(_MSSV))
                {
                    //lb_MHofSV_addMH.Text = "(" + _MSSV.ToString() + ")";
                    var K = SV.SingleOrDefault(r => r.MSSV == _MSSV);
                    _GT = txbTENGT_addGT.Text;
                    if(!K.GT.Any(r => r.GT == _GT))
                    {
                        _tienThuong = int.Parse(txbTIENTHG_addGT.Text);
                        K.GT.AddLast(new GiaiThuong(_GT, _tienThuong));
                        K.Update_dtGT();
                        dataGridView_GT.DataSource = K.dt_GT;
                        K.dt_GT.AcceptChanges();
                        //
                        txbTENGT_addGT.Text = "";
                        txbTIENTHG_addGT.Text = "";
                    }
                    else
                    {
                        throw TrungDuLieu_GT;
                    }
                }
                else
                {
                    throw KhongCoDuLieu_SV;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void txbTIENTHG_addGT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
        private void btn_delGT_Click(object sender, EventArgs e)
        {
            try
            {
                //checkedListBox_delGT.Items.AddRange(new object[] { "a","b","c"});
                _MSSV = null;
                _MSSV = txbMSSV_delGT.Text;
                if (checkSV(_MSSV))
                {
                    var K = SV.SingleOrDefault(r => r.MSSV == _MSSV);
                    foreach(string s in checkedListBox_delGT.CheckedItems)
                    {
                        var I = K.GT.SingleOrDefault(r => r.GT == s);
                        K.GT.Remove(I);
                        K.Update_dtGT();
                        dataGridView_MH.DataSource = K.dt_MH;
                        K.dt_MH.AcceptChanges();
                    }
                }
                else
                {
                    throw KhongCoDuLieu_SV;
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void txbMSSV_delGT_Leave(object sender, EventArgs e)
        {
            try
            {
                _MSSV = txbMSSV_delGT.Text;
                var K = SV.Single(r => r.MSSV == _MSSV);
                SetCheckList_GT(K.GT);
            }catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        /////////////////////////////////////////////Find
        private void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                _MSSV = txbMSSV_find.Text;
                if (checkSV(_MSSV))
                {
                    var K = SV.SingleOrDefault(r => r.MSSV == _MSSV);
                    listBox1.Items.Clear();
                    listBox1.Items.Add("MSSV: " + K.MSSV.ToString());
                    listBox1.Items.Add("Tên: " + K.name.ToString());
                    listBox1.Items.Add("Năm sinh: " + K.namsinh.ToString());
                    listBox1.Items.Add("Số môn học đã đăng ký: " + K.MN.Count.ToString());
                    listBox1.Items.Add("Số giải thưởng đã có: " + K.GT.Count.ToString());
                    dataGridView1.DataSource = K.dt_GT;
                    dataGridView2.DataSource = K.dt_MH;
                }
                else
                {
                    throw KhongCoDuLieu_SV;
                }
            }catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        #endregion


    }
}
