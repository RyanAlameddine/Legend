using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Legend
{
    public class Button
    {
        Texture2D buttontxture;
        Texture2D _button;
        Texture2D _buttonhover;
        SpriteFont _font;
        string _text;
        Vector2 _position;
        Vector2 _scaledPosition;
        Rectangle hitbox;
        Color color;
        
        public Button(Texture2D button, Texture2D buttonhover, SpriteFont font, string text, Vector2 position)
        {
            _font = font;
            _button = button;
            _text = text;
            _position = position;
            _buttonhover = buttonhover;
            buttontxture = _button;
            color = Color.Black;
            hitbox = new Rectangle((int) position.X, (int) position.Y, _button.Width, _button.Height);
        }

        public bool buttonpressed()
        {
            _scaledPosition = _position * Settings.Scale;
            hitbox = new Rectangle((int)(_scaledPosition.X), (int)(_scaledPosition.Y), (int)(_button.Width * Settings.Scale), (int)(_button.Height * Settings.Scale));
            if (hitbox.Contains((int)InputManager.mousePosition.X, (int)InputManager.mousePosition.Y))
            {
                buttontxture = _buttonhover;
                color = new Color(208, 159, 81);
                if (InputManager.ms.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }else
            {
                buttontxture = _button;
                color = Color.White;
            }
            return false;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttontxture, _scaledPosition, null, Color.White, 0f, Vector2.Zero, 1f*Settings.Scale, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(_font, _text, new Vector2(_scaledPosition.X + buttontxture.Width * Settings.Scale / 2, _scaledPosition.Y + buttontxture.Height * Settings.Scale / 2) - _font.MeasureString(_text) * Settings.Scale / 2, color, 0f, Vector2.Zero, Vector2.One * Settings.Scale, SpriteEffects.None, 0.6f);
        }
    }
}
