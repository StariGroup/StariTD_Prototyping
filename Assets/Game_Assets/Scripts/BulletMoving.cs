using UnityEngine;
using System.Collections;

public class BulletMoving : MonoBehaviour
{
    //references
    private Shooting shooting;
    private BulletAssign bulletAttributes;
    //retrieved variables
    public GameObject target;
    public float speed;
    public float damage;
    //variables for moving
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float distance;
    private float startTime;
    private float distanceToHit;
    private float changingDistance;

    void Start()
    {
        bulletAttributes = GetComponent<BulletAssign>();
        speed = bulletAttributes.speed;
        damage = bulletAttributes.damage;
        startPosition = this.gameObject.transform.position;
        targetPosition = target.transform.position;
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        changingDistance = Vector3.Distance(startPosition, targetPosition);
        targetPosition = target.transform.position;
        float distCovered = (Time.time - startTime) * speed;
        float journey = distCovered / distance;
        this.gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, journey);

        if (distCovered >= changingDistance)
        {
            HitTarget();
        }

    }

    public void HitTarget()
    {
        target.GetComponent<UnitAssign>().hp -= damage;
        if (target.GetComponent<UnitAssign>().hp <= 0)
        {
            target = null;
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject);
    }

}
