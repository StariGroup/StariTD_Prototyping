using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public class bullet
    {
        public float speed;
        public float damage;
        public float fireRate;
        public bullet(float _speed, float dmg, float _fireRate)
        {
            speed = _speed;
            damage = dmg;
            fireRate = _fireRate;
        }

    }
    //definitions of units
    public bullet smallBullet = new bullet(0.0001f, 50, 10f);

}