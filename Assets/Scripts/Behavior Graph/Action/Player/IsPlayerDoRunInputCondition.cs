using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsPlayerDoRunInput", story: "Is [PlayerMovement] run ", category: "Conditions", id: "ab33062b4f63475e4b8944998d32a34e")]
public partial class IsPlayerDoRunInputCondition : Condition
{
    [SerializeReference] public BlackboardVariable<PlayerMovement> PlayerMovement;
    bool run;

    public override bool IsTrue()
    {
        if (run) return true;

        else return false;
    }

    public override void OnStart()
    {
        run = PlayerMovement.Value.Run_input;
    }

    public override void OnEnd()
    {
    }
}
