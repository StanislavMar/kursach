using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading.Tasks;

namespace kursach
{
    class Emitter
    {
        public List<Particle> particles = new List<Particle>();

        Random rand = new Random();
        public int j;
        public int MousePositionX = 0;
        public int MousePositionY = 0;
        public float GravitationX = 0;
        public float GravitationY = 0.1f;
        public float _wind = 0f; // ветер
        public int _direction = 0;
        public int _spreading = 45;
        public int _speedMin = 1;
        public int _speedMax = 2;
        public int _radiusMin = 1;
        public int _radiusMax = 25;
        public int _lifeMin = 40;
        public int _lifeMax = 120;
        public int _particlesPerTick = 1;

        public virtual void ResetParticle(Particle particle)
        {
            particle._life = Particle.rand.Next(_lifeMin, _lifeMax);

            particle._x = rand.Next(0, 775);
            particle._y = 380;

            var direction = _direction
                + (double)Particle.rand.Next(_spreading)
                - _spreading / 2;

            var speed = Particle.rand.Next(_speedMin, _speedMax);

            particle._speedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle._speedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle._radius = Particle.rand.Next(_radiusMin, _radiusMax);
        }

        public void UpdateState()
        {
            int particlesToCreate = _particlesPerTick;


            foreach (var particle in particles)
            {
                particle._life -= 1;
                if (particle._life < 0)
                {
                    particle._life = 20 + Particle.rand.Next(100);
                    particle._x = rand.Next(0, 775);
                    particle._y = 380;
                    var _speed = 1 + Particle.rand.Next(10);
                    particle._speedX = (float)(Math.Cos(_direction / 180 * Math.PI) * _speed) - 5;
                    particle._speedY = (float)(Math.Sin(_direction / 180 * Math.PI) * _speed);
                    particle._radius = 2 + Particle.rand.Next(10);
                    particle._color = Color.Orange; //particle.GetColor();
                    if(particlesToCreate > 0)
                    {
                        particlesToCreate -= 1;
                        ResetParticle(particle);
                    }
                }
                else
                {
                    particle._speedX += (float)(GravitationX + _wind);
                    particle._speedY -= GravitationY;
                    particle._x += particle._speedX;
                    particle._y += particle._speedY;
                }
            }

            for (var i = 0; i < 10; ++i) // _amount
            {
                if (particles.Count < 1000)
                {
                    var particle = new Particle();
                    particles.Add(particle);
                    particle._x = rand.Next(0, 775);
                    particle._y = 0;
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
            /*while(particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }*/

        }

        public void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);

            }
        }
    }
}
