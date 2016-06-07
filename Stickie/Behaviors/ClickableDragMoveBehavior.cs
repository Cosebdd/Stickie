using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Stickie.Behaviors
{
    class ClickableDragMoveBehavior : Behavior<UIElement>
    {
        Point _startPoint = new Point();

        private Window _parentWindow
        {
            get { return Window.GetWindow(AssociatedObject); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseMove += UIElementPreviewMouseMove;
            AssociatedObject.PreviewMouseLeftButtonDown += UIElementOnPreviewMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewMouseMove -= UIElementPreviewMouseMove;
            AssociatedObject.PreviewMouseLeftButtonDown -= UIElementOnPreviewMouseLeftButtonDown;
        }

        private void UIElementPreviewMouseMove(object sender, MouseEventArgs e)
        {
            var control = (IInputElement)sender;
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
                if (_parentWindow != null)
                    _parentWindow.DragMove();

            }
        }

        private void UIElementOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition((IInputElement)sender);
        }
    }
}
