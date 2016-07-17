using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Legend
{
    public static class InputManager
    {
        public static Vector3 mousePosition;
        public static MouseState ms;
        public static KeyboardState ks;

        public static void Update(MouseState ms, KeyboardState ks)
        {
            //translating to mouse world coordinates
            mousePosition = GameApplication.graphics.GraphicsDevice.Viewport.Unproject(new Vector3(ms.X, ms.Y, 0), Camera.Main.Projection, Camera.Main.View, Matrix.Identity);
            InputManager.ms = ms;
            InputManager.ks = ks;
        }
    }
}
