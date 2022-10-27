using MyNamespace;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    private Vector3 riverPos;
    private UserGUI userGui;

    public CoastController rightStones;
    public CoastController leftStones;
    public BoatController boatConctroller;
    public MyNamespace.CharacterController[] members;

    void Awake()
    {
        Director director = Director.GetInstance();
        director.CurrentSecnController = this;
        userGui = gameObject.AddComponent<UserGUI>() as UserGUI;
        members = new MyNamespace.CharacterController[6];
        this.LoadResources();
    }

    public void LoadResources()
    {
        riverPos = new Vector3(0, -6, 0);
        GameObject riverObject = Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject)),
                            riverPos, Quaternion.identity, null) as GameObject;
        riverObject.name = "river";
        leftStones = new CoastController("left");

        rightStones = new CoastController("right");


        boatConctroller = new BoatController();

        for (int i = 0; i < 3; i++)
        {
            MyNamespace.CharacterController temp = new MyNamespace.CharacterController("priest" + i);
            temp.SetPosition(rightStones.GetEmptyPosition());
            temp.GetOnCoast(rightStones);
            rightStones.GetOnCoast(temp);
            members[i] = temp;
        }

        for (int i = 0; i < 3; i++)
        {
            MyNamespace.CharacterController temp = new MyNamespace.CharacterController("devil" + i);
            temp.SetPosition(rightStones.GetEmptyPosition());
            temp.GetOnCoast(rightStones);
            rightStones.GetOnCoast(temp);
            members[i + 3] = temp;
        }
    }

    private int WhetherOver()
    {
        int count = 0;
        int PriestCountRight = 0;
        int DevilCountRight = 0;
        int PriestCountLeft = 0;
        int DevilCountLeft = 0;


        PriestCountRight += rightStones.GetCharacterNum()[0];
        DevilCountRight += rightStones.GetCharacterNum()[1];
        PriestCountLeft += leftStones.GetCharacterNum()[0];
        DevilCountLeft += leftStones.GetCharacterNum()[1];



        // Lose
        if ((PriestCountRight < DevilCountRight && PriestCountRight > 0) ||
            (DevilCountLeft>PriestCountLeft  && PriestCountLeft > 0))
        {
            count = 1;
        }
        // Win
        if (PriestCountLeft + DevilCountLeft == 6)
        {
            count = 2;
        }

        if (boatConctroller.boat.loc == loc.right)
        {
            PriestCountRight += boatConctroller.GetCharacterNum()[0];
            DevilCountRight += boatConctroller.GetCharacterNum()[1];
        }
        else
        {
            PriestCountLeft += boatConctroller.GetCharacterNum()[0];
            DevilCountLeft += boatConctroller.GetCharacterNum()[1];
        }

        return count;
    }

    public void Boatmoved()
    {
        if (boatConctroller.IsEmpty()) return;
        boatConctroller.Move();
        userGui.flag = WhetherOver();
    }

    public void CharacterClicked(MyNamespace.CharacterController controller)
    {
        if (controller.character.IsOnBoat)
        {
            CoastController tempCoast = (boatConctroller.boat.loc == loc.right ? rightStones : leftStones);
            boatConctroller.GetOffBoat(controller.character.Name);
            controller.GetOnCoast(tempCoast);
            tempCoast.GetOnCoast(controller);
            controller.MoveTo(tempCoast.GetEmptyPosition());
            

        }
        else
        {
            CoastController tempCoast = controller.character.stone;

            if (boatConctroller.GetEmptyIndex() == -1) return;
            if (tempCoast.coast.Location != boatConctroller.boat.loc) return;


            tempCoast.GetOffCoast(controller.character.Name);
            controller.MoveTo(boatConctroller.GetEmptyPosition());
            controller.GetOnBoat(boatConctroller);
            boatConctroller.GetOnBoat(controller);

        }
        userGui.flag = WhetherOver();
    }

    public void Restart()
    {
        boatConctroller.Reset();
        rightStones.Reset();
        leftStones.Reset();
        for (int i = 0; i < 6; i++)
        {
            members[i].Reset();
        }
    }
}

