using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;

namespace APEng3Final.Engine
{
    class Game
    {
        Player player = new Player();
        Fire fire = new Fire(384, 384);
        Question question;

        int prevFirePos;

        bool correctAnswer;
        int numberTimer = 60;
        float numberAlpha = 1;

        bool paused = false;

        private Vector2[] firePositions =
        {
            new Vector2(192, 256),
            new Vector2(576, 256),
            new Vector2(384, 64),
            new Vector2(384, 384)
        };

        //the first answer is the correct one, the rest are incorrect
        static string[] answers0 = { "Green Dolphin \nStreet Prison", "Alkatraz", "It has no name", "Foo Fighters" };
        static string[] answers1 = { "A plankton inhabiting a \nhuman body", "A famous band", "A prison", "A disk" };
        static string[] answers2 = { "Stone Free", "Star Platinum", "Golden Wind", "The World" };
        static string[] answers3 = { "Shrinking people", "Exploding anything it\ntouches", "Anti gravity", "Controlling time" };
        static string[] answers4 = { "A manifestation of one's \nfighting spirit", "A state of being", "Not being in a chair", "You." };
        static string[] answers5 = { "To ascend beyond heaven.", "To kill Jotaro", "To kill Jolyne", "To revive DIO" };
        static string[] answers6 = { "A blind sharpshooter", "A prison guard", "A lawyer", "A ghost" };


        private Question[] questions =
        {
            new Question("What is the name of the prison Jolyne is sent too?", 100, 60, answers0),
            new Question("What is Foo Fighters?", 100, 60, answers1),
            new Question("What is the name of Jolyne's stand?", 100, 60, answers2),
            new Question("What is Guess' power?", 100, 60, answers3),
            new Question("What is a stand?", 100, 60, answers4),
            new Question("What is Father Pucci's goal?", 100, 60, answers5),
            new Question("Who is Johngalli A.?", 100, 60, answers6)
        };

        public static List<Tile> tileList = new List<Tile>();

        public static void CreateTile(Vector2 position)
        {
            Tile output = new Tile((int)position.X, (int)position.Y, "PlacedTile");
            output.Load();
            tileList.Add(output);
        }

        public void AddTile(Tile tile)
        {
            tileList.Add(tile);
        }

        public void LoadGame()
        {
            //load assets here

            for (int x = 0; x < 3; x++)
                CreateTile(new Vector2(128 + (64 * x), 320));

            for (int x = 0; x < 3; x++)
                CreateTile(new Vector2(512 + (64 * x), 320));

            for (int x = 0; x < 3; x++)
                CreateTile(new Vector2(256 + (128 * x), 160));

            player.Load();
            fire.Load();
        }

        public void Update(float DeltaTime)
        {
            if (IsKeyPressed(KeyboardKey.KEY_Q))
                paused = !paused;

            if (!paused)
            {
                player.PreUpdate();

                for (int x = 0; x < tileList.Count; x++)
                {
                    //Rectangle drawZone = new Rectangle((int)player.position.X - 16, (int)player.position.Y - 16, 32, 32);
                    Rectangle hitZone = new Rectangle(player.position.X - 16, player.position.Y - 16, 64, 64);
                    Rectangle hit = new Rectangle(tileList[x].position.X, tileList[x].position.Y, 32, 32);
                    if (CheckCollisionRecs(hit, hitZone))
                    {
                        player.HorizontalCollision(hit);
                        player.VerticalCollision(hit);
                    }

                    //Console.WriteLine(tileList[x]);
                }
                Rectangle fireHit = new Rectangle(fire.position.X, fire.position.Y, 32, 32);
                if (player.ItemCollision(fireHit))
                {
                    Random rnd = new Random();
                    int firePos = rnd.Next(4);
                    while (firePos == prevFirePos)
                        firePos = rnd.Next(4);

                    fire.position = firePositions[firePos];

                    prevFirePos = firePos;
                    paused = true;
                    //player.points++;

                    question = questions[rnd.Next(5)];
                }
            }
            else
            {
                question.Update();
                for (int x = 0; x < question.buttons.Length; x++)
                {
                    if (question.buttons[x].MouseOver() && IsMouseButtonPressed(MOUSE_LEFT_BUTTON))
                    {
                        if (question.buttons[x].ClickCheck())
                            player.points++;
                        else
                            player.Lives--;

                        correctAnswer = question.buttons[x].ClickCheck();
                        numberTimer = 120;
                        numberAlpha = 1f;

                        paused = false;
                    }
                }
            }
        }

        public void PreDraw()
        {
            //this happens before draw
            BeginDrawing();
            ClearBackground(BLACK);
            //code to happen before draw
            //this makes things draw behind the thing in draw
            fire.PreDraw();
            player.PreDraw();
            Draw();
        }

        public void Draw()
        {
            //this happens after PreDraw but before PostDraw, used for drawing most things
            fire.Draw();
            player.Draw();
            if (!paused)
                numberTimer--;

            if(numberTimer < 30)
                numberAlpha -= 0.03f;

            if (correctAnswer && numberTimer > 0)
                DrawText("+1", 120, 70, 20, ColorAlpha(GREEN, numberAlpha));
            else if (!correctAnswer && numberTimer > 0)
                DrawText("-1", 90, 50, 20, ColorAlpha(RED, numberAlpha));

            for (int x = 0; x < tileList.Count; x++)
            {
                Rectangle hit = new Rectangle(tileList[x].position.X, tileList[x].position.Y, 32, 32);
                if (CheckCollisionRecs(hit, player.drawZone))
                    tileList[x].Draw();
                else
                    DrawRectangle((int)tileList[x].position.X, (int)tileList[x].position.Y, 32, 32, BLACK);
            }

            PostDraw();
        }

        public void PostDraw()
        {
            //do some thing before the drawing is done.
            //this makes things appear in front of the draw
            if (paused)
                question.Draw();
            EndDrawing();
        }

        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
    }
}