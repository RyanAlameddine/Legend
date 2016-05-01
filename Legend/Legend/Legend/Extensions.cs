using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Legend
{
    public static class Extensions
    {
        public static Vector3 ToVector3(this Vector2 vector2, float z)
        {
            return new Vector3(vector2.X, vector2.Y, z);
        }

        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.X, vector3.Y);
        }
    }
}
