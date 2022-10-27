using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyNamespace;

public class SceneController : ActionManager
{
    public void MoveBoat(BoatController boatController)
    {
        Moveaction action = Moveaction.GetSSMoveToAction(boatController.GetDestination(), boatController.boat.movingSpeed);
        AddAction(boatController.boat.boatObject, action, this);
    }

    public void MoveCharacter(MyNamespace.CharacterController characterController, Vector3 destination)
    {
        Vector3 curpos = characterController.character.Role.transform.position;
        Vector3 midpos = curpos;

        if (curpos.y < destination.y  ) midpos.y = destination.y;
        else
        {
            midpos.x = destination.x;
        }
        SSAction action1 = Moveaction.GetSSMoveToAction(midpos, characterController.character.movingSpeed);
        SSAction action2 = Moveaction.GetSSMoveToAction(destination, characterController.character.movingSpeed);
        SSAction seqAction = ConstituteAction.GetConstitueAction(1, 0, new List<SSAction> { action1, action2 });
        AddAction(characterController.character.Role, seqAction, this);
    }
}
