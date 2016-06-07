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

        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
            if (window != null) action(window);
        }

    }

    public partial class DefaultStickieStyle
    {
        void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => SystemCommands.CloseWindow(w));
        }

    }
}
