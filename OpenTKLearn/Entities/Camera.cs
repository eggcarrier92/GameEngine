using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameEngine.RenderEngine;
using OpenTK.Windowing.Common;

namespace GameEngine.Entities
{
    public class Camera
    {
        private readonly RenderEngine.Window _window;
        private float _speed = 5f, _sensitivity = .1f;
        private Vector3 _position;

        public Vector3 position => _position;
        public float pitch { get; private set; }
        public float roll { get; private set; }
        public float yaw { get; private set; }

        public Camera() 
        {
            _window = RenderEngine.Window.instance;
            _window.UpdateFrame += Move;
            _window.MouseMove += Rotate;
        }

        private void Move(FrameEventArgs args)
        {
            Vector3 viewDirection = new(
                (float)Math.Sin(MathHelper.DegreesToRadians(yaw)), 
                0, 
                -(float)Math.Cos(MathHelper.DegreesToRadians(yaw)));
            Vector3 moveDirection = new();

            if (_window.IsKeyDown(Keys.W))
                moveDirection += viewDirection;
            if (_window.IsKeyDown(Keys.A))
                moveDirection += new Vector3(viewDirection.Z, 0, -viewDirection.X);
            if (_window.IsKeyDown(Keys.S))
                moveDirection -= viewDirection;
            if (_window.IsKeyDown(Keys.D))
                moveDirection -= new Vector3(viewDirection.Z, 0, -viewDirection.X);

            _position += moveDirection * _speed * (float)args.Time;

            if (_window.IsKeyDown(Keys.Q))
                _position.Y -= _speed * (float)args.Time;
            if (_window.IsKeyDown(Keys.E))
                _position.Y += _speed * (float)args.Time;
        }

        private void Rotate(MouseMoveEventArgs args)
        {
            pitch += args.DeltaY * _sensitivity;
            yaw += args.DeltaX * _sensitivity;
            pitch = Math.Clamp(pitch, -90, 90);


            yaw = yaw >  180f ? yaw - 360f : yaw;
            yaw = yaw < -180f ? yaw + 360f : yaw;
        }
    }
}
