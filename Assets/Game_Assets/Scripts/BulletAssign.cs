using UnityEngine;
using System.Collections;

public class BulletAssign : MonoBehaviour
{
    private Bullet round;
    public float speed;
    public float damage;

    void Start()
    {
        round = GameObject.FindGameObjectWithTag("Manager").GetComponent<Bullet>();

        switch (gameObject.name)
        {
            case "Bullet1":
                {
                    speed = round.smallBullet.speed;
                    damage = round.smallBullet.damage;

                    break;
                }
        }

    }

}
