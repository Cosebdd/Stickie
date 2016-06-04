using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Stickie.WindowBehaviorAppliers
{
    public class WindowNoMaximizeOnAeroSnapApplier : IWindowBehaviorApplier
    {
        private Window _window;

        private void OnLocationChanged(object sender, EventArgs e)
        {
            ResotreResizeGrip();
        }

        private void NoResizeGrip()
        {
            if (_window.ResizeMode != System.Windows.ResizeMode.NoResize)
            {
                _window.ResizeMode = System.Windows.ResizeMode.NoResize;
                _window.UpdateLayout();
            }
        }

        private void ResotreResizeGrip()
        {
            if (_window.ResizeMode == System.Windows.ResizeMode.NoResize)
            {
                // restore resize grips
                _window.ResizeMode = System.Windows.ResizeMode.CanResize;
                _window.UpdateLayout();
            }
        }

        public void ApplyBehavior(Window window)
        {
            _window = window;
            // TO DO: handle TitleBar Somehow
            _window.LocationChanged += OnLocationChanged;
        }
    }
}
