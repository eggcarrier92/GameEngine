namespace GameEngine.RenderEngine
{
    public class ModelTexture
    {
        private readonly int _textureID;

        public int textureID => _textureID;

        public ModelTexture(int id)
        {
            _textureID = id;
        }
    }
}
