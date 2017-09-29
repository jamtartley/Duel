using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Duel.Content;
using Duel.Utils;

namespace Duel.Particle
{
    internal class Particle : IWorldObject
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        internal int Duration { get; private set; }
        internal int Speed { get; private set; }

        private Texture2D image = RandomUtils.Rand.Next(2) == 0 ? AssetController.Line : AssetController.CircleBlur;
        private Color colour;
        private int timeAlive;

        internal Vector2 Size => image == null ? Vector2.Zero : new Vector2(image.Width / 2, image.Height / 2);
        internal bool IsDead => timeAlive >= Duration || GameRoot.Instance.ScreenBounds.Contains(Position) == false;

        internal Particle(Vector2 position, Vector2 velocity, int duration, int speed)
        {
            Position = position;
            Velocity = velocity;
            Duration = duration;
            Speed = speed;

            colour = Color.FromNonPremultiplied(RandomUtils.Rand.Next(256),
                RandomUtils.Rand.Next(256),
                RandomUtils.Rand.Next(256),
                RandomUtils.Rand.Next(256));
        }

        internal void ApplyForce(Vector2 force)
        {
            Velocity += force;
        }

        public void Update(GameTime gameTime)
        {
            timeAlive += gameTime.ElapsedGameTime.Milliseconds;
            Position += Velocity * Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, colour, Velocity.ToAngle(), Size, 1f, 0, 0);
        }
    }
}
