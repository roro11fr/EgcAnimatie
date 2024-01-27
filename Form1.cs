using System;
using System.Drawing;
using System.Windows.Forms;


using OpenTK.Graphics.OpenGL;

using OpenTK3_StandardTemplate_WinForms.helpers;
using OpenTK3_StandardTemplate_WinForms.objects;


namespace OpenTK3_StandardTemplate_WinForms
{
    public partial class MainForm : Form
    {
        private Axes mainAxis;
        private Rectangles re;
        private Camera cam;
        private Scene scene;
        private Sphere3D planet;
        private Point mousePosition;
        private Sphere3D planet3;
        private Sphere3D planet2;
        private float angle = 0.0f;
        private float angle1 = 0.0f;
        private float radius = 10f;
        Timer animationTimer;
        TextureFromBMP texture;
        public MainForm()
        {
            // general init
            InitializeComponent();

            // init VIEWPORT
            scene = new Scene();

            scene.GetViewport().Load += new EventHandler(this.mainViewport_Load);
            scene.GetViewport().Paint += new PaintEventHandler(this.mainViewport_Paint);
            scene.GetViewport().MouseMove += new MouseEventHandler(this.mainViewport_MouseMove);


            this.Controls.Add(scene.GetViewport());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // init RNG
            Randomizer.Init();

            // init CAMERA/EYE
            cam = new Camera(scene.GetViewport());

            // init AXESs
            mainAxis = new Axes(showAxes.Checked);
            re = new Rectangles();
          

            planet = new Sphere3D(1, 20, 20, 1, Color.Red);
            planet3 = new Sphere3D( 1, 20, 20,2,Color.Yellow);
            planet2 = new Sphere3D(1, 20, 20, 0.5f, Color.Black);

            animationTimer = new Timer();
            animationTimer.Interval = 16; // Update roughly every 16 milliseconds (60 FPS)
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void showAxes_CheckedChanged(object sender, EventArgs e)
        {
            mainAxis.SetVisibility(showAxes.Checked);

            scene.Invalidate();
        }

        private void changeBackground_Click(object sender, EventArgs e)
        {
            GL.ClearColor(Randomizer.GetRandomColor());

            scene.Invalidate();
        }

        private void resetScene_Click(object sender, EventArgs e)
        {
            showAxes.Checked = true;
            mainAxis.SetVisibility(showAxes.Checked);
            scene.Reset();
            cam.Reset();

            scene.Invalidate();
        }

        private void mainViewport_Load(object sender, EventArgs e)
        {

            scene.Reset();
        }

        private void mainViewport_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = new Point(e.X, e.Y);
            scene.Invalidate();
        }

        private void mainViewport_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            cam.SetView();

            if (enableRotation.Checked == true)
            {
                // Doar după axa Ox.
                GL.Rotate(Math.Max(mousePosition.X, mousePosition.Y), 1, 1, 1);
            }

            // GRAPHICS PAYLOAD
            mainAxis.Draw();



            if (enableAnimation.Checked)
            {
                animationTimer.Start();
                float xPlaneta = radius * (float)Math.Cos(angle);
                float yPlaneta = radius * (float)Math.Sin(angle);

                // Draw the first cube
                GL.PushMatrix();
                GL.Translate(xPlaneta, yPlaneta, 0);
                planet.DrawSphere();
                GL.PopMatrix();

                // Draw the second cube
                GL.PushMatrix();
                float x = 5 * (float)Math.Cos(angle1);
                float y = 5 * (float)Math.Sin(angle1);
                planet2.setXYZ(planet.x + xPlaneta, planet.y + yPlaneta, planet.z);
                GL.Translate(x, y, 0);
                planet2.DrawSphere();
                GL.PopMatrix();

                // Draw the third cube
                planet3.DrawSphere();
            }
            else
            {
                animationTimer.Stop();
            }
            if (enableObjectRotation.Checked == true)
            {
                // Rotatie a obiectului
                GL.PushMatrix();
                GL.Rotate(Math.Max(mousePosition.X, mousePosition.Y), 1, 1, 1);
                re.Draw();
                GL.PopMatrix();
            }
            else
            {
                re.Draw();
            }

            scene.GetViewport().SwapBuffers();
        }

        private void enableAnimation_CheckedChanged(object sender, EventArgs e)
        {
            scene.Invalidate();
        }
        private float deltaTime = 0.05f;
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Update animation variables here

            angle += deltaTime;
            angle1 += deltaTime * 2;


            // Redraw the scene
            scene.Invalidate();
        }

      
    }
}
