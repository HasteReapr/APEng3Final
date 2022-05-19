using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using System.Numerics;

namespace APEng3Final.Engine
{
    class Button
    {
        Vector2 position;
        int width;
        int height;
        string text;
        public bool isTrue;

        public Button(float x, float y, int width, int height)
        {
            text = "";
            position = new Vector2(x, y);
            this.width = width;
            this.height = height;
        }

        public Button(string text, float x, float y, int width, int height)
        {
            position = new Vector2(x, y);
            this.width = width;
            this.height = height;
            this.text = text;
        }
        public Button(string text, float x, float y, int width, int height, bool isTrue)
        {
            position = new Vector2(x, y);
            this.width = width;
            this.height = height;
            this.text = text;
            this.isTrue = isTrue;
        }

        public bool MouseOver()
        {
            Rectangle hitbox = new Rectangle(position.X, position.Y, width, height);
            return (CheckCollisionPointRec(GetMousePosition(), hitbox));
        }

        public void Click()
        {
            //call this using if(MouseOver() && MouseButtonDown(MOUSE_BUTTON_LEFT)

        }

        public bool ClickCheck()
        {
            return isTrue;
        }

        public void Draw()
        {
            DrawRectangle((int)position.X, (int)position.Y, width, height, GRAY);
            DrawRectangle((int)position.X, (int)position.Y, width, height, MouseOver() ? ColorAlpha(WHITE, 0.75f) : ColorAlpha(WHITE, 0f));
            DrawText(text, (int)position.X + 16, (int)position.Y + 16, 20, BLACK);
        }
    }
}
