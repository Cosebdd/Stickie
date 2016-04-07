using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Stickie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UnmaximizibleWindow
    {
        private IntPtr _hwnd;

        private Point _startPoint;

        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new StickerViewModel();
            DataContext = viewModel;
            _startPoint = new Point();
            
            AllowsTransparency = true;
            var listener = new User32KeyboardListener();
            listener.OnKeyPressed += listener_OnKeyPressed;
            listener.OnKeyReleased += listener_OnKeyReleased;   
            listener.HookKeyboard();
            Closed += (sender, args) => { listener.UnHookKeyboard(); };
        }

        private void listener_OnKeyReleased(object sender, KeyArgs e)
        {
            if (IsActive) return;
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                User32WindowHandler.SetWindowExTransparent(_hwnd);
            }
        }

        private void listener_OnKeyPressed(object sender, KeyArgs e)
        {
            if (IsActive) return;
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                User32WindowHandler.ResetWindowExTransparent(_hwnd);
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
            
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            User32WindowHandler.ResetWindowExTransparent(_hwnd);
        }

        private void MetroWindow_Deactivated(object sender, EventArgs e)
        {
            User32WindowHandler.SetWindowExTransparent(_hwnd);
            
        }

        private void TitleBox_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            var control = (IInputElement) sender;
            var currentPoint = e.GetPosition(control);
            if (e.LeftButton == MouseButtonState.Pressed &&
                control.IsMouseCaptured &&
                (Math.Abs(currentPoint.X - _startPoint.X) >
                 SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(currentPoint.Y - _startPoint.Y) >
                 SystemParameters.MinimumVerticalDragDistance))
            {
                // Prevent Click from firing
                control.ReleaseMouseCapture();
                DragMove();
            }
        }

        private void TitleBox_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition((IInputElement)sender);
        }
    }
}
