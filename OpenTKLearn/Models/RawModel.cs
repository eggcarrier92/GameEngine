namespace GameEngine.Models
{
    /// <summary>
    /// Represents a loaded model. It contains the ID of the VAO that contains the model's data, and holds the number of vertices in the model.
    /// </summary>
    public class RawModel
    {
        private readonly int _vaoID;
        private readonly int _vertexCount;

        /// <summary>The ID of the VAO which contains the data about all the geometry of this model.</summary>
        public int VaoID => _vaoID;
        /// <summary>The number of vertices in the model.</summary>
        public int VertexCount => _vertexCount;

        public RawModel(int vaoID, int vertexCount)
        {
            _vaoID = vaoID;
            _vertexCount = vertexCount;
        }
    }
}
