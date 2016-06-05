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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            List<IWindowBehaviorApplier> BehaviorList = new List<IWindowBehaviorApplier>();
            BehaviorList.Add(new WindowNoMaximizeOnAeroSnapApplier());
            BehaviorList.Add(new WindowOpacityApplier());
            BehaviorList.Add(new WindowClickThroughApplier());
            foreach (IWindowBehaviorApplier Behavior in BehaviorList)
                Behavior.ApplyBehavior(this);

            InitializeComponent();
            var viewModel = new StickerViewModel();
            DataContext = viewModel;
        }

    }
}
