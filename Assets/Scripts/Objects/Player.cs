using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{

    public class Player : Character
    {
        public Weapon Weapon;
        public void Fire(GameObject target)
        {
            if (Weapon == null)
                return;
            transform.rotation = Quaternion.LookRotation(target.transform.position);
            Weapon.Fire(target);

        }

    }
}
