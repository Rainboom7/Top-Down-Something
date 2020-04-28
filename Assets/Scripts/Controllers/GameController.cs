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
        event Action<float> BaseHealthChangeEvent;

        void StartGame();
		void StopGame();
		
		IHudView HudView { get; }
		IMenuView MenuView { get; }
	}

	[CreateAssetMenu(menuName = "Game Controller")]
	public class GameController : ScriptableObject, IGame
	{
		public event Action EndGameEvent;
		public event Action<float> BaseHealthChangeEvent;
		public event Action<float> PlayerHealthChangeEvent;

		public Character Player { get; set; }
        private readonly List<GameObject> _objects = new List<GameObject>();
        public Base Base { get; set; }

        private IGameView _view;


		public void AddObject(GameObject obj)
		{
			_objects.Add(obj);
		}

		public void RemoveObject(GameObject obj)
		{
			_objects.Remove(obj);
		}



		public void NewGame()
		{
			_view?.HudView?.Open(new HudController(this));
			_view?.StartGame();
		}

		public void OnOpen(IGameView view)
		{
			view.PlayerDeadEvent += OnPlayerDead;
            view.PlayerHealthChangeEvent += OnPlayerHealthChange;
            view.BaseHealthChangeEvent += OnBaseHealthChange;
			view.MenuView?.Open(new MenuController(this));
            _view = view;
		}

		public void OnClose(IGameView view)
		{
			view.PlayerDeadEvent -= OnPlayerDead;
            view.PlayerHealthChangeEvent -= OnPlayerHealthChange;
            view.BaseHealthChangeEvent -= OnBaseHealthChange;
            _view = null;
		}

		private void OnPlayerDead()
		{
			_view?.StopGame();
        
			_view?.HudView.EndGameView?.Open(new EndGameController(this));

			foreach (var o in _objects.ToArray()) 
				Destroy(o);
			_objects.Clear();

			EndGameEvent?.Invoke();
		}

        private void OnPlayerHealthChange(float value)
        {
            PlayerHealthChangeEvent?.Invoke(value);
        }
        private void OnBaseHealthChange(float value)
        {
            BaseHealthChangeEvent?.Invoke(value);
        }
    }
}
