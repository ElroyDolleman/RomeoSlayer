using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using E2DEngine.Helpers;
using E2DEngine.Graphics;

namespace RomeoSlayer.Resources
{
    class Assets
    {
        // Romeo SpriteSheets
        public static Sprite normalRomeoWalkAnimation;

        public static Sprite littleRomeoWalkAnimation;

        public static Sprite flyingRomeoFlyAnimation;

        public static Sprite jumpingRomeoGoUpAnimation;
        public static Sprite jumpingRomeoLandAnimation;
        public static Sprite jumpingRomeoJumpAnimation;
        public static Sprite jumpingRomeoFallAnimation;

        public static Sprite fallingHangRomeo;
        public static Sprite jumpingRomeoCrushed;
        public static Sprite littleRomeoCrushed;
        public static Sprite normalRomeoCrushed;

        public static Sprite hangRomeoBurn;
        public static Sprite jumpingRomeoBurn;
        public static Sprite littleRomeoSquash;
        public static Sprite normalRomeoBurn;

        public static Sprite rope;

        // Juliet
        public static Sprite juliet;

        // Croud
        public static E2DTexture croudSheet;
        public static Sprite crowd;
        public static Sprite clapAnimation;

        // Traps SpriteSheets
        public static Sprite trapButton;

        public static Sprite trapDoorsClose;

        public static Sprite flamethrowerOnAnimation;
        public static Sprite flamethrowerOffAnimation;

        public static Sprite sandSack;

        public static Sprite axe;
        public static Sprite axeButton;

        public static Sprite flamethrower;

        // Romeo Textures
        public static E2DTexture normalRomeoTexture;
        public static E2DTexture littleRomeoTexture;
        public static E2DTexture flyingRomeoTexture;

        public static E2DTexture jumpingRomeoGoUpTexture;
        public static E2DTexture jumpingRomeoLandTexture;
        public static E2DTexture jumpingRomeoInAirTexture;

        public static E2DTexture julietTexture;

        // Trap Textures
        public static E2DTexture trapSheetTexture;
        public static E2DTexture flamethrowerOnTexture;
        public static E2DTexture flamethrowerOffTexture;
        public static E2DTexture sandSackTexture;

        // Background
        public static E2DTexture stageBackground;
        public static E2DTexture startScreenBackground;
        public static E2DTexture darkLayer;
        public static Sprite logo;

        // UI
        public static E2DTexture UISheet;

        public static E2DTexture numberSheet;
        public static Sprite number;

        // Start Screen UI
        public static Sprite playButton;
        public static Sprite infoButton;
        public static Sprite optionsButton;
        public static Sprite backButton;

        // Game UI
        public static Sprite pauseButton;
        public static Sprite scoreBoard;
        public static Sprite restartButton;
        public static Sprite homeButton;

        public static void LoadAssets()
        {
            /* TEXTURES */

            // Background
            stageBackground = new E2DTexture(Directories.BACKGROUND_PATH, Directories.STAGE_BACKGROUND);
            startScreenBackground = new E2DTexture(Directories.BACKGROUND_PATH, Directories.START_SCREEN_BACKGROUND);
            darkLayer = new E2DTexture(Directories.BACKGROUND_PATH, Directories.DARK_LAYER);

            // UI
            UISheet = new E2DTexture(Directories.BACKGROUND_PATH, Directories.UI_SHEET);

            // Traps Textures
            trapSheetTexture = new E2DTexture(Directories.TRAP_PATH, Directories.TRAPSHEET);
            flamethrowerOnTexture = new E2DTexture(Directories.TRAP_PATH, Directories.FLAMETHROWER_ON);
            flamethrowerOffTexture = new E2DTexture(Directories.TRAP_PATH, Directories.FLAMETHROWER_OFF);

            sandSackTexture = new E2DTexture(Directories.TRAP_PATH, Directories.SANDSACK);

            // Romeo Textures
            normalRomeoTexture = new E2DTexture(Directories.NORMAL_ROMEO_PATH, Directories.NORMAL_ROMEO_WALK);
            littleRomeoTexture = new E2DTexture(Directories.LITTLE_ROMEO_PATH, Directories.LITTLE_ROMEO_WALK);
            flyingRomeoTexture = new E2DTexture(Directories.FLYING_ROMEO_PATH, Directories.FLYING_ROMEO_FLY);

            jumpingRomeoGoUpTexture = new E2DTexture(Directories.JUMPING_ROMEO_PATH, Directories.JUMPING_ROMEO_JUMP_ANIMATION);
            jumpingRomeoLandTexture = new E2DTexture(Directories.JUMPING_ROMEO_PATH, Directories.JUMPING_ROMEO_LAND_ANIMATION);
            jumpingRomeoInAirTexture = new E2DTexture(Directories.JUMPING_ROMEO_PATH, Directories.JUMPING_ROMEO_IN_AIR_ANIMATION);

            julietTexture = new E2DTexture(Directories.JULIET_PATH, Directories.JULIET_IDLE);


            /* SPRITES */

            // Romeo Animations
            normalRomeoWalkAnimation = new Sprite(normalRomeoTexture, new Rectangle(0, 0, 161, 161), 20, 30, 5);

            littleRomeoWalkAnimation = new Sprite(littleRomeoTexture, new Rectangle(0, 0, 161, 161), 20, 30, 5);

            flyingRomeoFlyAnimation = new Sprite(flyingRomeoTexture, new Rectangle(0, 0, 161, 188), 40, 30, 7);

            jumpingRomeoGoUpAnimation = new Sprite(jumpingRomeoGoUpTexture, new Rectangle(0, 0, 161, 188), 5, 30, 3);
            jumpingRomeoLandAnimation = new Sprite(jumpingRomeoLandTexture, new Rectangle(0, 0, 161, 188), 5, 30, 3);
            jumpingRomeoJumpAnimation = new Sprite(jumpingRomeoInAirTexture, new Rectangle(0, 0, 161, 188), 1, 0, 1);
            jumpingRomeoFallAnimation = new Sprite(jumpingRomeoInAirTexture, new Rectangle(0, 0, 161, 188), 9, 30, 3);

            jumpingRomeoGoUpAnimation.origin = new Vector2(jumpingRomeoGoUpAnimation.sourceRectangle.Width / 2, jumpingRomeoGoUpAnimation.sourceRectangle.Bottom);
            jumpingRomeoLandAnimation.origin = new Vector2(jumpingRomeoLandAnimation.sourceRectangle.Width / 2, jumpingRomeoLandAnimation.sourceRectangle.Bottom);
            jumpingRomeoJumpAnimation.origin = new Vector2(jumpingRomeoJumpAnimation.sourceRectangle.Width / 2, jumpingRomeoJumpAnimation.sourceRectangle.Bottom);
            jumpingRomeoFallAnimation.origin = new Vector2(jumpingRomeoFallAnimation.sourceRectangle.Width / 2, jumpingRomeoFallAnimation.sourceRectangle.Bottom);

            E2DTexture fallingHangRomeoTex = new E2DTexture(Directories.FLYING_ROMEO_PATH, Directories.FLYING_ROMEO_DEAD_ANIMATION);
            fallingHangRomeo = new Sprite(fallingHangRomeoTex, new Rectangle(0, 0, 161, 161), 20, 30, 5);
            fallingHangRomeo.origin = new Vector2(0, fallingHangRomeo.sourceRectangle.Height);

            E2DTexture burnRomeoTex = new E2DTexture(Directories.FLYING_ROMEO_PATH, "deathBurntFlying");
            hangRomeoBurn = new Sprite(burnRomeoTex, new Rectangle(0, 0, 161, 161), 29, 60, 6);

            burnRomeoTex = new E2DTexture(Directories.NORMAL_ROMEO_PATH, Directories.BURN_NORMAL_ROMEO);
            normalRomeoBurn = new Sprite(burnRomeoTex, new Rectangle(0, 0, 161, 161), 29, 30, 6);

            burnRomeoTex = new E2DTexture(Directories.LITTLE_ROMEO_PATH, "deathsquash_midget_Spritesheet161x161");
            littleRomeoSquash = new Sprite(burnRomeoTex, new Rectangle(0, 0, 161, 161), 20, 30, 5);

            burnRomeoTex = new E2DTexture(Directories.JUMPING_ROMEO_PATH, "deathburnt_pogo");
            jumpingRomeoBurn = new Sprite(burnRomeoTex, new Rectangle(0, 0, 161, 161), 6, 15, 6);

            burnRomeoTex = new E2DTexture(Directories.NORMAL_ROMEO_PATH, "deathsquash_Spritesheet161x161");
            normalRomeoCrushed = new Sprite(burnRomeoTex, new Rectangle(0, 0, 161, 161), 20, 30, 5);

            burnRomeoTex = new E2DTexture(Directories.JUMPING_ROMEO_PATH, "deathsquash_pogo_Spritesheet161x161");
            jumpingRomeoCrushed = new Sprite(burnRomeoTex, new Rectangle(0, 0, 161, 161), 20, 30, 5);

            juliet = new Sprite(julietTexture, new Rectangle(0, 0, 200, 200), 24, 60, 5);
            juliet.origin = new Vector2(juliet.Center.X, juliet.sourceRectangle.Height);

            rope = new Sprite(new E2DTexture(Directories.FLYING_ROMEO_PATH, Directories.ROPE));

            // Traps Animations
            trapButton = new Sprite(trapSheetTexture, new Rectangle(0, 192, 96, 96), 2, 0);
            trapButton.origin = trapButton.Center;

            trapDoorsClose = new Sprite(trapSheetTexture, new Rectangle(0, 0, 192, 96), 5, 0);
            trapDoorsClose.origin = trapDoorsClose.Center;

            flamethrowerOnAnimation = new Sprite(flamethrowerOnTexture, new Rectangle(0, 0, 124, 382), 19, 30, 5);
            flamethrowerOffAnimation = new Sprite(flamethrowerOffTexture, new Rectangle(0, 0, 124, 370), 4, 9);
            flamethrowerOnAnimation.origin = new Vector2(flamethrowerOnAnimation.Center.X, flamethrowerOnAnimation.sourceRectangle.Height);
            flamethrowerOffAnimation.origin = new Vector2(flamethrowerOffAnimation.Center.X, flamethrowerOffAnimation.sourceRectangle.Height - 20);

            flamethrower = new Sprite(trapSheetTexture, new Rectangle(202, 232, 80, 152));

            sandSack = new Sprite(trapSheetTexture, new Rectangle(0, 288, 96, 732), 2, 10);
            sandSack.origin = new Vector2(sandSack.Center.X, sandSack.sourceRectangle.Height);

            axe = new Sprite(trapSheetTexture, new Rectangle(1024 - 512, 192, 512, 576));
            axe.origin = new Vector2(512 / 2, 0);

            axeButton = new Sprite(trapSheetTexture, new Rectangle(200, 504, 112, 105));
            axeButton.origin = axeButton.Center;

            // UI
            playButton = new Sprite(UISheet, MathHelp.RectangleFromCenter(900, 338, 192, 192), 2, 0, 1);
            playButton.origin = playButton.Center;

            infoButton = new Sprite(UISheet, MathHelp.RectangleFromCenter(707, 334, 192, 192), 2, 0, 1);
            infoButton.origin = infoButton.Center;

            optionsButton = new Sprite(UISheet, MathHelp.RectangleFromCenter(502, 334, 192, 192), 2, 0, 1);
            optionsButton.origin = optionsButton.Center;

            logo = new Sprite(UISheet, new Rectangle(12, 634, 516, 290));
            logo.origin = logo.Center;

            pauseButton = new Sprite(UISheet, new Rectangle(594, 8, 110, 118));
            pauseButton.origin = pauseButton.Center;

            homeButton = new Sprite(UISheet, new Rectangle(0, 244, 192, 192));
            homeButton.origin = homeButton.Center;

            restartButton = new Sprite(UISheet, new Rectangle(196, 244, 192, 192));
            restartButton.origin = restartButton.Center;

            number = new Sprite(trapSheetTexture, new Rectangle(352, 816, 56, 96), 10, 0, 0, new Point(1, 1));

            scoreBoard = new Sprite(UISheet, new Rectangle(540, 672, 462, 332));
            scoreBoard.origin = scoreBoard.Center;

            // Croud
            croudSheet = new E2DTexture(Directories.CHARACTERS_PATH, Directories.CROUD);

            crowd = new Sprite(croudSheet, new Rectangle(0, 0, 48, 96), 7, 0);
            crowd.origin = crowd.Center;

            clapAnimation = new Sprite(croudSheet, new Rectangle(0, 288, 48, 32), 4, 30);
            clapAnimation.origin = clapAnimation.Center;
        }
    }

    class Directories
    {
        /* PATHS */

        // Characters
        public const string CHARACTERS_PATH = "Characters\\";        
        public const string NORMAL_ROMEO_PATH = CHARACTERS_PATH + "NormalRomeo\\";
        public const string LITTLE_ROMEO_PATH = CHARACTERS_PATH + "LittleRomeo\\";
        public const string FLYING_ROMEO_PATH = CHARACTERS_PATH + "FlyingRomeo\\";
        public const string JUMPING_ROMEO_PATH = CHARACTERS_PATH + "JumpingRomeo\\";
        public const string JULIET_PATH = CHARACTERS_PATH + "Juliet\\";

        // Background
        public const string BACKGROUND_PATH = "Background\\";

        // Font
        public const string FONT_PATH = "Fonts\\";

        // Traps
        public const string TRAP_PATH = "Traps\\";


        /* FILES */

        // Characters
        public const string NORMAL_ROMEO_WALK = "walkingRomeo";

        public const string LITTLE_ROMEO_WALK = "midgetWalk";

        public const string FLYING_ROMEO_FLY = "flying";

        public const string JUMPING_ROMEO_LAND_ANIMATION = "PogoLand";
        public const string JUMPING_ROMEO_JUMP_ANIMATION = "PogoGoUp";
        public const string JUMPING_ROMEO_IN_AIR_ANIMATION = "PogoAir";

        public const string ROPE = "rope";

        public const string JULIET_IDLE = "Juliet_idle";

        public const string CROUD = "crowd";

        public const string FLYING_ROMEO_DEAD_ANIMATION = "deathSquashFlying";
        public const string BURN_NORMAL_ROMEO = "deathburnt_Spritesheet161x161";

        // Traps
        public const string TRAPSHEET = "TrapSheet";
        public const string FLAMETHROWER_ON = "flamethrowerOn";
        public const string FLAMETHROWER_OFF = "flamethrowerOff";
        public const string SANDSACK = "SandSack";

        // Background
        public const string STAGE_BACKGROUND = "Background";
        public const string START_SCREEN_BACKGROUND = "StartScreenBack";
        public const string DARK_LAYER = "DarkLayer";

        public const string UI_SHEET = "UISheet";

        // Font
        public const string STANDARD_SPRITEFONT = "StandardSpritefont";
        public const string NUMBER_SHEET = "numberSheet";


    }
}
