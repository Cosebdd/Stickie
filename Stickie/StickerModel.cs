using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stickie
{
    internal class StickerModel
    {
        public string Title;
        public string Text;

        public StickerModel(string title, string text)
        {
            Title = title;
            Text = text;
        }

    }
}
