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
        public Base Base;

		[SerializeField]
		public MenuView _menuView;
		[SerializeField]
		private HudView _hudView;

		public event Action PlayerDeadEvent;
        public event Action<float> PlayerHealthChangeEvent;
        public event Action<float> BaseHealthChangeEvent;

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
            Debug.Log(gameObject.name);
            if (Controller.Player != null)
                Destroy(Controller.Player);
            Controller.Player = null;
            var player = Instantiate(PlayerPrefab, PlayerStartPoint.position, PlayerStartPoint.rotation);
            if (player == null)
                return;
            var health = player.Health;
            if (health == null)
                return;

            if (player.Weapon == null)
                return;
            player.Weapon.AmmoChangeEvent += OnAmmoChange;

            InputController.SetPlayer(player);
            FollowCamera.Target = player.transform;
            Controller.Player = player;
            Controller.AddObject(player.gameObject);

            health.DieEvent += OnPlayerDead;
            health.ChangeHealthEvent += OnPlayerHealthChange;
            OnPlayerHealthChange(health.Hitpoints);
            OnAmmoChange(player.Weapon.Ammo, player.Weapon.Ammo);
        }


		public void StartGame()
		{
            SpawnPlayer();
            Base.Health.DieEvent += OnPlayerDead;
            Base.Health.ChangeHealthEvent += OnBaseHealthChange;
            Controller.Base = Base;
            OnBaseHealthChange(Base.Health.Hitpoints);
			foreach(var o in GameObjects) 
				o.SetActive(true);
		}

		public void StopGame()
		{
			var player = Controller.Player;
            if (player != null && player.Health != null)
            {
                player.Health.DieEvent -= OnPlayerDead;
                player.Health.ChangeHealthEvent -= OnPlayerHealthChange;
                if(player.Weapon!=null)
                    player.Weapon.AmmoChangeEvent -= OnAmmoChange;


            }
            
            if (Base != null)
            {
                Base.Health.ChangeHealthEvent -= OnBaseHealthChange;
                if (Base.Health != null)
                    Base.Health.DieEvent -= OnPlayerDead;
            }

                    Controller.Base = null;
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
        private void OnBaseHealthChange(float value)
        {
            BaseHealthChangeEvent?.Invoke(value);
        }
        private void OnAmmoChange(int ammo, int maxAmmo)
        {
            _hudView?.SetAmmo(ammo, maxAmmo);
        }
    }
}
