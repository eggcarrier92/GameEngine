using OpenTK.Graphics.OpenGL4;
using GameEngine.Models;
using System.Runtime.Versioning;

namespace GameEngine.RenderEngine
{
    public class Loader
    {
        private readonly List<int> _vaos = new();
        private readonly List<int> _vbos = new();
        private readonly List<int> _textures = new();

        /// <summary>
        /// Creates a VAO and stores the model data into the attributes of the VAO.
        /// </summary>
        /// <param name="vertices">The 3D positions of each vertex</param>
        /// <param name="indices">The indices of the vertices</param>
        /// <returns>The loaded model</returns>
        public RawModel LoadToVAO(float[] vertices, float[] textureCoords, int[] indices)
        {
            int vaoID = CreateVAO();
            BindIndicesBuffer(indices);
            StoreDataInAttributeList(0,3, vertices);
            StoreDataInAttributeList(1,2, textureCoords);
            UnbindVAO();
            return new RawModel(vaoID, indices.Length);
        }

        public int LoadTexture(string fileName)
        {
            Texture texture = new(fileName);
            int textureID = texture.TextureID;
            _textures.Add(textureID);
            return textureID;
        }

        /// <summary>
        /// Deletes all the VAOs and the VBOs from the video memory
        /// </summary>
        public void CleanUp()
        {
            foreach (var vaoID in _vaos)
                GL.DeleteVertexArray(vaoID);
            foreach (var vboID in _vbos)
                GL.DeleteBuffer(vboID);
            foreach(var textureID in _textures)
                GL.DeleteTexture(textureID);
        }

        /// <summary>
        /// Creates a new VAO and binds it
        /// </summary>
        /// <returns>The ID of the created VAO</returns>
        private int CreateVAO() 
        {
            int vaoID = GL.GenVertexArray();
            _vaos.Add(vaoID);
            GL.BindVertexArray(vaoID);
            return vaoID;
        }

        /// <summary>
        /// Stores the data in the attribute list of the VAO
        /// </summary>
        /// <param name="attributeNumber">The number of the attribute of the VAO where the data is to be stored</param>
        /// <param name="data">The data to be stored in the VAO</param>
        private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, float[] data)
        {
            int vboID = GL.GenBuffer();
            _vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }

        private void BindIndicesBuffer(int[] indices)
        {
            int vboID = GL.GenBuffer();
            _vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
        }
    }
}
