using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duel.Utils
{
    internal static class RenderUtils
    {
        internal enum Alignment
        {
            CENTRE,
            LEFT,
            RIGHT,
            TOP,
            BOTTOM
        }

        internal static float GetTextFitScale(SpriteFont font, string text, Rectangle bounds, float maxFill)
        {
            Vector2 textDimens = font.MeasureString(text);
            float scaleFactor = Math.Min(bounds.Width * maxFill / textDimens.X,
                    bounds.Height * maxFill / textDimens.Y);
            return Math.Min(1f, scaleFactor);
        }

        internal static Vector2 GetTextOrigin(SpriteFont font, string text, Rectangle bounds, Alignment alignment, float scale = 1f)
        {
            Vector2 textSize = font.MeasureString(text) * scale;
            Vector2 origin = new Vector2(bounds.Center.X - textSize.X / 2,
                    bounds.Center.Y - textSize.Y / 2);

            switch (alignment)
            {
                default:
                case Alignment.CENTRE:
                    break;
                case Alignment.LEFT:
                    origin.X = bounds.Left;
                    break;
                case Alignment.RIGHT:
                    origin.X = bounds.Right - textSize.X;
                    break;
                case Alignment.TOP:
                    origin.Y = bounds.Top;
                    break;
                case Alignment.BOTTOM:
                    origin.Y = bounds.Bottom - textSize.Y;
                    break;
            }

            return origin;
        }

        internal static void DrawText(SpriteBatch spriteBatch, SpriteFont font, Rectangle bounds, string text, Color color, Alignment alignment, float maxFill = 1f)
        {
            string displayedText = text.ToUpper();
            float scale = GetTextFitScale(font, displayedText, bounds, maxFill);
            Vector2 origin = GetTextOrigin(font, displayedText, bounds, alignment, scale);

            spriteBatch.DrawString(font, displayedText, origin, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
