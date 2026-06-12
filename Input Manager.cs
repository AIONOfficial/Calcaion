using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class Input_Manager
    {


        private const int MaxDigits = 26;

       
        public static void AddNumber(Guna2TextBox txtDisplay, string number)
        {
        
            int digitCount = txtDisplay.Text.Replace(".", "").Replace("-", "").Length;

            if (digitCount < MaxDigits)
            {
              
                if (txtDisplay.Text == "0" && number != ".")
                {
                    txtDisplay.Text = number;
                }
                else
                {
                    txtDisplay.Text += number;
                }
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        
        public static void AddNumberFromSender(Guna2TextBox txtDisplay, object sender)
        {
            if (sender is Guna2Button clickedButton)
            {
                string number = clickedButton.Text;
                int digitCount = txtDisplay.Text.Replace(".", "").Replace("-", "").Length;

                if (digitCount < MaxDigits)
                {
                 
                    if (txtDisplay.Text == "0" && number != ".")
                    {
                        txtDisplay.Text = number;
                    }
                    else
                    {
                        txtDisplay.Text += number;
                    }
                }
                else
                {
                    System.Media.SystemSounds.Beep.Play();
                }
            }
        }
    }
}
