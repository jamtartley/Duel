using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duel.Particle
{
    internal interface IWorldObject
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
