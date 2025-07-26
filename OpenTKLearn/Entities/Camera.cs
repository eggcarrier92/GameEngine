using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;

namespace GameEngine.Entities
{
    public class Camera
    {
        private readonly RenderEngine.Window _window;
        private float _speed = 5f, _sensitivity = .1f;
        private Vector3 _position;

        public Vector3 Position => _position;
        public float Pitch { get; private set; }
        public float Roll { get; private set; }
        public float Yaw { get; private set; }

        public Camera()
        {
            _window = RenderEngine.Window.Instance;
            _window.UpdateFrame += Move;
            _window.MouseMove += Rotate;
        }

        private void Move(FrameEventArgs args)
        {
            Vector3 viewDirection = new(
                (float)Math.Sin(MathHelper.DegreesToRadians(Yaw)),
                0,
                -(float)Math.Cos(MathHelper.DegreesToRadians(Yaw)));
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

            if (_window.IsKeyDown(Keys.LeftShift))
                _position.Y -= _speed * (float)args.Time;
            if (_window.IsKeyDown(Keys.Space))
                _position.Y += _speed * (float)args.Time;
        }

        private void Rotate(MouseMoveEventArgs args)
        {
            Pitch += args.DeltaY * _sensitivity;
            Yaw += args.DeltaX * _sensitivity;
            Pitch = Math.Clamp(Pitch, -90, 90);


            Yaw = Yaw > 180f ? Yaw - 360f : Yaw;
            Yaw = Yaw < -180f ? Yaw + 360f : Yaw;
        }
    }
}
