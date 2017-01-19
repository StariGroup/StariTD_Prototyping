using UnityEngine;
using System.Collections;

public class UnitAssign : MonoBehaviour
{

    private Unit build;
    public string appelation;
    public float hp;
    public int id;
    public float currentHp;

    void Start()
    {
        build = GameObject.FindGameObjectWithTag("Manager").GetComponent<Unit>();

        switch (gameObject.name)
        {
            case "Chopek":
                {
                    appelation = build.chopek.appellation;
                    hp = build.chopek.health;
                    id = build.chopek.id;
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
