using UnityEngine;
using System.Collections;

public class MouseControler : MonoBehaviour {

    //references
    public EditorUI output;
    public Unit unit;
    public BuldingManager bManager;
    //racast
    RaycastHit hit;
    public Camera playerCamera;
    public GameObject target;
    public int ignoreLayer;
    //selecting
    public GameObject currentlySelected;
    public Vector3 mouseDownPoint;
    public Vector3 currentMousePosition;
    public bool selected = false;
    public Unit selectedUnit;

    void Awake ()
    {
        output = GameObject.FindGameObjectWithTag("Manager").GetComponent<EditorUI>();
        unit = GameObject.FindGameObjectWithTag("Unit").GetComponent<Unit>();
        bManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<BuldingManager>();
        mouseDownPoint = Vector3.zero;
	}
	
	void Update ()
    {

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPoint = hit.point;
            }
            Debug.Log("Hit " + hit.collider.name);
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
            else if (Input.GetMouseButtonUp(0) && playerClickedLeftMouse(mouseDownPoint))
            {
                if (hit.collider.transform.FindChild("Selector"))
                {
                    Debug.Log("Found selectable");
                    if (currentlySelected != hit.collider.gameObject)
                    {
                        GameObject SelectedObj = hit.collider.transform.FindChild("Selector").gameObject;
                        SelectedObj.SetActive(true);
                        selectedUnit = hit.collider.GetComponent<Unit>();
                        if (currentlySelected != null)
                        {
                            currentlySelected.transform.FindChild("Selector").gameObject.SetActive(false);
                        }
                        currentlySelected = hit.collider.gameObject;
                        selected = true;
                        output.name = selectedUnit.name;
                    }
                }
                else
                    Deselecting();
            }
            else if(hit.collider.name != "Terrain" && bManager.isPlaced)
            {
                ignoreLayer = 8;
            }
        }//end of raycast
        else if (Input.GetMouseButtonUp(0) && playerClickedLeftMouse(mouseDownPoint))
        {
            Deselecting();
        }



        Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red);
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
            selectedUnit = null;
            output.name = "BRAK";
        }
    }

    #endregion
}
