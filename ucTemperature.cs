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
    public partial class ucTemperature : UserControl
    {
        private Guna2TextBox activeTextBox;
        public ucTemperature()
        {
            InitializeComponent();
            activeTextBox = txtFromValue;
            txtFromValue.TextChanged += (s, ev) => PerformConversion();
            txtToValue.TextChanged += (s, ev) => PerformConversion();
           
            if (cmbFromUnit.Items.Count > 0)
            {
                cmbFromUnit.SelectedIndex = 0; 
            }

            if (cmbToUnit.Items.Count > 0)
            {
                cmbToUnit.SelectedIndex = 1; 
            }
            else if (cmbToUnit.Items.Count == 0)
            {
               
                cmbToUnit.Items.AddRange(new object[] { "Celsius", "Fahrenheit", "Kelvin" });
                cmbToUnit.SelectedIndex = 1;
            }

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

        
        private void btnSign_Click(object sender, EventArgs e)
        {
            if (txtFromValue.Text == "0") return;

            if (txtFromValue.Text.StartsWith("-"))
            {
                txtFromValue.Text = txtFromValue.Text.Substring(1); 
            }
            else
            {
                txtFromValue.Text = "-" + txtFromValue.Text; 
            }
            PerformConversion();
        }

        
        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtFromValue.Text.Length > 0)
            {
                txtFromValue.Text = txtFromValue.Text.Substring(0, txtFromValue.Text.Length - 1);
            }

            if (txtFromValue.Text == "" || txtFromValue.Text == "-")
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

        private void cmbFromUnit_SelectedIndexChanged(object sender, EventArgs e) => PerformConversion();
        private void cmbToUnit_SelectedIndexChanged(object sender, EventArgs e) => PerformConversion();

     
        private void PerformConversion()
        {
            if (activeTextBox == null) return;

            Guna2TextBox txtTarget = (activeTextBox == txtFromValue) ? txtToValue : txtFromValue;

            if (string.IsNullOrEmpty(activeTextBox.Text) || activeTextBox.Text == "." || activeTextBox.Text == "-")
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

            double celsius = 0;
            switch (fromUnit)
            {
                case "Celsius": celsius = inputValue; break;
                case "Fahrenheit": celsius = (inputValue - 32) * 5.0 / 9.0; break;
                case "Kelvin": celsius = inputValue - 273.15; break;
                default: return;
            }

            double result = 0;
            switch (toUnit)
            {
                case "Celsius": result = celsius; break;
                case "Fahrenheit": result = (celsius * 9.0 / 5.0) + 32; break;
                case "Kelvin": result = celsius + 273.15; break;
                default: return;
            }

            txtTarget.Text = result.ToString("0.####");
        }

        private void txtFromValue_KeyPress(object sender, KeyPressEventArgs e)
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
                PerformConversion();
                e.Handled = true;
            }

        }

        private void txtToValue_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ucTemperature_Load(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (this.ParentForm is Form1 mainForm)
                {
                    mainForm.ApplyTheme(this, Form1.IsDarkModeSaved);
                }
            }
        }

        private void ucTemperature_Resize(object sender, EventArgs e)
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

     
    

