using System;
using Controllers;
using Objects;
using UnityEngine;

namespace Views
{
	public class GameView : MonoBehaviour, IGameView
	{
		public GameController Controller;
		public Character PlayerPrefab;
		public Transform PlayerStartPoint;
		public InputController InputController;
		public FollowCamera FollowCamera;
        public GameObject[] GameObjects;
        public GameObject Base;

		[SerializeField]
		public MenuView _menuView;
		[SerializeField]
		private HudView _hudView;

		public event Action PlayerDeadEvent;
		public event Action<float> PlayerHealthChangeEvent;
		
		private void OnEnable()
		{
			Controller.OnOpen(this);
		}

		private void OnDisable()
		{
			Controller.OnClose(this);
		}


		private void SpawnPlayer()
		{
			var player = Instantiate(PlayerPrefab, PlayerStartPoint.position, PlayerStartPoint.rotation);
			if (player == null) 
				return;
			var health = player.Health;
			if (health == null) 
				return;

			InputController.SetPlayer(player);
			FollowCamera.Target = player.transform;
            Controller.Player = player;
            Controller.Base = Base;
			
			health.DieEvent += OnPlayerDead;
			health.ChangeHealthEvent += OnPlayerHealthChange;
			OnPlayerHealthChange(health.Hitpoints);
		}

		public void StartGame()
		{
			SpawnPlayer();
			foreach(var o in GameObjects) 
				o.SetActive(true);
		}

		public void StopGame()
		{
			var player = Controller.Player;
			if(player != null && player.Health != null)
			{
				player.Health.DieEvent -= OnPlayerDead;
				player.Health.ChangeHealthEvent -= OnPlayerHealthChange;
			}

			Controller.Player = null;
			
			foreach(var o in GameObjects) 
				o.SetActive(false);
		}

		public IHudView HudView => _hudView;
		public IMenuView MenuView => _menuView;

		private void OnPlayerDead()
		{
			PlayerDeadEvent?.Invoke();
		}
		
		private void OnPlayerHealthChange(float value)
		{
			PlayerHealthChangeEvent?.Invoke(value);
		}
	}
}
