using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    private Buildings build;
    private EditorUI output;
    private MouseControler mControler;
    public string name;
    public float hp;
    public int id;

	void Start ()
    {
        build = GameObject.FindGameObjectWithTag("Manager").GetComponent<Buildings>();
        output = GameObject.FindGameObjectWithTag("Manager").GetComponent<EditorUI>();
        mControler = GameObject.FindGameObjectWithTag("Controler").GetComponent<MouseControler>();

        if (gameObject.name == "Castle")
        {
            name = build.castle.name;
            hp = build.castle.health;
            id = build.castle.id;
        }
        if (gameObject.name == "Barracks")
        {
            name = build.barracks.name;
            hp = build.barracks.health;
            id = build.barracks.id;
        }
    }

}
