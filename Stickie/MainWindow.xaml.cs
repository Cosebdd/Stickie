using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Stickie.WindowBehaviorAppliers;
using System.Collections.Generic;

namespace Stickie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IntPtr _hwnd;

        private Point _startPoint;

        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new StickerViewModel();
            DataContext = viewModel;
            _startPoint = new Point();

            List<IWindowBehaviorApplier> BehaviorList = new List<IWindowBehaviorApplier>();
            BehaviorList.Add(new WindowNoMaximizeOnAeroSnapApplier());
            BehaviorList.Add(new WindowOpacityApplier());
            BehaviorList.Add(new WindowClickThroughApplier());
            foreach (IWindowBehaviorApplier Behavior in BehaviorList)
                Behavior.ApplyBehavior(this);
        }



        private void TitleBox_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            var control = (IInputElement) sender;
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
                DragMove();
            }
        }

        private void TitleBox_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition((IInputElement)sender);
        }
    }
}
