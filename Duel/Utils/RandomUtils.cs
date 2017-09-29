using System;

namespace Duel.Utils
{
    internal static class RandomUtils
    {
        internal static Random Rand = new Random();
        
        internal static float NextFloat(float min, float max)
        {
            return (float)Rand.NextDouble() * (max - min) + min;
        }
    }
}
