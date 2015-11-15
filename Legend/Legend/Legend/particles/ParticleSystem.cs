using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Legend.particles
{
    public class ParticleSystem
    {
        public Texture2D particleTxt;
        public Vector2 startSize;
        public Color color;
        public Vector2 speedX;
        public Vector2 speedY;
        public Vector2 rotationSpeed;
        public TimeSpan lifetime;
        public Vector2 drag;
        public Vector2 position;
        public TimeSpan SpawnTime;
        bool fadeOut;
        float test;
        TimeSpan Timer = new TimeSpan();

        public List<Particle> particles = new List<Particle>();
        
        public ParticleSystem(Texture2D particleTxt, float minStartSize, float maxStartSize, Color color, Vector2 speedX, Vector2 speedY, TimeSpan lifetime, float minDrag, float maxDrag, float minRotation, float maxRotation, Vector2 position, TimeSpan spawnTime, bool fadeOut)
        {
            this.particleTxt = particleTxt;
            this.startSize = new Vector2(minStartSize, maxStartSize);
            this.color = color;
            this.speedX = speedX;
            this.speedY = speedY;
            this.lifetime = lifetime;
            this.drag = new Vector2(minDrag, maxDrag);
            this.rotationSpeed = new Vector2(minRotation, maxRotation);
            this.SpawnTime = spawnTime;
            this.position = position;
            this.fadeOut = fadeOut;
            if (fadeOut)
            {
                if (lifetime.TotalSeconds <= 3)
                {
                    test = .0000075f;
                }
                else
                {
                    test = 7500000f;
                }
            }
        }

        public ParticleSystem(Texture2D particleTxt, float startSize, Color color, Vector2 speedX, Vector2 speedY, TimeSpan lifetime, float drag, float rotation, Vector2 position, TimeSpan spawnTime, bool fadeOut)
            :this(particleTxt, startSize, startSize, color, speedX, speedY, lifetime, drag, drag, rotation, rotation, position, spawnTime, fadeOut)
        {
        }

        public void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime;
            if (Timer > SpawnTime)
            {
                Timer = new TimeSpan(0);
                particles.Add(new Particle(particleTxt, startSize, color, speedX, speedY, rotationSpeed, drag, position));
            }
            for (int i = 0; i < particles.Count; i++)
            {
                if (fadeOut)
                {
                    if (lifetime.TotalSeconds <= 3)
                    {
                        particles[i].color = Color.Lerp(particles[i].color, Color.Transparent, (float)lifetime.TotalMilliseconds * test);
                    }
                    else
                    {
                        particles[i].color = Color.Lerp(particles[i].color, Color.Transparent, (float)lifetime.TotalMilliseconds / test);
                    }
                }
                particles[i].Update(gameTime);
                if (particles[i].life >= lifetime)
                {
                    particles.Remove(particles[i]);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle part in particles)
            {
                part.Draw(spriteBatch);
            }
        }

        public void addParticle()
        {
            particles.Add(new Particle(particleTxt, startSize, color, speedX, speedY, rotationSpeed, drag, position));
        }
    }
}
