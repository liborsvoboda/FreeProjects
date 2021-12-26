namespace main_form
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lb_usb_devices = new System.Windows.Forms.ListBox();
            this.chb_ubprint_port = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(-3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seznam Zazařízení USB: vytvořte jako local port";
            // 
            // lb_usb_devices
            // 
            this.lb_usb_devices.AccessibleName = "";
            this.lb_usb_devices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_usb_devices.FormattingEnabled = true;
            this.lb_usb_devices.Location = new System.Drawing.Point(0, 59);
            this.lb_usb_devices.Name = "lb_usb_devices";
            this.lb_usb_devices.ScrollAlwaysVisible = true;
            this.lb_usb_devices.Size = new System.Drawing.Size(311, 225);
            this.lb_usb_devices.TabIndex = 1;
            this.lb_usb_devices.SelectedIndexChanged += new System.EventHandler(this.lb_usb_devices_SelectedIndexChanged);
            this.lb_usb_devices.DoubleClick += new System.EventHandler(this.lb_usb_devices_doubleclick);
            // 
            // chb_ubprint_port
            // 
            this.chb_ubprint_port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_ubprint_port.AutoSize = true;
            this.chb_ubprint_port.Location = new System.Drawing.Point(209, 36);
            this.chb_ubprint_port.Name = "chb_ubprint_port";
            this.chb_ubprint_port.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chb_ubprint_port.Size = new System.Drawing.Size(102, 17);
            this.chb_ubprint_port.TabIndex = 2;
            this.chb_ubprint_port.Text = "Pouze Print Port";
            this.chb_ubprint_port.UseVisualStyleBackColor = true;
            this.chb_ubprint_port.CheckedChanged += new System.EventHandler(this.chb_ubprint_port_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 300);
            this.Controls.Add(this.chb_ubprint_port);
            this.Controls.Add(this.lb_usb_devices);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox lb_usb_devices;
        private System.Windows.Forms.CheckBox chb_ubprint_port;
    }
}

