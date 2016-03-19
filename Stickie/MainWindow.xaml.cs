using System;
using System.Windows.Input;
using System.Windows.Interop;

namespace Stickie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        private IntPtr _hwnd;

        public MainWindow()
        {
            InitializeComponent();
            AllowsTransparency = true;
            var listener = new User32KeyboardListener();
            listener.OnKeyPressed += _listener_OnKeyPressed;
            listener.OnKeyReleased += _listener_OnKeyReleased;
            listener.HookKeyboard();
            Closed += (sender, args) => { listener.UnHookKeyboard(); };
        }

        void _listener_OnKeyReleased(object sender, KeyArgs e)
        {
            if (!IsActive)
            {
                if (e.KeyPressed == Key.LeftCtrl || e.KeyPressed == Key.RightCtrl)
                {
                    User32WindowHandler.SetWindowExTransparent(_hwnd);
                }
            }
        }

        void _listener_OnKeyPressed(object sender, KeyArgs e)
        {
            if (!IsActive)
            {
                if (e.KeyPressed == Key.LeftCtrl || e.KeyPressed == Key.RightCtrl)
                {
                    User32WindowHandler.ResetWindowExTransparent(_hwnd);
                }
            }
        }

        

        private void MetroWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && Opacity <= 1)
                Opacity += 0.05;
            else if (Opacity >= 0.5)
                Opacity -= 0.05;
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
