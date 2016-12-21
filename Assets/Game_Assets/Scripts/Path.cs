using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

    public NavMeshAgent navMeshAgent;
    //zmienne trzeba zmienić na tablice
    public GameObject way1;
    public Vector3 waypoint1;
    public GameObject way2;
    public Vector3 waypoint2;
    public GameObject way3;
    public Vector3 waypoint3;

    void Start()
    {
        waypoint1 = way1.transform.position;
        waypoint2 = way2.transform.position;
        waypoint3 = way3.transform.position;
        navMeshAgent.SetDestination(waypoint1);
    }

	void Update ()
    {
        //zamiast if'ów dowolna pętla (for, foreach, while) raczej for albo foreach najlepsza opcja
        float dist = Vector3.Distance(this.gameObject.transform.position, waypoint1);
        float dist2 = Vector3.Distance(this.gameObject.transform.position, waypoint2);
        float dist3 = Vector3.Distance(this.gameObject.transform.position, waypoint3);
        if (dist <= 5f)
        {
            navMeshAgent.SetDestination(waypoint2);
        }
        if (dist2 <= 5f)
        {
            navMeshAgent.SetDestination(waypoint3);
        }
        if (dist3 <= 5f)
        {
            Destroy(this.gameObject);
        }

    }
}
