using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using MahApps.Metro.Controls;

namespace Stickie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AllowsTransparency = true;
        }

        private void MetroWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && this.Opacity <= 1)
                this.Opacity += 0.05;
            else if (this.Opacity >= 0.5)
                this.Opacity -= 0.05;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.TurnOffMaximizing(hwnd);
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.ResetWindowExTransparent(hwnd);
        }

        private void MetroWindow_Deactivated(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }

    }
}
