using UnityEngine;
using System.Collections;

public class BuildingPlacement : MonoBehaviour {

    public MouseControler mControler;

    void Start()
    {
        mControler = GameObject.FindGameObjectWithTag("Controler").GetComponent<MouseControler>();
    }

    void Update()
    {
        this.gameObject.transform.position = mControler.currentMousePosition;
    }
}
