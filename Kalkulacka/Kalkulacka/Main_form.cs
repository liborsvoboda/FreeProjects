using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
namespace Kalkulacka
{
    public partial class Kalkulacka : Form
    {

        const string allowed_characters = "0123456789+-/*=.,\r\b";
        const string input_operands = "+-/*";
        const string input_numbers = "0123456789.,";
        //float calculation_result,number_1,number_2;
        public string temp_string;


        string formula_result = "";
        string number1 = "";
        string operand = "";
        string number2 = "";
        bool calc_status = false;


        public Kalkulacka()
        {
            InitializeComponent();
        
        }
 

        private void btnC_Click(object sender, EventArgs e)
        {
            lst_history.Items.Clear();
            calc_status = false;
            number1 = "";
            number2 = "";
            txt_input.Clear();
            txt_calculating_part.Clear();
            txt_input.Focus();  
        }
 

        private void btnCE_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(number2) == false )
            {
                txt_input.Text = "";
             //   operand = "";
                number2 = "";
           //     calc_status = false;
                txt_calculating_part.Text = number1;
            }
            else if (string.IsNullOrEmpty(operand) == false || string.IsNullOrEmpty(number1) == false)
            {
                number1 = "";
                calc_status = false;
                txt_calculating_part.Text = "";
                txt_input.Text = "";
            }
            txt_input.Focus();  
        }
 


        private void btnback_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(Keys.Back)));
            
        }
 

        private void btnminus_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_minus.Text)));
            
        }

        private void btnformula_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_formula.Text)));
        }

        private void btnplus_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_plus.Text)));
        }


        private void btnmultiply_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_multiple.Text)));
        }


        private void btndivide_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_divide.Text)));
        }


        private void btnperiod_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_dot.Text)));
        }




        private void btnone_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_one.Text)));
        }

        private void btntwo_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_two.Text)));
        }
 
        private void btnthree_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_three.Text)));
        }
 
 
        private void btnfour_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_four.Text)));
        }
 
        private void btnfive_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_five.Text)));
        }
 
        private void btnsix_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_six.Text)));
        }
 
        private void btnseven_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_seven.Text)));
        }
 
        private void btneight_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_eight.Text)));
        }
 
        private void btnnine_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_nine.Text)));
        }
 

        private void btnzero_Click(object sender, EventArgs e)
        {
            Main_form_KeyPress(sender, new KeyPressEventArgs(Convert.ToChar(btn_zero.Text)));
        }




        private void Main_form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (allowed_characters.Contains(e.KeyChar.ToString()) == true)
            { //correct character
                if (e.KeyChar.ToString() == "-" && string.IsNullOrEmpty(number1) == true)
                {//correct calculating character
                    number1 = "-";
                }
                else if (input_operands.Contains(e.KeyChar.ToString()) == true && string.IsNullOrEmpty(number2) == true)
                {//correct calculating character
                   operand = e.KeyChar.ToString();
                   calc_status = true;
                }
                else if ((e.KeyChar.ToString() == "\r" || e.KeyChar.ToString() == "=" || input_operands.Contains(e.KeyChar.ToString()) == true) && string.IsNullOrEmpty(number2) == false)
                {//correct result character or operand + number2 exist

                    formula_result = Convert.ToString(Functions.Functions.fn_formula(number1, number2, operand));
                    lst_history.Items.Add(number1 + " " + operand + " " + number2 + " = " + formula_result);
                    lst_history.SelectedIndex = lst_history.Items.Count - 1;
                    number1 = formula_result;
                    operand = "";
                    number2 = "";
                    calc_status = false;
                    if (input_operands.Contains(e.KeyChar.ToString()) == true && e.KeyChar.ToString() != "\b" && e.KeyChar.ToString() != "\r" && e.KeyChar.ToString() != "=")
                    {// insert next operand 
                        operand = e.KeyChar.ToString();
                        calc_status = true;
                    }
                }
                else if (e.KeyChar.ToString() == "\b")
                {//correct back character
                    if (string.IsNullOrEmpty(number2) == false )
                    {//clean number2
                        number2 = "";

                    }
                    else if (string.IsNullOrEmpty(number2) == true && string.IsNullOrEmpty(operand) == false)
                    {//clean operand
                        operand ="";
                    }
                    else
                    {//clean number1
                        number1 = "";
                    }

                }

                else if (input_numbers.Contains(e.KeyChar.ToString()))
                {// correct number
                    if (calc_status == false)
                    {// first number
                        if ((string.IsNullOrEmpty(number1) == true || number1 =="-" ) && (e.KeyChar.ToString() == "." || e.KeyChar.ToString() == ","))
                        {
                            number1 = number1 + "0,";
                        }
                        else
                        {
                            number1 += e.KeyChar.ToString().Replace(".", ",");
                        }
                    }
                    else
                    {//second number
                        if ((string.IsNullOrEmpty(number2) == true || number2 =="-") && (e.KeyChar.ToString() == "." || e.KeyChar.ToString() == ","))
                        {
                            number2 = number2+ "0,";
                        }
                        else
                        {

                            number2 += e.KeyChar.ToString().Replace(".",",");
                        }
                    } 
                }

                txt_calculating_part.Text = number1 + operand;
                if (string.IsNullOrEmpty(operand) == false)
                {
                    txt_input.Text = number2;
                }
                else
                {
                    txt_input.Text = number1;
                }
            
            }
            else
            {// another character
                MessageBox.Show("Nepovolený znak: " + e.KeyChar.ToString());
            }

            txt_input.Focus();  
        }



        private void lst_history_SelectedIndexChanged(object sender, EventArgs e)
        {
            calc_status = false;
            number2 = "";
            temp_string = lst_history.SelectedItem.ToString();
            number1 = txt_calculating_part.Text = txt_input.Text = Functions.Functions.fn_load_history_line(temp_string);
            txt_input.Focus();
        }




        public void PrintDocument(object sender, EventArgs e)
        {
            string text_for_print = "";

            foreach (string s in lst_history.Items)
            {
                text_for_print += s + System.Environment.NewLine;
            }

            PrintDocument p = new PrintDocument();
            p.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(text_for_print, new Font("Times New Roman", 12), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

            };
            try
            {
                p.Print();
            }
            catch (Exception ex)
            {
                throw new Exception("Chyba tisku na Defaultní tiskárnu", ex);
            }
            txt_input.Focus();  
        }



     }
}