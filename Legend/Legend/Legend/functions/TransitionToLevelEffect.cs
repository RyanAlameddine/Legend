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
        Vector2 pos;
        Vector2 posrand;
        VisualizationData data = new VisualizationData();
        List<float> freqs = new List<float>();

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
            Game1.rendpos = Vector2.Zero;
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
                pos = Game1.rendpos;
                Vector2 normalpos = Vector2.Zero;
                posrand.X = random.Next(-25, 25);
                posrand.Y = random.Next(-25, 25);
                Game1.rendpos.X = normalpos.X + posrand.X;
                Game1.rendpos.Y = normalpos.Y + posrand.Y;
                
            }else if(Game1.rendpos != Vector2.Zero){
                Game1.rendpos = new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width, Game1.graphics.GraphicsDevice.Viewport.Height)/2;
                Game1.rendColor = Color.White;
            }
            
        }
    }
}
