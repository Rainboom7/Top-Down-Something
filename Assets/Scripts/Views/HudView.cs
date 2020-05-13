using Controllers;
using Objects;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

namespace Views
{
	public class HudView : BaseView<IHudView>, IHudView
	{
        protected override IHudView View => this;
        public List<PlayerScoreView> PlayerInfos;
        private int _currentPlayer=0;
        public void UpdateBoard(List<Photon.Realtime.Player> newPlayerList)
        {
            for (int i = 0; i < PlayerInfos.Capacity&&i<newPlayerList.Capacity; i++)
            {
                PlayerInfos[i].Name = newPlayerList[i].NickName;
                PlayerInfos[i].Score = ScoreExtensions.GetScore(newPlayerList[i]);
                PlayerInfos[i].UpdateView();
            }
        }
        public void AddPlayer(string name)
        {
            PlayerScoreView playerScore = PlayerInfos[_currentPlayer];
            playerScore.Name = name;
            playerScore.Score = 0;
            playerScore.UpdateView();
            _currentPlayer++;
        }
        
	}
}
