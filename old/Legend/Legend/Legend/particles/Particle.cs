using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.particles
{
    public class Particle
    {
        Texture2D particleTxt;
        float startSize;
        public Color color;
        Vector2 speed;
        float rotationSpeed;
        float drag;
        Random rand;
        Vector2 position;
        float rotation = 0f;
        public TimeSpan life;
        Vector2 origin;

        public Particle(Texture2D particleTxt, Vector2 startSize, Color color, Vector2 speedX, Vector2 speedY, Vector2 rotation, Vector2 drag, Vector2 position)
        {
            rand = Game1.rand;
            this.particleTxt = particleTxt;
            this.startSize = (float)(rand.Next((int)startSize.X, (int)startSize.Y) + rand.NextDouble());
            this.color = color;
            this.speed = new Vector2((float)(rand.Next((int)speedX.X, (int)speedX.Y)+rand.NextDouble()), (float)(rand.Next((int)speedY.X, (int)speedY.Y)+rand.NextDouble()));
            this.rotationSpeed = ((float)(rand.Next((int)rotation.X, (int)rotation.Y) + rand.NextDouble())) / 10;
            this.drag = (float)(rand.Next((int)(drag.X*100), (int)(drag.Y*100)))/100;
            this.position = position;
            this.origin = new Vector2((particleTxt.Width) / 2, (particleTxt.Height) / 2);
        }

        public void Update(GameTime gameTime)
        {
            life += gameTime.ElapsedGameTime;
            rotation += rotationSpeed;
            position += speed;
            speed.X /= drag;
            speed.Y /= drag;
            rotationSpeed /= drag;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(particleTxt, position * Settings.Scale, null, color, rotation, origin, startSize * Settings.Scale, SpriteEffects.None, 0.89f);
        }
    }
}
