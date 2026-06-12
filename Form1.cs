using Calculatornew;
using Guna.UI2.WinForms;
using Plotly.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Suite.Descriptions;
using static Microsoft.FSharp.Core.ByRefKinds;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;



namespace Calculator
{
    public partial class Form1 : Form

    {
        bool isCurrentModeDark = false;
        bool menuOpen = false;
        int menuTargetWidth = 295;
        int animationStep = 60;
        bool isAnimating = false;

        Calculator c = new Calculator();
        public static bool IsDarkModeSaved = false;
        public static bool IsSystemThemeSelected = false;
        public Form1()
        {


            InitializeComponent();
           

            c = new Calculator();
            guna2TextBox1.Text = "0";
            ApplyTheme(this, false);

        }

      
      
        private void Form1_Load(object sender, EventArgs e)
        {


            {

                pnlMenu.Width = 0;
                menuOpen = false;
                btnMenu.Text = "☰";
                pnlMenu.BorderRadius = 10;
                pnlMenu.BorderThickness = 1;
                pnlMenu.BorderColor = Color.DimGray;
                guna2Panel1.Controls.Clear();

            
                ucStandard standardPage = new ucStandard();
                standardPage.Dock = DockStyle.Fill;
                guna2Panel1.Controls.Add(standardPage);

                

                pnlMenu.BringToFront();
                btnMenu.BringToFront();






            }
        }





        private void guna2Button12_Click(object sender, EventArgs e)
        {
            c.EnterDigit(2);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "2");
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            c.EnterDot();
            guna2TextBox1.Text = c.GetDisplay();

        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            c.ChangeSign();
            guna2TextBox1.Text = c.GetDisplay();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            c.EnterDigit(0);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "0");
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            c.EnterDigit(1);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "1");
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            c.EnterDigit(4);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "4");
        }

        private void btnDigit3_Click(object sender, EventArgs e)
        {
            c.EnterDigit(3);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "3");
        }

        private void btnDigit5_Click(object sender, EventArgs e)
        {
            c.EnterDigit(5);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "5");
        }


        private void btnDigit6_Click(object sender, EventArgs e)
        {
            c.EnterDigit(6);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "6");
        }

        private void btnDigit7_Click(object sender, EventArgs e)
        {
            c.EnterDigit(7);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "7");
        }

        private void btnDigit8_Click(object sender, EventArgs e)
        {
            c.EnterDigit(8);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "8");
        }

        private void btnDigit9_Click(object sender, EventArgs e)
        {
            c.EnterDigit(9);
            guna2TextBox1.Text = c.GetDisplay().ToString();
            Input_Manager.AddNumber(guna2TextBox1, "9");
        }


        private void guna2Button6_Click(object sender, EventArgs e)
        {
            c.SetOperation("+");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            c.SetOperation("-");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            c.SetOperation("*");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            c.SetOperation("/");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            c.Calculate();
            guna2TextBox1.Text = c.GetDisplay();
        }

        private void btnReciprocal_Click_1(object sender, EventArgs e)
        {
            c.Reciprocal();
            guna2TextBox1.Text = c.GetDisplay();
        }

        private void btnPower_Click_1(object sender, EventArgs e)
        {
            c.SetOperation("^");
            guna2TextBox1.Text = c.GetDisplay().ToString();
        }

        private void btnSqrt_Click_1(object sender, EventArgs e)
        {
            c.SquareRoot();
            guna2TextBox1.Text = c.GetDisplay();
        }

        private void btnPercent_Click_1(object sender, EventArgs e)
        {
            c.Percent();
            guna2TextBox1.Text = c.GetDisplay();
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            c.ClearEntry();
            guna2TextBox1.Text = c.GetDisplay();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            c.Clear();
            guna2TextBox1.Text = "0";
        }

        private void btnBackspace_Click_1(object sender, EventArgs e)
        {
            c.Backspace();
            guna2TextBox1.Text = c.GetDisplay();
        }

        private void pnlMenu_Paint(object sender, PaintEventArgs e)
        {


        }

        private void btnMenu_Click(object sender, EventArgs e)
        {

            
            FixLayers();

           
            tmrMenu.Start();
        }



        private void tmrMenu_Tick(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            {
                if (menuOpen)
                {
                    pnlMenu.Width -= animationStep;
                    if (pnlMenu.Width <= 0)
                    {
                        pnlMenu.Width = 0;
                        tmrMenu.Stop();
                        isAnimating = false;
                        menuOpen = false;
                        btnMenu.Text = "☰";
                        pnlMenu.Visible = false;
                    }
                }
                else
                {
                    pnlMenu.Visible = true;
                    pnlMenu.Width += animationStep;
                    if (pnlMenu.Width >= menuTargetWidth)
                    {
                        pnlMenu.Width = menuTargetWidth;
                        tmrMenu.Stop();
                        isAnimating = false;
                        menuOpen = true;
                        btnMenu.Text = "✕";
                    }

                }
            }
        }

       
        public void ApplyTheme(Control parent, bool isDarkMode)
        {
            Color mainBgColor = isDarkMode
      ? Color.FromArgb(15, 17, 23)      
      : Color.FromArgb(248, 250, 255);

            Color menuBgColor = isDarkMode
                ? Color.FromArgb(24, 28, 38)
                : Color.FromArgb(242, 242, 242);

            Color themeOptionsPanelColor = isDarkMode
                ? Color.FromArgb(20, 10, 40)
                : Color.FromArgb(215, 210, 245);

            Color lightModeHoverPurple = Color.FromArgb(127, 86, 255);

            Color darkModeHoverAion = Color.FromArgb(0, 191, 255);

            Color primaryBlue = Color.FromArgb(0, 191, 255);
            Color accentPurple = Color.FromArgb(127, 86, 255);
            Color textColor = isDarkMode ? Color.FromArgb(242, 247, 255): Color.Black;
          
            Color specialTxtBg = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.FromArgb(255, 255, 255); 
            Color specialTxtBorder = isDarkMode ? Color.FromArgb(70, 70, 70) : Color.FromArgb(180, 180, 180); 

            if (parent is UserControl)
            {
                parent.BackColor = mainBgColor;
            }

            foreach (Control ctrl in parent.Controls)
            {
                
                
                if (ctrl is Guna.UI2.WinForms.Guna2Panel pnl)
                {
                    pnl.BackColor = mainBgColor;


                    if (pnl.Name == "pnlThemeOptions")
                    {
                        pnl.FillColor = themeOptionsPanelColor;
                    }
                    else if (pnl.Name == "pnlAbout")
                    {
                        pnl.FillColor = isDarkMode ? Color.FromArgb(28, 28, 28) : Color.FromArgb(195, 200, 207);
                    }
                    else if (pnl.Name == "pnlMenu" || pnl.Name == "guna2Panel2")
                    {
                        pnl.FillColor = menuBgColor;
                    }
                    else if (pnl.Name == "guna2Panel3")
                    {
                        pnl.FillColor = isDarkMode ? Color.FromArgb(24, 28, 38) : Color.FromArgb(248, 250, 255);
                    }
                    else if (pnl.Name == "guna2Panel4")
                    {
                        pnl.FillColor = isDarkMode ? Color.FromArgb(0, 191, 255) : Color.FromArgb(0, 191, 255);
                    }
                    else
                    {
                        pnl.FillColor = mainBgColor;
                    }
                }

              
                if (ctrl is UserControl uc)
                {
                    uc.BackColor = mainBgColor;
                }

                
                
                if (ctrl is Guna.UI2.WinForms.Guna2TextBox txt)
                {
                    txt.BackColor = mainBgColor;
                    txt.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;

                    if (txt.Name == "txtInput1" || txt.Name == "txtInput2" || txt.Name == "txtInput3")
                    {
                        txt.FillColor = specialTxtBg; 
                        txt.BorderThickness = 1;      
                        txt.BorderColor = specialTxtBorder; 
                        txt.FocusedState.BorderColor = isDarkMode ? darkModeHoverAion : lightModeHoverPurple; 
                    }
                    else
                    {
                      
                        txt.FillColor = mainBgColor;
                        txt.BorderThickness = 0;
                    }
                }

                if (ctrl is Guna.UI2.WinForms.Guna2ComboBox cmb)
                {
                    cmb.BackColor = mainBgColor;
                    cmb.FillColor = mainBgColor;
                    cmb.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;
                    cmb.BorderColor = isDarkMode ? Color.FromArgb(45, 45, 45) : Color.FromArgb(200, 200, 200);
                    cmb.ItemsAppearance.BackColor = mainBgColor;
                    cmb.ItemsAppearance.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;
                }

                
                if (ctrl is Guna.UI2.WinForms.Guna2Button btn)
                {
                    btn.Animated = true;
                    btn.BorderRadius = 12;
                    btn.UseTransparentBackground = false;
                   
                    
                    btn.HoverState.BorderColor =
                        isDarkMode? darkModeHoverAion : lightModeHoverPurple;


                    Form mainForm = ctrl.FindForm();
                    if (mainForm != null && mainForm.WindowState == FormWindowState.Maximized)
                    {
                        btn.Font = new Font(btn.Font.FontFamily, 11, FontStyle.Regular);
                    }
                    else
                    {
                        btn.Font = new Font(btn.Font.FontFamily, 11, FontStyle.Regular);
                    }

                    if (btn.Name == "btnAppTheme" || btn.Name == "btnApplyDark" || btn.Name == "btnApplyLight")
                    {
                        if (btn.Name == "btnApplyLight")
                            btn.Text = "";
                        else
                            btn.Text = "App theme\nSelect which app theme to display";

                        btn.BackColor = Color.Transparent;
                        btn.FillColor = themeOptionsPanelColor;
                        btn.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;
                        btn.ImageSize = new Size(24, 24);

                        btn.HoverState.FillColor = isDarkMode ? Color.FromArgb(0, 191, 255) : Color.FromArgb(195, 200, 207);
                        btn.HoverState.ForeColor = isDarkMode ? Color.Black : Color.Black;
                    }
                    else if (btn.Parent != null && (btn.Parent.Name == "pnlMenu" || btn.Parent.Name == "guna2Panel2"))
                    {
                        btn.BackColor = menuBgColor;
                        btn.FillColor = menuBgColor;
                        btn.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;

                        btn.HoverState.FillColor = isDarkMode ? darkModeHoverAion : lightModeHoverPurple;
                        btn.HoverState.ForeColor = isDarkMode ? Color.Black : Color.FromArgb(242, 247, 255);
                    }
                    else
                    {
                        btn.BackColor = mainBgColor;

                        if (btn.Name == "btnMenu")
                        {
                            btn.FillColor = mainBgColor;
                            btn.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;
                            btn.HoverState.FillColor = isDarkMode ? darkModeHoverAion : lightModeHoverPurple;
                            btn.HoverState.ForeColor = isDarkMode ? Color.Black : Color.FromArgb(242, 247, 255);
                        }
                        else if (btn.Text.Trim() == "=")
                        {
                            btn.FillColor = Color.FromArgb(0, 191, 255);
                            btn.ForeColor = Color.White;
                            btn.HoverState.FillColor =Color.FromArgb(80, 210, 255);
                            btn.HoverState.ForeColor = Color.FromArgb(242, 247, 255);
                            btn.BorderThickness = 0;
                            btn.FillColor = Color.FromArgb(0, 191, 255);
                        }
                        else
                        {
                            
                            btn.FillColor = isDarkMode ? Color.FromArgb(26, 31, 46): Color.White;
                            btn.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;
                            btn.BorderThickness = 1;
                            btn.BorderColor = isDarkMode
                                ? Color.FromArgb(40, 45, 60)
                                : Color.FromArgb(220, 220, 220);

                           
                            btn.HoverState.FillColor = isDarkMode ? darkModeHoverAion : lightModeHoverPurple;
                            btn.HoverState.ForeColor = isDarkMode ? Color.Black : Color.FromArgb(242, 247, 255);
                        }
                    }
                    if (btn.Name == "btnAbout")
                    {
                        btn.Text = "Calcaion\n© 2026 AION. All Rights Reserved\nVersion 1.0.0 (Genesis)";
                        btn.BackColor = Color.Transparent;
                        btn.FillColor = isDarkMode ? Color.FromArgb(20, 10, 40) : Color.FromArgb(215, 210, 245);
                        btn.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;
                        btn.HoverState.FillColor = isDarkMode ? Color.FromArgb(0, 191, 255) : Color.FromArgb(195, 200, 207);
                        btn.HoverState.ForeColor = isDarkMode ? Color.Black : Color.FromArgb(242, 247, 255);
                    }
                }

                
                
                if (ctrl is Guna.UI2.WinForms.Guna2RadioButton rb)
                {
                    rb.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;
                }

                if (ctrl is Label lbl)
                {
                    lbl.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;
                }

                if (ctrl is Guna.UI2.WinForms.Guna2HtmlLabel gunaLbl)
                {
                    gunaLbl.ForeColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;
                }

                
                
                
                if (ctrl is Guna.UI2.WinForms.Guna2ControlBox controlBox)
                {
                    controlBox.BackColor = mainBgColor;
                    controlBox.FillColor = mainBgColor;
                    controlBox.IconColor = isDarkMode ? Color.FromArgb(242, 247, 255) : Color.Black;

                    if (controlBox.ControlBoxType == Guna.UI2.WinForms.Enums.ControlBoxType.CloseBox)
                    {
                        controlBox.HoverState.FillColor = Color.FromArgb(220, 38, 38); ;
                        controlBox.HoverState.IconColor = Color.White;
                    }
                    else
                    {
                        controlBox.HoverState.FillColor = isDarkMode ? Color.FromArgb(45, 45, 45) : Color.FromArgb(220, 220, 220);
                        controlBox.IconColor = isDarkMode ? Color.FromArgb(242, 247, 255)  : Color.Black;
                    }
                }
                if (ctrl is Guna.UI2.WinForms.Guna2Panel gunaPnl) 
                {
                    if (gunaPnl.Name == "pnlThemeOptions")
                    {
                        gunaPnl.BackColor = Color.Transparent;
                        gunaPnl.FillColor = themeOptionsPanelColor;
                    }
                    else if (gunaPnl.Name == "pnlAbout")
                    {
                        gunaPnl.BackColor = Color.Transparent;
                        gunaPnl.FillColor = isDarkMode ? Color.FromArgb(25, 15, 55) : Color.FromArgb(215, 210, 245);


                    }
                    else if (gunaPnl.Name == "guna2Panel4")
                    {
                        gunaPnl.FillColor = isDarkMode ? Color.FromArgb(0, 191, 255) : Color.FromArgb(0, 191, 255);
                    }
                    else if (gunaPnl.Name == "gunapanel3")
                    {
                        gunaPnl.FillColor = isDarkMode ? Color.FromArgb(24, 28, 38) : Color.FromArgb(248, 250, 255);
                    }
                    else if (gunaPnl.Name == "pnlMenu" || gunaPnl.Name == "guna2Panel2")
                    {
                        gunaPnl.BackColor = Color.Transparent;
                        gunaPnl.FillColor = menuBgColor;
                    }
                    else
                    {
                        gunaPnl.BackColor = Color.Transparent;
                        gunaPnl.FillColor = mainBgColor;
                    }
                }

                
                if (ctrl is TableLayoutPanel tbl)
                {
                    tbl.BackColor = mainBgColor;
                }

                if (ctrl.HasChildren)
                {
                    ApplyTheme(ctrl, isDarkMode);
                }


            }
        }


        private void btnLengthConverter_Click(object sender, EventArgs e)
        {
            guna2Panel1.Controls.Clear();

            ucLength lengthPage = new ucLength();
            lengthPage.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(lengthPage);


            pnlMenu.BringToFront();
            btnMenu.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);
            tmrMenu.Start();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2Panel1.Controls.Clear();

            
            ucStandard standardPage = new ucStandard();
            standardPage.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(standardPage);

           
            pnlMenu.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);
            btnMenu.BringToFront();

            tmrMenu.Start();
        }
        private void FixLayers()
        {
           
            guna2Panel1.SendToBack();

            
            pnlMenu.BringToFront(); 

            
            btnMenu.BringToFront(); 
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2Panel1.Controls.Clear();

            ucTemperature tempPage = new ucTemperature();
            tempPage.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(tempPage);

            pnlMenu.BringToFront();
            btnMenu.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);
            tmrMenu.Start();
        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            guna2Panel1.Controls.Clear();

            ucVolume volumePage = new ucVolume();
            volumePage.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(volumePage);

         
            pnlMenu.BringToFront();
            btnMenu.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);

            tmrMenu.Start();

        }

        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            guna2Panel1.Controls.Clear();

            ucWeight weightPage = new ucWeight();
            weightPage.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(weightPage);

            pnlMenu.BringToFront();
            btnMenu.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);


            tmrMenu.Start();
        }

        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
            guna2Panel1.Controls.Clear();

            ucArea areaPage = new ucArea();
            areaPage.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(areaPage);

        
            pnlMenu.BringToFront();
            btnMenu.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);


            tmrMenu.Start();
        }

        private void guna2Button9_Click_1(object sender, EventArgs e)
        {
            guna2Panel1.Controls.Clear();

            ucTime timePage = new ucTime();
            timePage.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(timePage);

            pnlMenu.BringToFront();
            btnMenu.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);
            tmrMenu.Start();

        }

        private void btnGeometryMenu_Click(object sender, EventArgs e)
        {
          
            guna2Panel1.Controls.Clear();

           
            ucGeometry geometryPage = new ucGeometry();
            geometryPage.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(geometryPage);

            
            pnlMenu.BringToFront();
            btnMenu.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);
            tmrMenu.Start();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
           
            guna2Panel1.Controls.Clear();

           
            ucAngleConverter ucAngle = new ucAngleConverter();

            
            ucAngle.Dock = DockStyle.Fill;

           
            guna2Panel1.Controls.Add(ucAngle);
            ucAngle.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);
            tmrMenu.Start();

        }

        private void btnScientific_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucScientificCalculator());
        }
        private void LoadUserControl(UserControl uc)
        {
            guna2Panel1.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(uc);
            uc.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);
            tmrMenu.Start();
        }

        private void guna2Button10_Click_1(object sender, EventArgs e)
        {
          
            guna2Panel1.Controls.Clear();

            
            ucAngleConverter ucAngle = new ucAngleConverter();

          
            ucAngle.Dock = DockStyle.Fill;

          
            guna2Panel1.Controls.Add(ucAngle);
            ucAngle.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);


            tmrMenu.Start();

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
          
            guna2Panel1.Controls.Clear();

   
            ucSettings settingsControl = new ucSettings();

          
            settingsControl.Dock = DockStyle.Fill;

          
            guna2Panel1.Controls.Add(settingsControl);
            settingsControl.BringToFront();
            ApplyTheme(this, Form1.IsDarkModeSaved);
            tmrMenu.Start();
        }
        public void ApplyGlobalTheme()
        {

        }

        public bool CheckWindowsThemeIsDark()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    if (key != null) return (int)key.GetValue("AppsUseLightTheme") == 0;
                }
            }
            catch { }
            return false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
    }

}
