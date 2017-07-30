using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Alice
{
    /// <summary>
    /// Класс, описывающий персонажей
    /// </summary>
    public static class Entity 
    {
        // Массив сущностей
        public static EntityHandler[] ArrayEntity = new EntityHandler[1];
  
        public static void CreateEntity(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            /* Сдесь должны быть прописаны все нужные сущности */
            ArrayEntity[0] = new EntityHandler(content ,@"player\\Suiginto", 10, 5, 32, 16, 16,16, 0.08f);
            ArrayEntity[0].Name = "Suiginto";
        }
    }
}
