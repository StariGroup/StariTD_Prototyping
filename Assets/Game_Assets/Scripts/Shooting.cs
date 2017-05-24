using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    //references
    public BuildingAssign attribute;
    //targeting
    public string playerTag;
    public GameObject[] playerUnit;
    public GameObject nearestEnemy;
    public GameObject enemyToShoot;
    private float shortestDistance;
    public float range;
    public float distance;
    //shooting
    public float fireRating;
    private float fireCountdown;
    public Vector3 startingBulletPosition;
    public GameObject bulletToShot;

    void Start()
    {
        startingBulletPosition = this.gameObject.transform.position;
        attribute = GetComponent<BuildingAssign>();
        fireRating = attribute.fireRate;
        range = attribute.range;
        playerTag = "PlayerUnit";
        fireCountdown = 0f;
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
                fireCountdown = fireRating;
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
        bulletToShot = Instantiate(attribute.usingBullet, startingBulletPosition, Quaternion.identity) as GameObject;
        bulletToShot.name = "Bullet1";
        bulletToShot.GetComponent<BulletMoving>().target = enemyToShoot;
    }
}
