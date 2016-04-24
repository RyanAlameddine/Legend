using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Legend.tooltip
{
    public class ToolTipPlayer : ToolTipObj
    {
        Texture2D texture;
        Texture2D playermove;
        Texture2D playerattack;
        Rectangle frame = new Rectangle(0, 177, 72, 82);
        public Direction dir;
        TimeSpan elapsedtime;
        TimeSpan timePerFrame = new TimeSpan(0, 0, 0, 0, 150);
        bool indexzero = true;
        public bool running = false;

        public Rectangle[] leftWalkingFrames = {
                                             new Rectangle(132, 0, 66, 88),
                                             new Rectangle(0, 88, 61, 88)
                                         };
        public Rectangle[] rightWalkingFrames = {
                                             new Rectangle(61, 88, 66, 88),
                                             new Rectangle(127, 88, 60, 87)
                                         };
        public Rectangle[] upWalkingFrames = {
                                             new Rectangle(0, 0, 67, 88),
                                             new Rectangle(67, 0, 65, 88)
                                         };
        public Rectangle[] downWalkingFrames = {
                                             new Rectangle(0, 177, 72, 82),
                                             new Rectangle(72, 177, 71, 82)
                                         };
        public Rectangle[] attackFrames = {
                                              new Rectangle(0, 0, 84, 90),
                                              new Rectangle(84, 0, 78, 90),
                                              new Rectangle(0, 90, 78, 84),
                                              new Rectangle(79, 90, 78, 84)
                                          };

        public ToolTipPlayer(Texture2D playermove, Texture2D playerattack, Vector2 pos, float layerdepth, float scale, Direction dir)
            :base(scale, pos, layerdepth, ToolTipObjType.Player)
        {
            this.playermove = playermove;
            this.playerattack = playerattack;
            texture = playermove;
            this.dir = dir;
        }

        public override void Update(GameTime gameTime)
        {
            elapsedtime += gameTime.ElapsedGameTime;
            if (elapsedtime > timePerFrame && running)
            {
                elapsedtime = TimeSpan.Zero;
                if (dir == Direction.Up)
                {
                    if (indexzero) frame = upWalkingFrames[0];
                    else frame = upWalkingFrames[1];
                }
                else if (dir == Direction.Down)
                {
                    if (indexzero) frame = downWalkingFrames[0];
                    else frame = downWalkingFrames[1];
                }
                else if (dir == Direction.Left)
                {
                    if (indexzero) frame = leftWalkingFrames[0];
                    else frame = leftWalkingFrames[1];
                }
                else if (dir == Direction.Right)
                {
                    if (indexzero) frame = rightWalkingFrames[0];
                    else frame = rightWalkingFrames[1];
                }
                indexzero = !indexzero;
            }
            if (!running)
            {
                if (dir == Direction.Up) frame = upWalkingFrames[0];
                else if (dir == Direction.Down) frame = downWalkingFrames[0];
                else if (dir == Direction.Left) frame = leftWalkingFrames[0];
                else if (dir == Direction.Right) frame = rightWalkingFrames[0];
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 toolTipPos)
        {
            spriteBatch.Draw(texture, (pos + toolTipPos) * Settings.Scale, frame, Color.White, 0f, new Vector2(frame.Width/2, frame.Height/2), scale * Settings.Scale, SpriteEffects.None, layerdepth);
            base.Draw(spriteBatch, toolTipPos);
        }
    }
}
