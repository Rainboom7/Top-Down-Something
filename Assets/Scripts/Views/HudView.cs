using Controllers;
using Objects;
using TMPro;
using UnityEngine;

namespace Views
{
	public class HudView : BaseView<IHudView>, IHudView
	{
		protected override IHudView View => this;

		[SerializeField]
		private EndGameView _endGameView;

		public TextMeshProUGUI Basehealthtext;
		public TextMeshProUGUI HitpointsText;

		private Character _playerCharacter;

		public void SetBaseHealth(int value)
		{
			Basehealthtext.text = value.ToString();
		}

		public void SetHealth(float value)
		{
			HitpointsText.text = Mathf.RoundToInt(value) + "%";
		}

		public IEndGameView EndGameView => _endGameView;
	}
}
