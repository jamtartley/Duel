using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Duel.Content
{
    internal static class AssetController
    {
        private static string ART_ROOT = "Art/";
        private static string ART_FONT_ROOT = ART_ROOT + "Font/";
        private static string ART_OTHER_ROOT = ART_ROOT + "Other/";
        private static string ART_PARTICLE_ROOT = ART_ROOT + "Particle/";

        internal static Texture2D CircleBlur { get; private set; }
        internal static Texture2D Crosshair { get; private set; }
        internal static SpriteFont DebugFont { get; private set; }
        internal static Texture2D Line { get; private set; }
        internal static Texture2D Pixel { get; private set; }

        internal static void Load(ContentManager cm)
        {
            CircleBlur = cm.Load<Texture2D>(ART_PARTICLE_ROOT + "circle_blur");
            Crosshair = cm.Load<Texture2D>(ART_OTHER_ROOT + "crosshair");
            DebugFont = cm.Load<SpriteFont>(ART_FONT_ROOT + "debug");
            Line = cm.Load<Texture2D>(ART_PARTICLE_ROOT + "line");
            Pixel = new Texture2D(GameRoot.Instance.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
        }
    }
}
