using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public static class GlobalConstants
    {
        public const int InSpace = 1, OnPlanet = 2;
        public const int PlanetWaitSecondsMin = 5, PlanetWaitSecondsMax = 20, PlanetMinSpeed = 10, PlanetMaxSpeed = 45;
        public const int PlayerSpeed = 5;
        public const float MyShipSpeed = 2.5f;

        private static float screenWidth, screenHeight;
        private static Texture2D[] playerSprites, enemySprites;
        private static Texture2D bullet;

        public static float ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        public static float ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }

        public static Texture2D[] PlayerSprites
        {
            get { return playerSprites; }
            set { playerSprites = value; }
        }

        public static Texture2D[] EnemySprites
        {
            get { return enemySprites; }
            set { enemySprites = value; }
        }

        public static Texture2D Bullet
        {
            get { return bullet; }
            set { bullet = value; }
        }
    }
}
