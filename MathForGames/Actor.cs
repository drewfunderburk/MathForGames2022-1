using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    /// <summary>
    /// Create a new sub-class of actor that builds upon the functionality already given.
    /// Examples: 
    /// - Create simple collision detection between players and other actors.
    /// - Give NPC's simple AI
    /// - Create a simple golf game. Players would need to move an actor representing a 
    /// ball to an actor that would represent the hole. The player wins if those two actors 
    /// collide. For an added challenge, give the player the ability to switch clubs. Each
    /// club will change the magnitude of the vector applied to the ball's position.
    /// </summary>
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


        public Actor(float x, float y, Color color, char icon = ' ')
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
            _position.X += _velocity.X;
            _position.Y += _velocity.Y;

        }

        public virtual void Draw()
        {
            if(_position.X >= 0 && _position.X < Console.WindowWidth - 1 
                && _position.Y >= 0 && _position.Y < Console.WindowHeight - 1)
            {
                Console.SetCursorPosition((int)_position.X, (int)_position.Y);
                Console.Write(_icon);
            }
            
            Raylib.DrawRectangle((int)_position.X, (int)_position.Y, 10, 10, _color);
        }

        public virtual void End()
        {

        }

    }
}
