using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using GameEngine.Entities;
using GameEngine.Models;
using GameEngine.Shaders;
using GameEngine.Toolbox;

namespace GameEngine.RenderEngine
{
    public class Renderer
    {
        private static readonly float s_fov = 70f;
        private static readonly float s_nearPlane = 0.01f;
        private static readonly float s_farPlane = 1000f;

        private Matrix4 _projectionMatrix;
        private float _aspectRatio;

        public Renderer(StaticShader shader)
        {
            CreateProjectionMatrix();
            LoadProjectionMatrix(shader);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
        }

        public void LoadProjectionMatrix(StaticShader shader)
        {
            shader.Start();
            shader.LoadProjectionMatrix(_projectionMatrix);
            shader.Stop();
        }

        /// <summary>
        /// This method must be called each frame before any rendering to clear the screen of everything that was rendered the last frame.
        /// </summary>
        public void Prepare()
        {
            GL.ClearColor(0, 1, 1, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
        }

        /// <summary>
        /// Renders a model to the screen.
        /// </summary>
        /// <param name="model">The model to be rendered</param>
        public void Render(Entity entity, StaticShader shader)
        {
            TexturedModel texturedModel = entity.Model;
            RawModel rawModel = texturedModel.RawModel;
            GL.BindVertexArray(rawModel.VaoID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            Matrix4 transformationMatrix = Maths.CreateTransformationMatrix(entity.Position, entity.RotX, entity.RotY, entity.RotZ, entity.Scale);
            shader.LoadTransformationMatrix(transformationMatrix);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texturedModel.ModelTexture.TextureID);
            GL.DrawElements(PrimitiveType.Triangles, rawModel.VertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
        }

        public void CreateProjectionMatrix()
        {
            _aspectRatio = (float) Window.Instance.Size.X / Window.Instance.Size.Y;
            float yScale = 1f / (float) Math.Tan(MathHelper.DegreesToRadians(s_fov / 2f)) * _aspectRatio;
            float xScale = yScale / _aspectRatio;
            float frustumLength = s_farPlane - s_nearPlane;

            _projectionMatrix = new();
            _projectionMatrix.M11 = xScale;
            _projectionMatrix.M22 = yScale;
            _projectionMatrix.M33 = -((s_farPlane + s_nearPlane) / frustumLength);
            _projectionMatrix.M34 = -1;
            _projectionMatrix.M43 = -(2 * s_nearPlane * s_farPlane / frustumLength);
            _projectionMatrix.M44 = 0;
        }
    }
}
