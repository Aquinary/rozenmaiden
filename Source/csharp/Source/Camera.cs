using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace RozenMaiden
{
    static class Camera
    {
        #region Fields

        private static Matrix _transform;
        private static float _zoom = 2.0f;
        private static float _rotation = 0;
        private static Vector2 _position = new Vector2(0,0);
        private static float _speed = 7f;

        #endregion

        #region Properties

        public static float Zoom
        {
            get { return _zoom; }
            set { 
                    _zoom = value; 
                    if (_zoom < 0.1f) 
                    { _zoom = 0.1f; } 
                }
        }

        public static float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public static Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        #endregion

        #region Construct

        public static Matrix GetTransform()
        {
            _transform = Matrix.CreateTranslation
            (
                new Vector3(_position.X, _position.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(0, 0, 0)
            );
            
            return _transform;
        }

        #endregion

        #region PublicMethods

        public static void Update()
        {
            KeyboardState key = Keyboard.GetState();
            Vector2 tempDirection = _position; 

            if (key.IsKeyDown(Keys.Up))
            {
                tempDirection.Y += _speed;
            }

            if (key.IsKeyDown(Keys.Left))
            {
                tempDirection.X += _speed;
            }

            if (key.IsKeyDown(Keys.Down))
            {       
                tempDirection.Y -= _speed;
            }

            if (key.IsKeyDown(Keys.Right))
            {
                tempDirection.X -= _speed;
            }

            _position = tempDirection;
        }

        #endregion

    }
}

