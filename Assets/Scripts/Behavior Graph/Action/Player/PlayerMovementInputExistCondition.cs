using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "PlayerMovementInputExist", story: "[PlayerMovement]'s movement Exist", category: "Conditions", id: "f405d97cced7e95e285225bd37a45add")]
public partial class PlayerMovementInputExistCondition : Condition
{
    [SerializeReference] public BlackboardVariable<PlayerMovement> PlayerMovement;

    public override bool IsTrue()
    {
        return true;
    }

    public override void OnStart()
    {
        float left_right_input = PlayerMovement.Value.Left_Right_input;
        if (left_right_input != 0f) IsTrue();
    }

    public override void OnEnd()
    {
    }
}
