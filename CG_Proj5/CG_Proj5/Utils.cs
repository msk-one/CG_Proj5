using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using SharpDX;

namespace CG_Proj5
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
    }
    public struct Face
    {
        public int v1;
        public int v2;
        public int v3;
    }
    public class Mesh
    {
        public Vector3[] Vertices { get; private set; }
        public Face[] Faces { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public Mesh(int verticesCount, int facesCount)
        {
            Vertices = new Vector3[verticesCount];
            Faces = new Face[facesCount];
        }
    }

    public class Screen
    {
        private byte[] _buffer;
        private WriteableBitmap wBmp;

        public Screen(WriteableBitmap wBmp)
        {
            this.wBmp = wBmp;
            _buffer = new byte[wBmp.PixelWidth * wBmp.PixelHeight * 4];
        }

        public void DrawBresenham(Vector2 point0, Vector2 point1)
        {
            int x0 = (int)point0.X;
            int y0 = (int)point0.Y;
            int x1 = (int)point1.X;
            int y1 = (int)point1.Y;

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            var sx = (x0 < x1) ? 1 : -1;
            var sy = (y0 < y1) ? 1 : -1;
            var err = dx - dy;

            while (true)
            {
                DrawPoint(new Vector2(x0, y0));

                if ((x0 == x1) && (y0 == y1)) break;
                var e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x0 += sx; }
                if (e2 < dx) { err += dx; y0 += sy; }
            }
        }

        public void Clear(byte r, byte g, byte b, byte a)
        {
            for (var index = 0; index < _buffer.Length; index += 4)
            {
                _buffer[index] = b;
                _buffer[index + 1] = g;
                _buffer[index + 2] = r;
                _buffer[index + 3] = a;
            }
        }

        public void Show()
        {
            wBmp.Lock();
            wBmp.FromByteArray(_buffer, 0, _buffer.Length);
            wBmp.Unlock();
        }

        public void PutPixel(int x, int y, Color4 color)
        {
            var index = (x + y * wBmp.PixelWidth) * 4;
            _buffer[index] = (byte)(color.Blue * 255);
            _buffer[index + 1] = (byte)(color.Green * 255);
            _buffer[index + 2] = (byte)(color.Red * 255);
            _buffer[index + 3] = (byte)(color.Alpha * 255);
        }

        public Vector2 Project(Vector3 coord, Matrix transMat)
        {
            var point = Vector3.TransformCoordinate(coord, transMat);
            var x = point.X * wBmp.PixelWidth + wBmp.PixelWidth / 2.0f;
            var y = -point.Y * wBmp.PixelHeight + wBmp.PixelHeight / 2.0f;
            return (new Vector2(x, y));
        }

        public void DrawPoint(Vector2 point)
        {
            if (point.X >= 0 && point.Y >= 0 && point.X < wBmp.PixelWidth && point.Y < wBmp.PixelHeight)
            {
                PutPixel((int)point.X, (int)point.Y, new Color4(0.0f, 1.0f, 0.0f, 1.0f));
            }
        }

        public static Matrix TranslationMatrix(Vector3 T)
        {
            return new Matrix(1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, (float)T.X,
                (float)T.Y, (float)T.Z, 1.0f);
        }

        public static Matrix ScalingMatrix(Vector3 S)
        {
            return new Matrix((float)S.X, 0.0f, 0.0f, 0.0f, 0.0f, (float)S.Y, 0.0f, 0.0f, 0.0f, 0.0f, (float)S.Z, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);
        }

        public static Matrix ProjectionMatrix()
        {
            var m = Matrix.Identity;
            float aOV = 60;
            float N = 1.0f;
            float F = 100;

            float scale = (float)(1 / Math.Tan(aOV * 0.5 * Math.PI/180));
            m.M11 = scale;
            m.M22 = scale;

            m.M33 = -F / (F - N);
            m.M34 = -1;

            m.M43 = -F * N / (F - N);
            m.M44 = 0;

            return m;
        }

        public void Render(Camera camera, params Mesh[] meshes)
        {
            var viewMatrix = Matrix.LookAtLH(camera.Position, camera.Target, Vector3.UnitY);
            var projectionMatrix = ProjectionMatrix();

            foreach (Mesh mesh in meshes)
            {
                var worldMatrix = Matrix.RotationYawPitchRoll(mesh.Rotation.Y, mesh.Rotation.X, mesh.Rotation.Z)*TranslationMatrix(mesh.Position);
                var transformMatrix = worldMatrix*viewMatrix*projectionMatrix;

                foreach (var vertex in mesh.Vertices)
                {
                    var point = Project(vertex, transformMatrix);
                    DrawPoint(point);
                }

                foreach (var face in mesh.Faces)
                {
                    var vA = mesh.Vertices[face.v1];
                    var vB = mesh.Vertices[face.v2];
                    var vC = mesh.Vertices[face.v3];
                    var pA = Project(vA, transformMatrix);
                    var pB = Project(vB, transformMatrix);
                    var pC = Project(vC, transformMatrix);

                    DrawBresenham(pA, pB);
                    DrawBresenham(pB, pC);
                    DrawBresenham(pC, pA);
                }
            }
        }
    }
}
