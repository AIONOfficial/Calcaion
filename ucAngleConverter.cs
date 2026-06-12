using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class ucAngleConverter : UserControl
    {
        private Guna2TextBox activeTextBox;

       
        private bool isCalculating = false;

        public ucAngleConverter()
        {
            InitializeComponent();
            activeTextBox = txtDegree;

          
            txtDegree.Enter += (s, e) => activeTextBox = txtDegree;
            txtRadian.Enter += (s, e) => activeTextBox = txtRadian;
            txtGradian.Enter += (s, e) => activeTextBox = txtGradian;

           
            txtDegree.TextChanged += AngleTextBox_TextChanged;
            txtRadian.TextChanged += AngleTextBox_TextChanged;
            txtGradian.TextChanged += AngleTextBox_TextChanged;

           
            txtDegree.KeyPress += txtAngle_KeyPress;
            txtRadian.KeyPress += txtAngle_KeyPress;
            txtGradian.KeyPress += txtAngle_KeyPress;

          
            isCalculating = true;
            txtDegree.Text = "0";
            txtRadian.Text = "0";
            txtGradian.Text = "0";
            isCalculating = false;
        }

      
        private void AngleTextBox_TextChanged(object sender, EventArgs e)
        {
            
            if (isCalculating) return;

            Guna2TextBox currentInput = sender as Guna2TextBox;
            if (currentInput == null) return;

           
            string rawText = currentInput.Text;
            if (string.IsNullOrEmpty(rawText) || rawText == "." || rawText == "-")
            {
                isCalculating = true;
                if (currentInput == txtDegree) { txtRadian.Text = "0"; txtGradian.Text = "0"; }
                else if (currentInput == txtRadian) { txtDegree.Text = "0"; txtGradian.Text = "0"; }
                else if (currentInput == txtGradian) { txtDegree.Text = "0"; txtRadian.Text = "0"; }
                isCalculating = false;
                return;
            }

           
            if (!double.TryParse(rawText, out double inputValue)) return;

            isCalculating = true;

        
            if (currentInput == txtDegree)
            {
                txtRadian.Text = (inputValue * (Math.PI / 180.0)).ToString("0.########");
                txtGradian.Text = (inputValue * (200.0 / 180.0)).ToString("0.####");
            }
            else if (currentInput == txtRadian)
            {
                txtDegree.Text = (inputValue * (180.0 / Math.PI)).ToString("0.####");
                txtGradian.Text = (inputValue * (200.0 / Math.PI)).ToString("0.####");
            }
            else if (currentInput == txtGradian)
            {
                txtDegree.Text = (inputValue * (180.0 / 200.0)).ToString("0.####");
                txtRadian.Text = (inputValue * (Math.PI / 200.0)).ToString("0.########");
            }

            isCalculating = false;
        }

        
        private void btnNumbers_Click(object sender, EventArgs e)
        {
            if (activeTextBox == null) return;

            Guna2Button clickedButton = sender as Guna2Button;
            if (clickedButton == null) return;

            string buttonText = clickedButton.Text;

         
            int digitCount = activeTextBox.Text.Replace(".", "").Replace("-", "").Length;
            if (digitCount >= 26)
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            if (activeTextBox.Text == "0" && buttonText != ".")
            {
                activeTextBox.Text = buttonText;
                return;
            }

            if (buttonText == "." && activeTextBox.Text.Contains(".")) return;

            activeTextBox.Text += buttonText;
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (activeTextBox == null) return;

            if (activeTextBox.Text.Length > 0)
            {
                activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
            }

            if (activeTextBox.Text == "" || activeTextBox.Text == "-")
            {
                activeTextBox.Text = "0";
            }
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            isCalculating = true;
            txtDegree.Text = "0";
            txtRadian.Text = "0";
            txtGradian.Text = "0";
            isCalculating = false;

            activeTextBox = txtDegree;
            txtDegree.Focus();
        }

        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            if (activeTextBox == null || string.IsNullOrEmpty(activeTextBox.Text) || activeTextBox.Text == "0")
                return;

            if (activeTextBox.Text.StartsWith("-"))
            {
                activeTextBox.Text = activeTextBox.Text.Substring(1);
            }
            else
            {
                activeTextBox.Text = "-" + activeTextBox.Text;
            }
        }

        private void txtAngle_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox currentTextBox = sender as Guna2TextBox;
            if (currentTextBox == null) return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
                return;
            }

            
            if ((e.KeyChar == '.') && (currentTextBox.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
                return;
            }

        
            if (e.KeyChar == '-')
            {
                if (currentTextBox.Text.IndexOf('-') > -1 || currentTextBox.SelectionStart != 0)
                {
                    e.Handled = true;
                    return;
                }
            }

           
            int digitCount = currentTextBox.Text.Replace(".", "").Replace("-", "").Length;
            if (char.IsDigit(e.KeyChar) && digitCount >= 26)
            {
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            if (char.IsDigit(e.KeyChar) && currentTextBox.Text == "0")
            {
                currentTextBox.Text = e.KeyChar.ToString();
                currentTextBox.SelectionStart = currentTextBox.Text.Length;
                e.Handled = true;
            }
        }

        private void txtRadian_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
