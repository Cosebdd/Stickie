using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Stickie.Controls
{
    internal class TitleTextBox : TextBox
    {
        

        public TitleTextBox()
        {
            DisableControl(this, null);
            MouseDoubleClick += ActivateControl;
            LostFocus += DisableControl;
        }


        private void ActivateControl(object sender, EventArgs e)
        {
            Cursor = Cursors.IBeam;
            IsReadOnly = false;
            IsReadOnlyCaretVisible = true;
            SelectionOpacity = 0.5;
        }

        private void DisableControl(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
            IsReadOnly = true;
            IsReadOnlyCaretVisible = false;
            SelectionOpacity = 0;
        }


    }
}
