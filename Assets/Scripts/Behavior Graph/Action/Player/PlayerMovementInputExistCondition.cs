using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "PlayerMovementInputExist", story: "[PlayerMovement]'s movement Exist", category: "Conditions", id: "f405d97cced7e95e285225bd37a45add")]
public partial class PlayerMovementInputExistCondition : Condition
{
    [SerializeReference] public BlackboardVariable<PlayerMovement> PlayerMovement;
    float left_right_input;

    // return true if player did insert input
    public override bool IsTrue()
    {
        if (left_right_input != 0f) return true;
        
        return false;
    }

    public override void OnStart()
    {
        // get input from player
        left_right_input = PlayerMovement.Value.Left_Right_input;
    }

    public override void OnEnd()
    {
    }
}
