using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceGame
{
    public static class GlobalConstants
    {
        #region Constants

        public const int InMenu = 0, InSpace = 1, OnPlanet = 2;
        public const int PlanetWaitSecondsMin = 5, PlanetWaitSecondsMax = 20, PlanetMinSpeed = 10, PlanetMaxSpeed = 45;
        public const int PlayerSpeed = 5;

        #endregion

        #region GettersSetters

        private static int screenWidth, screenHeight, levelsBeaten = 0, lastScene;
        private static bool debugMode = false;
        private static Texture2D[] playerSprites = new Texture2D[4], enemySprites = new Texture2D[4], shipSprites = new Texture2D[2], planetSprites = new Texture2D[2];
        private static Texture2D bullet, enemyMeleeRange, healthPack, healthBar;
        private static SpriteBatch spriteBatch;
        private static SpriteFont font;
        private static Vector2 shipSpeed = new Vector2(2.5f, 2.5f);

        public static int ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        public static int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }

        public static int LevelsBeaten
        {
            get { return levelsBeaten; }
            set { levelsBeaten = value; }
        }

        public static int LastScene
        {
            get { return lastScene; }
            set { lastScene = value; }
        }

        public static bool CheatMode
        {
            get { return debugMode; }
            set { debugMode = value; }
        }

        public static Texture2D[] ShipSprites
        {
            get { return shipSprites; }
            set { shipSprites = value; }
        }

        public static Texture2D[] PlanetSprites
        {
            get { return planetSprites; }
            set { planetSprites = value; }
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

        public static Texture2D EnemyMeleeRange
        {
            get { return enemyMeleeRange; }
            set { enemyMeleeRange = value; }
        }

        public static Texture2D HealthPack
        {
            get { return healthPack; }
            set { healthPack = value; }
        }

        public static Texture2D HealthBar
        {
            get { return healthBar; }
            set { healthBar = value; }
        }

        public static SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        public static SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public static Vector2 ShipSpeed
        {
            get { return shipSpeed; }
        }

        #endregion
    }
}
