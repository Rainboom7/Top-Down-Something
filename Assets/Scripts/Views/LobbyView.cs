using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    public Text[] Players;
    private int _currentPlayer = 0;
    public event Action BeginGameAction;
    public void AddPlayer(string name)
    {
        Players[_currentPlayer].text = name;
        _currentPlayer++;
    }
    public void OnStartClick()
    {
        BeginGameAction?.Invoke();
    }

}
