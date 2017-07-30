using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice
{
    public class EntityHandler
    {
        float frame_x = 0;
        public string Name { get; set; }                                        // Виртуальное имя сущности              
        public float Speed { get; set; }                                        // Скорость сущности
        public Vector2 Position = Vector2.Zero;                                 // Позиция сущности 
        public Vector2 Direction = Vector2.Zero;                                // Ускорение сущности
        public Vector2 HitBox = Vector2.Zero;                                   // Размеры сущности
        public Vector2 Size = Vector2.Zero;
        public Texture2D Texture { get; set; }                                  // Текстура сущности
        /* Определение направления персонажа для покадровой анимации */
        public bool DirUp { get; set; }
        public bool DirDown { get; set; }
        public bool DirLeft { get; set; }
        public bool DirRight { get; set; }
        public bool Walk { get; set; }
        // Массив сущностей
        static EntityHandler[] ArrayEntity = new EntityHandler[1024];
        // Перечесление под направление движения
        public enum Type : int
        {
            DirUp = 0,
            DirDown = 1,
            DirLeft = 2,
            DirRight = 3,
        }
        public EntityHandler(Microsoft.Xna.Framework.Content.ContentManager content, string path_to_file, int position_x, int position_y, int width, int height, int hwidth, int hheight, float speed)
        {
            // Установка контекста текстуры
            Texture = content.Load<Texture2D>(path_to_file);
            // Устанавливаем позицию сущности. Множим ее на ширину и высоту ячеек карты и получаем нужное место
            this.Position = new Vector2(position_x * TileHandler.TileWidth, position_y * TileHandler.TileHeight);
            // Устанавливаем скорость
            this.Speed = speed;
            // Устанавливаем хитбокс
            this.HitBox = new Vector2(hwidth,hheight );
            this.Size = new Vector2(width, height);
        }

        public void Draw(GameWindow window, SpriteBatch spriteBatch)
        {
           
            int frame_y = 0;
            
            if (DirUp){ frame_y = 3; }
            if (DirDown) { frame_y = 0; }
            if (DirLeft) { frame_y = 1; }
            if (DirRight) { frame_y = 2; }
            if (Walk)
            {
                frame_x += 0.07f;
                if (frame_x >= 4) { frame_x = 0; }
            } else
            {
                frame_x = 1;
            }

            // Отрисовка сущности
            spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y-16), new Rectangle(32 * (int)frame_x, 32 * frame_y, 32, 32), Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
        }
        
        public void Collision(MapHandler world, float time)
        {
            // Осуществляем проверку коллизий
            Position.X = Position.X + Direction.X * time;
            _Collision(world, false);
            Position.Y = Position.Y + Direction.Y * time;
            _Collision(world, true);
            // Обнуляем ускорение по осям
            Direction.X = 0;
            Direction.Y = 0;
        }

        private void _Collision(MapHandler world, bool dir)
        {
            // Проверяем тайлы, находящиеся непосредственно рядом с сущностью
            for (int x = (int)Position.X / TileHandler.TileWidth; x < (Position.X + HitBox.X) / TileHandler.TileWidth; x++)
            {
                for (int y = (int)Position.Y / TileHandler.TileHeight; y < (Position.Y + HitBox.Y) / TileHandler.TileHeight; y++)
                {
                    // Проверка на коллижены осуществляется только в рамках мира
                    if ((x >= 0 && y >= 0) && (x < world.MapWidth && y < world.MapHeight))
                    {
                        // проверяем z = 4, т.е. слой с хитбоксами
                        if (world.Map[x, y, 4] != 0)
                        {
                            if (Direction.X > 0 && dir == false)
                            {
                                Position.X = x * TileHandler.TileWidth - HitBox.X;
                            }
                            if (Direction.X < 0 && dir == false)
                            {
                                Position.X = x * TileHandler.TileWidth + TileHandler.TileWidth;
                            }
                            if (Direction.Y > 0 && dir == true)
                            {
                                Position.Y = y * TileHandler.TileWidth - HitBox.Y;
                            }
                            if (Direction.Y < 0 && dir == true)
                            {
                                Position.Y = y * TileHandler.TileWidth + HitBox.Y;
                            }
                        }
                    }
                }
            }
        }

        public void SetDirection(EntityHandler.Type direction)
        { 
            switch (direction)
            {
                case Type.DirUp:
                    DirUp = true;
                    DirDown = false;
                    DirLeft = false;
                    DirRight = false;
                    Walk = true;
                    break;
                case Type.DirDown:
                    DirUp = false;
                    DirDown = true;
                    DirLeft = false;
                    DirRight = false;
                    Walk = true;
                    break;
                case Type.DirLeft:
                    DirUp = false;
                    DirDown = false;
                    DirLeft = true;
                    DirRight = false;
                    Walk = true;
                    break;
                case Type.DirRight:
                    DirUp = false;
                    DirDown = false;
                    DirLeft = false;
                    DirRight = true;
                    Walk = true;
                    break;
                default:
                    Walk = false;
                    break;
            }
        }

        public static void CreateEntity(int id, string name, int x, int y, int w, int h, string file)
        {
            
        }
    }
}
