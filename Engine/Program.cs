using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using System;

namespace APEng3Final.Engine
{
    class Program
    {
        public static void Main()
        {
            Game game = new Game();

            InitWindow(800, 480, "Jojo's Bizzare Adventure Stone Ocean Trivia Platformer");
            SetTargetFPS(60);

            // load an image for the window icon
            Image icon = LoadImage("Sprites/Logo.png");
            
            SetWindowIcon(icon);

            UnloadImage(icon);

            game.LoadGame();

            while (!WindowShouldClose())
            {
                float frameTime = GetFrameTime();

                float offset = 0;
                offset += GetFrameTime();
                while (offset > 0)
                {
                    offset -= 0.1f;
                }
                game.Update(frameTime);
                game.PreDraw();
            }

            CloseWindow();
        }
    }
}