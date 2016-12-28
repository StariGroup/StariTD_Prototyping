using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public GameObject[] way;
    public Vector3[] waypoint;
    public float[] dist;

    void Start()
    {
        waypoint[0] = way[0].transform.position;
        waypoint[1] = way[1].transform.position;
        waypoint[2] = way[2].transform.position;
        navMeshAgent.SetDestination(waypoint[0]);
    }

    void Update()
    {
        dist[0] = Vector3.Distance(this.gameObject.transform.position, waypoint[0]);
        dist[1] = Vector3.Distance(this.gameObject.transform.position, waypoint[1]);
        dist[2] = Vector3.Distance(this.gameObject.transform.position, waypoint[2]);

        for (int i = 0; i < 3; i++)
        {

            if (dist[i] <= 5f)
            {
                navMeshAgent.SetDestination(waypoint[i + 1]);
            }
        }
    }
}