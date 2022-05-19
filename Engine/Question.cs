using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using System;
using System.Numerics;
using System.Collections.Generic;

namespace APEng3Final.Engine
{
    class Question
    {
        string text;
        Vector2 position;
        public Button[] buttons = new Button[4];

        public Question(string text, int x, int y, string[] answers)
        {
            this.text = text;
            position = new Vector2(x, y);

            MakeButtons(x, y, answers);
        }

        public void MakeButtons(int x, int y, string[] answers)
        {
            Vector2[] buttonPosList =
            {
                new Vector2(x + 16, y + 128),
                new Vector2(x + 16, y + 256),
                new Vector2(x + 320, y + 128),
                new Vector2(x + 320, y + 256)
            };

            List<int> chosenList = new List<int>();

            Random rnd = new Random();

            int chosen;
            Vector2 buttonPos;

            chosen = rnd.Next(4);
            buttonPos = buttonPosList[chosen];
            chosenList.Add(chosen);
            Console.WriteLine(buttonPos);
            buttons[0] = new Button(answers[0], buttonPos.X, buttonPos.Y, 256, 64, true);

            while (chosenList.Contains(chosen))
                chosen = rnd.Next(4);
            buttonPos = buttonPosList[chosen];
            chosenList.Add(chosen);

            buttons[1] = new Button(answers[1], buttonPos.X, buttonPos.Y, 256, 64, false);

            while (chosenList.Contains(chosen))
                chosen = rnd.Next(4);
            buttonPos = buttonPosList[chosen];
            chosenList.Add(chosen);

            buttons[2] = new Button(answers[2], buttonPos.X, buttonPos.Y, 256, 64, false);

            while (chosenList.Contains(chosen))
                chosen = rnd.Next(4);
            buttonPos = buttonPosList[chosen];
            chosenList.Add(chosen);

            buttons[3] = new Button(answers[3], buttonPos.X, buttonPos.Y, 256, 64, false);
        }

        public void Draw()
        {
            DrawRectangle((int)position.X, (int)position.Y, 600, 360, DARKGRAY);
            DrawText(text, (int)position.X + 32, (int)position.Y + 64, 20, BLACK);

            for (int x = 0; x < buttons.Length; x++)
                buttons[x].Draw();
        }

        public void Update()
        {
            
        }
    }
}
