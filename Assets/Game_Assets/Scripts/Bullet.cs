using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public class bullet
    {
        public float speed;
        public float damage;
        public bullet(float _speed, float dmg)
        {
            speed = _speed;
            damage = dmg;
        }

    }
    //definitions of units
    public bullet smallBullet = new bullet(130f, 50);

}