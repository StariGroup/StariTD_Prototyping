using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    //targeting
    public string playerTag;
    public GameObject[] playerUnit;
    public GameObject nearestEnemy;
    public GameObject enemyToShoot;
    private float shortestDistance;
    public float range;
    public float distance;
    //shooting
    private float fireRate;
    private float fireCountdown;
    public GameObject bullet;
    private Vector3 shootingPosition;
    public GameObject newBullet;

    void Start()
    {
        playerTag = "PlayerUnit";
        range = 50f;
        fireRate = 1f;
        fireCountdown = 0f;
        shootingPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 20, this.gameObject.transform.position.z);
    }

    void Update()
    {
        shortestDistance = Mathf.Infinity;
        playerUnit = GameObject.FindGameObjectsWithTag(playerTag);

        foreach (GameObject enemy in playerUnit)
        {
            distance = Vector3.Distance(this.gameObject.transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (shortestDistance < range)
        {
            enemyToShoot = nearestEnemy;

            if (fireCountdown <= 0f)
            {
                Firing();
                fireCountdown = fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
        else
            enemyToShoot = null;
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, range);
    }

    public void Firing()
    {
        newBullet = Instantiate(bullet, shootingPosition, Quaternion.identity) as GameObject;
        newBullet.name = "Bullet1";
        newBullet.GetComponent<BulletMoving>().target = enemyToShoot;
    }
}
