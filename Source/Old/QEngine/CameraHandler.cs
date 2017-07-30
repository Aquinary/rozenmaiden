using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alice
{
    static class CameraHandler
    {
        private static Matrix transform;
        private static float zoom;
        private static float rotation;
        // Временная переменная, которая определяет дополнительное зумирование 
        public static int resize = 3;
        public static Vector2 Position;
        public static float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) { zoom = 0.1f; } }
        }

        public static float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }


        public static Matrix GetTransform()
        {
            transform = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0)) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
            Matrix.CreateTranslation(new Vector3(0, 0, 0));
            return transform;
        }

        
    }
}
