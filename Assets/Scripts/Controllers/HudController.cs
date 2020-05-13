using Core;

namespace Controllers
{
    public interface IHudView : IView
    {
       
      //  IEndGameView EndGameView { get; }
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
            _view = view;
        }

        public void OnClose(IHudView view)
        {
            _view = null;
        }

      
    }
}