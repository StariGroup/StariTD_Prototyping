using UnityEngine;
using System.Collections;

public class BulletMoving : MonoBehaviour
{
    public BulletAssign round;
    private Shooting enemyBuilding;
    public GameObject target;
    public Vector3 positionTest;
    public Vector3 startPosition;
    private float speed;
    private float damage;
    public float timeInterval;
    public float startTime;
    public float distance;

    void Start()
    {
        positionTest = new Vector3(30, 30, -30);
        startPosition = new Vector3(0, 2, -90);
        distance = Vector3.Distance(startPosition, target.transform.position);
        startTime = Time.time;   
        round = GetComponent<BulletAssign>();
        speed = round.speed;
        damage = round.damage;
    }

    void Update()
    { 
        timeInterval = Time.time - startTime;
        this.gameObject.transform.position = Vector3.Lerp(startPosition, target.transform.position, timeInterval * speed / distance);
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        if (Vector3.Distance(startPosition, target.transform.position) >= 10f)
        {
            HitTarget();
        }
        /*
        Vector3 dir = positionTest - this.gameObject.transform.position;
        float distancePerFrame = speed * Time.deltaTime;
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        if (dir.magnitude <= distancePerFrame)
        {
            HitTarget();
            return;
        }
        */

    }

    public void HitTarget()
    {
        target.GetComponent<UnitAssign>().hp -= damage;
        Destroy(this.gameObject);
        Debug.Log("Elo");
    }

}
