using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace RozenMaiden
{
    public class Player : Entity
    {
        public Player(string filePath) : base(filePath) { }

        protected override void UserEventUpdate()
        {
            KeyboardState key = Keyboard.GetState();
            Vector2 tempDirection = Direction; 

            if (key.IsKeyDown(Keys.W))
            {
                tempDirection.Y = -Speed;    
            } else

            if (key.IsKeyDown(Keys.S))
            {       
                tempDirection.Y = Speed;
            } else

            if (key.IsKeyDown(Keys.A))
            {
                tempDirection.X = -Speed;
            } else

            if (key.IsKeyDown(Keys.D))
            {
                tempDirection.X = Speed;
            }
            
            Direction = tempDirection;
        }
    }
}

