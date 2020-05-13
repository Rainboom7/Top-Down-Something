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


        void StartGame( );
		void StopGame();
		
		IHudView HudView { get; }
		IMenuView MenuView { get; }
	}

    [CreateAssetMenu(menuName = "Game Controller")]
    public class GameController : ScriptableObject, IGame
    {
        public event Action EndGameEvent;
        public NetworkManager NetworkManager;
        public FollowCamera FollowCamera;

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
            _view = view;
		}

		public void OnClose(IGameView view)
		{
	            _view = null;
		}

		private void OnPlayerDead()
		{
			_view?.StopGame();
        
			//_view?.HudView.EndGameView?.Open(new EndGameController(this));

			foreach (var o in _objects.ToArray()) 
				Destroy(o);
			_objects.Clear();
			EndGameEvent?.Invoke();
		}
        public void ChoosePlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
