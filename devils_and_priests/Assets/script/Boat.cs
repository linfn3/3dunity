using UnityEngine;

namespace MyNamespace
{
    public class Boat
    {
        public readonly Moveable moveable;
        
        public readonly Vector3 destination;
        public readonly Vector3 departure;
        public readonly Vector3[] departures;
        public readonly Vector3[] destinations;
        public CharacterController[] passengers = new CharacterController[2];

        public GameObject boatObject { get; set; }
        public loc loc { get; set; }

        public Boat()
        {
            // 船的起点和终点
            departure = new Vector3(5, 1, 0);
            destination = new Vector3(-5, 1, 0);
            loc = loc.right;

            departures = new Vector3[] { new Vector3(4.5f, 1.5f, 0), new Vector3(5.5f, 1.5f, 0) };
            destinations = new Vector3[] { new Vector3(-5.5f, 1.5f, 0), new Vector3(-4.5f, 1.5f, 0) };
            boatObject = Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), departure, Quaternion.identity, null) as GameObject;
            boatObject.name = "boat";

            moveable = boatObject.AddComponent(typeof(Moveable)) as Moveable;
            boatObject.AddComponent(typeof(UserGUI));
        }
    }
}

