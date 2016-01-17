using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Legend.weapons;
using Legend.levels.objects;

namespace Legend.characters
{
    public class Player
    {
        public Texture2D texture;
        public Texture2D playermove;
        Texture2D playerattack;
        public Vector2 _position;
        public Direction dir = Direction.Up;
        public Rectangle _frame = new Rectangle(0, 0, 12, 16);
        Rectangle hitbox = new Rectangle(0, 0, 12, 13);
        public Rectangle Hitbox
        {
            get
            {
                return hitbox;
            }
        }
        public float speedy = 0;
        public float speedx = 0;
        int currentFrame = 0;

        public TimeSpan _elapsedTime;
        TimeSpan _timePerFrame = new TimeSpan(0, 0, 0, 0, 150);

        Rectangle[] _currentAnimation;

        public Rectangle[] _leftWalkingFrames = {
                                             new Rectangle(132, 0, 66, 88),
                                             new Rectangle(0, 88, 61, 88)
                                         };
        public Rectangle[] _rightWalkingFrames = {
                                             new Rectangle(61, 88, 66, 88),
                                             new Rectangle(127, 88, 60, 87)
                                         };
        public Rectangle[] _upWalkingFrames = {
                                             new Rectangle(0, 0, 67, 88),
                                             new Rectangle(67, 0, 65, 88)
                                         };
        public Rectangle[] _downWalkingFrames = {
                                             new Rectangle(0, 177, 72, 82),
                                             new Rectangle(72, 177, 71, 82)
                                         };
        public Rectangle[] attackFrames = {
                                              new Rectangle(0, 0, 84, 90),
                                              new Rectangle(84, 0, 78, 90),
                                              new Rectangle(0, 90, 78, 84),
                                              new Rectangle(79, 90, 78, 84)
                                          };


        public Rectangle[] CurrentAnimation{
            get
            {
                return _currentAnimation;
            }
            set
            {
                _currentAnimation = value;
            }
        }
        public float speed = 3;
        public float scale = 1/5.5f;

        Keys k1;
        Keys k2;
        public PlayerState State = PlayerState.Idle;
        bool attackedlast = false;
        public bool resetspeed;
        public Player(Texture2D playermove, Texture2D playerattack, Vector2 position)
        {
            this.playermove = playermove;
            _position = position;
            this.playerattack = playerattack;
            texture = playermove;
            _currentAnimation = _upWalkingFrames;
            _frame = _upWalkingFrames[0];
        }

        public bool DidPickUpWeapon(KeyboardState ks, ItemOnFloor weapon)
        {
            if (hitbox.Intersects(weapon.HitBox) && ks.IsKeyDown(Keys.K))
            {
                return true;
            }
            return false;
        }

        public void Update(KeyboardState ks, List<Sprite> grassbarriers, MouseState ms, GameTime gameTime)
        {
            if (resetspeed)
            {
                speedx = 0;
                speedy = 0;
            }
            else
            {
                resetspeed = true;
            }
            if (ks.IsKeyDown(Keys.LeftShift))
            {
                _position.X = ms.X/Settings.Scale;
                _position.Y = ms.Y/Settings.Scale;
            }
            if (State == PlayerState.Moving)
            {
                _elapsedTime += gameTime.ElapsedGameTime;
                if (_elapsedTime >= _timePerFrame)
                {
                    currentFrame++;
                    _elapsedTime = TimeSpan.Zero;
                    if(currentFrame == _currentAnimation.Length){
                        currentFrame = 0;
                    }
                    _frame = _currentAnimation[currentFrame];
                }
            }
            else if (State == PlayerState.Attacking)
            {
                texture = playerattack;
            }

            if (State == PlayerState.Idle || State == PlayerState.Moving)
            {
                bool inter = false;
                for (int i = 0; i < grassbarriers.Count(); i++)
                {
                    if (grassbarriers[i].HitBox.Intersects(hitbox))
                    {
                        inter = true;
                    }
                }
                if (!inter)
                {
                    State = PlayerState.Moving;
                    if (ks.GetPressedKeys().Length == 0)
                    {
                        k1 = Keys.V;
                        k2 = Keys.V;
                    }
                    else if (ks.GetPressedKeys().Length == 1)
                    {
                        k1 = ks.GetPressedKeys()[0];
                        k2 = Keys.V;
                    }
                    else
                    {
                        k1 = ks.GetPressedKeys()[0];
                        k2 = ks.GetPressedKeys()[1];
                        if (k1 == Keys.S && k2 == Keys.W)
                        {
                            k2 = Keys.V;
                            k1 = Keys.W;
                        }
                        if (k1 == Keys.A && k2 == Keys.D)
                        {
                            k2 = Keys.V;
                            k1 = Keys.A;
                        }
                    }
                    if (ks.IsKeyDown(Keys.A) && ks.IsKeyDown(Keys.W))
                    {
                        speedy = -0.45f;
                        speedx = -0.45f;
                        
                        if (dir == Direction.Down || dir == Direction.Right)
                        {
                            _currentAnimation = _upWalkingFrames;

                            dir = Direction.Up;
                        }
                    }
                    else if (ks.IsKeyDown(Keys.A) && ks.IsKeyDown(Keys.S))
                    {
                        speedy = 0.45f;
                        speedx = -0.45f;
                        
                        if (dir == Direction.Up || dir == Direction.Right)
                        {
                            _currentAnimation = _downWalkingFrames;
                            _frame = _currentAnimation[0];

                            dir = Direction.Down;
                        }
                    }
                    else if (ks.IsKeyDown(Keys.D) && ks.IsKeyDown(Keys.W))
                    {
                        speedy = -0.45f;
                        speedx = 0.45f;
                        if (dir == Direction.Down || dir == Direction.Left)
                        {
                            _currentAnimation = _upWalkingFrames;
                            _frame = _currentAnimation[0];

                            dir = Direction.Up;
                        }
                    }
                    else if (ks.IsKeyDown(Keys.D) && ks.IsKeyDown(Keys.S))
                    {
                        speedy = 0.45f;
                        speedx = 0.45f;
                        
                        if (dir == Direction.Up || dir == Direction.Left)
                        {
                            _currentAnimation = _downWalkingFrames;
                            _frame = _currentAnimation[0];
                            
                            dir = Direction.Down;
                        }
                    }
                    else if (ks.IsKeyDown(Keys.W))
                    {
                        speedy = -0.5f;
                        if (dir != Direction.Up)
                        {
                            _currentAnimation = _upWalkingFrames;
                            _frame = _currentAnimation[0];

                        }

                        dir = Direction.Up;

                    }
                    else if (ks.IsKeyDown(Keys.S))
                    {
                        speedy = 0.5f;
                        if (dir != Direction.Down)
                        {
                            _currentAnimation = _downWalkingFrames;
                            _frame = _currentAnimation[0];

                        }
                        
                        dir = Direction.Down;

                    }
                    else if (ks.IsKeyDown(Keys.A))
                    {
                        speedx = -0.5f;
                        if (dir != Direction.Left)
                        {
                            _currentAnimation = _leftWalkingFrames;
                            _frame = _currentAnimation[0];
                        }
                        
                        dir = Direction.Left;
                    }
                    else if (ks.IsKeyDown(Keys.D))
                    {
                        speedx = 0.5f;
                        if (dir != Direction.Right)
                        {
                            _currentAnimation = _rightWalkingFrames;
                            _frame = _currentAnimation[0];
                        }

                        dir = Direction.Right;
                    }
                    else
                    {
                        State = PlayerState.Idle;
                    }
                }
                else
                {
                    _frame = _currentAnimation[0];
                    if (ks.IsKeyDown(Keys.W))
                    {
                        _currentAnimation = _upWalkingFrames;
                        _frame = _currentAnimation[0];
                        if (!(k1 == Keys.W) && !(k2 == Keys.W))
                        {
                            speedy = -0.5f;
                            
                            dir = Direction.Up;

                        }
                    }
                    else if (ks.IsKeyDown(Keys.S))
                    {
                        _currentAnimation = _downWalkingFrames;
                        _frame = _currentAnimation[0];
                        if (!(k1 == Keys.S) && !(k2 == Keys.S))
                        {
                            speedy = 0.5f;
                            
                            dir = Direction.Down;

                        }
                    }
                    if (ks.IsKeyDown(Keys.A))
                    {
                        _currentAnimation = _leftWalkingFrames;
                        _frame = _currentAnimation[0];
                        if (!(k1 == Keys.A) && !(k2 == Keys.A))
                        {
                            speedx = -0.5f;
                            
                            dir = Direction.Left;
                        }
                    }
                    else if (ks.IsKeyDown(Keys.D))
                    {
                        
                        _currentAnimation = _rightWalkingFrames;
                        _frame = _currentAnimation[0];
                        if (!(k1 == Keys.D) && !(k2 == Keys.D))
                        {
                            speedx = 0.5f;
                            
                            dir = Direction.Right;
                        }
                    }
                }

                _position.X += speedx * speed;
                _position.Y += speedy * speed;
                _position.X = MathHelper.Clamp(_position.X, 0, 298);
                _position.Y = MathHelper.Clamp(_position.Y, 0, 295);

                if (ks.IsKeyDown(Keys.J))
                {
                    if (!attackedlast)
                    {
                        Game1.inventory.sword.swing();
                        attackedlast = true;
                    }
                }
                else
                {
                    attackedlast = false;
                }
            }
            hitbox.X = (int)_position.X;
            hitbox.Y = (int)_position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, _position * Settings.Scale, _frame, Color.White, 0f, Vector2.Zero, scale * Settings.Scale, SpriteEffects.None, 0.5f);
        }
    }
}
