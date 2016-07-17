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
        //VisualizationData data = new VisualizationData();
        List<float> freqs = new List<float>();
        float rotation = 0.006f;
        float scale = 0f;
        Vector2 toPortal = Vector2.Zero;
        Vector2 distanceFromCenter;
        float speed = 0.001f;

        Texture2D pixel;

        public TransitionToLevelEffect()
        {
            pixel = new Texture2D(GameApplication.graphics.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });


            rand.X = random.Next(1, 20);
            rand.Y = random.Next(1, 20);
            rand.Z = random.Next(1, 20);
        }

        public void Reset(bool initialize)
        {
            if (initialize)
            {
                GameApplication.toinitialize = true;
            }
            GameApplication.resetRend = true;
            GameApplication.rendColor = Color.Black;
            Camera.Main.Offset = Vector2.Zero;
            Camera.Main.Rotation = (float) Math.PI;
            Camera.Main.Scale = Vector2.One;
            rotation = 0;
            scale = 0;
            toPortal = Vector2.Zero;
            distanceFromCenter = Vector2.Zero;
            speed = 0.001f;
        }

        public void Update()
        {
            /*freqs.Clear();
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
            }*/


            if (GameApplication.transitioneffect)
            {
                rand.X = random.Next(1, 10);
                rand.Y = random.Next(1, 10);
                rand.Z = random.Next(1, 10);
                color = GameApplication.rendColor;
                GameApplication.rendColor = new Color((int)(rand.X + color.R), (int)(rand.Y + color.G), (int)(rand.Z + color.B));
                if (GameApplication.rendColor.R >= 255 - GameApplication.rendOffset)
                {
                    GameApplication.rendColor.R = 0;
                }
                if (GameApplication.rendColor.G >= 255 - GameApplication.rendOffset)
                {
                    GameApplication.rendColor.G = 0;
                }
                if (GameApplication.rendColor.B >= 255 - GameApplication.rendOffset)
                {
                    GameApplication.rendColor.B = 0;
                }
                Microsoft.Xna.Framework.Media.MediaPlayer.Volume = (255 - GameApplication.rendOffset + float.Epsilon)/255;
                posrand.X = random.Next(-25, 25);
                posrand.Y = random.Next(-25, 25);
                speed += 0.00025f;
                distanceFromCenter = GameApplication.levellist[GameApplication.level - 1].portalobj.Position - new Vector2(155);
                toPortal = Vector2.Lerp(toPortal, distanceFromCenter, speed);
                Camera.Main.Offset = new Vector2(posrand.X, posrand.Y) + toPortal * Settings.Scale;
                rotation += speed/100;
                Camera.Main.Rotation += rotation;
                scale += speed/10;
                Camera.Main.Scale = Vector2.One-new Vector2(scale);
            }
            else if (Camera.Main.Offset != Vector2.Zero)
            {
                Camera.Main.Offset = new Vector2(GameApplication.graphics.GraphicsDevice.Viewport.Width, GameApplication.graphics.GraphicsDevice.Viewport.Height) / 2;
                GameApplication.rendColor = Color.White;
            }
            
        }
    }
}
