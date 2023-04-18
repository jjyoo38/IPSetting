using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Management;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.IO;
using IPSetting;
using WindowsFormsApp1;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        FileInfo info = new FileInfo("data.xml");
        string ManualIP = string.Empty;
        string ManualGateway = string.Empty;
        string ManualDNS = string.Empty;
        string IPAddress = string.Empty;
        string PC = string.Empty;
        string DNSServer = string.Empty;
        string Line = string.Empty;
        string Lancard = string.Empty;
        string GW = string.Empty;
        string Subnet = "255.255.255.0";
        

        public Form2(string LineCode1)
        {

            Line = LineCode1;

            InitializeComponent();
            FilladapterList();

            if (!info.Exists)
            {
                back.Text = "종료";
            }

            if (Line == "")
            {
                label1.Text = "IP 수동 입력";
                GW = string.Empty;
                DNSServer = string.Empty;
                dataGridView1.Hide();
            }

            else

            {
                label1.Text = Line;
                string query = "LineCode='" + Line + "'";

                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds.ReadXml("data.xml");
                    DataRow[] drr2;
                    drr2 = ds.Tables[0].Select(query);
                    dt = drr2.CopyToDataTable();

                    dataGridView1.DataSource = dt;
                    dataGridView1.ReadOnly = true;



                }
            }

        }

        private void FilladapterList()
        {
            adapterList.Items.Clear();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (
                    (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                  & (nic.OperationalStatus == OperationalStatus.Up)
                  & (!nic.Description.Contains("Microsoft"))
                  & (!nic.Description.Contains("Loopback"))
                    )

                    adapterList.Items.Add(nic.Description);

            }

            if (adapterList.Items.Count > 0)
            {
                adapterList.SelectedIndex = 0;
            }

            else
            {
                MessageBox.Show("사용 가능한 네트워크 어댑터가 없습니다.\r랜 케이블을 연결 후 다시 실행해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                FilladapterList();
            }

            else

            {
                adapterList.Items.Clear();
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (
                        (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                        & (!nic.Description.Contains("Microsoft"))
                        & (!nic.Description.Contains("Loopback"))
                       )

                        adapterList.Items.Add(nic.Description);

                }

                if (adapterList.Items.Count > 0)

                {
                    adapterList.SelectedIndex = 0;
                }

                else

                {
                    MessageBox.Show("사용 가능한 네트워크 어댑터가 없습니다.\r랜 케이블을 연결 후 다시 실행해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }

            }
        }



        public void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string Pccode = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string IPAddr = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

            IPAddress = IPAddr.Trim();
            PC = Pccode;
            int lastPoint = IPAddress.LastIndexOf(".");
            GW = IPAddress.Substring(0, lastPoint);
            GW = GW + ".1";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            string Pccode = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            string IPAddr = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

            IPAddress = IPAddr.Trim();
            PC = Pccode;
            int lastPoint = IPAddress.LastIndexOf(".");
            GW = IPAddress.Substring(0, lastPoint);
            GW = GW + ".1";
        }


        private void Back_Click(object sender, EventArgs e)
        {
            if (info.Exists)
            {
                this.DialogResult = DialogResult.OK;
                GC.Collect();
            }
            else
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        public void apply_Click(object sender, EventArgs e)
        {


            if (IPAddress.Length < 1)
            {
                MessageBox.Show("IP 주소가 비어있습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else

            {
                //IP 유효성 검증
                string Pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

                Regex r = new Regex(Pattern);
                Match m = r.Match(IPAddress);

                if (m.Success)
                {
                    if (PC.Contains("IPC"))
                    {
                        DNSServer = "10.243.151.93";
                    }
                    else
                    {
                        DNSServer = "10.240.31.130";
                    }

                    if (PC.Length > 0)
                   
                    {
                        applyIP();
                    }

                    else

                    {
                        IPAddress = ManualIP;
                        GW = ManualGateway;
                        DNSServer = ManualDNS;

                        applyIP();
                    }
                    

                }

                else

                {
                    MessageBox.Show("IP 주소의 형식이 올바르지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void adapterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.Description == adapterList.SelectedItem.ToString())
                {
                    Lancard = (nic.Description.ToString());
                }
            }
        }

        private void applyIP()
        {
            if (Form1.Ping(IPAddress) == true)
            {

                if (DialogResult.OK == MessageBox.Show("현재 가동중인 IP 주소입니다. IP가 충돌 할 경우 \r라인 가동에 문제가 생길 수 있습니다.\r계속하시겠습니까?", "경고", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    ChangeIPAddress(Lancard, IPAddress, Subnet, GW);
                    SetNameservers(Lancard, DNSServer);
                    if (ManualIP.Length == 0)
                    {
                        ShowPopup("");
                    }
                    else
                    {
                        ShowPopup("manual");
                    }
                }

                else
                {

                }
            }

            else

            {
                ChangeIPAddress(Lancard, IPAddress, Subnet, GW);
                SetNameservers(Lancard, DNSServer);
                if (ManualIP.Length == 0)
                {
                    ShowPopup("");
                }
                else
                {
                    ShowPopup("manual");
                }
            }

            GC.Collect();
        }

        private void ShowPopup(string type)
        {
            if(type == "manual")
            {
                MessageBox.Show("IP 주소가 " + IPAddress + "로 변경되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("IP 주소가 " + PC + "의 " + IPAddress + "로 변경되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        public void SetNameservers(string nic, string dnsServers)
        {
            using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (var networkConfigs = networkConfigMng.GetInstances())
                {
                    foreach (var managementObject in networkConfigs.Cast<ManagementObject>().Where(objMO => (bool)objMO["IPEnabled"] && objMO["Description"].Equals(nic)))
                    {
                        using (var newDNS = managementObject.GetMethodParameters("SetDNSServerSearchOrder"))
                        {
                            newDNS["DNSServerSearchOrder"] = dnsServers.Split(',');
                            managementObject.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                        }
                    }
                }
            }
        }

        public bool ChangeIPAddress(string sourceDescription, string sourceIPAddress, string sourceSubnetMask, string sourceGateway)

        {

            ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");

            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();



            foreach (ManagementObject managementObject in managementObjectCollection)

            {
                string description = managementObject["Description"] as string;

                if (string.Compare(description, sourceDescription, StringComparison.InvariantCultureIgnoreCase) == 0)

                {
                    try

                    {

                        ManagementBaseObject setGatewaysManagementBaseObject =

                            managementObject.GetMethodParameters("SetGateways");


                        setGatewaysManagementBaseObject["DefaultIPGateway"] = new string[] { sourceGateway };

                        setGatewaysManagementBaseObject["GatewayCostMetric"] = new int[] { 1 };



                        ManagementBaseObject enableStaticManagementBaseObject =

                            managementObject.GetMethodParameters("EnableStatic");



                        enableStaticManagementBaseObject["IPAddress"] = new string[] { sourceIPAddress };

                        enableStaticManagementBaseObject["SubnetMask"] = new string[] { sourceSubnetMask };



                        managementObject.InvokeMethod("EnableStatic", enableStaticManagementBaseObject, null);

                        managementObject.InvokeMethod("SetGateways", setGatewaysManagementBaseObject, null);



                        return true;

                    }

                    catch(Exception ex)

                    {
                        MessageBox.Show(ex.Message);
                        return false;

                    }

                }

            }

            return true;

        }

        //
        //IP 수동입력하는 부분
        //
        private void NumberClick(string Num)
        {
                     
            if (IPRadioButton.Checked)
            {
                if (textBox1.Text.Length >= 15) return;
                
                if (Num == "." && textBox1.Text.Length - textBox1.Text.Replace(".", "").Length > 2) return;
          
                ManualIP = ManualIP + Num;
                IPAddress = ManualIP;
                textBox1.Text = ManualIP;
            }

            if (GatewayRadioButton.Checked)

            {
                if (textBox2.Text.Length >= 15) return;
                if (Num == "." && textBox2.Text.Length - textBox2.Text.Replace(".", "").Length > 2) return;
                

                ManualGateway = ManualGateway + Num;
                textBox2.Text = ManualGateway;
            }

            if (DNSRadioButton.Checked)

            {
                if (textBox3.Text.Length >= 15) return;
                if (Num == "." && textBox3.Text.Length - textBox3.Text.Replace(".", "").Length > 2) return;

                ManualDNS = ManualDNS + Num;
                textBox3.Text = ManualDNS;
            }
                     
        }
        
        private void GatewayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (GatewayRadioButton.Checked)
            {
                if (textBox1.Text.Length != 0)
                {
                    

                    int lastPoint = textBox1.Text.LastIndexOf(".");

                    ManualGateway = textBox1.Text.Substring(0, lastPoint);

                    ManualGateway = ManualGateway + ".1";

                    textBox2.Text = ManualGateway;
                }

                else

                {
                    textBox2.Text = string.Empty;
                }
            }

            
        }


        private void del_Click(object sender, EventArgs e)
        {

            if (IPRadioButton.Checked)
            {
                if (textBox1.Text.Length > 0)
                {
                    ManualIP = ManualIP.Substring(0, ManualIP.Length - 1);
                    IPAddress = ManualIP;
                    textBox1.Text = ManualIP;
                }
            }

            if (GatewayRadioButton.Checked)

            {
                if (textBox2.Text.Length > 0)
                {
                    ManualGateway = string.Empty;
                    textBox2.Text = ManualGateway;
                }
            }

            if (DNSRadioButton.Checked)

            {
                if (textBox3.Text.Length > 0)
                {
                    ManualDNS = ManualDNS.Substring(0, ManualDNS.Length - 1);
                    textBox3.Text = ManualDNS;
                }

            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NumberClick("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NumberClick("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NumberClick("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NumberClick("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NumberClick("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NumberClick("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NumberClick("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            NumberClick("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            NumberClick("9");
        }

        private void button0_Click(object sender, EventArgs e)
        {
            NumberClick("0");
        }

        private void dot_Click(object sender, EventArgs e)
        {
           
            
            NumberClick(".");
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            //OA PC IP 원복용도
            IPAddress = "10.21.205.203";
            DNSServer = "10.240.31.130";
            GW = "10.21.205.1";

            ChangeIPAddress(Lancard, IPAddress, Subnet, GW);
            SetNameservers(Lancard, DNSServer);
        }
    }   
}

