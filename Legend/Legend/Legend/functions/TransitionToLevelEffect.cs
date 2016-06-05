using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.levels.functions
{
    public class TransitionToLevelEffect
    {
        Vector3 rand;
        Color color;
        Random random = new Random();
        Vector2 posrand;
        VisualizationData data = new VisualizationData();
        List<float> freqs = new List<float>();
        float rotation = 0.006f;
        float scale = 0f;
        Vector2 toPortal = Vector2.Zero;

        Texture2D pixel;

        public TransitionToLevelEffect()
        {
            pixel = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });


            rand.X = random.Next(1, 20);
            rand.Y = random.Next(1, 20);
            rand.Z = random.Next(1, 20);
        }

        public void Reset(bool initialize)
        {
            if (initialize)
            {
                Game1.toinitialize = true;
            }
            Game1.resetRend = true;
            Game1.rendColor = Color.Black;
            Camera.Main.Offset = Vector2.Zero;
            Camera.Main.Rotation = (float) Math.PI;
            Camera.Main.Scale = Vector2.One;
            rotation = 0;
            scale = 0;
        }

        public void Update()
        {
            freqs.Clear();
            float biggest = float.MinValue;
            MediaPlayer.GetVisualizationData(data);
            for (int i = 0; i < data.Frequencies.Count; i++)
            {
                float avg = 0;
                for(int ii = 0; ii < data.Frequencies.Count/4 && i < data.Frequencies.Count; i++, ii++)
                {
                    avg += data.Frequencies[i];
                }
                avg /= data.Frequencies.Count / 4;
                if (avg > biggest)
                {
                    biggest = avg;
                }
                freqs.Add(avg);
            }
            for(int i = 0; i < freqs.Count; i++)
            {
                freqs[i] /= biggest;
            }


            if (Game1.transitioneffect)
            {
                rand.X = random.Next(1, 10);
                rand.Y = random.Next(1, 10);
                rand.Z = random.Next(1, 10);
                color = Game1.rendColor;
                Game1.rendColor = new Color((int)(rand.X + color.R), (int)(rand.Y + color.G), (int)(rand.Z + color.B));
                if (Game1.rendColor.R >= 255 - Game1.rendOffset)
                {
                    Game1.rendColor.R = 0;
                }
                if (Game1.rendColor.G >= 255 - Game1.rendOffset)
                {
                    Game1.rendColor.G = 0;
                }
                if (Game1.rendColor.B >= 255 - Game1.rendOffset)
                {
                    Game1.rendColor.B = 0;
                }
                MediaPlayer.Volume = (255 - Game1.rendOffset + float.Epsilon)/255;
                Vector2 normalpos = Vector2.Zero;
                posrand.X = random.Next(-25, 25);
                posrand.Y = random.Next(-25, 25);
                toPortal = Vector2.Lerp(toPortal, Game1.levellist[Game1.level - 1].portalobj.Position, 0.03f);
                Camera.Main.Offset = new Vector2(normalpos.X + posrand.X, normalpos.Y + posrand.Y);
                rotation += 0.00015f;
                Camera.Main.Rotation += rotation;
                scale += 0.002f;
                Camera.Main.Scale = Vector2.One-new Vector2(scale);
            }
            else if (Camera.Main.Offset != Vector2.Zero)
            {
                Camera.Main.Offset = new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width, Game1.graphics.GraphicsDevice.Viewport.Height) / 2;
                Game1.rendColor = Color.White;
            }
            
        }
    }
}
