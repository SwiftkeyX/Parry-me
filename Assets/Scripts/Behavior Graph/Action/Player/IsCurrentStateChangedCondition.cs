using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsCurrentStateChanged", story: "[CurrentState] is [changed]", category: "Conditions", id: "c34470b3b9d98d0b41d12cfb5ed8eef9")]
public partial class IsCurrentStateChangedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<PlayerState> CurrentState;
    [SerializeReference] public BlackboardVariable<bool> Changed;

    public override bool IsTrue()
    {
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
