using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public static class GlobalConstants
    {
        public const int InSpace = 1, OnPlanet = 2;
        public const int PlanetWaitSecondsMin = 5, PlanetWaitSecondsMax = 20, PlanetMinSpeed = 10, PlanetMaxSpeed = 45;
        public const int ProtagonistSpeed = 5;
        public const float MyShipSpeed = 2.5f;

        private static float screenWidth, screenHeight;

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
    }
}
