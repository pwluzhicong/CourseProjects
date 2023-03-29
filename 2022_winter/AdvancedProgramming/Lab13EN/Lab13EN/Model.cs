using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using OpenTK.Mathematics;

namespace Lab13EN
{
    public class Model : IDisposable
    {
        public const string ResourcesPath = "Lab13EN.Resources";

        public string Name { get; set; }
        public string MeshPath { get; set; }
        public string TexturePath { get; set; }

        public Vector3 Position { get; set; } = new Vector3(0);
        public Quaternion Rotation { get; set; } = new Quaternion(0, 0, 0, 1);
        public Vector3 Scale { get; set; } = new Vector3(1);

        public Mesh Mesh { get; private set; }
        public Texture Texture { get; private set; }

        public Model(string name)
        {
            Name = name;
            MeshPath = $"{name}.obj";
            TexturePath = $"{name}.ppm";
            LoadMesh();
            LoadTexture();
        }

        // TODO: Stage 1a (1pt)
        // Load mesh file from resources in assembly in location $"{ResourcesPath}.{MeshPath}".
        // To read file from resources file use (example usage can be found in Texture.cs file, line 54-55):
        // https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly.getexecutingassembly?view=net-7.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly.getmanifestresourcenames?redirectedfrom=MSDN&view=netframework-4.8#System_Reflection_Assembly_GetManifestResourceNames
        // Mesh has a face list and a vertex list. Each vertex in the vertex list
        // is given an index and each face is made up of indices of three vertices.
        // Each vertex has three attributes: position, normal and texture coordinate.
        // The format of the file is simplified .obj:
        // Lines starts with either "v", "vn", "vt" or "f", followed by numbers.
        // If a line starts with "v" it is followed by 3 floats representing 3D position of the vertex
        // If a line starts with "vn" it is followed by 3 floats representing 3D normal vector of the vertex
        // If a line starts with "vt" it is followed by 2 floats representing 2D texture coordinate of the vertex
        //   Note: number of the lines starting "v", "vn", "vt" is the same, they all refer to the same vertices, they are just different attributes.
        // If line starts with "f" it is followed by 3 integers representing 3 indices from vertices list that builds single triangle
        // WARNING: Indices in obj file starts from 1! We expect indices starting from 0.
        // Create 4 lists:
        // - List of Vector3 for positions
        // - List of Vector3 for normal vectors
        // - List of Vector2 for texture coordinates
        // - List of Vector3i for triangle faces
        // and fill them accordingly.
        // At the end create new mesh as follows:
        // Mesh = new Mesh(positions, normals, texCoords, faces);
        public void LoadMesh()
        {
            
        }

        // TODO: Stage 1b (1pt)
        // Load texture file from resources in assembly in location $"{ResourcesPath}.{TexturePath}".
        // To read file from resources file use (example usage can be found in Texture.cs file, line 54-55):
        // https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly.getexecutingassembly?view=net-7.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly.getmanifestresourcenames?redirectedfrom=MSDN&view=netframework-4.8#System_Reflection_Assembly_GetManifestResourceNames
        // The format of the file is simplified .ppm text file:
        // First line contains two integers: width and height of the texture.
        // Then each line contains byte value of color component (in range 0 - 255) as follows:
        //   - red component of first pixel, 
        //   - green component of first pixel, 
        //   - blue component of first pixel, 
        //   - red component of second pixel, 
        //   - green component of second pixel, 
        //   - blue component of second pixel, 
        //   - etc. 
        // Just read first line as width and height (integers),
        // And then the rest of the lines into list of bytes.
        // Then at the end create new texture as follows:
        // Texture = new Texture(data.ToArray(), width, height);
        //   where data is the list of bytes.
        public void LoadTexture()
        {
            
        }

        public void Dispose()
        {
            Mesh?.Dispose();
            Texture?.Dispose();
            GC.SuppressFinalize(this);
        }

        //TODO: Stage 2 (0.5pt)
        // Create class ResourceNotFoundException inheriting Exception.
        // Throw this exception in LoadMesh() and LoadTexture() when resource is not found
        // (assembly.GetManifestResourceStream returned null).
        // The exception should contain the filename of resource that was not found in it's message.
        // For example if resource model.obj is not found
        // the message should be `Couldn't Find Resource "model.obj"`.
    }
}