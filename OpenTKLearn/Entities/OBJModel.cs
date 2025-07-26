using GameEngine.Models;
using GameEngine.RenderEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities
{
    public class OBJModel
    {
        public TexturedModel TexturedModel { get; private set; }

        public OBJModel(string modelPath, string texturePath, Loader loader)
        {
            RawModel rawModel = OBJLoader.LoadObjModel(modelPath, loader);
            ModelTexture texture = new(loader.LoadTexture(texturePath));
            TexturedModel = new(rawModel, texture);
        }
    }
}
