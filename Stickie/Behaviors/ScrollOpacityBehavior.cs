using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Stickie.Behaviors
{
    class ScrollOpacityBehavior : Behavior<UIElement>
    {
        private const double _minOpacity = 0.45;
        private const double _opacityDelta = 0.05;
        private const double _maxOpacity = 0.95;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseWheel += UIElement_MouseWheel;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseWheel -= UIElement_MouseWheel;
        }

        private void UIElement_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && AssociatedObject.Opacity < _maxOpacity)
                AssociatedObject.Opacity += _opacityDelta;
            else if (e.Delta < 0 && AssociatedObject.Opacity > _minOpacity)
                AssociatedObject.Opacity -= _opacityDelta;
        }
    }
}
