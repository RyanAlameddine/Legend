using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Legend
{
    public class Sprite
    {
        protected Texture2D _texture;
        protected Vector2 _position;
        protected Rectangle _sourceRectangle;
        protected Color _color;
        protected float _rotation;
        protected Vector2 _origin;
        protected float _scale;
        protected SpriteEffects _effects;
        protected float _layerDepth;
        protected Rectangle _hitbox;
        protected int _width;
        protected int _height;

        public float LayerDepth
        {
            get
            {
                return _layerDepth;
            }
            set
            {
                _layerDepth = value;
            }
        }

        public float Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
            }
        }

        public Rectangle HitBox
        {
            get
            {
                return _hitbox;
            }
        }

        public Sprite(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, Color color, int width, int height)
        {
            _texture = texture;
            _position = position;
            if (sourceRectangle.HasValue)
            {
                _sourceRectangle = sourceRectangle.Value;
            }
            else
            {
                _sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
            }
            _rotation = rotation;
            _origin = origin;
            _scale = scale;
            _effects = effects;
            _layerDepth = layerDepth;
            _hitbox = new Rectangle((int)position.X, (int)position.Y, width, height);
            _color = color;
            _width = width;
            _height = height;
        }

        public virtual void Update(GameTime gameTime)
        {
            _hitbox = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position * Settings.Scale, _sourceRectangle, _color, _rotation, _origin, _scale * Settings.Scale, _effects, _layerDepth);
        }
    }
}
