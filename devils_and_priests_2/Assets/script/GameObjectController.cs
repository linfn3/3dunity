using UnityEngine;

namespace MyNamespace
{
    public enum loc { left, right }

    public class CoastController
    {
        public Stone coast;

        public CoastController(string location)
        {
            coast = new Stone(location);
        }

        public int GetEmptyIndex()
        {
            for (int i = 0; i < coast.members.Length; ++i)
            {
                if (coast.members[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }



        // ÉÏ°¶
        public void GetOnCoast(CharacterController character)
        {
            int index = GetEmptyIndex();
            coast.members[index] = character;
        }


        public void GetOffCoast(string passenger_name)
        {
            for (int i = 0; i < coast.members.Length; i++)
            {
                if (coast.members[i] != null &&
                    coast.members[i].character.Name == passenger_name)
                {
                    coast.members[i] = null;
                }
            }
        }

        public Vector3 GetEmptyPosition()
        {
            Vector3 pos;
            Debug.Log(GetEmptyIndex());
            if(GetEmptyIndex() == -1)
            {
                pos = coast.positions[0];
            }
            else  pos= coast.positions[GetEmptyIndex()];
            pos.x *= (coast.Location == loc.right ? 1 : -1);
            return pos;
        }

        public int[] GetCharacterNum()
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < coast.members.Length; i++)
            {
                if (coast.members[i] != null)
                {
                    if (coast.members[i].character.Name.Contains("priest"))
                    {
                        count[0]++;
                    }
                    else
                    {
                        count[1]++;
                    }
                }
            }
            return count;
        }




        public void Reset()
        {
            coast.members = new CharacterController[6];
        }
    }

    public class BoatController
    {
        public Boat boat;

        public BoatController()
        {
            boat = new Boat();
        }
        public void SetPos()
        {
            if (boat.loc == loc.left)
            {
                boat.loc = loc.right;
            }
            else
            {
                boat.loc = loc.left;
            }
        }

        public Vector3 GetDestination()
        {
            if (boat.loc == loc.left) return boat.departure;
            return boat.destination;
        }


        public bool IsEmpty()
        {
            for (int i = 0; i < boat.passengers.Length; i++)
            {
                if (boat.passengers[i] != null)
                {
                    return false;
                }
            }
            return true;
        }
        public int GetEmptyIndex()
        {
            for (int i = 0; i < boat.passengers.Length; i++)
            {
                if (boat.passengers[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }



        public Vector3 GetEmptyPosition()
        {
            
            Vector3 position;
            int emptyIndex = GetEmptyIndex();
            if (boat.loc == loc.right)
            {
                position = boat.departures[emptyIndex];


            }
            else
            {
                position = boat.destinations[emptyIndex];
            }
            return position;
        }

        public void GetOnBoat(CharacterController character)
        {
            int index = GetEmptyIndex();
            boat.passengers[index] = character;
        }

        public void GetOffBoat(string passengerName)
        {
            for (int i = 0; i < boat.passengers.Length; i++)
            {
                if (boat.passengers[i] != null &&
                    boat.passengers[i].character.Name == passengerName)
                {
                    boat.passengers[i] = null;
                }
            }
        }

        public int[] GetCharacterNum()
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < boat.passengers.Length; i++)
            {
                if (boat.passengers[i] != null)
                {
                    if (boat.passengers[i].character.Name.Contains("priest"))
                    {
                        count[0]++;
                    }
                    else
                    {
                        count[1]++;
                    }
                }
            }
            return count;
        }

        public void Reset()
        {
            if (boat.loc == loc.left)
            {
                boat.loc = loc.right;
            }
            boat.boatObject.transform.position = boat.departure;
            boat.passengers = new CharacterController[2];
        }
    }

    public class CharacterController
    {
        readonly UserGUI userGui;
        public Character character;

        public CharacterController(string _name)
        {
            character = new Character(_name);
            userGui = character.Role.AddComponent(typeof(UserGUI)) as UserGUI;
            userGui.SetCharacterCtrl(this);
        }

        public void SetPosition(Vector3 position)
        {
            character.Role.transform.position = position;
        }


        public void GetOnBoat(BoatController boatCtrl)
        {
            character.Role.transform.parent = boatCtrl.boat.boatObject.transform;
            character.IsOnBoat = true;
            character.stone = null;

        }


        public void Reset()
        {
            character.stone = (Director.GetInstance().CurrentSecnController as FirstController).rightStones;
            GetOnCoast(character.stone);
            SetPosition(character.stone.GetEmptyPosition());
            character.stone.GetOnCoast(this);
        }
        public void GetOnCoast(CoastController stone)
        {
            character.Role.transform.parent = null;
            character.IsOnBoat = false;
            character.stone = stone;
            
        }
    }
}
