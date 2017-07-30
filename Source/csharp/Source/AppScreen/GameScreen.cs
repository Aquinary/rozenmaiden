using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RozenMaiden.AppScreen;
using RozenMaiden.GUI;

namespace RozenMaiden
{
    public class GameScreen
    {
        public Dialog MainDialog;


        public Player suiginto;
        public World[] world = new World[4];
        public Tile tworld;

        private GameState _gameState;
        private RenderTarget2D _gameRender;

        public GameScreen()
        {
            _gameRender = new RenderTarget2D
                (
                    Program.Game.GraphicsDevice,
                    Program.Game.Device.PreferredBackBufferWidth,
                    Program.Game.Device.PreferredBackBufferHeight
                );
                
            MainDialog = new Dialog("Texture\\GUI\\DialogBorder");
            MainDialog.Size = new Vector2(913, 188);
            MainDialog.Position = new Vector2(10,60);

            MainDialog.Text[0] = "Это обычный, ничем не примечательный диалог. ssssdasd asd asda sdas dsad asd asd sad sa fdsfs dfds";

            suiginto = new Player("Texture\\Entity\\Suiginto");
            suiginto.Size = new Vector2(32,32);
            suiginto.Position = new Vector2(100, 100);


            tworld = new Tile("Texture\\Level\\TileMap");
            world[0] = new World("Content/Level/l2_l2_f1.csv", 500, 500, tworld);
            world[1] = new World("Content/Level/l2_l2_f2.csv", 500, 500, tworld); 
            world[2] = new World("Content/Level/l2_l2_h.csv", 500, 500, tworld);
            world[3] = new World("Content/Level/l2_l2_t.csv", 500, 500, tworld);
        }

        public void Draw()
        {
            Program.Game.SpriteBatch.Begin(SpriteSortMode.Texture);
            Program.Game.SpriteBatch.Draw((Texture2D)_gameRender, new Vector2(0, 0), Color.White);
            Program.Game.SpriteBatch.End();
        }

        public void Update(GameState gameState)
        {
            
            //this._gameState = gameState;

            suiginto.Update(world[2]);
            MainDialog.Update();
            Camera.Update();
            Program.Game.GraphicsDevice.SetRenderTarget(_gameRender);        
            for (int i = 0; i < world.Length; i++)
            {
                if (world[i] != null)
                {
                    world[i].Draw();
                    if (i == 2)
                    {
                        suiginto.Draw();
                    }
                }
            }       
            Program.Game.GraphicsDevice.SetRenderTarget(null);


        }
    }
}