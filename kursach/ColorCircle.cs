using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach
{

    public class ColorCircle
    {
        public float X;
        public float Y;
        public int Radius;
        public int pRadius = 10;
        public Color _color = Color.Green;

        public Action<Particle> OnParticleOverlap;
        public static Particle particle;

        public ColorCircle(float x, float y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public void OverlapParticle(Particle particle)
        {
            OnParticleOverlap?.Invoke(particle);
        }

        public bool OverlapsWith(Particle particle)
        {
            /*
            float gX = X - particle.GetX();
            float gY = Y - particle.GetY();
            */
            float X2 = particle.GetX();
            float Y2 = particle.GetY();

            double r = Math.Sqrt((X2-X)*(X2-X)+(Y2-Y)*(Y2-Y));
            return (r < Radius + particle._radius);
        }

        public static Random rand = new Random();

        public virtual void Draw(Graphics g)
        {
            var color = _color;
            Pen Pen = new Pen(color, 3);
            g.DrawEllipse(Pen, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            Pen.Dispose();
        }
        
        public int GetRadius()
        {
            return Radius;
        }

        public int GetPrtRadius()
        {
            return pRadius;
        }

        public Color GetColor()
        {
            return _color;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        public void SetRadius(decimal _radius)
        {
            Radius = (int)_radius;
        }

        public void SetX(decimal _x)
        {
            X = (float)_x;
        }

        public void SetY(decimal _y)
        {
            Y = (float)_y;
        }
    }
}
