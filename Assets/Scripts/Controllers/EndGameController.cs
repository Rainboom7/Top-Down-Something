using System;
using Core;

namespace Controllers
{
    public interface IEndGameView : IView
    {
        event Action ReplayEvent;

        void SetScore(int value);
    }

    public class EndGameController : IController<IEndGameView>
    {
        private readonly IGame _game;
        private readonly int _score;

        private IEndGameView _view;

        public EndGameController(IGame game, int score)
        {
            _game = game;
            _score = score;
        }

        public void OnOpen(IEndGameView view)
        {
            view.SetScore(_score);
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