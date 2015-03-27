using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI___Lab_2
{
    public partial class Form1 : Form
    {
        private string m_oper1;
        private string m_oper2;
        private string m_oper3;
        private string m_negate;
        private string m_operator;
        private double m_memNum;
        private Button m_btn;
        public Form1()
        {
            InitializeComponent();

            m_oper1 = "";
            m_oper2 = "";
            m_oper3 = "";
            m_negate = "";
            m_operator = "";
            m_memNum = 0;
        }

        /*******************
        * 
        * Keyboard behavior
        *
        ********************/
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '+' ||
                 e.KeyChar == '-' ||
                 e.KeyChar == '*' ||
                 e.KeyChar == '/')
            {
                e.Handled = true;
                operatr(sender, e);
            }
            else if (e.KeyChar == '=' || e.KeyChar == '\r')
            {
                e.Handled = true;
                if(m_operator == "")
                {
                    return;
                }
                else
                {
                    equal_Click(sender, e);
                }
            }
            else if (e.KeyChar == '\b')
            {
                clear_Click(sender, e);
            }
            else if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                operand(sender, e);
            }
            else if (e.KeyChar == '.')
            {
                e.Handled = true;
                operand(sender, e);
            }
            else if (e.KeyChar == '%')
            {
                e.Handled = true;
                percentage_Click(sender, e);
            }
            else
            {
                e.Handled = true;
            }
        }

        /******************
        * 
        * Click behavior
        *
        *******************/
        private void clear_Click(object sender, EventArgs e)
        {
            // recognize input type
            Type input = e.GetType();
            string kB = input.ToString();

            if (kB == "System.EventArgs")
            {
                equal_Click(sender, e);
                return;
            }

            if( textBox1.Text == "0")
            {
                label2.Text = "";
                m_oper1 = "";
                m_oper2 = "";
                m_oper3 = "";
                m_operator = "";
                return;
            }
            else if(textBox1.Text == "Error")
            {
                textBox1.Text = "0";
                label2.Text = "";
                m_oper1 = "";
                m_oper2 = "";
                m_oper3 = "";
                m_operator = "";
                clear.Text = "AC";
                return;
            }

            m_negate = "";
            textBox1.Text = "0";
            m_oper1 = "0";
            clear.Text = "AC";
        }

        private void operand(object sender, EventArgs e)
        {
            // recognize input type
            Type input = e.GetType();
            string kB = input.ToString();

            if (kB == "System.EventArgs")
            {
                equal_Click(sender, e);
                return;
            }

            // ability to clear operand is recognized
            clear.Text = "C";

            if( kB == "System.Windows.Forms.KeyPressEventArgs" )
            {
                KeyPressEventArgs pressed = (KeyPressEventArgs)e;

                // if no left-hand operand exists, replace 0 with user entry
                if (textBox1.Text == "0") { textBox1.Text = ""; }

                // if no operator exists, set left-hand operand
                if (m_operator == "")
                {
                    textBox1.Text += pressed.KeyChar.ToString();
                    m_oper1 = textBox1.Text;

                    if (m_negate != "")
                    {
                        negateOper(sender, e);
                    }

                    m_oper1 = textBox1.Text;
                }
                // opertor exists, handle right-hand operand
                else
                {
                    if (m_oper2 == "")
                    {
                        textBox1.Text = pressed.KeyChar.ToString();
                        m_oper2 = textBox1.Text;

                        if (m_negate != "")
                        {
                            negateOper(sender, e);
                        }
                    }
                    else
                    {
                        if (pressed.KeyChar.ToString() == "±")
                        {
                            m_oper2 = textBox1.Text;
                            return;
                        }

                        textBox1.Text += m_btn.Text;
                    }

                    m_oper2 = textBox1.Text;
                }
            }
            else
            {
                // button data
                m_btn = sender as Button;

                // if no left-hand operand exists, replace 0 with user entry
                if (textBox1.Text == "0") { textBox1.Text = ""; }

                // if no operator exists, set left-hand operand
                if (m_operator == "")
                {
                    textBox1.Text += m_btn.Text;
                    m_oper1 = textBox1.Text;

                    if (m_negate != "")
                    {
                        negateOper(sender, e);
                    }

                    m_oper1 = textBox1.Text;
                }
                // opertor exists, handle right-hand operand
                else
                {
                    if (m_oper2 == "")
                    {
                        textBox1.Text = m_btn.Text;
                        m_oper2 = textBox1.Text;

                        if (m_negate != "")
                        {
                            negateOper(sender, e);
                        }
                    }
                    else
                    {
                        if (m_btn.Text == "±")
                        {
                            m_oper2 = textBox1.Text;
                            return;
                        }

                        textBox1.Text += m_btn.Text;
                    }

                    m_oper2 = textBox1.Text;
                }
            }

        }

        private void operatr(object sender, EventArgs e)
        {
            // recognize input type
            Type input = e.GetType();
            string kB = input.ToString();

            if (kB == "System.EventArgs")
            {
                equal_Click(sender, e);
                return;
            }

            if (kB == "System.Windows.Forms.KeyPressEventArgs")
            {
                KeyPressEventArgs pressed = (KeyPressEventArgs)e;

                if (m_operator != "")
                {
                    m_oper1 = textBox1.Text;
                    m_oper2 = "";
                    m_operator = pressed.KeyChar.ToString();
                }

                m_operator = pressed.KeyChar.ToString();
                label2.Text = m_operator;
            }
            else
            {
                m_btn = sender as Button;

                if (m_operator != "")
                {
                    m_oper1 = textBox1.Text;
                    m_oper2 = "";
                    m_operator = m_btn.Text;
                }

                m_operator = m_btn.Text;
                label2.Text = m_operator;
            }
        }

        private void equal_Click(object sender, EventArgs e)
        {
            // local operands
            double  oper1 = 0, 
                    oper2 = 0,
                    result = 0;

            // resets display if equal clicked while in error screen
            if(textBox1.Text == "Error")
            {
                clear_Click(sender, e);
                return;
            }

            // returns if there is no left-hand operand
            if (m_oper1 == "") { return; }

            // normalize calculation operands
            if (m_oper2 == "")
            {
                oper1 = Convert.ToDouble(m_oper1);
                oper2 = Convert.ToDouble(textBox1.Text);
            }
            else if(m_oper2 == m_oper3)
            {
                oper1 = Convert.ToDouble(textBox1.Text);
                oper2 = Convert.ToDouble(m_oper2);
            }
            else 
            {
                oper1 = Convert.ToDouble(m_oper1);
                oper2 = Convert.ToDouble(m_oper2);
            }
            
            // calculate
            if (m_operator == "+") {result = oper1 + oper2;}
            if (m_operator == "-") {result = oper1 - oper2;}
            if (m_operator == "×" || m_operator == "*") { result = oper1 * oper2; }
            if (m_operator == "÷" || m_operator == "/") 
            {
                if (oper2 == 0)
                { 
                    textBox1.Text = "Error";
                    return;
                }
                else 
                {
                    result = oper1 / oper2; 
                }
            }
           
            // prints result
            textBox1.Text = result.ToString();

            // stores right-hand operand for future, consecutive calculations
            m_oper3 = m_oper2.ToString();
        }

        /******************
        * 
        * Memory features
        *
        *******************/
        private void memClear_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label3.Text = "";
        }

        private void addToMem_Click(object sender, EventArgs e)
        {
            if(label3.Text == "")
            {
                label3.Text = "M";
            }

            double screenNum = Convert.ToDouble(textBox1.Text);
            m_memNum += screenNum;
        }

        private void rmFromMem_Click(object sender, EventArgs e)
        {
            double screenNum = Convert.ToDouble(textBox1.Text);
            m_memNum -= screenNum;
        }

        private void memRecall_Click(object sender, EventArgs e)
        {
            textBox1.Text = m_memNum.ToString();
        }

        /******************
        * 
        * Extra feature
        *
        *******************/
        private void percentage_Click(object sender, EventArgs e)
        {
            double percentage = Convert.ToDouble(textBox1.Text);
            percentage = percentage / 100; ;
            textBox1.Text = percentage.ToString();
        }

        private void negateOper(object sender, EventArgs e)
        {
            // recognize input type
            Type input = e.GetType();
            string kB = input.ToString();

            if (kB == "System.EventArgs")
            {
                equal_Click(sender, e);
                return;
            }

            m_btn = sender as Button;

            if (m_negate == "" && m_btn.Text == "±")
            {
                m_negate = m_btn.Text;
            }
            else if (m_negate != "" && m_btn.Text == "±")
            {
                m_negate = "";
            }

            if (m_operator != "" && m_oper2 == "")
            {
                return;
            }
            else if (m_operator != "" && m_oper2 != "")
            {
                double negate = Convert.ToDouble(m_oper2);
                negate = negate * -1;
                textBox1.Text = negate.ToString();
                m_oper2 = textBox1.Text;
                m_negate = "";
            }
            else if (m_oper1 != "")
            {
                double negate = Convert.ToDouble(textBox1.Text);
                negate = negate * -1;
                textBox1.Text = negate.ToString();
                m_oper1 = textBox1.Text;
                m_negate = "";
            }
        }
    }
}
