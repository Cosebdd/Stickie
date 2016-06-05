using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Stickie.WindowBehaviorAppliers
{
    public class WindowOpacityApplier : IWindowBehaviorApplier
    {
        private Window _window;

        public void ApplyBehavior(Window window)
        {
            _window = window;
            _window.AllowsTransparency = true;
            _window.MouseWheel += _window_MouseWheel;
        }

        private void _window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && _window.Opacity <= 1)
                _window.Opacity += 0.05;
            else if (_window.Opacity >= 0.5)
                _window.Opacity -= 0.05;
        }

    }
}
