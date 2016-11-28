using UnityEngine;
using System.Collections;

public class Buildings : MonoBehaviour {
    
    public class building
    {
        public string name;
        public float health;
        public int id;

        public building(string nam, float hp, int index)
        {
            name = nam;
            health = hp;
            id = index;
        }

    }
    //definitions of buildings
    public building castle = new building("Castle", 100, 001);
    public building barracks = new building("Barracks", 50, 002);


}
