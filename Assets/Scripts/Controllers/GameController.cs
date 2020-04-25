using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Core;

namespace Controllers
{
    public interface IGameView
	{
		event Action PlayerDeadEvent;
		event Action<float> PlayerHealthChangeEvent;
        
		void StartGame();
		void StopGame();
		
		IHudView HudView { get; }
		IMenuView MenuView { get; }
	}

	[CreateAssetMenu(menuName = "Game Controller")]
	public class GameController : ScriptableObject, IGame
	{
		public event Action EndGameEvent;
		public event Action<int> BaseHealthChangeEvent;
		public event Action<float> PlayerHealthChangeEvent;

		public Character Player { get; set; }
        private readonly List<GameObject> _objects = new List<GameObject>();
        public GameObject Base;

        private IGameView _view;
        [Range(1,500)]
        public int StartBaseHealth;
		private int _baseHealth;

		public void AddObject(GameObject obj)
		{
			_objects.Add(obj);
		}

		public void RemoveObject(GameObject obj)
		{
			_objects.Remove(obj);
		}

		public void DamageBase(int damage)
		{
			_baseHealth -= damage;
            BaseHealthChangeEvent?.Invoke(_baseHealth);	
		}

		public void NewGame()
		{
            _baseHealth = StartBaseHealth;
			_view?.HudView?.Open(new HudController(this));
			_view?.StartGame();
		}

		public void OnOpen(IGameView view)
		{
			view.PlayerDeadEvent += OnPlayerDead;
			view.PlayerHealthChangeEvent += OnPlayerHealthChange;
			view.MenuView?.Open(new MenuController(this));
            _view = view;
		}

		public void OnClose(IGameView view)
		{
			view.PlayerDeadEvent -= OnPlayerDead;
			view.PlayerHealthChangeEvent -= OnPlayerHealthChange;
			_view = null;
		}

		private void OnPlayerDead()
		{
			_view?.StopGame();
			
			//_view?.HudView.EndGameView?.Open(new EndGameController(this, _scores));

			foreach (var o in _objects.ToArray()) 
				Destroy(o);
			_objects.Clear();

			EndGameEvent?.Invoke();
		}

		private void OnPlayerHealthChange(float value)
		{
			PlayerHealthChangeEvent?.Invoke(value);
		}
	}
}
