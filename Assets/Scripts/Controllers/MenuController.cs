using System;
using Core;

namespace Controllers
{
    public interface IMenuView : IView
    {
        event Action PlayEvent;
    }

    public class MenuController : IController<IMenuView>
    {
        private readonly IGame _game;

        private IMenuView _view;

        public MenuController(IGame game)
        {
            _game = game;
        }

        public void OnOpen(IMenuView view)
        {
            view.PlayEvent += OnPlay;
            _view = view;
        }

        public void OnClose(IMenuView view)
        {
            view.PlayEvent -= OnPlay;
            _view = null;
        }

        private void OnPlay( )
        {
            _view?.Close(this);
            _game.NewGame();
        }
    }
}