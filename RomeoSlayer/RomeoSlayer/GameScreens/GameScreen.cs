#region Using Statements
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Graphics;

using RomeoSlayer.AI;
using RomeoSlayer.UI;
using RomeoSlayer.Traps;
using RomeoSlayer.Sound;
using RomeoSlayer.Entities;
using RomeoSlayer.Resources;
using RomeoSlayer.GameManagers;
#endregion

namespace RomeoSlayer.GameScreens
{
    class GameScreen : Screen
    {
        // Game
        ScoreDisplay scoreDisplay;
        Sprite scoreBoard;

        public GameUI gameUI;

        // Spawner
        Vector2 spawnPosition { get { return new Vector2(1024, 548); } }
        Vector2 airSpawnPosition { get { return new Vector2(1024, 360); } }

        RomeoSpawner spawner;

        // Juliet
        Juliet juliet;

        // Croud
        Crowd crowd;

        // Traps
        const int trapsAmount = 5;
        readonly Rectangle buttonActive, buttonNotActive;

        List<Button> trapButtons;
        List<Trap> traps;

        Axe axe;
        Button axeButton;
        readonly Rectangle axeButtonActive, axeButtonNotActive;

        public GameScreen()
        {
            // Background
            background = Assets.stageBackground;

            buttonNotActive = Assets.trapButton.sourceRectangle;
            buttonNotActive.X += 96;

            buttonActive = Assets.trapButton.sourceRectangle;

            axeButtonActive = new Rectangle(200, 504, 112, 105);
            axeButtonNotActive = new Rectangle(200, 504 - 105, 112, 105);
        }

        public override void Initialize()
        {
            // Music
            Managers.soundManager.PlayMusic(Sounds.gameMusic);

            // Trap Buttons
            trapButtons = new List<Button>();
            for (int i = 0; i < trapsAmount; i++)
            {
                Button newButton = new Button(Assets.trapButton, new Vector2(262 + (164 * i), 678), Managers.screenManager.mouseState);
                newButton.OnClick += new Button.OnClickEvent(ClickButton);

                trapButtons.Add(newButton);
            }

            // Traps
            traps = new List<Trap>();
            traps.Add(new TrapDoor(     new Vector2(trapButtons[traps.Count].position.X, 552), 1));
            traps.Add(new Flamethrower( new Vector2(trapButtons[traps.Count].position.X, 552 + 34)));
            traps.Add(new SandSack(     new Vector2(trapButtons[traps.Count].position.X, 228)));
            traps.Add(new SandSack(     new Vector2(trapButtons[traps.Count].position.X, 228)));
            traps.Add(new TrapDoor(     new Vector2(trapButtons[traps.Count].position.X, 552), traps.Count+1));

            // Spawner
            spawner = new RomeoSpawner(spawnPosition, airSpawnPosition, 126);

            // Juliet
            juliet = new Juliet(new Vector2(158, 380));

            // Game UI 
            gameUI = new GameUI();

            // Score
            scoreDisplay = new ScoreDisplay(new Vector2(692, 420));

            scoreBoard = (Sprite)Assets.scoreBoard.Clone();
            scoreBoard.position = Main.WindowCenter;

            // Axe
            axeButton = new Button((Sprite)Assets.axeButton.Clone(), new Vector2(100, 678), Managers.screenManager.mouseState);
            axe = new Axe(new Vector2(Main.WindowCenter.X, -48));

            axeButton.OnClick += new Button.OnClickEvent(ActivateAxe);

            crowd = new Crowd();
        }

        public override void Update(GameTime gameTime)
        {
            gameUI.Update(gameTime);

            if (!gameUI.pause)
                if (!gameUI.gameOver)
                {
                    // Spawner Update
                    int prevDifficulty = spawner.difficulty;

                    spawner.Update(gameTime);

                    if (prevDifficulty < spawner.difficulty) // Check if difficulty got bigger
                        crowd.ChangeState(CroudState.Happy);

                    // Buttons Update
                    foreach (Button button in trapButtons)
                    {
                        button.Update(gameTime);

                        if (traps[trapButtons.IndexOf(button)].active)
                            button.sourceRectangle = buttonActive;

                        else
                            button.sourceRectangle = buttonNotActive;
                    }

                    // Traps Update
                    foreach (Trap trap in traps)
                    {
                        if (trap is TrapDoor)
                        {
                            TrapDoor trapDoor = (trap as TrapDoor);

                            trapDoor.Update(gameTime);

                            if (trapDoor.active)
                                foreach (RomeoAI romeo in spawner.romeoList)
                                    if (trapDoor.CollideWithRomeo(romeo))
                                        romeo.Die(RomeoAI.DeadStates.Fall);
                        }

                        else if (trap is Flamethrower)
                        {
                            Flamethrower flamethrower = (trap as Flamethrower);

                            flamethrower.Update(gameTime);

                            if (flamethrower.active)
                                foreach (RomeoAI romeo in spawner.romeoList)
                                    if (flamethrower.CollideWithRomeo(romeo))
                                        romeo.Die(RomeoAI.DeadStates.Burn);
                        }

                        else if (trap is SandSack)
                        {
                            SandSack sandSack = (trap as SandSack);

                            sandSack.Update(gameTime);

                            if (sandSack.active)
                                foreach (RomeoAI romeo in spawner.romeoList)
                                    if (sandSack.CollideWithRomeo(romeo) && !romeo.isDead)
                                    {
                                        Managers.soundManager.PlaySound(Sounds.grunt);
                                        romeo.Die(RomeoAI.DeadStates.Crushed);
                                    }
                        }

                        else if (trap is SandSack)
                            (trap as SandSack).Update(gameTime);
                    }

                    // Axe
                    axe.Update(gameTime);

                    if (axe.CoolDownTimer < Axe.COOL_DOWN) 
                    {
                        if (axeButton.sourceRectangle != axeButtonNotActive)
                            axeButton.sourceRectangle = axeButtonNotActive;

                        foreach (RomeoAI romeo in spawner.romeoList)
                            if (axe.CollideWithRomeo(romeo))
                            {
                                Managers.soundManager.PlaySound(Sounds.scream);
                                romeo.Die(RomeoAI.DeadStates.Slayed);
                            }
                    }
                    else 
                    {
                        if (axeButton.sourceRectangle != axeButtonActive)
                            axeButton.sourceRectangle = axeButtonActive;

                        axeButton.Update(gameTime);
                    }
                    
                    // Croud
                    crowd.Update(gameTime);

                    // Julliet
                    juliet.Update(gameTime);
                }
        }

        private void ActivateAxe(Button button)
        {
            axe.Activate();
            crowd.ChangeState(CroudState.Surprise);
        }

        private void ClickButton(Button button)
        {
            if (trapButtons.IndexOf(button) < traps.Count)
            {
                Trap trap = traps[trapButtons.IndexOf(button)];

                if (!trap.active)
                {
                    if (trap is Flamethrower)
                        Managers.soundManager.PlaySound(Sounds.flamethrowerOn);

                    else if (trap is TrapDoor)
                        Managers.soundManager.PlaySound(Sounds.trapDoorOpen);

                    trap.active = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Croud
            crowd.Draw(spriteBatch);

            // Background
            spriteBatch.DrawE2DTexture(Assets.darkLayer, Vector2.Zero);
            base.Draw(spriteBatch);
            
            // Traps back
            foreach (Trap trap in traps)
                if (trap is TrapDoor)
                    trap.Draw(spriteBatch);

            // Spawner
            spawner.Draw(spriteBatch);

            // Juliet
            juliet.Draw(spriteBatch);

            // Trap Buttons
            foreach (Button button in trapButtons)
                button.Draw(spriteBatch);

            axeButton.Draw(spriteBatch);

            // Traps front
            foreach (Trap trap in traps)
                if (!(trap is TrapDoor))
                    trap.Draw(spriteBatch);

            axe.Draw(spriteBatch);

            // UI
            gameUI.Draw(spriteBatch);

            // Score
            if (gameUI.gameOver)
            {
                scoreBoard.Draw(spriteBatch);
                scoreDisplay.Draw(spriteBatch, spawner.romeoKills);
            }

            spriteBatch.End();
        }
    }
}
