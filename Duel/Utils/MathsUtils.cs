using System;

using Microsoft.Xna.Framework;

namespace Duel.Utils
{
    internal static class MathsUtils
    {
        internal static Vector2 VectorBetween(Vector2 source, Vector2 dest)
        {
            return dest - source;
        }

        internal static Vector2 FromPolar(float angle, float magnitude)
        {
            return magnitude * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        internal static float ToAngle(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }
    }
}
