using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace eindprojectGameDev.Map
{
    public static class Background
    {
        private static Texture2D backgroundTexture;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, GlobalSettings.Width, GlobalSettings.Height), Color.Purple);
        }
        public static void Initialize(Texture2D texture)
        {
            backgroundTexture = texture;
        }
    }
}
