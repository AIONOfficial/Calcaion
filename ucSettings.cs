using Calculator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculatornew
{
    public partial class ucSettings : UserControl
    {
        public ucSettings()
        {
            InitializeComponent();


        }
        private void ucSettings_Load(object sender, EventArgs e)
        {
          
            if (Form1.IsSystemThemeSelected)
            {
                rbSystem.Checked = true;
            }
            else if (Form1.IsDarkModeSaved)
            {
                rbDark.Checked = true;
            }
            else
            {
                rbLight.Checked = true;
            }
        }

        private void btnAppTheme_Click(object sender, EventArgs e)
        {
          
            pnlThemeOptions.Visible = !pnlThemeOptions.Visible;

          
            if (pnlThemeOptions.Visible)
            {
                btnAppTheme.CustomImages.Image = Properties.Resources.chevron_up;
            }
            else
            {
              
                btnAppTheme.CustomImages.Image = Properties.Resources.chevron_down;
            }
        }
        private void rbTheme_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2RadioButton rb && rb.Checked)
            {
                if (this.ParentForm is Form1 mainForm)
                {
                    bool targetThemeIsDark = false;

                    if (rb == rbLight)
                    {
                        Form1.IsDarkModeSaved = false;
                        Form1.IsSystemThemeSelected = false;
                        targetThemeIsDark = false;
                    }
                    else if (rb == rbDark)
                    {
                        Form1.IsDarkModeSaved = true;
                        Form1.IsSystemThemeSelected = false;
                        targetThemeIsDark = true;
                    }
                    else if (rb == rbSystem)
                    {
                        Form1.IsSystemThemeSelected = true;
                        bool currentSystemTheme = mainForm.CheckWindowsThemeIsDark();
                        Form1.IsDarkModeSaved = currentSystemTheme;
                        targetThemeIsDark = currentSystemTheme;
                    }

                    
                    mainForm.ApplyTheme(mainForm, targetThemeIsDark);

                    
                    foreach (Form openForm in Application.OpenForms)
                    {
                    
                        UpdateAllUserControls(openForm, targetThemeIsDark);
                    }
                }
            }
        }

       
        private void UpdateAllUserControls(Control parent, bool isDark)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is UserControl uc)
                {
                    if (this.ParentForm is Form1 mainForm)
                    {
                        mainForm.ApplyTheme(uc, isDark);
                    }
                }

                if (ctrl.HasChildren)
                {
                    UpdateAllUserControls(ctrl, isDark);
                }
            }



        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnGithub_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://github.com/AIONOfficial",
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("Unable to open the browser. Please visit manually:\nhttps://github.com/AIONOfficial", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGmail_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "mailto:feedback.aion@gmail.com?subject=Calcaion Feedback",
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("No default email client found. Please contact us at:\nfeedback.aion@gmail.com", "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
          
            pnlAbout.Visible = !pnlAbout.Visible;

            
            if (pnlAbout.Visible)

            {


                btnAbout.CustomImages.Image = Properties.Resources.chevron_up; 
            }
            else
            {
                btnAbout.CustomImages.Image = Properties.Resources.chevron_down; 
            }
        }

        private async void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

            string currentVersion = "1.0.0";

           
            string url = "https://raw.githubusercontent.com/AIONOfficial/Calcaion-Update/refs/heads/main/update.txt";

            btnCheckUpdate.Text = "Checking...";
            btnCheckUpdate.Enabled = false;

            try
            {
                using (HttpClient client = new HttpClient())
                {
               
                    client.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue { NoCache = true };
                    client.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.UtcNow;
                    string latestVersion = await client.GetStringAsync(url);
                    latestVersion = latestVersion.Trim();

                    if (latestVersion != currentVersion)
                    {
                        DialogResult result = MessageBox.Show(
                            $"A new update ({latestVersion}) is available!\nDo you want to download it now?",
                            "Update Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = "https://github.com/AIONOfficial/Calcaion/releases",
                                UseShellExecute = true
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show("You are using the latest version of Calcaion.", "Up to Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
              
                MessageBox.Show($"Debug Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnCheckUpdate.Text = "Check for Updates";
                btnCheckUpdate.Enabled = true;
            }
        }
    }
}

