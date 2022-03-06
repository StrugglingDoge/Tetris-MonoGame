﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tetris.Game.InGame;
using Tetris.Game.Managers;
using Tetris.Util;

namespace Tetris.Game.Mode.Modes
{
    public class Challenge : CustomMode
    {
        //TODO: Idea is random challenge(3 tetris', 2 double lines & 1 tetris, etc.) every X amount of seconds. If player fails challenge, depending on how much was completed of the challenge, is punished with Y rows.

        private float RemainingTime { get; set; } = 1000f;

        private int[] CurrentChallenge { get; set; } = new int[3];
        private string[] ChallengeDesc { get; set; } = new string[3];

        private ushort TotalChal { get; set; } = 4;
        
        public Challenge(string name, string objective) : base(name, objective) {}
        
        public override void OnGameStart()
        {
            //If the level selected is higher than ten, limit it to 8 rows generated to make it more fair.
            var rowsToGenerate = ScoreHandler.Instance.Level > 10 ? 8 : ScoreHandler.Instance.Level - 3;

            if (ScoreHandler.Instance.Level > 3)
                TetrisBoard.Instance.RandomBlock(rowsToGenerate);
            base.OnGameStart();
        }

        public override void OnRowRemove()
        {
            //for hardcore mode
            if (TetrisBoard.Instance.GetActualLines() is > 1 and < 4 &&
                !ScoreHandler.Instance.WasTSpin)
                if (TetrisBoard.Instance.GetTotalLines() != 4) // don't punish for getting a tetris
                    TetrisBoard.Instance.RandomBlock(TetrisBoard.Instance.GetActualLines() - 1);
            base.OnRowRemove();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        private string ChallengeName(int chal)
        {
            switch (chal)
            {
                case 0:
                    return "Double Line";
                case 1:
                    return "Triple Line";
                case 2:
                    return "Tetris";
                default:
                    return "";
            }
        }
        
        /// <summary>
        /// Executes the given challenge
        /// </summary>
        /// <param name="chal"></param>
        private void ToChallenge(int chal)
        {
            switch (chal)
            {
                case 0:
                    
                    break;
            }
        }
        
        private void GenerateChallenge()
        {
            for (int i = 0; i < CurrentChallenge.Length; i++)
            {
                int chal = GameManager.Instance.Random.Next(0, TotalChal);
                int count = GameManager.Instance.Random.Next(1, 3);
                CurrentChallenge[i] = chal;
                string name = ChallengeName(chal) + (count > 1 ? "s" : "");
                ChallengeDesc[i] = $"{count}x {name}";
            }
        }
    }
}