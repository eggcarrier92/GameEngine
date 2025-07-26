using GameEngine.RenderEngine;
using OpenTK.Windowing.Common;
using System.Runtime.Versioning;

namespace GameEngine.EngineTester
{
    public class Program
    {
        private static void Main()
        {
            Window window = Window.CreateWindow(1920, 1080, "Game Engine");
            window.CursorGrabbed = true;
            window.VSync = VSyncMode.Off;
            _ = new Game(window);
            window.Run();
            Console.WriteLine("Closed");
        }
    }
}