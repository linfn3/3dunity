using UnityEngine;

namespace MyNamespace
{
    public class Stone
    {
        public readonly Vector3 destination;
        public readonly Vector3[] positions;
        readonly GameObject stone;
        readonly Vector3 departure;
        

        public CharacterController[] members;
        public loc Location { get; set; }

        public Stone(string _location)
        {
            // 左右两岸的位置
            departure = new Vector3(9, 1, 0);
            destination = new Vector3(-9, 1, 0);
            // 岸上角色站的位置
            positions = new Vector3[] {
                new Vector3(6.5f, 2.25f, 0),
                new Vector3(7.5f, 2.25f, 0),
                new Vector3(8.5f, 2.25f, 0),
                new Vector3(9.5f, 2.25f, 0),
                new Vector3(10.5f, 2.25f, 0),
                new Vector3(11.5f, 2.25f, 0),};
            members = new CharacterController[6];

            if (_location == "right")
            {
                stone = Object.Instantiate(Resources.Load("Prefabs/Stone", typeof(GameObject)),
                        departure, Quaternion.identity, null) as GameObject;
                Location = loc.right;
                stone.name = "departure";
                
            }
            else
            {
                stone = Object.Instantiate(Resources.Load("Prefabs/Stone", typeof(GameObject)),
                        destination, Quaternion.identity, null) as GameObject;
                Location = loc.left;
                stone.name = "destination";
            }
        }
    }
}
