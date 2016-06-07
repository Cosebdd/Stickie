using System;
using System.Windows;


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
