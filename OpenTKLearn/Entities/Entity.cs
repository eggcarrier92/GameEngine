using OpenTK.Mathematics;
using GameEngine.Models;

namespace GameEngine.Entities
{
    public class Entity
    {
        private readonly TexturedModel _model;
        private Vector3 _position;
        private float _rotX, _rotY, _rotZ;
        private float _scale;

        public TexturedModel Model => _model;
        public Vector3 Position => _position;
        public float RotX => _rotX;
        public float RotY => _rotY;
        public float RotZ => _rotZ;
        public float Scale { get => _scale; set { _scale = value; } }

        public Entity(TexturedModel model, Vector3 position, float rotX, float rotY, float rotZ, float scale)
        {
            _model = model;
            _position = position;
            _rotX = rotX;
            _rotY = rotY;
            _rotZ = rotZ;
            _scale = scale;
        }
        public void Translate(float dx, float dy, float dz)
        {
            _position.X += dx;
            _position.Y += dy;
            _position.Z += dz;
        }
        public void Rotate(float dx, float dy, float dz)
        {
            _rotX += dx;
            _rotY += dy;
            _rotZ += dz;
        }
    }
}
