using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach
{
    public partial class Main : Form
    {

        public static ColorCircle colorCircle;
        public static KillCircle killCircle;

        public Main()
        {
            InitializeComponent();
            canvas.Image = new Bitmap(canvas.Width, canvas.Height); // привязка изображения
            InitColorCircle();
            InitKillCircle();
        }

        Emitter emitter = new Emitter();

        private void InitColorCircle()
        {
            colorCircle = new ColorCircle(
                canvas.Image.Width / 2,
                canvas.Image.Height / 2,
                50
                );

            colorCircle.OnParticleOverlap += (prt) =>
            {
                (prt as Particle).SetColor(colorCircle._color);
            };
        }

        public void InitKillCircle()
        {

            killCircle = new KillCircle(
                canvas.Image.Width * 2,
                canvas.Image.Height * 2,
                50
                );

            killCircle.OnParticleOverlap += (prt) =>
            {
                prt.SetLife(0);
                killCircle.SetCount(1);
            };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();
            killCircle.UpdateState();

            using (var g = Graphics.FromImage(canvas.Image))
            {
                g.Clear(Color.White);

                /*
                foreach(var particle in Particle)
                {
                    // проверяю было ли пересечение с игроком
                    if (ColorCircle.OverlapsWith(Particle)
                    {
                        // и если было вывожу информацию на форму
                        txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
                    }
                }
                */

                foreach (var particle in emitter.particles.ToList())
                {
                    if (colorCircle.OverlapsWith(particle))
                    {
                        colorCircle.OverlapParticle(particle);
                    }

                    if (killCircle.OverlapsWith(particle))
                    {
                        killCircle.OverlapParticle(particle);
                    }
                }

                emitter.Render(g);
                colorCircle.Draw(g);
                killCircle.Draw(g);
            }

            canvas.Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            colorCircle.SetRadius(trackBar1.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            colorCircle.SetX(trackBar2.Value);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            colorCircle.SetY(trackBar3.Value);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                colorCircle.SetColor(colorDialog1.Color);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            double r = Math.Sqrt((e.X - killCircle.GetX()) * (e.X - killCircle.GetX()) + (e.Y - killCircle.GetY()) * (e.Y - killCircle.GetY()));

            if (e.Button == MouseButtons.Left)
            {
                killCircle.SetX(e.X);
                killCircle.SetY(e.Y);
                killCircle.SetColor(Color.Purple, Color.Black);
                killCircle.SetRadius(50);
                killCircle.Count = 0;
            }
            else
            {
                if ((e.Button == MouseButtons.Right)&&(r <= killCircle.GetRadius()))
                {
                    killCircle.SetColor(Color.White, Color.White);
                    killCircle.SetRadius(0);
                    killCircle.Count = 0;
                    killCircle.SetX(-10);
                    killCircle.SetY(-10);
                }
                else
                {
                    if ((e.Button == MouseButtons.Middle) && (r <= killCircle.GetRadius()))
                    {
                        killCircle.Count = 0;
                    }
                }
            }
        }
        private void canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            double r = Math.Sqrt((e.X - killCircle.GetX()) * (e.X - killCircle.GetX()) + (e.Y - killCircle.GetY()) * (e.Y - killCircle.GetY()));
            {
                if ((e.Delta > 0)&&(r <= killCircle.GetRadius())&&(killCircle.Radius + 5 < 200)) killCircle.Radius += 5;
                else
                {
                    if ((killCircle.Radius - 5 > 0) && (r <= killCircle.GetRadius())) killCircle.Radius -= 5;
                }
            }
        }

        /*
        public virtual GraphicsPath GetGraphicsPath()
        {
            // пока возвращаем пустую форму
            return new GraphicsPath();
        }

        public virtual bool Overlaps(Particle particle, Graphics g)
        {
            // берем информацию о форме
            var path1 = this.GetGraphicsPath();
            var path2 = particle.GetGraphicsPath();

            // применяем к объектам матрицы трансформации
            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            // используем класс Region, который позволяет определить 
            // пересечение объектов в данном графическом контексте
            var region = new Region(path1);
            region.Intersect(path2); // пересекаем формы
            return !region.IsEmpty(g); // если полученная форма не пуста то значит было пересечение
        }
        */

    }
}
