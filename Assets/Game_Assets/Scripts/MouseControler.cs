using UnityEngine;
using System.Collections;

public class MouseControler : MonoBehaviour {

    //references
    public EditorUI output;
    public BuildingManager bManager;
    //racast
    RaycastHit hit;
    public Camera playerCamera;
    //selecting
    public Vector3 mouseDownPoint;
    public Vector3 currentMousePosition;

    void Awake ()
    {
        output = GameObject.FindGameObjectWithTag("Manager").GetComponent<EditorUI>();
        bManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<BuildingManager>();
        mouseDownPoint = Vector3.zero;
	}
	
	void Update ()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPoint = hit.point;
            }
            if (hit.collider.name == "Terrain")
            {
                currentMousePosition = hit.point;
                if (Input.GetMouseButtonDown(1))
                {
                    GameObject targetObj = Instantiate(target, hit.point, Quaternion.identity) as GameObject;
                    targetObj.name = "Target";
                }
                else if (Input.GetMouseButtonUp(0) && playerClickedLeftMouse(mouseDownPoint))
                    Deselecting();
            }//end of terrain
            else if (Input.GetMouseButtonUp(0) && playerClickedLeftMouse(mouseDownPoint))//if player clicked left mouse
            {
                if (hit.collider.transform.FindChild("Selector"))
                {
                    if (currentlySelected != hit.collider.gameObject)
                    {
                        GameObject SelectedObj = hit.collider.transform.FindChild("Selector").gameObject;
                        SelectedObj.SetActive(true);
                        if (currentlySelected != null)
                        {
                            currentlySelected.transform.FindChild("Selector").gameObject.SetActive(false);
                        }
                        currentlySelected = hit.collider.gameObject;
                        selected = true;
                        output.appellation = currentlySelected.name;
                    }
                }
                else
                    Deselecting();
            }
            /*
            else if (hit.collider.name != "Terrain" && bManager.isBuilding)
            {
               
            }
            */

            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red);

        }//end of raycast
        else if (Input.GetMouseButtonUp(0) && playerClickedLeftMouse(mouseDownPoint))
        {
            Deselecting();
        }

	}

    #region helpers
    public bool playerClickedLeftMouse(Vector3 hitPoint)
    {
        float clickZone = 1.3f;

        if (
            (mouseDownPoint.x < hitPoint.x + clickZone && mouseDownPoint.x > hitPoint.x - clickZone) &&
            (mouseDownPoint.y < hitPoint.y + clickZone && mouseDownPoint.y > hitPoint.y - clickZone) &&
            (mouseDownPoint.z < hitPoint.z + clickZone && mouseDownPoint.z > hitPoint.z - clickZone)
        )
            return true;
        else return false;

    }

    public void Deselecting()
    {
        if (currentlySelected != null)
        {
            currentlySelected.transform.FindChild("Selector").gameObject.SetActive(false);
            currentlySelected = null;
            selected = false;
            output.appellation = "BRAK";
        }
    }

    #endregion
}
