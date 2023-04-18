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

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        string IPAddress = string.Empty;
        string PC = string.Empty;
        string GW = "10.243.151.1";
        string Subnet = "255.255.255.0";
        string[] DNSServer = {"10.240.31.130"};
        string Line = string.Empty;
        string Lancard = string.Empty;

        string p_line = string.Empty;
        
        public Form2(string LineCode1)
        {

            Line = LineCode1;
            
            InitializeComponent();
            label1.Text = Line;
            string query = "LineCode='"+Line+"'";

            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.ReadXml("data.xml");
                DataRow[] drr2;
                drr2 = ds.Tables[0].Select(query);
                dt = drr2.CopyToDataTable();
                                
                dataGridView1.DataSource = dt;
                dataGridView1.ReadOnly = true;

                FilladapterList();
                
            }    
        }
        
        private void FilladapterList()
        {
            adapterList.Items.Clear();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if  (
                    (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                  & (nic.OperationalStatus == OperationalStatus.Up)
                  & (!nic.Description.Contains("Microsoft"))
                  & (!nic.Description.Contains("Loopback"))
                    )
                
                    adapterList.Items.Add(nic.Description);
                
            }
            adapterList.SelectedIndex = 0;
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           if  (checkBox1.Checked == true)
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
                       )

                        adapterList.Items.Add(nic.Description);

                }
                adapterList.SelectedIndex = 0;
            }
        }

       


        private void Back_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string Pccode = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string IPAddr = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        
            // IPAddress 끝에 공백이 포함될 경우 IP 변경안됨. 191231 Trim() 함수 추가
                        
            IPAddress = IPAddr.Trim();
            PC = Pccode;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            string Pccode = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            string IPAddr = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

            // IPAddress 끝에 공백이 포함될 경우 IP 변경안됨. 191231 Trim() 함수 추가
                        
            IPAddress = IPAddr.Trim();
            PC = Pccode;
        }




        public void apply_Click(object sender, EventArgs e)
        {
            ChangeIPAddress(Lancard, IPAddress, Subnet, GW);
            SetNameservers(Lancard, DNSServer);
            MessageBox.Show("IP 주소가 " + PC + "의 " + IPAddress + " 로 변경되었습니다.","정보",MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        public static void SetNameservers(string nicDescription, string[] dnsServers)
        {
            using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (var networkConfigs = networkConfigMng.GetInstances())
                {
                    foreach (var managementObject in networkConfigs.Cast<ManagementObject>().Where(mo => (bool)mo["IPEnabled"] && (string)mo["Description"] == nicDescription))
                    {
                        using (var newDNS = managementObject.GetMethodParameters("SetDNSServerSearchOrder"))
                        {
                            newDNS["DNSServerSearchOrder"] = dnsServers;
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

                    catch

                    {

                        return false;

                    }

                }

            }

            return true;

        }

        





        private void button1_Click(object sender, EventArgs e)
        {   
            //OA PC IP 원복 (테스트용)
         ChangeIPAddress(Lancard, "10.21.205.203", "255.255.255.0", "10.21.205.1");
         SetNameservers(Lancard, DNSServer);

        }

    }   
}

