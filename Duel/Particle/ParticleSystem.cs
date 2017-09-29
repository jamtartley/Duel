using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Duel.Particle
{
    internal static class ParticleSystem
    {
        internal static List<Emitter> Emitters = new List<Emitter>();
        internal static List<Repeller> Repellers = new List<Repeller>();

        internal static int ParticlesCreated => Emitters.Sum(e => e.ParticlesCreated);
        internal static int ParticlesKilled => Emitters.Sum(e => e.ParticlesKilled);

        private static bool IsOnScreen(Vector2 position)
        {
            return GameRoot.Instance.ScreenBounds.Contains(position);
        }

        internal static void AddEmitter(Vector2 position)
        {
            Emitters.Add(new Emitter(position));
        }

        internal static void AddRepeller(Vector2 position)
        {
            Repellers.Add(new Repeller(position));
        }

        internal static void Update(GameTime gameTime)
        {
            foreach (Emitter emitter in Emitters)
            {
                emitter.Update(gameTime);

                foreach (Particle particle in emitter.Particles)
                {
                    foreach (Repeller repeller in Repellers)
                    {
                        repeller.ApplyForce(particle);
                    }
                }
            }

            Vector2 mousePos = Mouse.GetState().Position.ToVector2();

            // TODO: Refactor input
            // Don't create if there is a repeller less than 100px away
            if (Mouse.GetState().RightButton == ButtonState.Pressed
                && Repellers.Any(r => Vector2.Distance(r.Position, mousePos) <= 100) == false)
            {
                AddRepeller(mousePos);
            }
        }

        internal static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Emitter emitter in Emitters)
            {
                emitter.Draw(spriteBatch);
            }

            foreach (Repeller repeller in Repellers)
            {
                repeller.Draw(spriteBatch);
            }
        }
    }
}
