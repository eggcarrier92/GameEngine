using GameEngine.Models;
using GameEngine.RenderEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities
{
    public class Quad
    {
        private readonly float[] _vertices = {
                -0.5f,  0.5f, 0f,//v0
				 0.5f,  0.5f, 0f,//v1
				 0.5f, -0.5f, 0f,//v2
				-0.5f, -0.5f, 0f,//v3
		};


        private readonly int[] _indices = {
                0,1,3,//top left triangle (v0, v1, v3)
				3,1,2//bottom right triangle (v3, v1, v2)
		};

        private readonly float[] _textureCoords = {
            0,1,
            0,0,
            1,0,
            1,1,
        };
        private readonly TexturedModel _texturedModel;

        public TexturedModel TexturedModel => _texturedModel;

        public Quad(string texturePath, Loader loader)
        {
            RawModel rawModel = loader.LoadToVAO(_vertices, _textureCoords, _indices);
            ModelTexture texture = new(loader.LoadTexture(texturePath));
            _texturedModel = new(rawModel, texture);
        }

    }
}
