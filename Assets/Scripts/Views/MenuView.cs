﻿using System;
 using Controllers;
using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	 public class MenuView : BaseView<IMenuView>, IMenuView
	 {
		 protected override IMenuView View => this;

		public event Action PlayEvent;
        public event Action<Player> ChoosePlayer;
        [HideInInspector]
        public NetworkManager NetworkManager;
        public TMP_InputField RoomName;
        public TMP_InputField PlayerName;

        public void ActionPlay()
        {
            PlayEvent?.Invoke();
        }
        public void Choose(string playerPrefabPath)
        {
            if(NetworkManager!=null)
                NetworkManager.PlayerPrefabPath = playerPrefabPath;
        }
        public void CreateRoom()
        {
            if (NetworkManager != null)
            {
                if (NetworkManager.IsPlayerSelected())
                {
                    PlayerPrefs.SetString("PlayerName", PlayerName.text);
                    NetworkManager.CreateGame(RoomName.text);
                    gameObject.SetActive(false);
                }
            }
           
        }
        public void RandomConnect() {
            if (NetworkManager.IsPlayerSelected())
            {   
                PlayerPrefs.SetString("PlayerName", PlayerName.text);
                NetworkManager.ConnectToRandomRoom();
                gameObject.SetActive(false);
            }
          
        }
	 }
}