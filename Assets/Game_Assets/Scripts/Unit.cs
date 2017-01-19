using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public class unit
    {
        public string appellation;
        public float health;
        public int id;

        public unit(string _name, float hp, int index)
        {
            appellation = _name;
            health = hp;
            id = index;
        }

    }
    //definitions of units
    public unit chopek = new unit("Chopek", 25, 003);

}