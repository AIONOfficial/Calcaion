using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public class Calculator
    
        


    {
        
        private double number1 = 0;

       
        private string currentText = "";

       
        private string operation = "";

        
        private bool newNumber = true;


       
        public void EnterDigit(int digit)
        {
            if (newNumber)
            {
                currentText = digit.ToString();
                newNumber = false;
            }
            else
            {
                currentText += digit.ToString();
            }
        }


        
        public void EnterDot()
        {
            if (newNumber)
            {
                currentText = "0.";
                newNumber = false;
                return;
            }

            if (!currentText.Contains("."))
            {
                currentText += ".";
            }
        }


       
        public void ChangeSign()
        {
            if (string.IsNullOrEmpty(currentText))
                return;

            if (currentText.StartsWith("-"))
                currentText = currentText.Substring(1);
            else
                currentText = "-" + currentText;
        }


        
        public void SetOperation(string op)
        {
            number1 = GetCurrentNumber();
            operation = op;
            newNumber = true;
        }


        // محاسبه نتیجه
        public void Calculate()
        {
            double number2 = GetCurrentNumber();
            double result = 0;

            if (operation == "+")
                result = number1 + number2;

            else if (operation == "-")
                result = number1 - number2;

            else if (operation == "*")
                result = number1 * number2;

            else if (operation == "/")
            {
                if (number2 == 0)
                {
                    currentText = "Cannot divide by zero";
                    newNumber = true;
                    return;
                }

                result = number1 / number2;
            }

            else if (operation == "^")
                result = Math.Pow(number1, number2);

            currentText = result.ToString();
            number1 = result;      
            newNumber = true;
        }


      
        public void SquareRoot()
        {
            double num = GetCurrentNumber();

            if (num < 0)
            {
                currentText = "Error";
                newNumber = true;
                return;
            }


            double result = Math.Sqrt(num);

            currentText = result.ToString();
            newNumber = true;
        }
        public void Percent()  
        {
            double num = GetCurrentNumber();
            double result = num / 100;

            currentText = result.ToString();
            newNumber = true;
        }
        public void Reciprocal() 
        {
            double num = GetCurrentNumber();
            if (num == 0)
            {
                
                currentText = "Cannot divide by zero";
                newNumber = true;
            }
            else
            {
                double result = 1.0 / num;
                currentText = result.ToString();
                newNumber = true;
            }
        }



       
        public void Clear()
        {
            number1 = 0;
            currentText = "";
            operation = "";
            newNumber = true;
        }
        public void ClearEntry()
        {
            currentText = "0"; 
            newNumber = true;  
        }
        public void Backspace()
        {
            if (currentText.Length > 1)
                currentText = currentText.Remove(currentText.Length - 1);
            else
                currentText = "0";

            newNumber = false;
        }

       
        private double GetCurrentNumber()
        {
            if (string.IsNullOrEmpty(currentText))
                return 0;

            return double.Parse(currentText);
        }


        
        public string GetDisplay()
        {
            if (string.IsNullOrEmpty(currentText))
                return "0";

            return currentText;
        }
    }

}


