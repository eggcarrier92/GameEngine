using GameEngine.Models;
using GameEngine.RenderEngine;


namespace GameEngine.Entities
{
    public class Cube
    {
		private readonly float[] _vertices = {
			// back
			-0.5f, 0.5f,-0.5f, 
			-0.5f,-0.5f,-0.5f, 
			 0.5f,-0.5f,-0.5f, 
			 0.5f, 0.5f,-0.5f, 
			// front
			-0.5f, 0.5f, 0.5f, 
			-0.5f,-0.5f, 0.5f, 
			 0.5f,-0.5f, 0.5f, 
			 0.5f, 0.5f, 0.5f, 
			// right
			 0.5f, 0.5f,-0.5f, 
			 0.5f,-0.5f,-0.5f, 
			 0.5f,-0.5f, 0.5f, 
			 0.5f, 0.5f, 0.5f, 
			// left
			-0.5f, 0.5f,-0.5f,
			-0.5f,-0.5f,-0.5f,
			-0.5f,-0.5f, 0.5f,
			-0.5f, 0.5f, 0.5f,
			// top
			-0.5f, 0.5f, 0.5f,
			-0.5f, 0.5f,-0.5f,
			 0.5f, 0.5f,-0.5f,
			 0.5f, 0.5f, 0.5f,
			// bottom
			-0.5f,-0.5f, 0.5f,
			-0.5f,-0.5f,-0.5f,
			 0.5f,-0.5f,-0.5f,
			 0.5f,-0.5f, 0.5f

	};

		private readonly int[] _indices = {
			3,1,0,
			2,1,3,

			4,5,7,
			7,5,6,

			11,9,8,
			10,9,11,

			12,13,15,
			15,13,14,

			19,17,16,
			18,17,19,

			20,21,23,
			23,21,22

	};

		private readonly float[] _textureCoords = {

			1,1,
			1,0,
			0,0,
			0,1,

			0,1,
			0,0,
			1,0,
			1,1,

			1,1,
			1,0,
			0,0,
			0,1,

			0,1,
			0,0,
			1,0,
			1,1,

			0,0,
			0,1,
			1,1,
			1,0,

			1,0,
			1,1,
			0,1,
			0,0,
	};
		private readonly TexturedModel _texturedModel;

        public TexturedModel TexturedModel => _texturedModel;

        public Cube(string texturePath, Loader loader)
        {
            RawModel rawModel = loader.LoadToVAO(_vertices, _textureCoords, _indices);
            ModelTexture texture = new(loader.LoadTexture(texturePath));
            _texturedModel = new(rawModel, texture);
        }
    }
}
