using GameEngine.Models;
using OpenTK.Mathematics;

namespace GameEngine.RenderEngine
{
    public class OBJLoader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Wrong error")]
        public static RawModel LoadObjModel(string fileName, Loader loader)
        {
            List<Vector3> vertices      = new();
            List<Vector2> textureCoords = new();
            List<Vector3> normals       = new();
            List<int>     indices       = new();

            float[] verticesArray      = Array.Empty<float>();
            float[] textureCoordsArray = Array.Empty<float>();
            float[] normalsArray       = Array.Empty<float>();
            int[]   indicesArray       = Array.Empty<int>();

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Unable to open \"" + fileName + "\", does not exist.");
            }

            foreach (string line in File.ReadAllLines(fileName))
            {
                string formattedLine = line.Replace('.', ',');
                string[] currentLine = formattedLine.Split(' ');

                switch (currentLine[0])
                {
                    case "v":
                        vertices.Add(new Vector3(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3])));
                        break;
                    case "vt":
                        textureCoords.Add(new Vector2(float.Parse(currentLine[1]), 1f - float.Parse(currentLine[2])));
                        break;
                    case "vn":
                        normals.Add(new Vector3(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3])));
                        break;
                    default:
                        break;
                }
            }

            textureCoordsArray = new float[vertices.Count * 2];
            normalsArray       = new float[vertices.Count * 3];
            verticesArray      = new float[vertices.Count * 3];

            foreach (string line in File.ReadAllLines(fileName))
            {
                if (!line.StartsWith("f "))
                    continue;

                string[] currentLine = line.Split(' ');
                foreach (string word in currentLine.Skip(1))
                {
                    string[] vertex = word.Split("/").ToArray();

                    int currentVertexPointer = int.Parse(vertex[0]) - 1; // subtract 1 because obj starts at 1 and our array starts at 0
                    // Add indices
                    indices.Add(currentVertexPointer);
                    // Add texture coords
                    Vector2 currentTextureCoords = textureCoords[int.Parse(vertex[1]) - 1];
                    textureCoordsArray[currentVertexPointer * 2] = currentTextureCoords.X;
                    textureCoordsArray[currentVertexPointer * 2 + 1] = 1 - currentTextureCoords.Y;
                    // Add normals
                    Vector3 currentNorm = normals[int.Parse(vertex[2]) - 1];
                    normalsArray[currentVertexPointer * 3] = currentNorm.X;
                    normalsArray[currentVertexPointer * 3 + 1] = currentNorm.Y;
                    normalsArray[currentVertexPointer * 3 + 2] = currentNorm.Z;
                }
            }

            indicesArray = new int[indices.Count];

            int vertexPointer = 0;
            foreach (var vertex in vertices)
            {
                verticesArray[vertexPointer++] = vertex.X;
                verticesArray[vertexPointer++] = vertex.Y;
                verticesArray[vertexPointer++] = vertex.Z;
            }
            for (int i = 0; i < indices.Count; i++)
            {
                indicesArray[i] = indices[i];
            }
            return loader.LoadToVAO(verticesArray, textureCoordsArray, indicesArray);
        }
    }
}
