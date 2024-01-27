
using OpenTK.Graphics.OpenGL;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK3_StandardTemplate_WinForms
{
    internal class Sphere3D
    {
       
        public float x, y, z, size;
        Color color;
        

        public Sphere3D()
        {
            x = y = z = 1;
            size = 1;
            this.color = Color.DarkRed;

        }

        public Sphere3D(float x, float y, float z, float size, Color color)
        {
            this.color = color;

            this.x = x;
            this.y = y;
            this.z = z;
            this.size = size;
          
        }
        public void setXYZ(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void DrawSphere()
        {
            const int slices = 30;
            const int stacks = 30;

            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < stacks; i++)
            {
                double lat0 = Math.PI * (-0.5 + (double)(i) / stacks);
                double z0 = Math.Sin(lat0) * size;
                double zr0 = Math.Cos(lat0) * size;

                double lat1 = Math.PI * (-0.5 + (double)(i + 1) / stacks);
                double z1 = Math.Sin(lat1) * size;
                double zr1 = Math.Cos(lat1) * size;

                for (int j = 0; j < slices; j++)
                {
                    double lng0 = 2 * Math.PI * (double)(j) / slices;
                    double x0 = Math.Cos(lng0) * zr0;
                    double y0 = Math.Sin(lng0) * zr0;

                    double lng1 = 2 * Math.PI * (double)(j + 1) / slices;
                    double x1 = Math.Cos(lng1) * zr0;
                    double y1 = Math.Sin(lng1) * zr0;

                    double x2 = Math.Cos(lng1) * zr1;
                    double y2 = Math.Sin(lng1) * zr1;

                    double x3 = Math.Cos(lng0) * zr1;
                    double y3 = Math.Sin(lng0) * zr1;

                    // Draw the quad
                    GL.Vertex3(x0 + x, y0 + y, z0 + z);
                    GL.Vertex3(x1 + x, y1 + y, z0 + z);
                    GL.Vertex3(x2 + x, y2 + y, z1 + z);
                    GL.Vertex3(x3 + x, y3 + y, z1 + z);
                }
            }
            GL.Color3(color);

            GL.End();
        }

    }
}
