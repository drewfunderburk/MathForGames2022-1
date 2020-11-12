using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MathLibrary;
using Raylib_cs;
using Vector3 = MathLibrary.Vector3;

namespace MathForGames3D
{
    class Actor
    {
        private Actor _parent;
        private Actor[] _children = new Actor[0];

        private Matrix4 _globalTransform = new Matrix4();
        private Matrix4 _localTransform = new Matrix4();
        private Matrix4 _translation = new Matrix4();
        private Matrix4 _rotation = new Matrix4();
        private Matrix4 _scale = new Matrix4();

        private Vector3 _rotationAngle = new Vector3();
        private float _collisionRadius = 10;

        public bool Started { get; set; }
        public bool WillDestroy { get; set; }

        public Vector3 Forward
        { get { return new Vector3(_localTransform.m11, _localTransform.m21, _localTransform.m31).Normalized; } }

        public Vector3 GlobalPosition
        { get { return new Vector3(_globalTransform.m14, _globalTransform.m24, _globalTransform.m34); } }
        
        public Vector3 LocalPosition
        {
            get { return new Vector3(_localTransform.m14, _localTransform.m24, _localTransform.m34); }
            set { SetTranslation(value); }
        }

        public Vector3 Velocity { get; set; } = new Vector3();

        public Actor()
        {
            LocalPosition = new Vector3();
            UpdateTransform();
        }

        public Actor(float x, float y, float z)
        {
            LocalPosition = new Vector3(x, y, z);

            UpdateTransform();
        }

        public Actor(Vector3 position) : this(position.X, position.Y, position.Z) { }

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

        public void SetTranslation(Vector3 position)
        { _translation = Matrix4.CreateTranslation(position); }

        public void SetRotation(Vector3 radians)
        {
            _rotationAngle = radians;
            _rotation = Matrix4.CreateCombinedRotation(radians);
        }

        public void Rotate(Vector3 radians)
        {
            _rotationAngle += radians;
            _rotation *= Matrix4.CreateCombinedRotation(radians);
        }

        public void SetScale(Vector3 scale)
        {
            _scale = Matrix4.CreateScale(scale);
        }

        private void UpdateGlobalTransform()
        {
            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = _localTransform;

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


        #region CORE
        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            Rotate(new Vector3(0.1f, 0, 0));
            UpdateTransform();
            LocalPosition += Velocity * deltaTime;
        }

        public virtual void Draw()
        {
            Raylib.DrawSphere(
                new System.Numerics.Vector3(
                    GlobalPosition.X, 
                    GlobalPosition.Y, 
                    GlobalPosition.Z), 
                1, 
                Color.BLUE);
        }

        public virtual void End()
        {
            Started = false;
        }
        #endregion
    }
}
