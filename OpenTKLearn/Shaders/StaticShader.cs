using GameEngine.Entities;
using GameEngine.Toolbox;
using OpenTK.Mathematics;

namespace GameEngine.Shaders
{
    public class StaticShader : ShaderProgram
    {
        private static readonly string _vertexFile = "Resources/Shaders/shader.vert";
        private static readonly string _fragmentFile = "Resources/Shaders/shader.frag";

        private int _location_transformationMatrix;
        private int _location_projectionMatrix;
        private int _location_viewMatrix;

        public StaticShader() : base(_vertexFile, _fragmentFile) { }

        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
            BindAttribute(1, "textureCoords");
        }

        protected override void GetAllUniformLocations()
        {
            _location_transformationMatrix = GetUniformLocation("transformationMatrix");
            _location_projectionMatrix = GetUniformLocation("projectionMatrix");
            _location_viewMatrix = GetUniformLocation("viewMatrix");
        }
        public void LoadTransformationMatrix(Matrix4 matrix)
        {
            LoadMatrix(_location_transformationMatrix, matrix);
        }
        public void LoadProjectionMatrix(Matrix4 matrix)
        {
            LoadMatrix(_location_projectionMatrix, matrix);
        }
        public void LoadViewMatrix(Camera camera)
        {
            LoadMatrix(_location_viewMatrix, Maths.CreateViewMatrix(camera));
        }
    }
}
