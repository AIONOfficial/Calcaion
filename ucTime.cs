using Calculatornew;
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
    public partial class ucTime : UserControl
    {
        private Guna2TextBox activeTextBox;
        public ucTime()
        {

            InitializeComponent();
            activeTextBox = txtFromValue;

            txtFromValue.TextChanged += (s, ev) => PerformConversion();
            txtToValue.TextChanged += (s, ev) => PerformConversion();
           
            if (cmbFromUnit.Items.Count > 0) cmbFromUnit.SelectedIndex = 4; 
            if (cmbToUnit.Items.Count > 0) cmbToUnit.SelectedIndex = 3;   
            txtFromValue.Text = "0";
            txtToValue.Text = "0";
            UpdateConverterLayout();
        }

      
        private void btnNumbers_Click(object sender, EventArgs e)
        {
            if (activeTextBox != null)
            {
               
                Input_Manager.AddNumberFromSender(activeTextBox, sender);

                
                PerformConversion();
            }
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {

        }

     
        private void btnCE_Click(object sender, EventArgs e)
        {
            txtFromValue.Text = "0";
            txtToValue.Text = "0";
        }

        private void cmbFromUnit_SelectedIndexChanged(object sender, EventArgs e) => PerformConversion();
        private void cmbToUnit_SelectedIndexChanged(object sender, EventArgs e) => PerformConversion();

        private void PerformConversion()
        {
            
            if (activeTextBox == null) return;

          
            Guna2TextBox txtTarget = (activeTextBox == txtFromValue) ? txtToValue : txtFromValue;

       
            if (string.IsNullOrEmpty(activeTextBox.Text) || activeTextBox.Text == "0" || activeTextBox.Text == ".")
            {
                txtTarget.Text = "0";
                return;
            }

            if (cmbFromUnit.SelectedItem == null || cmbToUnit.SelectedItem == null) return;

            
            if (!double.TryParse(activeTextBox.Text, out double inputValue)) return;

            
            string fromUnit = "";
            string toUnit = "";

            if (activeTextBox == txtFromValue)
            {
                fromUnit = cmbFromUnit.SelectedItem.ToString().Trim();
                toUnit = cmbToUnit.SelectedItem.ToString().Trim();
            }
            else
            {
                fromUnit = cmbToUnit.SelectedItem.ToString().Trim();
                toUnit = cmbFromUnit.SelectedItem.ToString().Trim();
            }

          
            double seconds = 0;
            switch (fromUnit)
            {
                case "Microseconds": seconds = inputValue / 1000000.0; break;
                case "Milliseconds": seconds = inputValue / 1000.0; break;
                case "Seconds": seconds = inputValue; break;
                case "Minutes": seconds = inputValue * 60.0; break;
                case "Hours": seconds = inputValue * 3600.0; break;
                case "Weeks": seconds = inputValue * 604800.0; break;
                case "Years": seconds = inputValue * 31557600.0; break;
                default: return;
            }

            
            double result = 0;
            switch (toUnit)
            {
                case "Microseconds": result = seconds * 1000000.0; break;
                case "Milliseconds": result = seconds * 1000.0; break;
                case "Seconds": result = seconds; break;
                case "Minutes": result = seconds / 60.0; break;
                case "Hours": result = seconds / 3600.0; break;
                case "Weeks": result = seconds / 604800.0; break;
                case "Years": result = seconds / 31557600.0; break;
                default: return;
            }

          
            txtTarget.Text = result.ToString("0.########");
        }

        private void txtFromValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox currentTextBox = sender as Guna2TextBox;
            if (currentTextBox == null) return;

        
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                return;
            }

            
            if ((e.KeyChar == '.') && (currentTextBox.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
                return;
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

                
                PerformConversion();

                e.Handled = true; 
            }
        }

        private void txtToValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox currentTextBox = sender as Guna2TextBox;
            if (currentTextBox == null) return;

            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                return;
            }

          
            if ((e.KeyChar == '.') && (currentTextBox.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
                return;
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

                
                PerformConversion();

                e.Handled = true; 
            }
        }

        private void txtFromValue_Enter(object sender, EventArgs e)
        {
            activeTextBox = txtFromValue;

        }

        private void txtToValue_Enter(object sender, EventArgs e)
        {
            activeTextBox = txtToValue;

        }

        private void ucTime_Load(object sender, EventArgs e)
        {

        }

        private void ucTime_Resize(object sender, EventArgs e)
        {
            UpdateConverterLayout();
        }
        private void UpdateConverterLayout()
        {

            if (pnlInputs == null || pnlNumPad == null) return;

            if (this.Width > 700)
            {
              
                pnlInputs.Dock = DockStyle.None;
                pnlNumPad.Dock = DockStyle.None;

                pnlNumPad.Width = 750; 
                pnlNumPad.Dock = DockStyle.Right;

                pnlInputs.Dock = DockStyle.Fill;
                pnlInputs.Padding = new Padding(40, 60, 40, 40);
            }
            else
            {
               
                pnlInputs.Dock = DockStyle.None;
                pnlNumPad.Dock = DockStyle.None;

                pnlInputs.Height = 450; 
                pnlInputs.Dock = DockStyle.Top;

                pnlNumPad.Dock = DockStyle.Fill;
                pnlInputs.Padding = new Padding(20, 60, 20, 10);
            }

            pnlInputs.Invalidate();
            pnlNumPad.Invalidate();
        }
    }
}
