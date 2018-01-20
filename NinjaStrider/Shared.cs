using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NinjaStrider
{
    class Shared
    {
        public static Vector2 stage;
        public static bool gameOver;
        public static bool actionScene;
        public static int score = 0;
        public static float gameTimer = 0;
        public static bool godMode = false;
        public static bool activatedGodMode = false;
        public static string systemMessage = "";
    }
}
