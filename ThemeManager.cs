using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Calculatornew
{
    public static class ThemeManager
    {
        public enum AppTheme { Light, Dark, System }
        public static AppTheme CurrentTheme { get; private set; } = AppTheme.Light;
        private static AppTheme _userSelection = AppTheme.Light; 

        public static Color BackgroundColor => CurrentTheme == AppTheme.Dark ? Color.FromArgb(32, 32, 32) : Color.WhiteSmoke;
        public static Color TextBoxColor => CurrentTheme == AppTheme.Dark ? Color.FromArgb(45, 45, 45) : Color.White;
        public static Color TextBoxForeColor => CurrentTheme == AppTheme.Dark ? Color.White : Color.Black;
        public static Color ButtonColor => CurrentTheme == AppTheme.Dark ? Color.FromArgb(50, 50, 50) : Color.White;
        public static Color ButtonForeColor => CurrentTheme == AppTheme.Dark ? Color.White : Color.Black;

        public static event Action ThemeChanged;

        
        static ThemeManager()
        {
          
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }

        public static void SetTheme(AppTheme theme)
        {
            _userSelection = theme; 
            UpdateThemeTarget();
        }

        private static void UpdateThemeTarget()
        {
            if (_userSelection == AppTheme.System)
            {
             
                CurrentTheme = IsWindowsDarkMode() ? AppTheme.Dark : AppTheme.Light;
            }
            else
            {
                CurrentTheme = _userSelection;
            }

         
            ThemeChanged?.Invoke();
        }


        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
  
            if (e.Category == UserPreferenceCategory.General || e.Category == UserPreferenceCategory.VisualStyle)
            {
                if (_userSelection == AppTheme.System)
                {
                    UpdateThemeTarget();
                }
            }
        }

        private static bool IsWindowsDarkMode()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("AppsUseLightTheme");
                        if (value != null) return (int)value == 0;
                    }
                }
            }
            catch { }
            return false;
        }
    }
}