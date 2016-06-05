using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Stickie.Controls
{
    internal class TitleTextBox : TextBox
    {
        private Point _startPoint;

        private Window _parentWindow {
            get { return Window.GetWindow(this); }
        }

        public TitleTextBox()
        {
            DisableControl(this, null);
            _startPoint = new Point();
            MouseDoubleClick += ActivateControl;
            LostFocus += DisableControl;
            PreviewMouseMove += TitleTextBox_PreviewMouseMove;
            PreviewMouseLeftButtonDown += TitleBox_OnPreviewMouseLeftButtonDown;
        }

        private void TitleTextBox_PreviewMouseMove(object sender, MouseEventArgs e)
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


        private void ActivateControl(object sender, EventArgs e)
        {
            Cursor = Cursors.IBeam;
            IsReadOnly = false;
            IsReadOnlyCaretVisible = true;
            SelectionOpacity = 0.5;
        }

        private void DisableControl(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
            IsReadOnly = true;
            IsReadOnlyCaretVisible = false;
            SelectionOpacity = 0;
        }

        private void TitleBox_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition((IInputElement)sender);
        }

    }
}
