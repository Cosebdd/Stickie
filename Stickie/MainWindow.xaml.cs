using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
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
            InitializeComponent();
            var viewModel = new StickerViewModel();
            DataContext = viewModel;
        }

    }
}
