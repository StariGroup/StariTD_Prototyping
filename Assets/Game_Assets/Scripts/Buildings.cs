using UnityEngine;
using System.Collections;

public class Buildings : MonoBehaviour
{

    public class building
    {
        //class variables
        public string appellation;
        public float health;
        public int id;
        public float fireRate;
        public float range;

        public building(string _name, float hp, float _fireRate, float _range, int index)
        {
            appellation = _name;
            health = hp;
            fireRate = _fireRate;
            range = _range;
            id = index;
        }

    }
    //definitions of buildings
                                           //Name               HP      FR      Range   ID
    public building nexus =     new building("Base",            100,    0.5f,   50f,    001);
    public building barracks =  new building("Barracks",        50,     0,      0,      002);
    public building tower =     new building("ShootingTower",   40,     0.8f,     50f,  003);

 
}
