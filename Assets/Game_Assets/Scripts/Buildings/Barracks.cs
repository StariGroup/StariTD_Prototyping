﻿using UnityEngine;
using System.Collections;

public class Barracks : MonoBehaviour {

    private float timer;
    private GameObject newUnit;
    public GameObject chopek;
    private Vector3 spawningPosition;

    void Start()
    {
        timer = 0;
        spawningPosition = new Vector3(12, 5, -200);
    }
	
	void Update ()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= 5)
        {
            newUnit = Instantiate(chopek, spawningPosition, Quaternion.identity) as GameObject;
            newUnit.name = "Chopek";
            timer = 0;
            Debug.Log("Unit have spawned");
        }
    }
}