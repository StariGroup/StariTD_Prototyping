using UnityEngine;
using System.Collections;

public class BuldingManager : MonoBehaviour {

    public GameObject barracks;
    public BuildingPlacement placement;
    public MouseControler mControler;
    public bool isPlaced = false;
    GameObject buildingBarracks;

    void Start()
    {
        mControler = GameObject.FindGameObjectWithTag("Controler").GetComponent<MouseControler>();
    }

    void Update ()
    {
	    if(Input.GetKeyDown("b") && !isPlaced)
        {
            buildingBarracks = Instantiate(barracks, Vector3.zero, Quaternion.identity) as GameObject;
            buildingBarracks.name = "Barracks";
            //buildingBarracks.AddComponent<BuildingPlacement>();
            isPlaced = true;
        }
        if (Input.GetMouseButtonDown(0) && isPlaced)
        {
            Destroy(buildingBarracks.GetComponent<BuildingPlacement>());
            buildingBarracks.transform.position = mControler.currentMousePosition;
            isPlaced = false;
        }
        if (Input.GetMouseButtonDown(1) && isPlaced)
        {
            Destroy(buildingBarracks);
            isPlaced = false;
        }
    }
}
