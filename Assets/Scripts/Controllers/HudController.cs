using Core;

namespace Controllers
{
    public interface IHudView : IView
    {
        void SetBaseHealth(float value);
        void SetHealth(float value);

        IEndGameView EndGameView { get; }
    }

    public class HudController : IController<IHudView>
    {
        private readonly IGame _game;
        
        private IHudView _view;

        public HudController(IGame game)
        {
            _game = game;
        }

        public void OnOpen(IHudView view)
        {
            _game.EndGameEvent += OnEndGame;
            _game.BaseHealthChangeEvent += OnBaseHealthChanged;
            _game.PlayerHealthChangeEvent += OnPlayerHealthChanged;
            _view = view;
        }

        public void OnClose(IHudView view)
        {
            _game.EndGameEvent -= OnEndGame;
            _game.BaseHealthChangeEvent -= OnBaseHealthChanged;
            _game.PlayerHealthChangeEvent -= OnPlayerHealthChanged;

            _view = null;
        }

        private void OnEndGame()
        {
            _view?.Close(this);
        }

        private void OnBaseHealthChanged(float baseHealth)
        {
            _view.SetBaseHealth(baseHealth);
        }

        private void OnPlayerHealthChanged(float health)
        {
            _view?.SetHealth(health);
        }
    }
}