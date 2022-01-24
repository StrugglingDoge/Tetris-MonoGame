﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tetris.Game;
using Tetris.Game.InGame;
using Tetris.Game.Managers;
using Tetris.Game.Mode;
using Tetris.GUI.Elements;
using Tetris.Multiplayer.Network;
using Tetris.Util;

namespace Tetris.GUI.Screens
{
    public class GuiGamePreferences : GuiScreen
    {
        public override void SetUp()
        {
            base.SetUp();
            Buttons.Add(new Button(1, new Vector2(840, 265), ">", Globals.Hoog48, false));
            Buttons.Add(new Button(2, new Vector2(440, 265), "<", Globals.Hoog48, false));
            Buttons.Add(new Button(3, new Vector2(840, 365), ">", Globals.Hoog48, false));
            Buttons.Add(new Button(4, new Vector2(440, 365), "<", Globals.Hoog48, false));
            Buttons.Add(new Button(5, new Vector2(640, 510), "Start", Globals.Hoog48));
            Buttons.Add(new Button(6, new Vector2(640, 610), "Back", Globals.Hoog48));
            Buttons[0].OnClick += LevelRight;
            Buttons[1].OnClick += LevelLeft;
            Buttons[2].OnClick += ModeRight;
            Buttons[3].OnClick += ModeLeft;
            Buttons[4].OnClick += Start;
            Buttons[5].OnClick += s => Gui.SetCurrentScreen(new GuiMainMenu());
            ButtonsDrawn = true;
        }

        public override void DrawScreen(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.DrawScreen(spriteBatch, gameTime);
            var mult = Opacity > 0.5f ? 0.5f : Opacity;
            spriteBatch.Begin();
            spriteBatch.DrawBorderedRect(new Rectangle(320, 30, 640, 450), PanelBackgroundCol * mult,
                PanelBorderCol * Opacity);

            spriteBatch.DrawCenteredString(Globals.Hoog48,
                $"Level {ScoreHandler.Instance.SelectedLevel}", new Vector2(640, 300),
                Color.White * Opacity);
            spriteBatch.DrawCenteredString(Globals.Hoog48,
                $"{ModeManager.Instance.GetCurrentMode().Name}", new Vector2(640, 400),
                Color.White * Opacity);
            spriteBatch.DrawCenteredString(Globals.Hoog24,
                "Objective:", new Vector2(640, 50),
                Color.White * Opacity);
            spriteBatch.DrawCenteredString(Globals.Hoog24,
                $"{ModeManager.Instance.GetCurrentMode().Objective}", new Vector2(640, 160),
                Color.White * Opacity);

            foreach (var but in Buttons) but.Draw(spriteBatch, Color.White);
            spriteBatch.End();
        }

        private void Start(object sender)
        {
            Gui.Instance.SetCurrentScreen(new GuiInGame());
            NetworkManager.Instance.SendPacket(6);
        }

        private void ModeLeft(object sender)
        {
            var modes = ModeManager.Instance.GameModes;
            for (int i = 0; i < modes.Count; i++)
            {
                if (modes[i].Name == ModeManager.Instance.GetCurrentMode().Name)
                {
                    int newMode = i-1;
                    if (newMode == -1)
                        newMode = modes.Count-1;
                    ModeManager.Instance.SetCurrentMode(modes[newMode].Name);
                    break;
                }
            }
        }

        private void ModeRight(object sender)
        {
            var modes = ModeManager.Instance.GameModes;
            for (int i = 0; i < modes.Count; i++)
            {
                if (modes[i].Name == ModeManager.Instance.GetCurrentMode().Name)
                {
                    int newMode = i+1;
                    if (newMode == modes.Count)
                        newMode = 0;
                    ModeManager.Instance.SetCurrentMode(modes[newMode].Name);
                    break;
                }
            }
        }

        private void LevelLeft(object sender)
        {
            var level = ScoreHandler.Instance.SelectedLevel;
            if (level == 1)
                level = 20;
            else
                level--;
            ScoreHandler.Instance.SelectedLevel = level;
        }

        private void LevelRight(object sender)
        {
            var level = ScoreHandler.Instance.SelectedLevel;
            if (level == 20)
                level = 1;
            else
                level++;
            ScoreHandler.Instance.SelectedLevel = level;
        }
    }
}