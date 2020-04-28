using System;
using Controllers;
using TMPro;
using UnityEngine;

namespace Views
{
	public class EndGameView : BaseView<IEndGameView>, IEndGameView
	{
		protected override IEndGameView View => this;

		public event Action ReplayEvent;
	
		public void ActionReplay()
		{
            ReplayEvent?.Invoke();
            
		}
	}
}
