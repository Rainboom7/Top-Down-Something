using System;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public interface IEndGameView : IView
    {
        event Action LeaveEvent;

    }

    public class EndGameController : IController<IEndGameView>
    {
        private readonly IGame _game;

        private IEndGameView _view;

        public EndGameController(IGame game)
        {
            _game = game;
        }

        public void OnOpen(IEndGameView view)
        {
            view.LeaveEvent += OnReplay;
            _view = view;
        }

        public void OnClose(IEndGameView view)
        {
            view.LeaveEvent -= OnReplay;
            _view = null;
        }

        private void OnReplay()
        {
            _view?.Close();
        //    _game.NewGame();
        }
    }
}