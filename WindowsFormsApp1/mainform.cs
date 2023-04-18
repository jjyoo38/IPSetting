using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        FileInfo info = new FileInfo("data.xml");
        string connstr = ("Data Source=10.243.151.14;Initial Catalog=ASANMOD;Persist Security Info=True;User ID=********;Password=********");
                     

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                if (Ping("10.243.151.14") == true)

                   {

                    DBStatus.Text = "DB Online";
                    DBStatus.BackColor = Color.LimeGreen;
              
                    SqlConnection conn = new SqlConnection(connstr);
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();

                    cmd.Connection = conn;
                    cmd.CommandText = "select LineCode, PCCode, IPAddress from MPC";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "MPC");
                    ds.WriteXml(@"data.xml");

                    MessageBox.Show("DB 연결 성공. \rMPC 테이블 → data.xml 업데이트 완료.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    conn.Close();

                   }

                else

                {
                    if (!info.Exists)
                    {
                        if (DialogResult.No == MessageBox.Show("DB 연결 실패.\rdata.xml 파일을 찾을 수 없습니다.\r수동 입력 모드로 들어가시겠습니까?", "오류", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                        {
                            Exit.PerformClick();
                        }

                        else

                        {
                            GotoManual.PerformClick();
                        }

                    }

                    else

                    {
                        DBStatus.Text = "DB Offline";
                        DBStatus.BackColor = Color.OrangeRed;
                        MessageBox.Show("DB 연결 실패. \r기존에 저장되어 있는 data.xml 파일에서 \rIP 주소 목록을 불러옵니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            dataver.Text = "data.xml 버전 : " + info.LastWriteTime;
        }
           
    


        private void CP_Click(object sender, EventArgs e)

        {
            string LineCode1 = "CP1";
            Form2 showForm2 = new Form2(LineCode1);
            
            showForm2.ShowDialog();
        }

        private void FE_Click(object sender, EventArgs e)
        {
            string LineCode1 = "FE1";
            Form2 showForm2 = new Form2(LineCode1);

            showForm2.ShowDialog();
        }

        private void RC_Click(object sender, EventArgs e)
        {
            string LineCode1 = "RC1";
            Form2 showForm2 = new Form2(LineCode1);

            showForm2.ShowDialog();
        }

        private void ARL_Click(object sender, EventArgs e)
        {
            string LineCode1 = "ARL";
            Form2 showForm2 = new Form2(LineCode1);

            showForm2.ShowDialog();
        }

        private void ARR_Click(object sender, EventArgs e)
        {
            string LineCode1 = "ARR";
            Form2 showForm2 = new Form2(LineCode1);

            showForm2.ShowDialog();
        }

        private void AFL_Click(object sender, EventArgs e)
        {
            string LineCode1 = "AFL";
            Form2 showForm2 = new Form2(LineCode1);

            showForm2.ShowDialog();


        }
        private void AFR_Click(object sender, EventArgs e)
        {
                string LineCode1 = "AFR";
                Form2 showForm2 = new Form2(LineCode1);

                showForm2.ShowDialog();
        }

        private void FC_Click(object sender, EventArgs e)
        { 
            string LineCode1 = "FC1";
            Form2 showForm2 = new Form2(LineCode1);

            showForm2.ShowDialog();
        }

        private void ST_Click(object sender, EventArgs e)
        {
            string LineCode1 = "ST3";
            Form2 showForm2 = new Form2(LineCode1);

            showForm2.ShowDialog();
        }

        private void GotoManual_Click(object sender, EventArgs e)
        {
            string LineCode1 = "";
            Form2 showForm2 = new Form2(LineCode1);

            showForm2.ShowDialog();
        }


        private void Exit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }


        public static bool Ping(string ip)
        {
            try

            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;

                string data = "abcdefghijklmnopqrstuvwxyz012345";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1000;
                PingReply reply = pingSender.Send(ipAddress, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }        
    }    
}
