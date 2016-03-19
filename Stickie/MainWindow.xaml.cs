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
        private User32KeyboardListener _listener;
        private IntPtr _hwnd;

        public MainWindow()
        {
            InitializeComponent();
            this.AllowsTransparency = true;
            _listener = new User32KeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;
            _listener.OnKeyReleased += _listener_OnKeyReleased;
            _listener.HookKeyboard();

        }

        void _listener_OnKeyReleased(object sender, KeyArgs e)
        {
            if (!this.IsActive)
            {
                if (e.KeyPressed == Key.LeftCtrl || e.KeyPressed == Key.RightCtrl)
                {
                    User32WindowHandler.SetWindowExTransparent(_hwnd);
                }
            }
        }

        void _listener_OnKeyPressed(object sender, KeyArgs e)
        {
            if (!this.IsActive)
            {
                if (e.KeyPressed == Key.LeftCtrl || e.KeyPressed == Key.RightCtrl)
                {
                    User32WindowHandler.ResetWindowExTransparent(_hwnd);
                }
            }
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
            _hwnd = new WindowInteropHelper(this).Handle;
            User32WindowHandler.TurnOffMaximizing(_hwnd);
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            User32WindowHandler.ResetWindowExTransparent(_hwnd);
        }

        private void MetroWindow_Deactivated(object sender, EventArgs e)
        {
            User32WindowHandler.SetWindowExTransparent(_hwnd);
        }


    }
}
