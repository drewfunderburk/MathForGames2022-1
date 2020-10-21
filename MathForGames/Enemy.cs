using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Enemy : Actor
    {
        private Actor _target;
        private Color _alertColor;
        public Actor Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Enemy(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {

        }

        public Enemy(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _alertColor = Color.RED;
        }

        public bool CheckTargetInSight()
        {
            if (Target == null)
                return false;

            Vector2 direction = Vector2.Normalize(Position - Target.Position);

            if (Vector2.DotProduct(Forward, direction) > 0)
                return true;

            return false;
        }

        public override void Update(float deltaTime)
        {
            if(CheckTargetInSight())
            {
                _rayColor = Color.RED;
            }
            else
            {
                _rayColor = Color.BLUE;
            }
            base.Update(deltaTime);
        }
    }
}
