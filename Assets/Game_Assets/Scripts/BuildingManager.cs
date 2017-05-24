using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

    //references
    private BuildingPlacement placement;
    private MouseControler mControler;
    private BuildingsPrefabs prefab;
    //variables
    public bool isBuilding;
    public bool enableToPut;
    public GameObject toBuild;
    public GameObject toBuildPrefab;

    void Start()
    {
        mControler = GameObject.FindGameObjectWithTag("Controler").GetComponent<MouseControler>();
        prefab = GameObject.FindGameObjectWithTag("Manager").GetComponent<BuildingsPrefabs>();
        isBuilding = false;
        enableToPut = true;
    }

    void Update()
    {
        if(Input.GetKeyDown("b"))
        {
            BarracksBuild();
        }
        else if (Input.GetKeyDown("t"))
        {
            ShootingTowerBuild();
        }
    }


    public void BarracksBuild()
    {
        if (isBuilding)
        {

        }
        else
        {
            isBuilding = true;
            toBuildPrefab = prefab.barracks;
            PutBuilding(toBuildPrefab);
        }
    }

    public void ShootingTowerBuild()
    {
        if (isBuilding)
        {

        }
        else
        {
            isBuilding = true;
            toBuildPrefab = prefab.shootingTower;
            PutBuilding(toBuildPrefab);
        }
    }

    public void PutBuilding(GameObject toPut)
    {
        toBuild = Instantiate(toPut, Vector3.zero, Quaternion.identity) as GameObject;
        toBuild.name = toBuildPrefab.name;
        toBuild.tag = "ConstructingBuilding";
        toBuild.transform.position = mControler.currentMousePosition;
        toBuild.AddComponent<BuildingPlacement>();
    }

    void LateUpdate()
    {
        if (isBuilding)
        {

        }

        if (Input.GetMouseButtonDown(0) && isBuilding && enableToPut)
        {
            toBuild.GetComponent<BuildingPlacement>().enabled = false;
            toBuild.layer = 8;
            toBuild.GetComponent<Collider>().enabled = true;
            isBuilding = false;
        }
    }

}
