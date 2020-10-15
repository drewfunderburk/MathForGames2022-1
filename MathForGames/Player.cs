using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        public Player(float x, float y, Color color, char icon = ' ')
            : base(x,y,color, icon)
        {

        }

        public override void Update()
        {
            int keyPressedX = Raylib.GetKeyPressed();
            switch (keyPressedX)
            {
                case 97:
                    _velocity.X = -1;
                    _velocity.Y = 0;
                    break;
                case 100:
                    _velocity.X = 1;
                    _velocity.Y = 0;
                    break;
                case 119:
                    _velocity.Y = -1;
                    _velocity.X = 0;
                    break;
                case 115:
                    _velocity.Y = 1;
                    _velocity.X = 0;
                    break;
                default:
                    _velocity.X = 0;
                    _velocity.Y = 0;
                    break;
            }
            base.Update();
        }
    }
}
