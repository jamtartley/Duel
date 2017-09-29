using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Duel.Content;
using Duel.Utils;

namespace Duel.Particle
{
    internal class Emitter : IWorldObject
    {
        private const int MAX_PARTICLES = 2000;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; } = Vector2.Zero;

        internal Vector2 Target { get; set; } = Vector2.Zero;
        internal List<Particle> Particles = new List<Particle>();
        internal int ParticlesCreated;
        internal int ParticlesKilled;

        internal bool IsFull => Particles.Count >= MAX_PARTICLES;

        internal Emitter(Vector2 position)
        {
            Position = position;
        }

        private void AddParticle()
        {
            if (IsFull)
            {
                return;
            }

            const float spreadWidth = 0.1f;
            float angle =  MathsUtils.VectorBetween(Position, Target).ToAngle();
            float spread = RandomUtils.NextFloat(-spreadWidth / 2, spreadWidth / 2);
            //Quaternion aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, angle);
            Vector2 pVel = MathsUtils.FromPolar(angle + spread, 1);

            Particles.Add(new Particle(Position,
                pVel, 
                RandomUtils.Rand.Next(5000, 10000), 
                RandomUtils.Rand.Next(5, 10)));

            ParticlesCreated++;
        }

        public void Update(GameTime gameTime)
        {
            // TODO: Refactor input
            Target = Mouse.GetState().Position.ToVector2();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                AddParticle();
            }

            foreach (Particle p in Particles)
            {
                p.Update(gameTime);
            }

            ParticlesKilled += Particles.Count(p => p.IsDead);
            Particles.RemoveAll(p => p.IsDead);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetController.Pixel, Position, Color.White);

            foreach (Particle p in Particles)
            {
                p.Draw(spriteBatch);
            }
        }
    }
}
