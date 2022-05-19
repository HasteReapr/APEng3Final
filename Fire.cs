using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using APEng3Final.Engine;

using System.Numerics;

namespace APEng3Final
{
    class Fire
    {
        public Vector2 position;
        private Texture sprite;
        public Fire(int x, int y)
        {
            //sprite is 32x32
            position = new Vector2(x, y);
        }

        public void Load()
        {
            sprite = LoadTexture("Sprites/DivineFlame.png");
        }

        public void PreDraw()
        {
            DrawCircle((int)position.X + 16, (int)position.Y + 16, 16, GRAY);
            DrawCircle((int)position.X + 16, (int)position.Y + 16, 20, ColorAlpha(GRAY, 0.5f));
            DrawCircle((int)position.X + 16, (int)position.Y + 16, 24, ColorAlpha(GRAY, 0.25f));
            DrawCircle((int)position.X + 16, (int)position.Y + 16, 24, ColorAlpha(YELLOW, 0.1f));
        }

        public void Draw()
        {
            Sprite.Animate(sprite, position, 32, 32, WHITE, 8, 0.1f);
        }
    }
}
