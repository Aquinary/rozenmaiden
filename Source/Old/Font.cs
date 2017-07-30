using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice
{
    class Font
    {
        // Шрифты
        public static FontHandler[] Array = new FontHandler[1];

        public static void Create(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            /* Сдесь должны быть прописаны все нужные шрифты*/
            // Основной шрифт
            Array[0] = new FontHandler(content, "fonts\\font");
        }
    }
}
