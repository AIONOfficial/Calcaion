using Calculatornew;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms; 

namespace Calculator
{
    public partial class ucGeometry : UserControl
    {
        private Guna2TextBox activeTextBox;

        public ucGeometry()
        {
            InitializeComponent();
            activeTextBox = txtInput1;

           
            txtInput1.Enter += (s, e) => activeTextBox = txtInput1;
            txtInput2.Enter += (s, e) => activeTextBox = txtInput2;
            txtInput3.Enter += (s, e) => activeTextBox = txtInput3;

         
            txtInput1.TextChanged += (s, ev) => CalculateGeometry();
            txtInput2.TextChanged += (s, ev) => CalculateGeometry();
            txtInput3.TextChanged += (s, ev) => CalculateGeometry();

            txtInput1.KeyPress += txtGeometry_KeyPress;
            txtInput2.KeyPress += txtGeometry_KeyPress;
            txtInput3.KeyPress += txtGeometry_KeyPress;

            if (cmbShapes.Items.Count > 0) cmbShapes.SelectedIndex = 0;
        }

        
        private void cmbShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            txtInput1.Text = "0"; txtInput2.Text = "0"; txtInput3.Text = "0";
            txtAreaResult.Text = "0"; txtPerimeterResult.Text = "0";

            if (cmbShapes.SelectedItem == null) return;

            string selectedShape = cmbShapes.SelectedItem.ToString().Substring(2).Trim();

            
            if (selectedShape == "Sphere" || selectedShape == "Cylinder" || selectedShape == "Cone" || selectedShape == "Pyramid")
            {
                lblPerimeterResult.Text = "Total Surface Area :";
                lblAreaResult.Text = "Volume :";
            }
            else
            {
                lblPerimeterResult.Text = "Perimeter :";
                lblAreaResult.Text = "Area :";
            }

           
            switch (selectedShape)
            {
                case "Circle":
                case "Semi-Circle":
                    SetupInputs(true, "Radius :", false, "", false, "");
                    break;
                case "Square":
                    SetupInputs(true, "Side :", false, "", false, "");
                    break;
                case "Rectangle":
                    SetupInputs(true, "Width :", true, "Length :", false, "");
                    break;
                case "Triangle":
                    SetupInputs(true, "Side A :", true, "Side B / Base :", true, "Side C / Height :");
                    break;
                case "Parallelogram":
                    SetupInputs(true, "Base :", true, "Height :", true, "Side :");
                    break;
                case "Rhombus":
                    SetupInputs(true, "Diagonal 1 :", true, "Diagonal 2 :", true, "Side :");
                    break;
                case "Trapezoid":
                    SetupInputs(true, "Base A :", true, "Base B :", true, "Height / Sides :");
                    break;
                case "Ellipse":
                    SetupInputs(true, "Semi-major Axis :", true, "Semi-minor Axis :", false, "");
                    break;
                case "Regular Polygon":
                    SetupInputs(true, "Number of Sides :", true, "Side Length :", false, "");
                    break;
                case "Sphere":
                    SetupInputs(true, "Radius :", false, "", false, "");
                    break;
                case "Cylinder":
                    SetupInputs(true, "Radius :", true, "Height :", false, "");
                    break;
                case "Cone":
                    SetupInputs(true, "Radius :", true, "Height :", false, "");
                    break;
                case "Pyramid":
                    SetupInputs(true, "Base Side :", true, "Height :", false, "");
                    break;
            }

         
            activeTextBox = txtInput1;
            txtInput1.Focus();
        }

        private void SetupInputs(bool in1, string lbl1, bool in2, string lbl2, bool in3, string lbl3)
        {
            txtInput1.Visible = in1; lblInput1.Visible = in1; lblInput1.Text = lbl1;
            txtInput2.Visible = in2; lblInput2.Visible = in2; lblInput2.Text = lbl2;
            txtInput3.Visible = in3; lblInput3.Visible = in3; lblInput3.Text = lbl3;

            if (in1) activeTextBox = txtInput1;
            else if (in2) activeTextBox = txtInput2;
            else if (in3) activeTextBox = txtInput3;
        }

        
        private void CalculateGeometry()
        {
            if (cmbShapes.SelectedItem == null) return;

            string selectedShape = cmbShapes.SelectedItem.ToString().Substring(2).Trim();

        
            double in1 = (string.IsNullOrEmpty(txtInput1.Text) || txtInput1.Text == ".") ? 0 : (double.TryParse(txtInput1.Text, out double res1) ? res1 : 0);
            double in2 = (string.IsNullOrEmpty(txtInput2.Text) || txtInput2.Text == ".") ? 0 : (double.TryParse(txtInput2.Text, out double res2) ? res2 : 0);
            double in3 = (string.IsNullOrEmpty(txtInput3.Text) || txtInput3.Text == ".") ? 0 : (double.TryParse(txtInput3.Text, out double res3) ? res3 : 0);

            double out1 = 0;
            double out2 = 0;

            switch (selectedShape)
            {
                case "Circle":
                    out1 = Math.PI * in1 * in1;
                    out2 = 2 * Math.PI * in1;
                    break;

                case "Semi-Circle":
                    out1 = (Math.PI * in1 * in1) / 2.0;
                    out2 = (Math.PI * in1) + (2 * in1);
                    break;

                case "Square":
                    out1 = in1 * in1;
                    out2 = 4 * in1;
                    break;

                case "Rectangle":
                    out1 = in1 * in2;
                    out2 = 2 * (in1 + in2);
                    break;

                case "Triangle":
                    out1 = (in2 * in3) / 2.0;
                    out2 = in1 + in2 + in3;
                    break;

                case "Parallelogram":
                    out1 = in1 * in2;
                    out2 = 2 * (in1 + in3);
                    break;

                case "Rhombus":
                    out1 = (in1 * in2) / 2.0;
                    out2 = in3 > 0 ? 4 * in3 : 4 * (Math.Sqrt(Math.Pow(in1 / 2.0, 2) + Math.Pow(in2 / 2.0, 2)));
                    break;

                case "Trapezoid":
                    out1 = ((in1 + in2) * in3) / 2.0;
                    double slantSide = Math.Sqrt(Math.Pow((in2 - in1) / 2.0, 2) + in3 * in3);
                    out2 = in1 + in2 + (2 * slantSide);
                    break;

                case "Ellipse":
                    out1 = Math.PI * in1 * in2;
                    out2 = Math.PI * (3 * (in1 + in2) - Math.Sqrt((3 * in1 + in2) * (in1 + 3 * in2)));
                    break;

                case "Regular Polygon":
                    int n = (int)in1;
                    double s = in2;
                    if (n >= 3 && s > 0)
                    {
                        out1 = (n * s * s) / (4 * Math.Tan(Math.PI / n));
                        out2 = n * s;
                    }
                    break;

                case "Sphere":
                    out1 = (4.0 / 3.0) * Math.PI * Math.Pow(in1, 3);
                    out2 = 4 * Math.PI * in1 * in1;
                    break;

                case "Cylinder":
                    out1 = Math.PI * in1 * in1 * in2;
                    out2 = (2 * Math.PI * in1 * in2) + (2 * Math.PI * in1 * in1);
                    break;

                case "Cone":
                    out1 = (1.0 / 3.0) * Math.PI * in1 * in1 * in2;
                    double coneSlant = Math.Sqrt(in1 * in1 + in2 * in2);
                    out2 = (Math.PI * in1 * coneSlant) + (Math.PI * in1 * in1);
                    break;

                case "Pyramid":
                    out1 = (1.0 / 3.0) * (in1 * in1) * in2;
                    double pyramidSlant = Math.Sqrt(Math.Pow(in1 / 2.0, 2) + in2 * in2);
                    out2 = (in1 * in1) + (2 * in1 * pyramidSlant);
                    break;
            }

            txtAreaResult.Text = out1.ToString("0.####");
            txtPerimeterResult.Text = out2.ToString("0.####");
        }

        
        private void btnNumbers_Click(object sender, EventArgs e)
        {
            
            if (activeTextBox != null && activeTextBox.Visible)
            {
                Input_Manager.AddNumberFromSender(activeTextBox, sender);
                CalculateGeometry(); 
            }
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
    
            if (activeTextBox != null && activeTextBox.Visible && activeTextBox.Text.Length > 0)
            {
                activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
                if (activeTextBox.Text == "") activeTextBox.Text = "0";
                CalculateGeometry();
            }
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            txtInput1.Text = "0"; txtInput2.Text = "0"; txtInput3.Text = "0";
            txtAreaResult.Text = "0"; txtPerimeterResult.Text = "0";

            activeTextBox = txtInput1;
            txtInput1.Focus();
        }

       
        private void txtInputs_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox tb = sender as Guna2TextBox;
            if (tb != null && string.IsNullOrEmpty(tb.Text))
            {
                tb.TextChanged -= txtInputs_TextChanged;
                tb.Text = "0";
                tb.TextChanged += txtInputs_TextChanged;
            }

            CalculateGeometry();
        }
        private void txtGeometry_KeyPress(object sender, KeyPressEventArgs e)
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
                CalculateGeometry(); 
                e.Handled = true;
            }
        }

        private void lblPerimeterResult_Click(object sender, EventArgs e)
        {

        }

        private void ucGeometry_Load(object sender, EventArgs e)
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

