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
        private Vector2 _facing;
        protected ConsoleColor _color;
        protected Color _rayColor;
        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get { return _facing; }
            set { _facing = value; }
        }


        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
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
                _velocity = value;
            }
        }


        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
            Forward = new Vector2(1, 0);
        }

        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x,y,icon,color)
        {
            _rayColor = rayColor;
        }

        private void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;

            Forward = Velocity.Normalized;
        }

        public virtual void Start()
        {
            Started = true;
        }

        
        public virtual void Update(float deltaTime)
        {
            UpdateFacing();
            _position += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            Raylib.DrawText(_icon.ToString(), (int)(_position.X * 32), (int)(_position.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(Position.X * 32),
                (int)(Position.Y * 32),
                (int)((Position.X + Forward.X) * 32),
                (int)((Position.Y + Forward.Y) * 32),
                Color.WHITE
            );

            Console.ForegroundColor = _color;


            //Only draws the actor on the console if it is within the bounds of the window
            if(Position.X >= 0 && Position.X < Console.WindowWidth 
                && Position.Y >= 0  && Position.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)_position.X, (int)_position.Y);
                Console.Write(_icon);
            }
            
            Console.ForegroundColor = Game.DefaultColor;
            

        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
