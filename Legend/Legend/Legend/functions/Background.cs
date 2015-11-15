using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Legend.functions;

namespace Legend.levels.functions
{
    public class Background
    {
        public List<Tile> materials = new List<Tile>();
        public Background(Texture2D material)
        {
            for (int i = 0; i < (320 / material.Width) * (320 / material.Height); i++)
            {
                materials.Add(new Tile(material, Color.White));
            }
            foreach (Tile t in materials)
            {
                t.Hitbox = new Rectangle(0, 0, 4, 4);
            }
            for (int y = 0; y < (320 / materials[0].texture.Height); y++)
            {
                for (int x = 0; x < (320 / materials[0].texture.Width); x++)
                {
                    if (x + y * (320 / materials[0].texture.Height) >= materials.Count)
                    {
                        break;
                    }
                    //multiply y by 320/materials[0].texture.height because that's the number of columns per row and y represents the row
                    materials[x + y * (320 / materials[0].texture.Height)].Hitbox.X = x * materials[x + y].texture.Width;
                    materials[x + y * (320 / materials[0].texture.Height)].Hitbox.Y = y * materials[x + y].texture.Height;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < (320 / materials[0].texture.Height); y++)
            {
                for (int x = 0; x < (320 / materials[0].texture.Width); x++)
                {
                    if (x + y * (320 / materials[0].texture.Height) >= materials.Count)
                    {
                        break;
                    }
                    materials[x + y*(320 / materials[0].texture.Height)].Draw(spriteBatch);
                }
            }
        }



    }
}
