using Controllers;
using Objects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	public class HudView : BaseView<IHudView>, IHudView
	{
		protected override IHudView View => this;

		[SerializeField]
		private EndGameView _endGameView;

		public Text BaseHealthtext;
        public Text HitpointsText;
        public Text AmmoText;


        private Character _playerCharacter;
        public Action<int> SelectPlayerEvent;

		public void SetBaseHealth(float value)
		{
			BaseHealthtext.text = value.ToString();
		}

        public void SetHealth(float value)
        {
            HitpointsText.text = value.ToString();
        }
        public void SetAmmo(int currentAmmo, int maxAmmo)
        {
            AmmoText.text = currentAmmo.ToString() + '/' + maxAmmo.ToString();

        }
        public void SelectPlayer(int index) {
            SelectPlayerEvent?.Invoke(index);

        }
		public IEndGameView EndGameView => _endGameView;
	}
}
