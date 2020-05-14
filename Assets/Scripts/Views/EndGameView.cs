﻿using System;
using System.Collections.Generic;
using Controllers;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

namespace Views
{
	public class EndGameView : BaseView, IEndGameView
	{
	
		public event Action LeaveEvent;
        public List<PlayerScoreView> PlayerInfos;
        private int _currentPlayer = 0;
        public void EndGame(List<Photon.Realtime.Player> newPlayerList)
        {
            int i;
            for ( i= 0; i < PlayerInfos.Capacity && i < newPlayerList.Capacity; i++)
            {
                PlayerInfos[i].Name = newPlayerList[i].NickName;
                PlayerInfos[i].Score = ScoreExtensions.GetScore(newPlayerList[i]);
                PlayerInfos[i].UpdateView();
            }
            while (i <= 3)
            {
                PlayerInfos[i].Name = "";
                PlayerInfos[i].Score = -1;
                PlayerInfos[i].UpdateView();
                i++;
            }
            gameObject.SetActive(true);
        }
        public void OnLeaveClick() {
            LeaveEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
