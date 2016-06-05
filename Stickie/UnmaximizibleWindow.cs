﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Stickie
{
    [TemplatePart(Name = PART_TitleBar, Type = typeof(UIElement))]
    public class UnmaximizibleWindow : Window 
    {
        private const string PART_TitleBar = "PART_TitleBar";

        public override void OnApplyTemplate()
        {
            var titleBar = GetTemplateChild(PART_TitleBar) as UIElement;
            titleBar.PreviewMouseLeftButtonDown += NoResizeGrip;
            base.OnApplyTemplate();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            ResotreResizeGrip();
            base.OnLocationChanged(e);
        }

        private void NoResizeGrip(object sender, MouseButtonEventArgs e)
        {
            if (this.ResizeMode != System.Windows.ResizeMode.NoResize)
            {
                this.ResizeMode = System.Windows.ResizeMode.NoResize;
                this.UpdateLayout();
            }
        }

        private void ResotreResizeGrip()
        {
            if (this.ResizeMode == System.Windows.ResizeMode.NoResize)
            {
                // restore resize grips
                this.ResizeMode = System.Windows.ResizeMode.CanResize;
                this.UpdateLayout();
            }
        }


    }
}
