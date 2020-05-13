using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Core;
using Photon.Realtime;
using System;
using Objects;
using Views;
using Photon.Pun.UtilityScripts;

namespace Controllers
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public GameController Controller;
        [HideInInspector]
        public string PlayerPrefabPath;
        public event Action OnJoinedRoomEvent;
        public GameObject BasePoint;
        public string BasePrefabPath;
        public GameObject LoadingText;
        public MenuView MainMenu;
        public HudView HudView;
        public LobbyView LobbyView;
        private Coroutine _scoreCoroutine;

        private List<PlayerController> _players = new List<PlayerController>();


        public void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
            LobbyView.BeginGameAction += OnGameStart;

        }

        public bool IsPlayerSelected()
        {
            return (!PlayerPrefabPath.Equals(""));
        }
        public override void OnCreatedRoom()
        {

        }

        [PunRPC]
        private void Spawn()
        {      
            Debug.Log(PhotonNetwork.CurrentRoom);
            GameObject player = PhotonNetwork.Instantiate(PlayerPrefabPath, transform.position, Quaternion.identity);
        }
        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
        }
        public override void OnJoinedRoom()
        {
            LobbyView.gameObject.SetActive(true);
            LobbyView.AddPlayer(PlayerPrefs.GetString("PlayerName"));
            Debug.Log("OnJoinedRoom");
            base.OnJoinedRoom();
           
        }
        public void OnGameStart() {
            _scoreCoroutine = StartCoroutine(UpdateScore());
           photonView.RPC("Spawn", RpcTarget.All);
            Controller.Base = PhotonNetwork.Instantiate(BasePrefabPath, BasePoint.transform.position, Quaternion.Euler(90f, 0, 0)).GetComponentInChildren<Base>();
            Controller.NewGame();
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("OnCreateRoomFailed");
            base.OnCreateRoomFailed(returnCode, message);

        }
        public override void OnConnectedToMaster()
        {
            LoadingText.SetActive(false);
            Debug.Log("OnConnectedToMaster");
            MainMenu.gameObject.SetActive(true);
     
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed :" + message);
            base.OnJoinRandomFailed(returnCode, message);

        }
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            Debug.Log("OnLeftRoom");
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

        }
        public void AddPlayer(PlayerController player)
        {
            if (!_players.Contains(player))
            {
                _players.Add(player);
            }
            
        }
        public void CreateGame(string room)
        {
            if (PhotonNetwork.InRoom)
                PhotonNetwork.LeaveRoom();
            PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"] = PlayerPrefs.GetString("PlayerName");
            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("PlayerName");
            PhotonNetwork.CreateRoom(room);
        }
        public void ConnectToRandomRoom()
        {
            PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"] = PlayerPrefs.GetString("PlayerName");
            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("PlayerName");
            PhotonNetwork.JoinRandomRoom();
        }
        private IEnumerator UpdateScore()
        {
            while (true)
            {
                var list = new List<Photon.Realtime.Player>(PhotonNetwork.PlayerList);
                list.Sort((player1, player2) => ScoreExtensions.GetScore(player2).CompareTo(ScoreExtensions.GetScore(player1)));
                HudView?.UpdateBoard(list);
                yield return new WaitForSeconds(0.5f);
            }

        }
      
     

     
       



    }
}
