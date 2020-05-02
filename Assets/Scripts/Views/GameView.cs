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
        public List<Player> PlayerPrefab;
        public List<Player> ActivePlayers;
        public Transform PlayerStartPoint;
		public InputController InputController;
		public FollowCamera FollowCamera;
        public GameObject[] GameObjects;
        public Base Base;
        public PatrolController PatrolController;

		[SerializeField]
		public MenuView _menuView;
		[SerializeField]
		private HudView _hudView;

		public event Action PlayerDeadEvent;
        public event Action<float> PlayerHealthChangeEvent;
        public event Action<float> BaseHealthChangeEvent;

        private void OnEnable()
		{
            if (PlayerPrefab != null)
                ActivePlayers = new List<Player>();
            Controller.OnOpen(this);

        }

        private void OnDisable()
        {
            Controller.OnClose(this);
        }

        private void SetPlayer(int index) {
            if (index < 0 && index > ActivePlayers.Capacity)
                return;


            var player = Controller.Player;
            if (player != null && player.Health != null)
            {
                player.Health.DieEvent -= OnPlayerDead;
                player.Health.ChangeHealthEvent -= OnPlayerHealthChange;
                if (player.Weapon != null)
                    player.Weapon.AmmoChangeEvent -= OnAmmoChange;
            }

            player = ActivePlayers[index];
            PatrolController?.StartPatroling();
            if (player == null)
                return;
            player.Movement?.StopPatrol();
            if(player.Behaviour!=null)
                 player.Behaviour.IsPlayer = true;
            var health = player.Health;
            if (health == null)
                return;
            if (player.Weapon == null)
                return;
            player.Weapon.AmmoChangeEvent += OnAmmoChange;
            InputController.SetPlayer(player);
            FollowCamera.Target = player.transform;
            Controller.Player = player;
        

            health.DieEvent += OnPlayerDead;
            health.ChangeHealthEvent += OnPlayerHealthChange;
            OnPlayerHealthChange(health.Currenthealth);
            OnAmmoChange(player.Weapon.Ammo, player.Weapon.MaxAmmo);

        }


        private void SpawnPlayers()
        {

            if (Controller.Player != null)
                Destroy(Controller.Player);
            Controller.Player = null;
            for (int i = 0; i < PlayerPrefab.Capacity; i++)
            {
                ActivePlayers.Add(Instantiate(PlayerPrefab[i], PlayerStartPoint.position + new Vector3(0, 0, 0.5f * i), PlayerStartPoint.rotation));
                Controller.AddObject(ActivePlayers[i].gameObject);
            }
            if(PatrolController!=null)
                PatrolController.Patrolers = ActivePlayers;
        }
           
        


		public void StartGame()
		{

            _hudView.SelectPlayerEvent += SetPlayer;
            SpawnPlayers();
            SetPlayer(0);
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
            if (ActivePlayers != null)
                ActivePlayers.Clear();
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
