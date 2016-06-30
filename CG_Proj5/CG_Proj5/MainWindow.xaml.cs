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
using SharpDX;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CG_Proj5
{

    public partial class MainWindow : Window
    {
        public WriteableBitmap writeableBmp;
        private Screen _screen;
        private Mesh mesh;
        private Camera camera;

        public MainWindow()
        {
            InitializeComponent();
            //currCameraLoc = new Vector3(20,20,20);
            writeableBmp = BitmapFactory.New(850, 726);
            mesh = new Mesh(8, 12);
            
            camera = new Camera();
            _screen = new Screen(writeableBmp);

            mainImage.Source = writeableBmp;

            mesh.Vertices[0] = new Vector3(-1, 1, 1);
            mesh.Vertices[1] = new Vector3(1, 1, 1);
            mesh.Vertices[2] = new Vector3(-1, -1, 1);
            mesh.Vertices[3] = new Vector3(1, -1, 1);
            mesh.Vertices[4] = new Vector3(-1, 1, -1);
            mesh.Vertices[5] = new Vector3(1, 1, -1);
            mesh.Vertices[6] = new Vector3(1, -1, -1);
            mesh.Vertices[7] = new Vector3(-1, -1, -1);

            mesh.Faces[0] = new Face { v1 = 0, v2 = 1, v3 = 2 };
            mesh.Faces[1] = new Face { v1 = 1, v2 = 2, v3 = 3 };
            mesh.Faces[2] = new Face { v1 = 1, v2 = 3, v3 = 6 };
            mesh.Faces[3] = new Face { v1 = 1, v2 = 5, v3 = 6 };
            mesh.Faces[4] = new Face { v1 = 0, v2 = 1, v3 = 4 };
            mesh.Faces[5] = new Face { v1 = 1, v2 = 4, v3 = 5 };

            mesh.Faces[6] = new Face { v1 = 2, v2 = 3, v3 = 7 };
            mesh.Faces[7] = new Face { v1 = 3, v2 = 6, v3 = 7 };
            mesh.Faces[8] = new Face { v1 = 0, v2 = 2, v3 = 7 };
            mesh.Faces[9] = new Face { v1 = 0, v2 = 4, v3 = 7 };
            mesh.Faces[10] = new Face { v1 = 4, v2 = 5, v3 = 6 };
            mesh.Faces[11] = new Face { v1 = 4, v2 = 6, v3 = 7 };

            camera.Position = new Vector3(0, 0, 10.0f);
            camera.Target = Vector3.Zero;

        }


        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Position = new Vector3(mesh.Position.X, mesh.Position.Y, mesh.Position.Z - 0.25f);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Position = new Vector3(mesh.Position.X, mesh.Position.Y, mesh.Position.Z + 0.25f);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void leftButton_rot_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y - 0.11f, mesh.Rotation.Z);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void upButton_rot_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Rotation = new Vector3(mesh.Rotation.X + 0.11f, mesh.Rotation.Y, mesh.Rotation.Z);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void rightButton_rot_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y + 0.11f, mesh.Rotation.Z);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void downButton_rot_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Rotation = new Vector3(mesh.Rotation.X - 0.11f, mesh.Rotation.Y, mesh.Rotation.Z);
            _screen.Render(camera, mesh);
            _screen.Present();
        }


        //Cone
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            mesh = new Mesh(7, 6);
            mesh.Vertices[0] = new Vector3(0, 2, 0);
            mesh.Vertices[1] = new Vector3(-1, -1, 1);
            mesh.Vertices[2] = new Vector3(-2, -1, 0);
            mesh.Vertices[3] = new Vector3(-1, -1, -1);
            mesh.Vertices[4] = new Vector3(1, -1, -1);
            mesh.Vertices[5] = new Vector3(2, -1, 0);
            mesh.Vertices[6] = new Vector3(1, -1, 1);

            mesh.Faces[0] = new Face { v1 = 0, v2 = 1, v3 = 6 };
            mesh.Faces[1] = new Face { v1 = 2, v2 = 1, v3 = 0 };
            mesh.Faces[2] = new Face { v1 = 2, v2 = 3, v3 = 0 };
            mesh.Faces[3] = new Face { v1 = 3, v2 = 4, v3 = 0 };
            mesh.Faces[4] = new Face { v1 = 4, v2 = 5, v3 = 0 };
            mesh.Faces[5] = new Face { v1 = 5, v2 = 6, v3 = 0 };

            _screen.Clear(0, 0, 0, 255);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        //Cube
        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            mesh = new Mesh(8, 12);
            mesh.Vertices[0] = new Vector3(-1, 1, 1);
            mesh.Vertices[1] = new Vector3(1, 1, 1);
            mesh.Vertices[2] = new Vector3(-1, -1, 1);
            mesh.Vertices[3] = new Vector3(1, -1, 1);
            mesh.Vertices[4] = new Vector3(-1, 1, -1);
            mesh.Vertices[5] = new Vector3(1, 1, -1);
            mesh.Vertices[6] = new Vector3(1, -1, -1);
            mesh.Vertices[7] = new Vector3(-1, -1, -1);

            mesh.Faces[0] = new Face { v1 = 0, v2 = 1, v3 = 2 };
            mesh.Faces[1] = new Face { v1 = 1, v2 = 2, v3 = 3 };
            mesh.Faces[2] = new Face { v1 = 1, v2 = 3, v3 = 6 };
            mesh.Faces[3] = new Face { v1 = 1, v2 = 5, v3 = 6 };
            mesh.Faces[4] = new Face { v1 = 0, v2 = 1, v3 = 4 };
            mesh.Faces[5] = new Face { v1 = 1, v2 = 4, v3 = 5 };
            mesh.Faces[6] = new Face { v1 = 2, v2 = 3, v3 = 7 };
            mesh.Faces[7] = new Face { v1 = 3, v2 = 6, v3 = 7 };
            mesh.Faces[8] = new Face { v1 = 0, v2 = 2, v3 = 7 };
            mesh.Faces[9] = new Face { v1 = 0, v2 = 4, v3 = 7 };
            mesh.Faces[10] = new Face { v1 = 4, v2 = 5, v3 = 6 };
            mesh.Faces[11] = new Face { v1 = 4, v2 = 6, v3 = 7 };

            _screen.Clear(0, 0, 0, 255);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void zUpButton_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y, mesh.Rotation.Z + 0.11f);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void zDownButton_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y, mesh.Rotation.Z - 0.11f);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void moveLeft_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Position = new Vector3(mesh.Position.X - 0.15f, mesh.Position.Y, mesh.Position.Z);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void moveRight_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Position = new Vector3(mesh.Position.X + 0.15f, mesh.Position.Y, mesh.Position.Z);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void moveUp_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Position = new Vector3(mesh.Position.X, mesh.Position.Y - 0.15f, mesh.Position.Z);
            _screen.Render(camera, mesh);
            _screen.Present();
        }

        private void moveDown_Click(object sender, RoutedEventArgs e)
        {
            _screen.Clear(0, 0, 0, 255);
            mesh.Position = new Vector3(mesh.Position.X, mesh.Position.Y + 0.15f, mesh.Position.Z);
            _screen.Render(camera, mesh);
            _screen.Present();
        }
    }
}
