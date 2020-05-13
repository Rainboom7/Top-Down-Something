using System;
using System.Collections.Generic;
using Controllers;
using Objects;
using UnityEngine;

namespace Views
{
	public class GameView : MonoBehaviour, IGameView
	{
		public GameController Controller;
        public Player ActivePlayer;
        public Transform PlayerStartPoint;
		public PlayerController InputController;
		public FollowCamera FollowCamera;
        public Spawner[] GameObjects;
        public Base Base;
        public PatrolController PatrolController;
        public NetworkManager NetworkManager;

		[SerializeField]
		public MenuView _menuView;
		[SerializeField]
		private HudView _hudView;

		public event Action PlayerDeadEvent;
        public event Action<float> PlayerHealthChangeEvent;
        public event Action<float> BaseHealthChangeEvent;

        private void OnEnable()
        {
            Controller.FollowCamera = FollowCamera;
            Controller.NetworkManager = NetworkManager;
            _menuView.NetworkManager = NetworkManager;
            Controller.OnOpen(this);
        }
      

        private void OnDisable()
        {
            Controller.OnClose(this);
        }

		public void StartGame( )
		{
            foreach (var o in GameObjects)
            {         
                o.gameObject.SetActive(true);
            }

		}

		public void StopGame()
		{
   

            if (Base != null)
            {
               // if (Base.Health != null)
                   // Base.Health.DieEvent -= OnPlayerDead;
            }

                    Controller.Base = null;    
			
			foreach(var o in GameObjects) 
				o.gameObject.SetActive(false);
		}

		public IHudView HudView => _hudView;
		public IMenuView MenuView => _menuView;

		private void OnPlayerDead()
		{
			PlayerDeadEvent?.Invoke();
		}
    }
}
