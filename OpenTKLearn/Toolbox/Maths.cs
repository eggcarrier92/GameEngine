using GameEngine.Entities;
using OpenTK.Mathematics;

namespace GameEngine.Toolbox
{
    public class Maths
    {
        public static Matrix4 CreateTransformationMatrix(Vector3 translation, float rx, float ry, float rz, float scale)
        {
            Matrix4 matrix = Matrix4.Identity;
            matrix *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rx));
            matrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(ry));
            matrix *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rz));
            matrix *= Matrix4.CreateScale(scale);
            matrix *= Matrix4.CreateTranslation(translation);
            return matrix;
        }
        public static Matrix4 CreateViewMatrix(Camera camera)
        {
            Matrix4 viewMatrix = Matrix4.Identity;
            Vector3 cameraPos = camera.Position;
            Vector3 negativeCameraPos = -cameraPos;
            viewMatrix *= Matrix4.CreateTranslation(negativeCameraPos);
            viewMatrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(camera.Yaw));
            viewMatrix *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(camera.Pitch));
            return viewMatrix;
        }
    }
}
