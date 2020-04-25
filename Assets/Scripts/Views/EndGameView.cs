using System;
using Controllers;
using TMPro;

namespace Views
{
	public class EndGameView : BaseView<IEndGameView>, IEndGameView
	{
		protected override IEndGameView View => this;

		public TextMeshProUGUI ScoreText;

		public event Action ReplayEvent;
		
		public void SetScore(int value)
		{
			ScoreText.text = value.ToString();
		}

		public void ActionReplay()
		{
			ReplayEvent?.Invoke();
		}
	}
}
