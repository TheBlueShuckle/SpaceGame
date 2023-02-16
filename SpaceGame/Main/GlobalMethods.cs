using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame.Main
{
    public static class GlobalMethods
    {
        public static Vector2 GetCenter(Vector2 pos, float width, float height)
        {
            return new Vector2(pos.X + width / 2, pos.Y + height / 2);
        }

        public static bool CheckPointIntersects(Rectangle rectangle, Vector2 point)
        {
            if (point.X < rectangle.Right && point.X > rectangle.Left && point.Y < rectangle.Bottom && point.Y > rectangle.Top)
            {
                return true;
            }

            return false;
        }
    }
}
