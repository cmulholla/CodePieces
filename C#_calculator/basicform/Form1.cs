using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basicform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //2
            textBox1.Text += "2";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //0
            textBox1.Text += "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //1
            textBox1.Text += "1";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.EndsWith("-"))
            {
                if (textBox1.Text.Replace(" ", "").Length > 1)
                    parseAdd(textBox1.Text, "-");
            }
            else if (textBox1.Text.EndsWith("+"))
                parseAdd(textBox1.Text, "+");
            else if (textBox1.Text.EndsWith("*"))
                parseAdd(textBox1.Text, "*");
            else if (textBox1.Text.EndsWith("/"))
                parseAdd(textBox1.Text, "/");
            else if (textBox1.Text.EndsWith("%"))
                parseAdd(textBox1.Text, "%");
            else //comment out to get debug data
                label1.Text = "";
        }

        /*public bool checkNumbs(string str)
        {
            Console.WriteLine(str);
            if (str.StartsWith("-")) //remove the first - if it exists to make a number negative
                str = "" + str.Remove(0, 1);

            if (str.Contains("-"))
                return true;
            else if (str.Contains("+"))
                return true;
            else if (str.Contains("*"))
                return true;
            else if (str.Contains("%"))
                return true;
            else if (str.Contains("/"))
                return true;
            else
                return false;
        }*/

        public void parseAdd(string str, string changer)
        {
            clearIfNeed();
            if (str.Length == 0)
                return;
            bool negate = false;
            if (str[0] == '-')
                negate = true;
            str = str.Replace(changer, ""); //removes the +, -, *, or / from the number
            try
            {
                float tempf = float.Parse(str);
                if (negate)
                {
                    textBox2.Text += "(-" + scitodec(-tempf) + ") " + changer + " ";
                }
                else
                    textBox2.Text += "(" + scitodec(tempf) + ") " + changer + " ";
                //label1.Text = "Successfully added.";
            }
            catch (FormatException e)
            {
                label1.Text = e.Message + " " + str;
            }
            
            textBox1.Text = "";
        }

        public string scitodec(float numb)
        {
            bool negate=false;
            if (numb == 0)
                return "0";
            else if (numb <= 0)
            {
                negate = true;
                numb = -numb;
            }
            //label1.Text = "\"" + numb + "\"";
            //Console.WriteLine(numb);
            if (negate) //the function doesn't like dealing with negative numbers so I have to do it manually
                return "-" + Decimal.Parse(numb + "", System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint);
            return "" + Decimal.Parse(numb + "", System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint);
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                //might not use this lol
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //enter button
            parseAdd(textBox1.Text, " ");
            if (textBox2.Text.Length == 0)
                textBox2.Text += "  ";
            if (textBox2.Text[textBox2.Text.Length-1] != ' ')
            {
                textBox2.Text += "  ";
            }
            textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Length - 2); //get rid of extra spaces from the parseAdd
            textBox1.Text = "= " + scitodec(compile(textBox2.Text));
            textBox2.Text += textBox1.Text;
            textBox1.SelectAll();
            textBox1.Focus();
        }

        public float compile(string str)
        {
            str = str.Replace(" ", "");
            if (str == "") //return 0 if there's nothing
                return 0;

            int index, numInd1, numInd2;
            float snum1, snum2, fnum;
            //do all multiplication, division, and modulus first
            while (str.Contains('%') || str.Contains("*") || str.Contains("/"))
            {
                //find which * or / comes first
                index = str.IndexOf("*");
                if (str.IndexOf("/") < index)
                    index = str.IndexOf("/");
                if (str.IndexOf("*") == -1)
                    index = str.IndexOf("/");
                if (str.IndexOf("/") == -1)
                    index = str.IndexOf("*");
                if (str.IndexOf("%") >= 0 && (index == -1 || str.IndexOf("%") < index)) //if it's found and it's the only operator or the first operator
                    index = str.IndexOf("%");

                //multiply or divide the two numbers
                //parse the two numbers
                numInd1 = str.LastIndexOf('(', index);
                numInd2 = str.IndexOf(')', index);
                snum1 = float.Parse(str.Substring(numInd1 + 1, index - 2 - numInd1));
                snum2 = float.Parse(str.Substring(index + 2, numInd2 - index - 2));
                //multiply or divide
                if (str[index] == '*')
                    fnum = snum1 * snum2;
                else if (str[index] == '/')
                    fnum = snum1 / snum2;
                else
                    fnum = snum1 % snum2;
                //replace the new number with the old two numbers
                str = str.Remove(numInd1, numInd2 - numInd1 + 1);
                str = str.Insert(numInd1, "(" + scitodec(fnum) + ")");
            }

            //now do addition and subtracted
            while (str.Contains("+") || str.IndexOf("-", 2)>=0)
            {
                //find which * or / comes first
                index = str.IndexOf("+");
                if (str.IndexOf("-", 2) < index)
                    index = str.IndexOf("-", 2);
                if (str.IndexOf("+") == -1)
                    index = str.IndexOf("-", 2);
                if (str.IndexOf("-", 2) == -1)
                    index = str.IndexOf("+");

                //multiply or divide the two numbers
                //parse the two numbers
                numInd1 = str.LastIndexOf('(', index);
                numInd2 = str.IndexOf(')', index);
                snum1 = float.Parse(str.Substring(numInd1 + 1, index - 2));
                snum2 = float.Parse(str.Substring(index + 2, numInd2 - index - 2));
                //multiply or divide
                if (str[index] == '+')
                    fnum = snum1 + snum2;
                else
                    fnum = snum1 - snum2;
                //replace the new number with the old two numbers
                str = str.Remove(numInd1, numInd2 - numInd1 + 1);
                str = str.Insert(numInd1, "(" + scitodec(fnum) + ")");
            }
            //return when there's no more calculations to do
            str = str.Replace(" ", "");
            str = "" + str.Remove(0, 1);
            str = "" + str.Remove(str.Length-1, 1);
            return float.Parse(str);
        }

        public void clearIfNeed()
        {
            if (textBox1.Text.Contains("=") || textBox2.Text.Contains("="))
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void three_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //3
            textBox1.Text += "3";
        }

        private void four_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //4
            textBox1.Text += "4";
        }

        private void five_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //5
            textBox1.Text += "5";
        }

        private void six_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //6
            textBox1.Text += "6";
        }

        private void seven_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //7
            textBox1.Text += "7";
        }

        private void eight_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //8
            textBox1.Text += "8";
        }

        private void nine_Click(object sender, EventArgs e)
        {
            clearIfNeed();
            //9
            textBox1.Text += "9";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            clearIfNeed();
            //.
            textBox1.Text += ".";
        }

        private void mod_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IndexOf("=") == 0)
                textBox1.Text = textBox1.Text.Replace("=", "");
            //modulus
            textBox1.Text += "%";
        }

        private void plus_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IndexOf("=") == 0)
                textBox1.Text = textBox1.Text.Replace("=", "");
            //+
            textBox1.Text += "+";
        }

        private void minus_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IndexOf("=") == 0)
                textBox1.Text = textBox1.Text.Replace("=", "");
            //-
            textBox1.Text += "-";
        }

        private void times_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IndexOf("=") == 0)
                textBox1.Text = textBox1.Text.Replace("=", "");
            //*
            textBox1.Text += "*";
        }

        private void divide_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IndexOf("=") == 0)
                textBox1.Text = textBox1.Text.Replace("=", "");
            // /
            textBox1.Text += "/";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }
    }
}
