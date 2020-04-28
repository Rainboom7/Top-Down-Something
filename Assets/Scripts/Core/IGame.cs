using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public interface IGame
    {
        event Action EndGameEvent;
        event Action<float> BaseHealthChangeEvent;
        event Action<float> PlayerHealthChangeEvent;

        void NewGame();


    }
}
