using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Management;

namespace global_functions
{
    public class app_functions
    {
        //public override bool refresh_device_list()
        //{
        //    string finalpath;
        //    string Win32_PnPEntity = "Select * From Win32_PnPEntity Where DeviceID like '%USB\\\\VID_%' OR DeviceID like '%USB\\VID_%'";
        //    //string strDeviceName = "USB\\\\VID_"; string Win32_PnPEntity = "Select * From Win32_PnPEntity  WHERE Caption like '%USB%'";



        //    ManagementObjectSearcher mySearcher = new ManagementObjectSearcher(Win32_PnPEntity);
        //    foreach (ManagementObject mobj in mySearcher.Get())
        //    {
        //        //MessageBox.Show(mobj["DeviceID"].ToString());
        //        //string strDeviceID = mobj["DeviceID"].ToString();
        //        finalpath = mobj["DeviceID"].ToString() + mobj["Service"].ToString();
        //        finalpath = finalpath.Replace(@"\", "#");

        //        main_form.Form1.lb_usb_devices.Items.Add(finalpath);
        //    }
        //}
    }
}
