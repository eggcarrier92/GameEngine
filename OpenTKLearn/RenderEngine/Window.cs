using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using GameEngine.Entities;
using GameEngine.Models;
using GameEngine.Shaders;
using GameEngine.Toolbox;
using System.Runtime.Versioning;

namespace GameEngine.RenderEngine
{
    public class Window : GameWindow
    {
#pragma warning disable CS8618 
        public static Window instance { get; private set; }
#pragma warning restore CS8618 

        private readonly Loader _loader = new();
        private readonly StaticShader _shader;
        private readonly Renderer _renderer;
        private readonly Camera _camera;

        private bool _readyToRender  = true;

        private readonly Entity _cube;
        private readonly Entity _ground;
        private int _frames;

        public static Window CreateWindow(int width, int height, string title)
        {
            NativeWindowSettings nativeWindowSettings = new()
            {
                Size = new Vector2i(width, height),
                Title = title,
                Flags = ContextFlags.ForwardCompatible,
            };
            GameWindowSettings gameWindowSettings = GameWindowSettings.Default;
            gameWindowSettings.RenderFrequency = 0f;
            return new Window(gameWindowSettings, nativeWindowSettings);
        }

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
            : base(gameWindowSettings, nativeWindowSettings) 
        {
            instance = this;
            _shader = new();
            _renderer = new(_shader);
            _cube = new(
                new Cube("Resources/Textures/texture.png", _loader).texturedModel, 
                new Vector3(-0.3f, 1f, -2), 0, 0, 0, 1);
            _ground = new(
                new Quad("Resources/Textures/grass.png", _loader).texturedModel,
                new Vector3(0, -0.1f, 0), 90, 0, 0, 10);
            _camera = new();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            ShowData();
        }

        async private void ShowData()
        {
            // FPS
            Console.WriteLine("FPS: " + _frames);
            _frames = 0;
            //Console.WriteLine("Transformation matrix:\n" + Maths.CreateTransformationMatrix(_entity.position, _entity.rotX, _entity.rotY, _entity.rotZ, _entity.scale));
            await Task.Delay(1000);
            ShowData();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            if (!_readyToRender)
                return;

            _renderer.Prepare();
            _shader.Start();
            _shader.LoadViewMatrix(_camera);
            _renderer.Render(_ground, _shader);
            _renderer.Render(_cube, _shader);
            _shader.Stop();
            _frames++;

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);


            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
                _readyToRender = false;
            }

            _cube.Rotate(360 * (float) args.Time, 360 * (float) args.Time, 0);
            //_quad.scale -= 1f * (float) args.Time;
            //_cube.Translate(0, 0, 1 * (float)args.Time);
        }

        protected override void OnResize(ResizeEventArgs args)
        {
            base.OnResize(args);

            GL.Viewport(0, 0, Size.X, Size.Y);
            _renderer.CreateProjectionMatrix();
            _renderer.LoadProjectionMatrix(_shader);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            _loader.CleanUp();
            _shader.CleanUp();
        }
    }
}
