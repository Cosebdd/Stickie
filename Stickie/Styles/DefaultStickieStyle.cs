using Stickie.WindowBehaviorAppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Stickie.Styles
{
    internal static class LocalExtensions
    {
        public static Window GetWindowFromTemplate(this object templateFrameworkElement)
        {
            Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
            if (window != null)
                return window;
            else
                return null;
        }

        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
            if (window != null) action(window);
        }

        public static IntPtr GetWindowHandle(this Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            return helper.Handle;
        }
    }

    public partial class DefaultStickieStyle
    {
        private Point _titleBarDragStartPoint = new Point();

        void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => SystemCommands.CloseWindow(w));
        }

        void TitleBarLMDownPreview(object sender, MouseEventArgs e)
        {
            var control = (IInputElement)sender;
            _titleBarDragStartPoint = e.GetPosition((IInputElement)sender);
        }

        void TitleBarLMDown(object sender, MouseEventArgs e)
        {
            var control = (IInputElement)sender;
            control.CaptureMouse();
        }

        void TitleBarMouseMovePreview(object sender, MouseEventArgs e)
        {
            var control = (IInputElement)sender;
            var currentPoint = e.GetPosition(control);
            if (e.LeftButton == MouseButtonState.Pressed &&
                control.IsMouseCaptured &&
                (Math.Abs(currentPoint.X - _titleBarDragStartPoint.X) >
                 SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(currentPoint.Y - _titleBarDragStartPoint.Y) >
                 SystemParameters.MinimumVerticalDragDistance))
            {
                // Prevent Click from firing
                control.ReleaseMouseCapture();
                sender.ForWindowFromTemplate(w => w.DragMove());
            }
        }

        void TitleBarLMUpPreview(object sender, MouseEventArgs e)
        {
            var control = (IInputElement)sender;
            if (control.IsMouseCaptured)
                control.ReleaseMouseCapture();
        }

        private void NoResizeGrip(object sender, RoutedEventArgs e)
        {
            Window window = sender.GetWindowFromTemplate();

            if (window.ResizeMode != System.Windows.ResizeMode.NoResize)
            {
                window.ResizeMode = System.Windows.ResizeMode.NoResize;
                window.UpdateLayout();
            }
        }

        private void ResotreResizeGrip(object sender, RoutedEventArgs e)
        {
            Window window = sender.GetWindowFromTemplate();

            if (window.ResizeMode == System.Windows.ResizeMode.NoResize)
            {
                window.ResizeMode = System.Windows.ResizeMode.CanResize;
                window.UpdateLayout();
            }
        }
    }
}
