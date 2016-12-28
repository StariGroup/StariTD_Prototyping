using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private Unit unit;
    private EnemyBuilding enemyBuilding;
    private GameObject target;
    private float speed;

	void Start ()
    {
        enemyBuilding = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBuilding>();
        unit = GameObject.FindGameObjectWithTag("Unit").GetComponent<Unit>();
        speed = 130f;
        target = enemyBuilding.enemyToShoot;
    }
	
	void Update ()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 dir = target.transform.position - this.gameObject.transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distancePerFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distancePerFrame, Space.World);
	}

    public void HitTarget()
    {
        target.GetComponent<Unit>().hp -= 25;
        Destroy(this.gameObject);
    }

}
