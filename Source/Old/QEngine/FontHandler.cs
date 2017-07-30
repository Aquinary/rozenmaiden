using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice
{
    class FontHandler
    {
        public SpriteFont font;
        public FontHandler(Microsoft.Xna.Framework.Content.ContentManager content, string path_to_texture)
        {
            font = content.Load<SpriteFont>(path_to_texture);
        }
    }
}
