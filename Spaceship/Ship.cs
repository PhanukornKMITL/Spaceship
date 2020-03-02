using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{
    class Ship
    {
        
        public int speed = 240;
        static public Vector2 defaultPosition = new Vector2(640,300);
        public Vector2 position = defaultPosition;



        public void shipUpdate(GameTime gameTime,Controller gameController)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; //dt is mean delta time

            if (gameController.inGame)
            {

                if (kState.IsKeyDown(Keys.Right) && position.X + 38 < 1280 )
                {
                    position.X += speed * dt;
                }

                if (kState.IsKeyDown(Keys.Left) && position.X - 38 > 0)
                {
                    position.X -= speed * dt;
                }

                if (kState.IsKeyDown(Keys.Down) && position.Y +38 < 600)
                {
                    position.Y += speed * dt;
                }
                if (kState.IsKeyDown(Keys.Up) && position.Y - 38 > 0)
                {
                    position.Y -= speed * dt;
                }
            }


        }


    }
}
