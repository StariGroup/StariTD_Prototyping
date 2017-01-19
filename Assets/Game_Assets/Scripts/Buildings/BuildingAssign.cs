using UnityEngine;
using System.Collections;

public class BuildingAssign : MonoBehaviour
{
    private Buildings build;
    public GameObject ShootingTowerBullet;
    public string appelation;
    public float hp;
    public int id;
    public float currentHp;
    void Start()
    {
        build = GameObject.FindGameObjectWithTag("Manager").GetComponent<Buildings>();

        switch (gameObject.name)
        {
            case "Base":
                {
                    appelation = build.castle.appellation;
                    hp = build.castle.health;
                    id = build.castle.id;
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
                    this.gameObject.AddComponent<Shooting>();
                    this.gameObject.GetComponent<Shooting>().bullet = ShootingTowerBullet;
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
