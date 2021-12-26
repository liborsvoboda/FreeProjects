namespace Kalkulacka
{
    partial class Kalkulacka
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kalkulacka));
            this.txt_input = new System.Windows.Forms.TextBox();
            this.btn_c = new System.Windows.Forms.Button();
            this.btn_ce = new System.Windows.Forms.Button();
            this.lst_history = new System.Windows.Forms.ListBox();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_minus = new System.Windows.Forms.Button();
            this.btn_plus = new System.Windows.Forms.Button();
            this.btn_multiple = new System.Windows.Forms.Button();
            this.btn_divide = new System.Windows.Forms.Button();
            this.btn_formula = new System.Windows.Forms.Button();
            this.btn_dot = new System.Windows.Forms.Button();
            this.btn_zero = new System.Windows.Forms.Button();
            this.btn_seven = new System.Windows.Forms.Button();
            this.btn_eight = new System.Windows.Forms.Button();
            this.btn_nine = new System.Windows.Forms.Button();
            this.btn_four = new System.Windows.Forms.Button();
            this.btn_five = new System.Windows.Forms.Button();
            this.btn_six = new System.Windows.Forms.Button();
            this.btn_one = new System.Windows.Forms.Button();
            this.btn_two = new System.Windows.Forms.Button();
            this.btn_three = new System.Windows.Forms.Button();
            this.txt_calculating_part = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_input
            // 
            this.txt_input.Location = new System.Drawing.Point(8, 79);
            this.txt_input.Name = "txt_input";
            this.txt_input.ReadOnly = true;
            this.txt_input.Size = new System.Drawing.Size(144, 20);
            this.txt_input.TabIndex = 0;
            this.txt_input.WordWrap = false;
            // 
            // btn_c
            // 
            this.btn_c.BackColor = System.Drawing.Color.MistyRose;
            this.btn_c.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_c.Location = new System.Drawing.Point(7, 105);
            this.btn_c.Name = "btn_c";
            this.btn_c.Size = new System.Drawing.Size(29, 23);
            this.btn_c.TabIndex = 1;
            this.btn_c.Text = "C";
            this.btn_c.UseVisualStyleBackColor = false;
            this.btn_c.Click += new System.EventHandler(this.btnC_Click);
            // 
            // btn_ce
            // 
            this.btn_ce.BackColor = System.Drawing.Color.MistyRose;
            this.btn_ce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_ce.Location = new System.Drawing.Point(42, 105);
            this.btn_ce.Name = "btn_ce";
            this.btn_ce.Size = new System.Drawing.Size(29, 23);
            this.btn_ce.TabIndex = 2;
            this.btn_ce.Text = "CE";
            this.btn_ce.UseVisualStyleBackColor = false;
            this.btn_ce.Click += new System.EventHandler(this.btnCE_Click);
            // 
            // lst_history
            // 
            this.lst_history.FormattingEnabled = true;
            this.lst_history.Location = new System.Drawing.Point(8, 12);
            this.lst_history.Name = "lst_history";
            this.lst_history.Size = new System.Drawing.Size(127, 43);
            this.lst_history.TabIndex = 3;
            this.lst_history.TabStop = false;
            this.lst_history.SelectedIndexChanged += new System.EventHandler(this.lst_history_SelectedIndexChanged);
            // 
            // btn_print
            // 
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_print.Location = new System.Drawing.Point(138, 12);
            this.btn_print.Margin = new System.Windows.Forms.Padding(0);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(14, 43);
            this.btn_print.TabIndex = 4;
            this.btn_print.Text = "Tisk";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.PrintDocument);
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.MistyRose;
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_back.Location = new System.Drawing.Point(77, 105);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(29, 23);
            this.btn_back.TabIndex = 5;
            this.btn_back.Text = "<--";
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btnback_Click);
            // 
            // btn_minus
            // 
            this.btn_minus.BackColor = System.Drawing.Color.LightGreen;
            this.btn_minus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_minus.Location = new System.Drawing.Point(112, 105);
            this.btn_minus.Name = "btn_minus";
            this.btn_minus.Size = new System.Drawing.Size(39, 23);
            this.btn_minus.TabIndex = 6;
            this.btn_minus.Text = "-";
            this.btn_minus.UseVisualStyleBackColor = false;
            this.btn_minus.Click += new System.EventHandler(this.btnminus_Click);
            // 
            // btn_plus
            // 
            this.btn_plus.BackColor = System.Drawing.Color.LightGreen;
            this.btn_plus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_plus.Location = new System.Drawing.Point(112, 134);
            this.btn_plus.Name = "btn_plus";
            this.btn_plus.Size = new System.Drawing.Size(39, 23);
            this.btn_plus.TabIndex = 7;
            this.btn_plus.Text = "+";
            this.btn_plus.UseVisualStyleBackColor = false;
            this.btn_plus.Click += new System.EventHandler(this.btnplus_Click);
            // 
            // btn_multiple
            // 
            this.btn_multiple.BackColor = System.Drawing.Color.LightGreen;
            this.btn_multiple.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_multiple.Location = new System.Drawing.Point(112, 163);
            this.btn_multiple.Name = "btn_multiple";
            this.btn_multiple.Size = new System.Drawing.Size(39, 23);
            this.btn_multiple.TabIndex = 8;
            this.btn_multiple.Text = "*";
            this.btn_multiple.UseVisualStyleBackColor = false;
            this.btn_multiple.Click += new System.EventHandler(this.btnmultiply_Click);
            // 
            // btn_divide
            // 
            this.btn_divide.BackColor = System.Drawing.Color.LightGreen;
            this.btn_divide.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_divide.Location = new System.Drawing.Point(112, 192);
            this.btn_divide.Name = "btn_divide";
            this.btn_divide.Size = new System.Drawing.Size(39, 23);
            this.btn_divide.TabIndex = 9;
            this.btn_divide.Text = "/";
            this.btn_divide.UseVisualStyleBackColor = false;
            this.btn_divide.Click += new System.EventHandler(this.btndivide_Click);
            // 
            // btn_formula
            // 
            this.btn_formula.BackColor = System.Drawing.Color.LightGreen;
            this.btn_formula.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_formula.Location = new System.Drawing.Point(112, 221);
            this.btn_formula.Name = "btn_formula";
            this.btn_formula.Size = new System.Drawing.Size(39, 23);
            this.btn_formula.TabIndex = 10;
            this.btn_formula.Text = "=";
            this.btn_formula.UseVisualStyleBackColor = false;
            this.btn_formula.Click += new System.EventHandler(this.btnformula_Click);
            // 
            // btn_dot
            // 
            this.btn_dot.BackColor = System.Drawing.Color.Bisque;
            this.btn_dot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_dot.Location = new System.Drawing.Point(77, 221);
            this.btn_dot.Name = "btn_dot";
            this.btn_dot.Size = new System.Drawing.Size(29, 23);
            this.btn_dot.TabIndex = 11;
            this.btn_dot.Text = ".";
            this.btn_dot.UseVisualStyleBackColor = false;
            this.btn_dot.Click += new System.EventHandler(this.btnperiod_Click);
            // 
            // btn_zero
            // 
            this.btn_zero.BackColor = System.Drawing.Color.Bisque;
            this.btn_zero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_zero.Location = new System.Drawing.Point(7, 221);
            this.btn_zero.Name = "btn_zero";
            this.btn_zero.Size = new System.Drawing.Size(64, 23);
            this.btn_zero.TabIndex = 12;
            this.btn_zero.Text = "0";
            this.btn_zero.UseVisualStyleBackColor = false;
            this.btn_zero.Click += new System.EventHandler(this.btnzero_Click);
            // 
            // btn_seven
            // 
            this.btn_seven.BackColor = System.Drawing.Color.Bisque;
            this.btn_seven.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_seven.Location = new System.Drawing.Point(7, 134);
            this.btn_seven.Name = "btn_seven";
            this.btn_seven.Size = new System.Drawing.Size(29, 23);
            this.btn_seven.TabIndex = 13;
            this.btn_seven.Text = "7";
            this.btn_seven.UseVisualStyleBackColor = false;
            this.btn_seven.Click += new System.EventHandler(this.btnseven_Click);
            // 
            // btn_eight
            // 
            this.btn_eight.BackColor = System.Drawing.Color.Bisque;
            this.btn_eight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_eight.Location = new System.Drawing.Point(42, 134);
            this.btn_eight.Name = "btn_eight";
            this.btn_eight.Size = new System.Drawing.Size(29, 23);
            this.btn_eight.TabIndex = 14;
            this.btn_eight.Text = "8";
            this.btn_eight.UseVisualStyleBackColor = false;
            this.btn_eight.Click += new System.EventHandler(this.btneight_Click);
            // 
            // btn_nine
            // 
            this.btn_nine.BackColor = System.Drawing.Color.Bisque;
            this.btn_nine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_nine.Location = new System.Drawing.Point(77, 134);
            this.btn_nine.Name = "btn_nine";
            this.btn_nine.Size = new System.Drawing.Size(29, 23);
            this.btn_nine.TabIndex = 15;
            this.btn_nine.Text = "9";
            this.btn_nine.UseVisualStyleBackColor = false;
            this.btn_nine.Click += new System.EventHandler(this.btnnine_Click);
            // 
            // btn_four
            // 
            this.btn_four.BackColor = System.Drawing.Color.Bisque;
            this.btn_four.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_four.Location = new System.Drawing.Point(7, 163);
            this.btn_four.Name = "btn_four";
            this.btn_four.Size = new System.Drawing.Size(29, 23);
            this.btn_four.TabIndex = 16;
            this.btn_four.Text = "4";
            this.btn_four.UseVisualStyleBackColor = false;
            this.btn_four.Click += new System.EventHandler(this.btnfour_Click);
            // 
            // btn_five
            // 
            this.btn_five.BackColor = System.Drawing.Color.Bisque;
            this.btn_five.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_five.Location = new System.Drawing.Point(42, 163);
            this.btn_five.Name = "btn_five";
            this.btn_five.Size = new System.Drawing.Size(29, 23);
            this.btn_five.TabIndex = 17;
            this.btn_five.Text = "5";
            this.btn_five.UseVisualStyleBackColor = false;
            this.btn_five.Click += new System.EventHandler(this.btnfive_Click);
            // 
            // btn_six
            // 
            this.btn_six.BackColor = System.Drawing.Color.Bisque;
            this.btn_six.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_six.Location = new System.Drawing.Point(77, 163);
            this.btn_six.Name = "btn_six";
            this.btn_six.Size = new System.Drawing.Size(29, 23);
            this.btn_six.TabIndex = 18;
            this.btn_six.Text = "6";
            this.btn_six.UseVisualStyleBackColor = false;
            this.btn_six.Click += new System.EventHandler(this.btnsix_Click);
            // 
            // btn_one
            // 
            this.btn_one.BackColor = System.Drawing.Color.Bisque;
            this.btn_one.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_one.Location = new System.Drawing.Point(7, 192);
            this.btn_one.Name = "btn_one";
            this.btn_one.Size = new System.Drawing.Size(29, 23);
            this.btn_one.TabIndex = 19;
            this.btn_one.Text = "1";
            this.btn_one.UseVisualStyleBackColor = false;
            this.btn_one.Click += new System.EventHandler(this.btnone_Click);
            // 
            // btn_two
            // 
            this.btn_two.BackColor = System.Drawing.Color.Bisque;
            this.btn_two.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_two.Location = new System.Drawing.Point(42, 192);
            this.btn_two.Name = "btn_two";
            this.btn_two.Size = new System.Drawing.Size(29, 23);
            this.btn_two.TabIndex = 20;
            this.btn_two.Text = "2";
            this.btn_two.UseVisualStyleBackColor = false;
            this.btn_two.Click += new System.EventHandler(this.btntwo_Click);
            // 
            // btn_three
            // 
            this.btn_three.BackColor = System.Drawing.Color.Bisque;
            this.btn_three.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_three.Location = new System.Drawing.Point(77, 192);
            this.btn_three.Name = "btn_three";
            this.btn_three.Size = new System.Drawing.Size(29, 23);
            this.btn_three.TabIndex = 21;
            this.btn_three.Text = "3";
            this.btn_three.UseVisualStyleBackColor = false;
            this.btn_three.Click += new System.EventHandler(this.btnthree_Click);
            // 
            // txt_calculating_part
            // 
            this.txt_calculating_part.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_calculating_part.Enabled = false;
            this.txt_calculating_part.Location = new System.Drawing.Point(8, 60);
            this.txt_calculating_part.Name = "txt_calculating_part";
            this.txt_calculating_part.ReadOnly = true;
            this.txt_calculating_part.Size = new System.Drawing.Size(53, 13);
            this.txt_calculating_part.TabIndex = 22;
            this.txt_calculating_part.TabStop = false;
            this.txt_calculating_part.WordWrap = false;
            // 
            // Kalkulacka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(158, 246);
            this.Controls.Add(this.txt_calculating_part);
            this.Controls.Add(this.btn_three);
            this.Controls.Add(this.btn_two);
            this.Controls.Add(this.btn_one);
            this.Controls.Add(this.btn_six);
            this.Controls.Add(this.btn_five);
            this.Controls.Add(this.btn_four);
            this.Controls.Add(this.btn_nine);
            this.Controls.Add(this.btn_eight);
            this.Controls.Add(this.btn_seven);
            this.Controls.Add(this.btn_zero);
            this.Controls.Add(this.btn_dot);
            this.Controls.Add(this.btn_formula);
            this.Controls.Add(this.btn_divide);
            this.Controls.Add(this.btn_multiple);
            this.Controls.Add(this.btn_plus);
            this.Controls.Add(this.btn_minus);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.lst_history);
            this.Controls.Add(this.btn_ce);
            this.Controls.Add(this.btn_c);
            this.Controls.Add(this.txt_input);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Kalkulacka";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kalkulačka";
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Main_form_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_c;
        private System.Windows.Forms.Button btn_ce;
        private System.Windows.Forms.ListBox lst_history;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_minus;
        private System.Windows.Forms.Button btn_plus;
        private System.Windows.Forms.Button btn_multiple;
        private System.Windows.Forms.Button btn_divide;
        private System.Windows.Forms.Button btn_formula;
        private System.Windows.Forms.Button btn_dot;
        private System.Windows.Forms.Button btn_zero;
        private System.Windows.Forms.Button btn_seven;
        private System.Windows.Forms.Button btn_eight;
        private System.Windows.Forms.Button btn_nine;
        private System.Windows.Forms.Button btn_four;
        private System.Windows.Forms.Button btn_five;
        private System.Windows.Forms.Button btn_six;
        private System.Windows.Forms.Button btn_one;
        private System.Windows.Forms.Button btn_two;
        private System.Windows.Forms.Button btn_three;
        private System.Windows.Forms.TextBox txt_input;
        private System.Windows.Forms.TextBox txt_calculating_part;
    }
}

