using GameEngine.Entities;
using GameEngine.RenderEngine;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace GameEngine.EngineTester
{
    public class Game
    {
        private readonly Window _window;
        private readonly Loader _loader;

        private readonly Entity _cube;
        private readonly Entity _ground;
        private readonly Entity _object;

        public Game(Window window)
        {
            _window = window;
            _window.UpdateFrame += OnUpdateFrame;
            _loader = _window.Loader;
            _cube = new(
                new Cube("Resources/Textures/texture.png", _loader).TexturedModel,
                new Vector3(-0.3f, 0.4f, -5), 0, 0, 0, 1f);
            _ground = new(
                new Quad("Resources/Textures/grass.png", _loader).TexturedModel,
                new Vector3(0, -0.1f, 0), 90, 0, 0, 10);
            _object = new(
                new OBJModel("Resources/Models/stall.obj", "Resources/Textures/stallTexture.png", _loader).TexturedModel,
                new Vector3(-1f, 0f, 0f), 0, 0, 0, 1);
            _window.Entities.Add(_cube);
            _window.Entities.Add(_ground);
            _window.Entities.Add(_object);
        }

        private void OnUpdateFrame(FrameEventArgs args)
        {
            _cube.Rotate(0, 360 * (float)args.Time, 0);
            //_cube.Translate(0, 0, 1 * (float)args.Time);
        }
    }
}
