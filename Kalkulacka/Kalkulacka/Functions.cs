using System;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Windows.Forms;


namespace Functions
{

    static class Functions
    {
        static float calculation_result,number1,number2;



        public static float fn_formula(string no1,string no2,string ope) 
        {

            number1 = Convert.ToSingle(no1);
            number2 = Convert.ToSingle(no2);

            switch (ope)
            {
                case "+":
                    calculation_result = number1 + number2;
                    break;
                case "-":
                    calculation_result = number1 - number2;
                    break;
                case "/":
                    calculation_result = number1 / number2;
                    break;
                case "*":
                    calculation_result = number1 * number2;
                    break;
                default:
                    throw new ArgumentException("Neznámý operátor ", ope);

            }
            return calculation_result;
        }



        public static string fn_load_history_line(string lst_record)
        {
            return lst_record.Substring(lst_record.LastIndexOf(" ")+1, lst_record.Length - lst_record.LastIndexOf(" ") -1);
        }      


    }
}
