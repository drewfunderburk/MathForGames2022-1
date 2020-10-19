using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Actor
    {
        protected char _icon = ' ';
        protected Vector2 _position;
        protected Vector2 _velocity;
        protected Color _color;
        

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position.X = value.X;
                _position.Y = value.Y;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity.X = value.X;
                _velocity.Y = value.Y;
            }
        }


        public Actor(float x, float y,Color color, char icon = ' ' )
        {
            _icon = icon;
            _position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
            
        }

        public virtual void Start()
        {

        }

        
        public virtual void Update()
        {
            _position += _velocity;
            _position.X = Math.Clamp(_position.X, 0, Console.WindowWidth-1);
            _position.Y = Math.Clamp(_position.Y, 0, Console.WindowHeight-1);

        }

        public virtual void Draw()
        {
            Console.SetCursorPosition((int)_position.X, (int)_position.Y);
            Console.Write(_icon);
            Raylib.DrawRectangle((int)_position.X, (int)_position.Y, 10, 10, _color);
            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void End()
        {

        }

    }
}
