using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK3_StandardTemplate_WinForms
{
    public class TextureFromBMP
    {
        public int id;
        public int texWidth;
        public int texHeight;

        public TextureFromBMP(int _id, int _texWidth, int _texHeight)
        {
            id = _id;
            texWidth = _texWidth;
            texHeight = _texHeight;
        }

    }
}
