using System.ComponentModel;
using System.Runtime.CompilerServices;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
