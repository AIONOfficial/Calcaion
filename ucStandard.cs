using Calculatornew;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public partial class ucStandard : UserControl
    {
        
        Calculator c = new Calculator();

        public ucStandard()
        {
            InitializeComponent();

        }

        
        public void ApplyThemeColors(bool isDark)
        {

        }

        private void ColorButtonsRecursive(Control parent, Color btnColor, Color foreColor, bool isDark)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2Button btn)
                {
                    btn.FillColor = btnColor;
                    btn.ForeColor = foreColor;
                    btn.HoverState.FillColor = isDark ? Color.FromArgb(70, 70, 70) : Color.FromArgb(230, 230, 230);
                }
                else if (control.HasChildren)
                {
                    ColorButtonsRecursive(control, btnColor, foreColor, isDark);
                }
            }
        }

       
        private void btnDigit0_Click(object sender, EventArgs e) => HandleDigitInput("0", 0);
        private void btnDigit1_Click(object sender, EventArgs e) => HandleDigitInput("1", 1);
        private void btnDigit2_Click(object sender, EventArgs e) => HandleDigitInput("2", 2);
        private void btnDigit3_Click(object sender, EventArgs e) => HandleDigitInput("3", 3);
        private void btnDigit4_Click(object sender, EventArgs e) => HandleDigitInput("4", 4);
        private void btnDigit5_Click(object sender, EventArgs e) => HandleDigitInput("5", 5);
        private void btnDigit6_Click(object sender, EventArgs e) => HandleDigitInput("6", 6);
        private void btnDigit7_Click(object sender, EventArgs e) => HandleDigitInput("7", 7);
        private void btnDigit8_Click(object sender, EventArgs e) => HandleDigitInput("8", 8);
        private void btnDigit9_Click(object sender, EventArgs e) => HandleDigitInput("9", 9);

        private void HandleDigitInput(string digitStr, int digitValue)
        {
            c.EnterDigit(digitValue);
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

       
        private void btnDot_Click(object sender, EventArgs e)
        {
            c.EnterDot();
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            c.ChangeSign();
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

     
        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.SetOperation("+");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            c.SetOperation("-");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            c.SetOperation("*");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            c.SetOperation("/");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            c.SetOperation("^");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

       
        private void btnEquals_Click(object sender, EventArgs e)
        {
            c.Calculate();
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnReciprocal_Click(object sender, EventArgs e)
        {
            c.Reciprocal();
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            c.SquareRoot();
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            c.Percent();
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

     
        private void btnCE_Click(object sender, EventArgs e)
        {
            c.ClearEntry();
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            c.Clear();
            guna2TextBox1.Text = "0";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            c.Backspace();
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void ucStandard_Load(object sender, EventArgs e)
        {
          
            if (this.Visible)
            {
                if (this.ParentForm is Form1 mainForm)
                {
                    mainForm.ApplyTheme(this, Form1.IsDarkModeSaved);
                }
            }
        }
    }
}