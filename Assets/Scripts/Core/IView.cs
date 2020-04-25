using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public interface IView 
    {
        void Open<T>(IController<T> controller) where T : IView;
        void Close<T>(IController<T> controller) where T : IView;
    }
}