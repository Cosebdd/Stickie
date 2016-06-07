using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Interop;

namespace Stickie.Behaviors
{
    class WindowClickThroughBehavior : Behavior<Window>
    {
        private Window _window;

        private IntPtr _hwnd;

        protected override void OnAttached()
        {
            base.OnAttached();
            _window = AssociatedObject;
            _window.SourceInitialized += _window_SourceInitialized;
            _window.Activated += _window_Activated;
            _window.Deactivated += _window_Deactivated;
            HookKeyboard();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            _window.SourceInitialized -= _window_SourceInitialized;
            _window.Activated -= _window_Activated;
            _window.Deactivated -= _window_Deactivated;
        }

        private void _window_SourceInitialized(object sender, EventArgs e)
        {
            _hwnd = new WindowInteropHelper(_window).Handle;
        }

        private void _window_Deactivated(object sender, EventArgs e)
        {
            User32WindowHandler.SetWindowExTransparent(_hwnd);
        }

        private void _window_Activated(object sender, EventArgs e)
        {
            User32WindowHandler.ResetWindowExTransparent(_hwnd);
        }

        private void HookKeyboard()
        {
            var listener = new User32KeyboardListener();
            listener.OnKeyPressed += listener_OnKeyPressed;
            listener.OnKeyReleased += listener_OnKeyReleased;
            listener.HookKeyboard();
            _window.Closed += (sender, args) => { listener.UnHookKeyboard(); };
        }

        private void listener_OnKeyReleased(object sender, KeyArgs e)
        {
            if (_window.IsActive) return;
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                User32WindowHandler.SetWindowExTransparent(_hwnd);
            }
        }

        private void listener_OnKeyPressed(object sender, KeyArgs e)
        {
            if (_window.IsActive) return;
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                User32WindowHandler.ResetWindowExTransparent(_hwnd);
            }
        }
    }
}
