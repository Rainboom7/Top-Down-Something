using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public abstract class Weapon : MonoBehaviour
    {
        public float Damage;
        public abstract void Fire();
        
    }
}