using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach
{

    public class KillCircle
    {

        public int MousePositionX = 0;
        public int MousePositionY = 0;
        public float X;
        public float Y;
        public int Radius;
        public int Count;
        public Color _color = Color.Purple;
        public Color _color2 = Color.Black;

        public Action<Particle> OnParticleOverlap;
        public static Particle particle;

        public KillCircle(float x, float y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public void UpdateState()
        {
            if (Count <= 10) _color = Color.FromArgb(255, 0, 255, 0);
            else if (Count > 10 && Count < 50) _color = Color.FromArgb(255, 50, 205, 0);
            else if (Count >= 50 && Count < 100) _color = Color.FromArgb(255, 100, 155, 0);
            else if (Count >= 100 && Count < 200) _color = Color.FromArgb(255, 150, 105, 0);
            else if (Count >= 150 && Count < 300) _color = Color.FromArgb(255, 200, 55, 0);
            else if (Count >= 300 && Count < 500) _color = Color.FromArgb(255, 255, 0, 0);
            else _color = Color.FromArgb(255, 150, 0, 255);

            /*
             * float k = Math.Min(1f, Count / 500);
             * int thick = (int)(k * 255);
             * int cRed = thick;
             * int cGreen = 255 - thick;
             * var color = Color.FromArgb(255, cRed, cGreen, 0);
             * Не работает он((((
             */ 
        }

        public void OverlapParticle(Particle particle)
        {
            OnParticleOverlap?.Invoke(particle);
        }

        public bool OverlapsWith(Particle particle)
        {
            float X2 = particle.GetX();
            float Y2 = particle.GetY();

            double r = Math.Sqrt((X2 - X) * (X2 - X) + (Y2 - Y) * (Y2 - Y));
            return (r < Radius);
        }

        public static Random rand = new Random();

        public virtual void Draw(Graphics g)
        {
            SolidBrush SB1 = new SolidBrush(_color);
            SolidBrush SB2 = new SolidBrush(_color2);
            g.FillEllipse(SB1, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            g.DrawString(Convert.ToString(Count), new Font("Verdana", 8), SB2, X, Y);
            SB1.Dispose();
            SB2.Dispose();
        }

        public void SetColor(Color first, Color second)
        {
            _color = first;
            _color2 = second;
        }

        public void SetRadius(int radius)
        {
            Radius = radius;
        }

        public void SetCount(int count)
        {
            Count += count;
        }

        public void SetX(int x)
        {
            X = x;
        }

        public void SetY(float y)
        {
            Y = y;
        }

        public int GetRadius()
        {
            return Radius;
        }

        public float GetX()
        {
            return X;
        }

        public float GetY()
        {
            return Y;
        }
    }
}
