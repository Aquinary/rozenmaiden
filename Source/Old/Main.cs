using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Alice
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        // Граф. девайс
        GraphicsDeviceManager graphics;
        // Хранилище под спрайты                                            
        SpriteBatch spriteBatch;
        // Таймер-переменная для стаблизации FPS                                                         
        TimerHandler Timer;                                                        
        // Мир
        MapHandler World = new MapHandler(70, 70, 7);
        // Рендер-текстура
        RenderTarget2D Render;
        // Определяет отображаемые размеры в Render2D
        Rectangle RenderArea;
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            // Задаем размеры приложения
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            this.IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = false;
            graphics.ApplyChanges();
            this.Window.Position = new Point(0, 0);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Создаем объекты
            // Без этого работать не будет, так как не подгрузятся ресурсы
            // Сущности
            Entity.CreateEntity(Content);
            // Спрайты для местности
            Tile.CreateTile(Content);
            // Шрифты
            Font.Create(Content);
            // GUI элементы
            GUI.Create(Content);
            // Определяем свойства камеры
            CameraHandler.Position = new Vector2(0f, 0f);
            CameraHandler.Rotation = 0f;
            CameraHandler.Zoom = 1.0f;
            // Определяем размеры зоны отображения
            RenderArea = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            // Таймер для стабилизации FPS
            Timer = new TimerHandler();
            this.IsMouseVisible = true;
            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Подгружаем уровень из внешенго файла
            World.Read("level\\template_floor1.csv",0);
            World.Read("level\\template_floor2.csv", 2);
            World.Read("level\\template_floor3.csv", 3);
            World.Read("level\\template_hitbox.csv", 4);
            World.Read("level\\template_hight.csv", 5);
            World.Read("level\\template_actor.csv", 6);
            Render = new RenderTarget2D(GraphicsDevice, RenderArea.Width, RenderArea.Height);
        }

      
        protected override void UnloadContent()
        {
            Render.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            float time = Timer.GetMilliseconds();
            Timer.Reset();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            int speed = 10;
            KeyboardState key = new KeyboardState();
            key = Keyboard.GetState();
            /* Движение камеры */
            if (key.IsKeyDown(Keys.W)) { CameraHandler.Position.Y += speed; }
            if (key.IsKeyDown(Keys.S)) { CameraHandler.Position.Y -= speed; }
            if (key.IsKeyDown(Keys.A)) { CameraHandler.Position.X += speed; }
            if (key.IsKeyDown(Keys.D)) { CameraHandler.Position.X -= speed; }
           
            Entity.ArrayEntity[0].Walk = false;
            /* Управление сущностью */
            if (key.IsKeyDown(Keys.Up))
            {
                Entity.ArrayEntity[0].Direction.Y = -Entity.ArrayEntity[0].Speed;
                Entity.ArrayEntity[0].SetDirection(EntityHandler.Type.DirUp);
               // Entity.ArrayEntity[0].Walk = true;
            }
            if (key.IsKeyDown(Keys.Down))
            {
                Entity.ArrayEntity[0].Direction.Y = Entity.ArrayEntity[0].Speed;
                Entity.ArrayEntity[0].SetDirection(EntityHandler.Type.DirDown);
            }
            if (key.IsKeyDown(Keys.Left))
            {
                Entity.ArrayEntity[0].Direction.X = -Entity.ArrayEntity[0].Speed;
                Entity.ArrayEntity[0].SetDirection(EntityHandler.Type.DirLeft);
            }
            if (key.IsKeyDown(Keys.Right))
            {
                Entity.ArrayEntity[0].Direction.X = Entity.ArrayEntity[0].Speed;
                Entity.ArrayEntity[0].SetDirection(EntityHandler.Type.DirRight);
            }
            // Проверка коллизий для всех сущностей 
            // TODO: но они могут проходит друг через друга. Исправить.
            foreach (EntityHandler e in Entity.ArrayEntity)
            {
                // проверяем
                e.Collision(World, time);
            }
              GUI.Update();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            // Если кликаем на кнопку "Новая игра"...
            if (GUI.Button[0].click) { SceneHandler.Active = 1; } //...активируем сцену 1, на которой происходит отрисовка
            // Кнопка "В меню"
            if (GUI.Button[1].click) { SceneHandler.Active = 0; }//...активируем сцену с меню

            switch (SceneHandler.Active)
            {
                case 0:
                    GraphicsDevice.SetRenderTarget(Render);
                    GraphicsDevice.Clear(Color.Transparent);
                    GUI.Button[0].Draw(Window, spriteBatch);
                    GraphicsDevice.SetRenderTarget(null);
                    spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, null);
                    // Рисуем рендер-текстуру
                    spriteBatch.Draw((Texture2D)Render, new Rectangle(0, 0, Window.ClientBounds.Width * CameraHandler.resize, Window.ClientBounds.Height * CameraHandler.resize), Color.White);

                    spriteBatch.End();
                    break;
                case 1:
                    //отрисовка того-то
                    GraphicsDevice.SetRenderTarget(Render);
                    GraphicsDevice.Clear(Color.Transparent);
                    //Рисуем в текстуру
                    // В качестве параметра передаем набор сушностей
                    World.Draw(Window, spriteBatch, Tile.ArrayTile[0], Entity.ArrayEntity);
                    GUI.Button[1].Draw(Window, spriteBatch);
                    GraphicsDevice.SetRenderTarget(null);
                    // Переключаемя на нормальный режим
                    spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, null);
                    // Рисуем рендер-текстуру
                    spriteBatch.Draw((Texture2D)Render, new Rectangle(0, 0, Window.ClientBounds.Width * CameraHandler.resize, Window.ClientBounds.Height * CameraHandler.resize), Color.White);
                    spriteBatch.End();
                    break;
                default:
                    break;
            }
            // Переключаемся на другой рендер
           
            
        }

    }
}
