using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    private Buildings build;
    private MouseControler mControler;
    public string appelation;
    public float hp;
    public int id;

    void Start ()
    {
        build = GameObject.FindGameObjectWithTag("Manager").GetComponent<Buildings>();
        mControler = GameObject.FindGameObjectWithTag("Controler").GetComponent<MouseControler>();

        if (gameObject.name == "Castle")
        {
            appelation = build.castle.appellation;
            hp = build.castle.health;
            id = build.castle.id;
        }
        if (gameObject.name == "Barracks")
        {
            appelation = build.barracks.appellation;
            hp = build.barracks.health;
            id = build.barracks.id;

        }
        if (gameObject.name == "Chopek")
        {
            appelation = build.chopek.appellation;
            hp = build.chopek.health;
            id = build.chopek.id;
        }
        if (gameObject.name == "EnemyTower")
        {
            appelation = build.tower.appellation;
            hp = build.tower.health;
            id = build.tower.id;
        }
    }

}
