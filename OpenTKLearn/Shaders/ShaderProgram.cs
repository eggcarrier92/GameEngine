using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GameEngine.Shaders
{
    public abstract class ShaderProgram
    {
        private readonly int _programID;
        private readonly int _vertexShaderID;
        private readonly int _fragmentShaderID;

        public ShaderProgram(string vertexFile, string fragmentFile)
        {
            _vertexShaderID = LoadShader(vertexFile,ShaderType.VertexShader);
            _fragmentShaderID = LoadShader(fragmentFile,ShaderType.FragmentShader);
            _programID = GL.CreateProgram();
            GL.AttachShader(_programID, _vertexShaderID);
            GL.AttachShader(_programID,_fragmentShaderID);
            BindAttributes();
            GL.LinkProgram(_programID);
            GL.ValidateProgram(_programID);
            GetAllUniformLocations();

        }

        protected abstract void GetAllUniformLocations();

        protected int GetUniformLocation(string uniformName)
        {
            return GL.GetUniformLocation(_programID, uniformName);
        }

        public void Start()
        {
            GL.UseProgram(_programID);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        public void CleanUp()
        {
            Stop();
            GL.DetachShader(_programID, _vertexShaderID);
            GL.DetachShader(_programID, _fragmentShaderID);
            GL.DeleteShader(_vertexShaderID);
            GL.DeleteShader(_fragmentShaderID);
            GL.DeleteProgram(_programID);
        }

        protected abstract void BindAttributes();

        protected void BindAttribute(int attribute, string variableName)
        {
            GL.BindAttribLocation(_programID, attribute, variableName);
        }

        protected void LoadFloat(int location, float value)
        {
            GL.Uniform1(location, value);
        }

        protected void LoadVector(int location, Vector3 vector)
        {
            GL.Uniform3(location, vector.X, vector.Y, vector.Z);
        }

        protected void LoadBool(int location, bool value)
        {
            float toLoad = 0;
            if (value)
                toLoad = 1;
            GL.Uniform1(location, toLoad);
        }

        protected void LoadMatrix(int location, Matrix4 matrix)
        {
            GL.UniformMatrix4(location, false, ref matrix);
        }

        private static int LoadShader(string file, ShaderType shaderType)
        {
            string shaderSource = File.ReadAllText(file);
            int shaderID = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderID, shaderSource);
            GL.CompileShader(shaderID);
            return shaderID;
        }
    }
}
