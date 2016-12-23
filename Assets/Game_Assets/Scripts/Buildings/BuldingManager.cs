using UnityEngine;
using System.Collections;

public class BuldingManager : MonoBehaviour {

    //references
    public BuildingPlacement placement;
    public MouseControler mControler;
    //building
    public GameObject buildingBarracks;
    public GameObject barracks;
    public bool isPlaced = false;
    public GameObject barracksUnit1;
    //time for selecting
    public float timer;
    public bool counting;

    void Start()
    {
        mControler = GameObject.FindGameObjectWithTag("Controler").GetComponent<MouseControler>();
        timer = 0;
        counting = false;
    }

    void Update ()
    {
        if (Input.GetKeyDown("b") && !isPlaced)
        {
            mControler.Deselecting();
            buildingBarracks = Instantiate(barracks, Vector3.zero, Quaternion.identity) as GameObject;
            buildingBarracks.name = "Barracks";
            buildingBarracks.AddComponent<BuildingPlacement>();
            isPlaced = true;
        }
        if (Input.GetMouseButtonDown(0) && isPlaced)
        {
            Destroy(buildingBarracks.GetComponent<BuildingPlacement>());
            buildingBarracks.transform.position = mControler.currentMousePosition;
            buildingBarracks.AddComponent<Barracks>();
            isPlaced = false;
            counting = true;
            //unit
            buildingBarracks.GetComponent<Barracks>().chopek = barracksUnit1;
        }
        if (Input.GetMouseButtonDown(1) && isPlaced)
        {
            Destroy(buildingBarracks);
            isPlaced = false;
        }
        //counting time for enable to select units
        if (counting && !isPlaced)
        {
            timer += Time.deltaTime;
            if(timer >= 0.5)
            {
                counting = false;
                timer = 0;
            }
        }
    }
}
