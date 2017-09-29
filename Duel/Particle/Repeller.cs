using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Duel.Content;
using Duel.Utils;

namespace Duel.Particle
{
    internal class Repeller : IWorldObject, IForceActor
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; } = Vector2.Zero;

        internal float Mass { get; private set; } = RandomUtils.Rand.Next(10, 250);
        internal int Size { get; private set; } = 32;

        private Rectangle bounds => new Rectangle((int)Position.X - Size / 2,
            (int)Position.Y - Size / 2,
            Size,
            Size);

        internal Repeller(Vector2 position)
        {
            Position = position;
        }

        public void ApplyForce(Particle p)
        {
            Vector2 dir = MathsUtils.VectorBetween(Position, p.Position);
            float force = Mass / (float)Math.Pow(dir.X * dir.X + Mass / 2 + dir.Y * dir.Y + Mass, 1.5f);

            p.ApplyForce(dir * force);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetController.CircleBlur, bounds, Color.Red);
            RenderUtils.DrawText(spriteBatch, AssetController.DebugFont, bounds, Mass.ToString(), Color.White, RenderUtils.Alignment.CENTRE, .75f);
        }
    }
}
