using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    /// <summary>
    /// This is the base class for all objects that will 
    /// be moved or interacted with in the game
    /// </summary>
    class Actor
    {
        protected char _icon = ' ';
        protected ConsoleColor _color;
        protected Color _rayColor;

        protected Actor _parent;
        protected Actor[] _children;

        protected Matrix3 _globalTransform = new Matrix3();

        protected Matrix3 _localTransform = new Matrix3();
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();

        public bool Started { get; private set; }

        // X-axis forward
        public Vector2 Forward 
        { get { return new Vector2(_localTransform.m11, _localTransform.m21).Normalized; } }

        public Vector2 LocalPosition
        {
            get { return new Vector2(_localTransform.m13, _localTransform.m23); }
            set { SetTranslation(value); }
        }

        public Vector2 Velocity { get; set; }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            LocalPosition = new Vector2(x, y);
            Velocity = new Vector2();
            _color = color;
            LocalPosition.X = x;
            LocalPosition.Y = y;
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x,y,icon,color)
        {
            _rayColor = rayColor;
        }

        public bool AddChild(Actor child)
        {
            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length + 1];

            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }
            tempArray[_children.Length] = child;
            _children = tempArray;
            child._parent = this;
            child._globalTransform = this._localTransform;
            return true;
        }

        public bool RemoveChild(Actor child)
        {
            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length - 1];
            bool childRemoved = false;

            int j = 0;
            for (int i = 0; i < tempArray.Length; i++)
            {
                if (_children[i] != child)
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                {
                    childRemoved = true;
                }
            }

            _children = tempArray;
            child._parent = null;
            return childRemoved;
        }

        public void SetTranslation(Vector2 position)
        {
            _translation.m13 = position.X;
            _translation.m23 = position.Y;
        }

        public void SetRotation(float radians)
        {
            _rotation.m11 = (float)Math.Cos(radians);
            _rotation.m21 = (float)-Math.Sin(radians);
            _rotation.m12 = (float)Math.Sin(radians);
            _rotation.m22 = (float)Math.Cos(radians);
        }

        public void SetScale(float x, float y)
        {
            _scale.m11 = x;
            _scale.m22 = y;
        }

        private void UpdateTransform()
        {
            _localTransform = _translation * _rotation * _scale;
        }

        public virtual void Start()
        {
            Started = true;
        }

        
        public virtual void Update(float deltaTime)
        {
            // Update Transform
            UpdateTransform();

            //Increase position by the current velocity
            LocalPosition += Velocity * deltaTime;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement
            //Raylib.DrawText(_icon.ToString(), (int)(Position.X * 32), (int)(Position.Y * 32), 32, _rayColor);
            Raylib.DrawLine(
                (int)(LocalPosition.X * 32),
                (int)(LocalPosition.Y * 32),
                (int)((LocalPosition.X + Forward.X) * 32),
                (int)((LocalPosition.Y + Forward.Y) * 32),
                Color.WHITE
            );

            //Changes the color of the console text to be this actors color
            Console.ForegroundColor = _color;

            //Only draws the actor on the console if it is within the bounds of the window
            if(LocalPosition.X >= 0 && LocalPosition.X < Console.WindowWidth 
                && LocalPosition.Y >= 0  && LocalPosition.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)LocalPosition.X, (int)LocalPosition.Y);
                Console.Write(_icon);
            }
            
            //Reset console text color to be default color
            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
