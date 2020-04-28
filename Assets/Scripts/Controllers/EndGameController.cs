using System;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public interface IEndGameView : IView
    {
        event Action ReplayEvent;

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
            view.ReplayEvent += OnReplay;
            _view = view;
        }

        public void OnClose(IEndGameView view)
        {
            view.ReplayEvent -= OnReplay;
            _view = null;
        }

        private void OnReplay()
        {
            _view?.Close(this);
            _game.NewGame();
        }
    }
}