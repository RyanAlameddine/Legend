using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Legend.tooltip
{
    public class KeyAnimation
    {
        List<Key> keys;
        TimeSpan timer;
        TimeSpan speed;
        bool wasdown = false;
        int index = 0;
        KeyAnimationType type = KeyAnimationType.No;
        ToolTipPlayer player;
        public KeyAnimation(List<Key> keys)
        {
            this.keys = keys;
            speed = new TimeSpan(0, 0, 2);
            timer = speed;
        }

        public KeyAnimation(List<Key> keys, ToolTipPlayer player)
            :this(keys)
        {
            this.player = player;
            type = KeyAnimationType.Player;
        }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime;
            if (timer <= TimeSpan.Zero)
            {
                Key key = keys[index];
                if (!key.down && !wasdown)
                {
                    timer = speed;
                    key.down = true;
                    if (type == KeyAnimationType.Player)
                    {
                        player.running = true;
                        if (key.key.ToString().ToLower() == 'W'.ToString().ToLower()) player.dir = Direction.Up;
                        else if (key.key.ToString().ToLower() == 'A'.ToString().ToLower()) player.dir = Direction.Left;
                        else if (key.key.ToString().ToLower() == 'S'.ToString().ToLower()) player.dir = Direction.Down;
                        else if (key.key.ToString().ToLower() == 'D'.ToString().ToLower()) player.dir = Direction.Right;
                        else
                        {
                            player.running = false;
                        }
                    }
                }
                else if (key.down)
                {
                    timer = new TimeSpan(0, 0, 0, speed.Seconds / 2);
                    key.down = false;
                    wasdown = true;
                    if (type == KeyAnimationType.Player) player.running = false;
                }
                else if (wasdown)
                {
                    timer = new TimeSpan(0, 0, 0, speed.Seconds / 2);
                    keys[index].layerdepth -= .001f;
                    index = keys.IndexOf(key) + 1;
                    wasdown = false;
                    if (index > keys.Count - 1)
                    {
                        index = 0;
                    }
                    keys[index].layerdepth += .001f;
                    if (type == KeyAnimationType.Player)
                    {
                        player.running = false;
                        if (keys[index].key.ToString().ToLower() == 'W'.ToString().ToLower()) player.dir = Direction.Up;
                        else if (keys[index].key.ToString().ToLower() == 'A'.ToString().ToLower()) player.dir = Direction.Left;
                        else if (keys[index].key.ToString().ToLower() == 'S'.ToString().ToLower()) player.dir = Direction.Down;
                        else if (keys[index].key.ToString().ToLower() == 'D'.ToString().ToLower()) player.dir = Direction.Right;
                    }
                }
            }
        }
    }
}
