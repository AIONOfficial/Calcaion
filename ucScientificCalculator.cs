using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Calculatornew
{
    public partial class ucScientificCalculator : UserControl
    {
        private Guna2TextBox activeTextBox;
        private string lastResult = "0";
        private bool isOperatorPressed = false;
        private bool isHypMode = false;
        private bool isMain2ndActive = false; 

        public ucScientificCalculator()
        {
            InitializeComponent();
            activeTextBox = txtDisplay;
            txtDisplay.Enter += TextBox_Enter;
            txtDisplay.KeyPress += txtScientific_KeyPress;
            txtDisplay.Text = "0";
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (sender is Guna2TextBox textBox) activeTextBox = textBox;
        }

       
        private void btnInput_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna2Button btn)) return;
            if (activeTextBox == null) activeTextBox = txtDisplay;

            if (pnlTrigMenu != null) pnlTrigMenu.Visible = false;
            if (pnlFuncMenu != null) pnlFuncMenu.Visible = false;

            string input = btn.Text.Trim();

            if (activeTextBox.Text.Length >= 40 && !isOperatorPressed)
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            if (activeTextBox.Text == "0" || activeTextBox.Text == "Error" || isOperatorPressed)
            {
                activeTextBox.Text = (input == ".") ? "0" : "";
                isOperatorPressed = false;
            }

            if (input == ".")
            {
                string currentTokens = activeTextBox.Text;
                string lastNumber = currentTokens.Split(new char[] { '+', '-', '*', '/', '%', '^', '(', ')' }).LastOrDefault();
                if (lastNumber != null && lastNumber.Contains(".")) return;
            }

            activeTextBox.Text += input;
        }

        
        private void btnConstants_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna2Button btn)) return;
            if (activeTextBox == null) activeTextBox = txtDisplay;

            if (activeTextBox.Text == "0" || activeTextBox.Text == "Error" || isOperatorPressed)
            {
                activeTextBox.Text = "";
                isOperatorPressed = false;
            }

            string txt = btn.Text.ToLower().Trim();
            if (txt.Contains("π") || txt == "pi")
                activeTextBox.Text += Math.PI.ToString(CultureInfo.InvariantCulture);
            else if (txt == "e")
                activeTextBox.Text += Math.E.ToString(CultureInfo.InvariantCulture);
        }

        
        private void btnOperators_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna2Button btn)) return;
            if (activeTextBox == null) activeTextBox = txtDisplay;

            string op = btn.Text.Trim().ToLower();
            string currentText = activeTextBox.Text;

            if (currentText == "Error") currentText = "0";

            if (op == "mod") op = "%";
            else if (op == "×") op = "*";
            else if (op == "÷") op = "/";

            if (op == "exp")
            {
                if (currentText == "0" || isOperatorPressed) currentText = "1";
                activeTextBox.Text = currentText + "*10^(";
                isOperatorPressed = false;
                return;
            }

            if (currentText.EndsWith("+") || currentText.EndsWith("-") ||
                currentText.EndsWith("*") || currentText.EndsWith("/") || currentText.EndsWith("%") || currentText.EndsWith("^"))
            {
                currentText = currentText.Remove(currentText.Length - 1);
            }

            activeTextBox.Text = currentText + op;
            isOperatorPressed = false;
        }

       
        private void btnAdvancedMath_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna2Button btn)) return;
            if (activeTextBox == null) activeTextBox = txtDisplay;

            string op = btn.Text.Trim();
            string currentText = activeTextBox.Text;

            if (currentText == "0" || currentText == "Error" || isOperatorPressed)
            {
                currentText = "0";
                isOperatorPressed = false;
            }

            if (op == "1/x") activeTextBox.Text = currentText == "0" ? "1/" : $"1/({currentText})";
            else if (op == "10ˣ" || op == "10^x" || op == "10 Hux") activeTextBox.Text = currentText == "0" ? "10^(" : $"10^({currentText})";
            else if (op == "2ˣ" || op == "2^x" || op == "2\u02e3") activeTextBox.Text = currentText == "0" ? "2^(" : $"2^({currentText})";
            else if (op == "|(x)|" || op == "|x|") activeTextBox.Text = $"abs({currentText})";
            else if (op == "xʸ" || op == "x^y") activeTextBox.Text = currentText + "^(";
            else if (op == "²√x" || op == "√") activeTextBox.Text = currentText == "0" ? "sqrt(" : $"sqrt({currentText})";
            else if (op == "³√x") activeTextBox.Text = currentText == "0" ? "cbrt(" : $"cbrt({currentText})";
            else if (op == "x²") activeTextBox.Text = $"({currentText})^2";
            else if (op == "x³") activeTextBox.Text = $"({currentText})^3";
            else if (op == "log") activeTextBox.Text = currentText == "0" ? "log(" : $"log({currentText})";
            else if (op == "ln") activeTextBox.Text = currentText == "0" ? "ln(" : $"ln({currentText})";
            else if (op == "n!") activeTextBox.Text = $"factorial({currentText})";

            if (op == "1/x" || op == "10ˣ" || op == "10^x" || op == "10 Hux" || op == "2ˣ" || op == "2^x" || op == "2\u02e3" || op == "xʸ" || op == "x^y" || op == "²√x" || op == "√" || op == "³√x" || op == "log" || op == "ln")
                isOperatorPressed = false;
            else
                isOperatorPressed = true;
        }

     
        private void btnFuncMenu_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna2Button btn)) return;
            if (activeTextBox == null) activeTextBox = txtDisplay;

            string op = btn.Text.Trim();
            string currentText = activeTextBox.Text;

            if (currentText == "0" || currentText == "Error" || isOperatorPressed)
            {
                currentText = "0";
                isOperatorPressed = false;
            }

            if (op == "rand")
            {
                activeTextBox.Text = new Random().NextDouble().ToString(CultureInfo.InvariantCulture);
                isOperatorPressed = false;
                if (pnlFuncMenu != null) pnlFuncMenu.Visible = false;
                return;
            }

            switch (op)
            {
                case "[x]": activeTextBox.Text = $"floor({currentText})"; break;
                case "⌈x⌉": activeTextBox.Text = $"ceiling({currentText})"; break;
                case "|(x)|":
                case "|x|": activeTextBox.Text = $"abs({currentText})"; break;
                case "deg": activeTextBox.Text = $"deg({currentText})"; break;
                case "dms": activeTextBox.Text = $"dms({currentText})"; break;
            }

            isOperatorPressed = true;
            if (pnlFuncMenu != null) pnlFuncMenu.Visible = false;
        }

        
        private void btnTrigFunctions_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna2Button btn)) return;
            if (activeTextBox == null) activeTextBox = txtDisplay;

            string func = btn.Text.ToLower().Trim();
            string current = activeTextBox.Text.Trim();

            if (string.IsNullOrEmpty(current) || current == "Error") current = "0";

            
            if (isMain2ndActive)
            {
                if (func == "sin") ;
                else if (func == "cos") ;
                else if (func == "tan") ;
                else if (func == "sec") ;
                else if (func == "csc") ;
                else if (func == "cot") ;
            }

            if (isHypMode)
            {
                if (func == "sin") func = "sinh";
                else if (func == "cos") func = "cosh";
                else if (func == "tan") func = "tanh";
                else if (func == "sec") func = "sech";
                else if (func == "csc") func = "csch";
                else if (func == "cot") func = "coth";
               
            }

            if (current == "0" || isOperatorPressed)
            {
                activeTextBox.Text = $"{func}(";
                isOperatorPressed = false;
            }
            else
            {
                activeTextBox.Text = $"{func}({current})";
                isOperatorPressed = true;
            }

            if (pnlTrigMenu != null) pnlTrigMenu.Visible = false;
        }

        
        private void btnMain2nd_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna2Button btn2nd)) return;

            isMain2ndActive = !isMain2ndActive;

          
            btn2nd.FillColor = isMain2ndActive ? Color.FromArgb(0, 90, 158) : Color.White;
            btn2nd.ForeColor = isMain2ndActive ? Color.White : Color.Black;

            
            if (isMain2ndActive)
            {
                if (btnSquare != null) btnSquare.Text = "x³";
                if (btnRoot != null) btnRoot.Text = "³√x";
                if (btnTenToX != null) btnTenToX.Text = "2\u02e3";
            }
            else
            {
                if (btnSquare != null) btnSquare.Text = "x²";
                if (btnRoot != null) btnRoot.Text = "²√x";
                if (btnTenToX != null) btnTenToX.Text = "10\u02e3";
            }

          
            UpdateTrigButtonsText();
        }

       
        private void btnHyp_Click(object sender, EventArgs e)
        {
            if (!(sender is Guna2Button btn)) return;

            isHypMode = !isHypMode;
            btn.FillColor = isHypMode ? Color.FromArgb(0, 90, 158) : Color.White;
            btn.ForeColor = isHypMode ? Color.White : Color.Black;

            UpdateTrigButtonsText();
        }

        private void UpdateTrigButtonsText()
        {
            if (btnSin == null || btnCos == null || btnTan == null) return;

            if (isMain2ndActive && isHypMode)
            {
                btnSin.Text = "asinh"; btnCos.Text = "acosh"; btnTan.Text = "atanh";
                if (btnSec != null) btnSec.Text = "asech"; if (btnCsc != null) btnCsc.Text = "acsch"; if (btnCot != null) btnCot.Text = "acoth";
            }
            else if (isMain2ndActive)
            {
                btnSin.Text = "arcsin"; btnCos.Text = "arccos"; btnTan.Text = "arctan";
                if (btnSec != null) btnSec.Text = "arcsec"; if (btnCsc != null) btnCsc.Text = "arccsc"; if (btnCot != null) btnCot.Text = "arccot";
            }
            else if (isHypMode)
            {
                btnSin.Text = "sinh"; btnCos.Text = "cosh"; btnTan.Text = "tanh";
                if (btnSec != null) btnSec.Text = "sech"; if (btnCsc != null) btnCsc.Text = "csch"; if (btnCot != null) btnCot.Text = "coth";
            }
            else
            {
                btnSin.Text = "sin"; btnCos.Text = "cos"; btnTan.Text = "tan";
                if (btnSec != null) btnSec.Text = "sec"; if (btnCsc != null) btnCsc.Text = "csc"; if (btnCot != null) btnCot.Text = "cot";
            }
        }

        private void btnTrigMenuToggle_Click(object sender, EventArgs e)
        {
            if (pnlTrigMenu != null)
            {
                pnlTrigMenu.Visible = !pnlTrigMenu.Visible;
                if (pnlFuncMenu != null) pnlFuncMenu.Visible = false;
                pnlTrigMenu.BringToFront();
            }
        }

        private void btnFuncMenuToggle_Click(object sender, EventArgs e)
        {
            if (pnlFuncMenu != null)
            {
                pnlFuncMenu.Visible = !pnlFuncMenu.Visible;
                if (pnlTrigMenu != null) pnlTrigMenu.Visible = false;
                pnlFuncMenu.BringToFront();
            }
        }

        private void btnPositiveNegative_Click(object sender, EventArgs e)
        {
            if (activeTextBox == null) activeTextBox = txtDisplay;
            string currentText = activeTextBox.Text.Trim();

            if (currentText == "0" || currentText == "Error" || string.IsNullOrEmpty(currentText)) return;

            if (currentText.StartsWith("(-") && currentText.EndsWith(")"))
                activeTextBox.Text = currentText.Substring(2, currentText.Length - 3);
            else
                activeTextBox.Text = $"(-{currentText})";
        }

        private void btnAns_Click(object sender, EventArgs e)
        {
            if (activeTextBox == null) activeTextBox = txtDisplay;
            if (activeTextBox.Text == "0" || isOperatorPressed || activeTextBox.Text == "Error") activeTextBox.Text = "";
            activeTextBox.Text += lastResult;
            isOperatorPressed = false;
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            if (activeTextBox == null) activeTextBox = txtDisplay;
            activeTextBox.Text = "0";
            isOperatorPressed = false;
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (activeTextBox == null) activeTextBox = txtDisplay;
            string txt = activeTextBox.Text;

            if (txt.Length > 0 && txt != "0" && txt != "Error")
                activeTextBox.Text = txt.Remove(txt.Length - 1);

            if (string.IsNullOrEmpty(activeTextBox.Text) || activeTextBox.Text == "-")
                activeTextBox.Text = "0";
        }

      
        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                if (activeTextBox == null) activeTextBox = txtDisplay;
                string formula = activeTextBox.Text.Trim();

                if (string.IsNullOrEmpty(formula) || formula == "Error") return;

                int openCount = formula.Count(f => f == '(');
                int closeCount = formula.Count(f => f == ')');
                while (openCount > closeCount)
                {
                    formula += ")";
                    closeCount++;
                }

                formula = ParseCustomFunctions(formula);

                if (formula == "Error")
                {
                    activeTextBox.Text = "Error";
                    isOperatorPressed = true;
                    return;
                }

                formula = formula.Replace("×", "*").Replace("÷", "/");

                DataTable table = new DataTable();
                object result = table.Compute(formula, string.Empty);
                double finalVal = Convert.ToDouble(result, CultureInfo.InvariantCulture);

                if (double.IsNaN(finalVal) || double.IsInfinity(finalVal))
                {
                    activeTextBox.Text = "Error";
                }
                else
                {
                    activeTextBox.Text = Math.Round(finalVal, 10).ToString(CultureInfo.InvariantCulture);
                    lastResult = activeTextBox.Text;
                }
                isOperatorPressed = true;
            }
            catch
            {
                activeTextBox.Text = "Error";
                isOperatorPressed = true;
            }
        }

        private string ParseCustomFunctions(string formula)
        {
            if (string.IsNullOrEmpty(formula)) return formula;

            formula = formula.Replace("10\u02e3", "10^").Replace("10ˣ", "10^");
            formula = formula.Replace("x\u02b8", "^").Replace("xʸ", "^").Replace("x^y", "^");
            formula = formula.Replace("|(x)|", "abs").Replace("|x|", "abs");
            formula = formula.Replace("[x]", "floor").Replace("\u2308x\u2309", "ceiling").Replace("⌈x⌉", "ceiling");

            formula = formula.ToLower().Trim();

            string[] functions = { "factorial","asinh", "acosh", "atanh", "sinh", "cosh", "tanh", "coth", "sech", "csch",
                                   "sin", "cos", "tan", "cot", "sec", "csc",
                                   "floor", "ceiling", "sqrt", "cbrt", "log", "dms", "deg", "abs", "ln" };

            foreach (var func in functions)
            {
                while (formula.Contains(func + "("))
                {
                    int startIndex = formula.IndexOf(func + "(");
                    int openParen = startIndex + func.Length + 1;
                    int closeParen = FindClosingParenthesis(formula, openParen - 1);

                    if (closeParen == -1) break;

                    string inside = formula.Substring(openParen, closeParen - openParen);
                    inside = ParseCustomFunctions(inside);

                    if (inside == "Error") return "Error";

                    double value = 0;
                    if (!double.TryParse(inside, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                    {
                        try
                        {
                            string finalInside = inside.Replace("×", "*").Replace("÷", "/");
                            value = Convert.ToDouble(new DataTable().Compute(finalInside, string.Empty), CultureInfo.InvariantCulture);
                        }
                        catch { return "Error"; }
                    }

                    double calculatedResult = 0;
                    double rad = value * (Math.PI / 180.0);

                    switch (func)
                    {
                        case "sin":
                            calculatedResult = Math.Sin(rad);
                            break;
                        case "cos": calculatedResult = Math.Cos(rad); break;
                        case "tan": if (Math.Abs(value % 180) == 90) return "Error"; calculatedResult = Math.Tan(rad); break;
                        case "cot": if (value % 180 == 0) return "Error"; calculatedResult = 1.0 / Math.Tan(rad); break;
                        case "sec": calculatedResult = 1.0 / Math.Cos(rad); break;
                        case "csc": calculatedResult = 1.0 / Math.Sin(rad); break;

                        case "sinh": calculatedResult = Math.Sinh(value); break;
                        case "cosh": calculatedResult = Math.Cosh(value); break;
                        case "tanh": calculatedResult = Math.Tanh(value); break;
                        case "coth": if (value == 0) return "Error"; calculatedResult = 1.0 / Math.Tanh(value); break;
                        case "sech": calculatedResult = 1.0 / Math.Cosh(value); break;
                        case "csch": if (value == 0) return "Error"; calculatedResult = 1.0 / Math.Sinh(value); break;
                        case "sqrt": if (value < 0) return "Error"; calculatedResult = Math.Sqrt(value); break;
                        case "cbrt": calculatedResult = Math.Pow(value, 1.0 / 3.0); break;
                        case "log": if (value <= 0) return "Error"; calculatedResult = Math.Log10(value); break;
                        case "ln": if (value <= 0) return "Error"; calculatedResult = Math.Log(value); break;
                        case "abs": calculatedResult = Math.Abs(value); break;
                        case "floor": calculatedResult = Math.Floor(value); break;
                        case "ceiling": calculatedResult = Math.Ceiling(value); break;
                        case "deg": calculatedResult = value * (180.0 / Math.PI); break;
                        case "dms":
                            int d = (int)value;
                            double mNotTrunc = (Math.Abs(value) - Math.Abs(d)) * 60.0;
                            int m = (int)mNotTrunc;
                            double s = (mNotTrunc - m) * 60.0;
                            calculatedResult = double.Parse($"{d}.{m:D2}{Math.Round(s):D2}", CultureInfo.InvariantCulture);
                            break;
                        case "factorial":
                            if (value < 0 || value != Math.Floor(value) || value > 170) return "Error";
                            double fact = 1;
                            for (int i = 2; i <= value; i++) fact *= i;
                            calculatedResult = fact;
                            break;
                    }

                    formula = formula.Remove(startIndex, closeParen - startIndex + 1)
                                     .Insert(startIndex, calculatedResult.ToString(CultureInfo.InvariantCulture));
                }
            }

            if (formula.Contains("^"))
            {
                string[] parts = formula.Split('^');
                if (parts.Length == 2)
                {
                    try
                    {
                        double baseVal = Convert.ToDouble(new DataTable().Compute(parts[0].Replace("×", "*").Replace("÷", "/"), string.Empty), CultureInfo.InvariantCulture);
                        double expVal = Convert.ToDouble(new DataTable().Compute(parts[1].Replace("×", "*").Replace("÷", "/"), string.Empty), CultureInfo.InvariantCulture);
                        formula = Math.Pow(baseVal, expVal).ToString(CultureInfo.InvariantCulture);
                    }
                    catch { return "Error"; }
                }
            }

            return formula;
        }

        private int FindClosingParenthesis(string str, int start)
        {
            int count = 0;
            for (int i = start; i < str.Length; i++)
            {
                if (str[i] == '(') count++;
                else if (str[i] == ')')
                {
                    count--;
                    if (count == 0) return i;
                }
            }
            return -1;
        }

        
        private void txtScientific_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] allowedChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                    '.', '+', '-', '*', '/', '%', '^', '(', ')', '\b' };

            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == '=')
            {
                e.Handled = true;
                btnEqual_Click(this, EventArgs.Empty);
                return;
            }

            if (!allowedChars.Contains(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (txtDisplay.Text == "0" && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                txtDisplay.Text = "";
            }
        }

        private void ucScientificCalculator_Load(object sender, EventArgs e)
        {

        }
    }
}