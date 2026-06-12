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
    public partial class ucLength : UserControl
    {
        private Guna2TextBox activeTextBox;

        public ucLength()
        {
            InitializeComponent();
            activeTextBox = txtFromValue;

            txtFromValue.TextChanged += (s, ev) => PerformConversion();
            txtToValue.TextChanged += (s, ev) => PerformConversion();

            if (cmbFromUnit.Items.Count > 0) cmbFromUnit.SelectedIndex = 1;
            if (cmbToUnit.Items.Count > 0) cmbToUnit.SelectedIndex = 2;  
            txtFromValue.Text = "0";

            
            this.Resize += ucLength_Resize;

            
            UpdateConverterLayout();
        }

        private void ucConverter_Load(object sender, EventArgs e)
        {
            activeTextBox = txtFromValue; 
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformConversion();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformConversion();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (activeTextBox != null)
            {
               
                Input_Manager.AddNumberFromSender(activeTextBox, sender);

                
                PerformConversion();
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (txtFromValue.Text.Length > 0)
            {
                
                txtFromValue.Text = txtFromValue.Text.Substring(0, txtFromValue.Text.Length - 1);
            }

            
            if (txtFromValue.Text == "")
            {
                txtFromValue.Text = "0";
            }

         
            PerformConversion();
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            txtFromValue.Text = "0";
            txtToValue.Text = "0";
        }

        private void PerformConversion()
        {
            
            if (activeTextBox == null) return;

            Guna2TextBox txtTarget = (activeTextBox == txtFromValue) ? txtToValue : txtFromValue;

           
            if (string.IsNullOrEmpty(activeTextBox.Text) || activeTextBox.Text == "0" || activeTextBox.Text == ".")
            {
                txtTarget.Text = "0";
                return;
            }

            if (cmbFromUnit.SelectedItem == null || cmbToUnit.SelectedItem == null)
            {
                return;
            }

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

           
            double valueInMeters = 0;
            switch (fromUnit)
            {
                case "Millimeters": valueInMeters = inputValue / 1000.0; break;
                case "Centimeters": valueInMeters = inputValue / 100.0; break;
                case "Meters": valueInMeters = inputValue; break;
                case "Kilometers": valueInMeters = inputValue * 1000.0; break;
                case "Inches": valueInMeters = inputValue * 0.0254; break;
                case "Feet": valueInMeters = inputValue * 0.3048; break;
                case "Miles": valueInMeters = inputValue * 1609.344; break;
                default: return;
            }

           
            double result = 0;
            switch (toUnit)
            {
                case "Millimeters": result = valueInMeters * 1000.0; break;
                case "Centimeters": result = valueInMeters * 100.0; break;
                case "Meters": result = valueInMeters; break;
                case "Kilometers": result = valueInMeters / 1000.0; break;
                case "Inches": result = valueInMeters / 0.0254; break;
                case "Feet": result = valueInMeters / 0.3048; break;
                case "Miles": result = valueInMeters / 1609.344; break;
                default: return;
            }

            
            txtTarget.Text = result.ToString("0.######");
        }

        private void txtFromValue_Enter(object sender, EventArgs e)
        {
            activeTextBox = txtFromValue;
        }

        private void txtToValue_Enter(object sender, EventArgs e)
        {
            activeTextBox = txtToValue;
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

        private void txtToValue_TextChanged(object sender, EventArgs e)
        {
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

        private void txtToValue_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void ucLength_Load(object sender, EventArgs e)
        {
        }

        private void ucLength_Resize(object sender, EventArgs e)
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
                pnlInputs.Padding = new Padding(40, 50, 40, 40);
            }
            else
            {
                
                pnlInputs.Dock = DockStyle.None;
                pnlNumPad.Dock = DockStyle.None;

                pnlInputs.Height = 450; 
                pnlInputs.Dock = DockStyle.Top;

                pnlNumPad.Dock = DockStyle.Fill;
                pnlInputs.Padding = new Padding(20, 50, 20, 10);
            }

            pnlInputs.Invalidate();
            pnlNumPad.Invalidate();
        }
    }
}

    
