using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;


namespace main_form
{
    public partial class Form1 : Form
    {

        bool printport_only = false;

        public Form1()
        {
            InitializeComponent();

            string finalpath;
            string strDeviceName = "USB\\\\VID_"; 
            //string Win32_PnPEntity = "Select * From Win32_PnPEntity Where DeviceID like '%USB\\\\VID_%' OR DeviceID like '%USB\\VID_%'  ";
            string Win32_PnPEntity = "Select * From Win32_PnPEntity Where DeviceID like '%" + strDeviceName + "%' ";
            //string strDeviceName = "USB\\\\VID_"; string Win32_PnPEntity = "Select * From Win32_PnPEntity  WHERE Caption like '%USB%'";

            
            
            ManagementObjectSearcher mySearcher = new ManagementObjectSearcher(Win32_PnPEntity);
            foreach (ManagementObject mobj in mySearcher.Get()) {
                //MessageBox.Show(mobj["DeviceID"].ToString());
                //string strDeviceID = mobj["DeviceID"].ToString();
                finalpath = mobj["DeviceID"].ToString() + mobj["ClassGuid"].ToString();
                finalpath = finalpath.Replace(@"\", "#");
                lb_usb_devices.Items.Add(finalpath);
            }


            //string strDeviceName = "USB\\\\VID_"; 
            string strDeviceName1 = @"USB\\VID_";
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void lb_usb_devices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lb_usb_devices_doubleclick(object sender, EventArgs e)
        {
            MessageBox.Show(@"\\?" + Convert.ToString(lb_usb_devices.SelectedItem));
           // \?usb#vid_vvvv&pid_pppp#
             //   \?usb#vid_vvvv&pid_pppp&mi_ii#aaaaaaaaaaaaaaaa#{gggggggg-gggg-gggg-gggg-gggggggggggg}
        }

        private void chb_ubprint_port_CheckedChanged(object sender, EventArgs e)
        {
         if (chb_ubprint_port.Checked == true) {printport_only = true;}
         else {printport_only = false;}
         refresh_device_list();
        }

      
    
    


        public void refresh_device_list()
        {

            lb_usb_devices.Items.Clear();
            string finalpath;
            string strDeviceName = "USB\\\\VID_";
            string Win32_PnPEntity;

            if (printport_only == true) {Win32_PnPEntity = "Select * From Win32_PnPEntity Where DeviceID like '%" + strDeviceName + "%' AND Service = 'usbprint' "; }
                else{Win32_PnPEntity = "Select * From Win32_PnPEntity Where DeviceID like '%" + strDeviceName + "%' "; }

            //string strDeviceName = "USB\\\\VID_"; string Win32_PnPEntity = "Select * From Win32_PnPEntity  WHERE Caption like '%USB%'";



            ManagementObjectSearcher mySearcher = new ManagementObjectSearcher(Win32_PnPEntity);
            foreach (ManagementObject mobj in mySearcher.Get())
            {
                //MessageBox.Show(mobj["DeviceID"].ToString());
                //string strDeviceID = mobj["DeviceID"].ToString();
                finalpath = mobj["DeviceID"].ToString() + mobj["ClassGuid"].ToString();
                finalpath = finalpath.Replace(@"\", "#");

                lb_usb_devices.Items.Add(finalpath);
            }
        }






    }

}

//[Dynamic, Provider("CIMWin32"), UUID("{FE28FD98-C875-11d2-B352-00104BC97924}"), AMENDMENT]
//class Win32_PnPEntity : CIM_LogicalDevice
//{
//  uint16   Availability;
//  string   Caption;
//  string   ClassGuid;
//  string   CompatibleID[];
//  uint32   ConfigManagerErrorCode;
//  boolean  ConfigManagerUserConfig;
//  string   CreationClassName;
//  string   Description;
//  string   DeviceID;
//  boolean  ErrorCleared;
//  string   ErrorDescription;
//  string   HardwareID[];
//  datetime InstallDate;
//  uint32   LastErrorCode;
//  string   Manufacturer;
//  string   Name;
//  string   PNPClass;
//  string   PNPDeviceID;
//  uint16   PowerManagementCapabilities[];
//  boolean  PowerManagementSupported;
//  boolean  Present;
//  string   Service;
//  string   Status;
//  uint16   StatusInfo;
//  string   SystemCreationClassName;
//  string   SystemName;
//};