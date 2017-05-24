using UnityEngine;
using System.Collections;

public class BuildingAssign : MonoBehaviour
{
    //references
    private Buildings build;
    private BulletToBuilding bulletAssignment;
    public BuildingsPrefabs buildingAssignment;
    //variables
    public string appelation;
    public float hp;
    public int id;
    public float fireRate;
    public float range;
    //prefabs
    public GameObject usingBullet;

    void Start()
    {
        bulletAssignment = GameObject.FindGameObjectWithTag("Manager").GetComponent<BulletToBuilding>();
        buildingAssignment = GameObject.FindGameObjectWithTag("Manager").GetComponent<BuildingsPrefabs>();

        build = GameObject.FindGameObjectWithTag("Manager").GetComponent<Buildings>();

        switch (gameObject.name)
        {
            case "Base":
                {
                    appelation = build.nexus.appellation;
                    hp = build.nexus.health;
                    id = build.nexus.id;
                    //for shooting
                    fireRate = build.nexus.fireRate;
                    range = build.nexus.range;
                    usingBullet = bulletAssignment.baseBullet;
                    break;
                }
            case "Barracks":
                {
                    appelation = build.barracks.appellation;
                    hp = build.barracks.health;
                    id = build.barracks.id;
                    break;
                }
            case "ShootingTower":
                {
                    appelation = build.tower.appellation;
                    hp = build.tower.health;
                    id = build.tower.id;
                    //for shooting
                    fireRate = build.tower.fireRate;
                    range = build.tower.range;
                    usingBullet = bulletAssignment.shootingTowerBullet;
                    break;
                }
        }

    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
