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
        protected Sprite _sprite;

        protected Actor _parent;
        protected Actor[] _children;

        protected Matrix3 _globalTransform = new Matrix3();

        protected Matrix3 _localTransform = new Matrix3();
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();

        protected float _rotationAngle = 0;
        protected float _collisionRadius;

        protected Color color = Color.GREEN;

        public bool Started { get; private set; }

        // X-axis forward
        public Vector2 Forward
        { get { return new Vector2(_localTransform.m11, _localTransform.m21).Normalized; } }

        public Vector2 GlobalPosition
        {
            get { return new Vector2(_globalTransform.m13, _globalTransform.m23); }
        }

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
        public Actor(float x, float y)
        {
            LocalPosition = new Vector2(x, y);
            Velocity = new Vector2();
            _sprite = new Sprite("Images/player.png");
            _children = new Actor[0];
            _collisionRadius = 20;

            UpdateTransform();
            UpdateGlobalTransform();
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

        public void Rotate(float radians)
        {
            _rotationAngle += radians;
            // Here if I want it later. Prevent angle from being a ridiculous value
            //_rotationAngle %= (float)(Math.PI * 2);
            SetRotation(_rotationAngle);
        }

        public void SetScale(float x, float y)
        {
            _scale.m11 = x;
            _scale.m22 = y;
        }

        // This may or may not work
        public void LookAt(Vector2 position)
        {
            // Find the direction to look at
            Vector2 direction = (position - LocalPosition).Normalized;

            // Get dotproduct between forward and direction to look
            float dotProduct = Vector2.DotProduct(Forward, direction);

            // If actor is already facing that direction, return
            if (dotProduct >= 1)
                return;


            // Get angle to face
            float angle = (float)Math.Acos(dotProduct);

            // Get perpendicular vector to direction
            Vector2 perpVector = new Vector2(direction.Y, -direction.X);

            // Get dotproduct between forward and perpendicular vector
            float perpDotProduct = Vector2.DotProduct(perpVector, Forward);

            // Negate angle if perDotProduct is negative
            if (perpDotProduct != 0)
                angle *= -perpDotProduct / Math.Abs(perpDotProduct);
            /*
            Raylib.DrawLine(
                (int)GlobalPosition.X, 
                (int)GlobalPosition.Y, 
                (int)(GlobalPosition.X + (Forward.X * 50)), 
                (int)(GlobalPosition.Y + (Forward.Y * 50)), 
                Color.RED);
            Raylib.DrawLine(
                (int)GlobalPosition.X,
                (int)GlobalPosition.Y,
                (int)(GlobalPosition.X + (direction.X * 50)),
                (int)(GlobalPosition.Y + (direction.Y * 50)),
                Color.GREEN);
            Raylib.DrawLine(
                (int)GlobalPosition.X,
                (int)GlobalPosition.Y,
                (int)(GlobalPosition.X + (perpVector.X * 50)),
                (int)(GlobalPosition.Y + (perpVector.Y * 50)),
                Color.BLUE);
            */
            Rotate(angle);
        }

        protected void UpdateGlobalTransform()
        {
            if (_parent != null)
            {
                _globalTransform = _parent._globalTransform * _localTransform;
            }
            else
            {
                _globalTransform = _localTransform;
            }

            for (int i = 0; i < _children.Length; i++)
            {
                _children[i].UpdateGlobalTransform();
            }
        }

        private void UpdateTransform()
        {
            UpdateGlobalTransform();
            _localTransform = _translation * _rotation * _scale;
            for (int i = 0; i < _children.Length; i++)
            {
                _children[i]._globalTransform = _localTransform;
            }
        }

        public bool CheckCollision(Actor other)
        {
            float distance = (other.GlobalPosition - this.GlobalPosition).Magnitude;
            if (distance < other._collisionRadius + _collisionRadius)
                return true;
            else
                return false;
        }

        public virtual void OnCollision(Actor other)
        {
            // Check if objects are really collided
            if (!CheckCollision(other))
                return;

            color = Color.RED;
        }

        public virtual void Start()
        {
            Started = true;
        }


        public virtual void Update(float deltaTime)
        {
            // Console.WriteLine(GlobalPosition.X + ", " + GlobalPosition.Y);
            // Update Transform
            UpdateTransform();

            color = Color.GREEN;

            //Increase position by the current velocity
            LocalPosition += Velocity * deltaTime;
        }

        public virtual void Draw()
        {
            Raylib.DrawCircleLines((int)GlobalPosition.X, (int)GlobalPosition.Y, _collisionRadius, color);
            _sprite.Draw(_globalTransform);
        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
