using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace RozenMaiden
{
    public class Entity
    {
        #region Fields

        private Texture2D _texture;
        private string _name;
        private Vector2 _position = new Vector2(200, 200);
        private Vector2 _direction;
        private Vector2 _size = new Vector2(32,32);
        private Vector2 _hitboxSize = new Vector2(25,16);
        private float _speed = 1.5f;
        private Vector2 _frame;
        private float _timeStep = 0.10f;

        #endregion

        #region Properties

        /// <summary>
        /// Контейнер для текстуры
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        /// <summary>
        /// Имя сущности 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Позиция
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Размеры
        /// </summary>
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Размеры хитбокса
        /// </summary>
        public Vector2 HitBoxSize
        {
            get { return _hitboxSize; }
            set { _hitboxSize = value; }
		}

        /// <summary>
        /// Ускорение
        /// </summary>
		public Vector2 Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}

        /// <summary>
        /// Шаг анимации
        /// </summary>
        public float TimeStep
        {
            get { return _timeStep; }
            set { _timeStep = value; }
        }

        /// <summary>
        /// Скорость перемещения
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        #endregion

        #region Construct

        public Entity(string filePath)
        {
            try
            {
				this.Texture = Program.Game.Content.Load<Texture2D>(filePath);
            }
            catch (Exception ex)
            {
				string tempError = ex.Message + "\n" + filePath; // формирование строчки с текстом ошибки
                MessageBox.Show(tempError, "Error!");
            }
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Отрисовка сущности
        /// </summary>
        public void Draw()
        {
            Program.Game.SpriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointWrap, null, null,null, Camera.GetTransform());
            Program.Game.SpriteBatch.Draw
			(
			 	this.Texture, 
                this.Position, 
			 	new Rectangle
                (
                    (int)_frame.X * (int)_size.X, 
                    (int)_frame.Y * (int)_size.Y, 
                    (int)_size.X,
                    (int)_size.Y
                ), 
                Color.White,
                0,
                new Vector2(4,16),
                new Vector2(1,1),
                SpriteEffects.None,
                0
			);
            Program.Game.SpriteBatch.End();
        }

        /// <summary>
        /// Обновление логики сущности
        /// </summary>
        public void Update(World world)
        {
            UserEventUpdate();
			AnimationUpdate();                 
            PositionUpdate(world);
        }

        /// <summary>
        /// Обновление коллизий
        /// </summary>
        protected void CollisionUpdate(World world, bool dir)
        {           
            float leftTile = Position.X / world.TileMap.Size.X;
            float rightTile = (Position.X + HitBoxSize.X) / world.TileMap.Size.X;
            float topTile = Position.Y / world.TileMap.Size.Y;
            float bottomTile = (Position.Y + HitBoxSize.Y) / world.TileMap.Size.Y;

            for (int x = (int)leftTile; x < rightTile; x++)
            {
                for (int y = (int)topTile; y < bottomTile; y++)
                {
                    if ((x >= 0 && y >= 0) && (x < world.MapWidth && y < world.MapHeight))
                    {
                        if (world.Map[x, y] != 0)
                        {
                            if (Direction.X > 0 && dir == false)
                            {
                                Position = new Vector2 
                                (
                                    x * world.TileMap.Size.X - HitBoxSize.X, 
                                    Position.Y
                                );
                            }

                            if (Direction.X < 0 && dir == false)
                            {
                                Position = new Vector2
                                (
                                    (x * world.TileMap.Size.X) + world.TileMap.Size.X, 
                                    Position.Y
                                );
                            }

                            if (Direction.Y > 0 && dir == true)
                            {
                                Position = new Vector2 
                                (
                                    Position.X,
                                    y * world.TileMap.Size.Y - HitBoxSize.Y 
                                );
                            }

                            if (Direction.Y < 0 && dir == true)
                            {
                                Position = new Vector2 
                                (
                                    Position.X,
                                    (y * world.TileMap.Size.Y) + world.TileMap.Size.Y
                                );
                            }
                        }
                    }
                }
            }
        }

        #endregion

		#region ProtectedMethods


		/// <summary>
		/// Обновление позиции
		/// </summary>
        protected void PositionUpdate(World world)
		{
            Position = new Vector2
            (
                Position.X + Direction.X, 
                Position.Y + Direction.Y
            );

            CollisionUpdate(world, true);
            CollisionUpdate(world, false);

            Direction = new Vector2(0, 0);
		}

		/// <summary>
		/// Обновление анимации
		/// </summary>
		protected void AnimationUpdate()
		{
			if (Direction.Y < 0){ _frame.Y = 3; }
			if (Direction.Y > 0) { _frame.Y = 0; }
			if (Direction.X < 0) { _frame.Y = 1; }
			if (Direction.X > 0) { _frame.Y = 2; }
			if (Direction.Y != 0 || Direction.X !=0)
			{
				_frame.X += TimeStep;
				if (_frame.X >= 4) { _frame.X = 0; }
			} else
			{
				_frame.X = 1;
			}
		}
            
        /// <summary>
        /// Пользовательские события
        /// </summary>
        protected virtual void UserEventUpdate()
        {

        }
		#endregion
    }
}