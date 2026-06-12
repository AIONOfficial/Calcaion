using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;


namespace Calculator
{
    public class theme
    {
        public static Color DarkBackColor = Color.FromArgb(30, 30, 30);
        public static Color LightBackColor = Color.White;

        public static void ApplyTheme(Form form, bool isDark)
        {
            if (isDark)
            {
                form.BackColor = DarkBackColor;
               
            }
            else
            {
                form.BackColor = LightBackColor;
            }
        }
       

    }
}
