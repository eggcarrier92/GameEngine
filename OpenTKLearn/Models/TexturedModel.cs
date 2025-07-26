using GameEngine.RenderEngine;

namespace GameEngine.Models
{
    public class TexturedModel
    {
        private readonly RawModel _rawModel;
        private readonly ModelTexture _modelTexture;

        public RawModel RawModel => _rawModel;
        public ModelTexture ModelTexture => _modelTexture;

        public TexturedModel(RawModel model, ModelTexture texture)
        {
            _rawModel = model;
            _modelTexture = texture;
        }
    }
}
