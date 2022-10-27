using UnityEngine;

namespace MyNamespace
{
    public class Character
    {
        public CoastController stone { get; set; }
        public bool IsOnBoat { get; set; }
        public Moveable moveable;
        public GameObject Role { get; set; }

        public string Name
        {
            get
            {
                return Role.name;
            }
            set
            {
                Role.name = value;
            }
        }

        public Character(string objectName)
        {
            
            if(objectName.Contains("devil"))
            {
                Role = Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            }
            else 
            {
                Role = Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            }

            
            Name = objectName;
            IsOnBoat = false;
            moveable = Role.AddComponent(typeof(Moveable)) as Moveable;
        }
    }
}
