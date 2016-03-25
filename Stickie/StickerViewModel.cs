using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Stickie.Annotations;

namespace Stickie
{
    internal class StickerViewModel : INotifyPropertyChanged
    {
        private readonly StickerModel _stickerModel = new StickerModel("NEW NOTE", "...");

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Title
        {
            get { return _stickerModel.Title; }
            set
            {
                if (value == Title) return;
                _stickerModel.Title = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return _stickerModel.Text; }
            set
            {
                if (value == _stickerModel.Text) return;
                _stickerModel.Text = value;
                OnPropertyChanged();
            }
        }
    }
}
