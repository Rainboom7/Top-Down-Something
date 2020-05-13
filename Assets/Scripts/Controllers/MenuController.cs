using System;
using Core;
using Objects;

namespace Controllers
{
    public interface IMenuView : IView
    {
        event Action<Player> ChoosePlayer;
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

            view.ChoosePlayer += Choose;
            view.PlayEvent += Start;
            _view = view;
        }

        public void OnClose(IMenuView view)
        {
            view.ChoosePlayer -= Choose;
            view.PlayEvent -= Start;
            _view = null;
        }

        private void Choose(Player player)
        {
            _view?.Close(this);
            _game.ChoosePlayer(player);
        }
        private void Start()
        {

            _view?.Close(this);
            _game.NewGame();

        }
    }
}