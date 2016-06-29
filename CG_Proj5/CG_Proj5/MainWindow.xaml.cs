using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CG_Proj5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /// 

    public class Camera
    {
        public Vector3D Position { get; set; }
        public Vector3D Target { get; set; }
    }
    public class Mesh
    {
        public string Name { get; set; }
        public Vector3D[] Vertices { get; private set; }
        public Vector3D Position { get; set; }
        public Vector3D Rotation { get; set; }

        public Mesh(string name, int verticesCount)
        {
            Vertices = new Vector3D[verticesCount];
            Name = name;
        }
    }

    public class Device
    {
        private byte[] backBuffer;
        private WriteableBitmap bmp;

        public Device(WriteableBitmap bmp)
        {
            this.bmp = bmp;
            // the back buffer size is equal to the number of pixels to draw
            // on screen (width*height) * 4 (R,G,B & Alpha values). 
            backBuffer = new byte[bmp.PixelWidth * bmp.PixelHeight * 4];
        }

        // This method is called to clear the back buffer with a specific color
        public void Clear(byte r, byte g, byte b, byte a)
        {
            for (var index = 0; index < backBuffer.Length; index += 4)
            {
                // BGRA is used by Windows instead by RGBA in HTML5
                backBuffer[index] = b;
                backBuffer[index + 1] = g;
                backBuffer[index + 2] = r;
                backBuffer[index + 3] = a;
            }
        }

        // Once everything is ready, we can flush the back buffer
        // into the front buffer. 
        public void Present()
        {
            using (var stream = bmp.)
            {
                // writing our byte[] back buffer into our WriteableBitmap stream
                stream.Write(backBuffer, 0, backBuffer.Length);
            }
            // request a redraw of the entire bitmap
           
        }

        // Called to put a pixel on screen at a specific X,Y coordinates
        public void PutPixel(int x, int y, Color4 color)
        {
            // As we have a 1-D Array for our back buffer
            // we need to know the equivalent cell in 1-D based
            // on the 2D coordinates on screen
            var index = (x + y * bmp.PixelWidth) * 4;

            backBuffer[index] = (byte)(color.Blue * 255);
            backBuffer[index + 1] = (byte)(color.Green * 255);
            backBuffer[index + 2] = (byte)(color.Red * 255);
            backBuffer[index + 3] = (byte)(color.Alpha * 255);
        }

        // Project takes some 3D coordinates and transform them
        // in 2D coordinates using the transformation matrix
        public Vector2 Project(Vector3 coord, Matrix transMat)
        {
            // transforming the coordinates
            var point = Vector3.TransformCoordinate(coord, transMat);
            // The transformed coordinates will be based on coordinate system
            // starting on the center of the screen. But drawing on screen normally starts
            // from top left. We then need to transform them again to have x:0, y:0 on top left.
            var x = point.X * bmp.PixelWidth + bmp.PixelWidth / 2.0f;
            var y = -point.Y * bmp.PixelHeight + bmp.PixelHeight / 2.0f;
            return (new Vector2(x, y));
        }

        // DrawPoint calls PutPixel but does the clipping operation before
        public void DrawPoint(Vector2 point)
        {
            // Clipping what's visible on screen
            if (point.X >= 0 && point.Y >= 0 && point.X < bmp.PixelWidth && point.Y < bmp.PixelHeight)
            {
                // Drawing a yellow point
                PutPixel((int)point.X, (int)point.Y, new Color4(1.0f, 1.0f, 0.0f, 1.0f));
            }
        }

        // The main method of the engine that re-compute each vertex projection
        // during each frame
        public void Render(Camera camera, params Mesh[] meshes)
        {
            // To understand this part, please read the prerequisites resources
            var viewMatrix = Matrix.LookAtLH(camera.Position, camera.Target, Vector3.UnitY);
            var projectionMatrix = Matrix.PerspectiveFovRH(0.78f,
                                                           (float)bmp.PixelWidth / bmp.PixelHeight,
                                                           0.01f, 1.0f);

            foreach (Mesh mesh in meshes)
            {
                // Beware to apply rotation before translation 
                var worldMatrix = Matrix.RotationYawPitchRoll(mesh.Rotation.Y,
                                                              mesh.Rotation.X, mesh.Rotation.Z) *
                                  Matrix.Translation(mesh.Position);

                var transformMatrix = worldMatrix * viewMatrix * projectionMatrix;

                foreach (var vertex in mesh.Vertices)
                {
                    // First, we project the 3D coordinates into the 2D space
                    var point = Project(vertex, transformMatrix);
                    // Then we can draw on screen
                    DrawPoint(point);
                }
            }
        }
    }

    public partial class MainWindow : Window
    {
        private Vector3D currCameraLoc;
        private List<Vector3D> currObject; 

        public MainWindow()
        {
            InitializeComponent();
            currCameraLoc = new Vector3D(20,20,20);
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void leftButton_rot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void upButton_rot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rightButton_rot_Click(object sender, RoutedEventArgs e)
        {

        }

        //Sphere
        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        //Cone
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        //Cube
        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            var mesh = new Mesh("Cube", 8);
            mesh.Vertices[0] = new Vector3D(-1, 1, 1);
            mesh.Vertices[1] = new Vector3D(1, 1, 1);
            mesh.Vertices[2] = new Vector3D(-1, -1, 1);
            mesh.Vertices[3] = new Vector3D(-1, -1, -1);
            mesh.Vertices[4] = new Vector3D(-1, 1, -1);
            mesh.Vertices[5] = new Vector3D(1, 1, -1);
            mesh.Vertices[6] = new Vector3D(1, -1, 1);
            mesh.Vertices[7] = new Vector3D(1, -1, -1);
        }

        //Cyllinder
        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
