using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Stickie.WindowBehaviorAppliers
{
    class WindowNoMaximizeOnAeroSnapApplier : IWindowBehaviorApplier
    {
        private Window _window;

        private void OnLocationChanged(object sender, EventArgs e)
        {
            //ResotreResizeGrip();
        }



        public void ApplyBehavior(Window window)
        {
            _window = window;
            // TO DO: handle TitleBar Somehow
            //_window.LocationChanged += OnLocationChanged;
        }

    }
}
