using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetState", story: "Set [CurrentState] to [NewState]", category: "Action", id: "fdcc41daaf20d234dc211d15ab8bbbcb")]
public partial class SetStateAction : Action
{
    [SerializeReference] public BlackboardVariable<PlayerState> CurrentState;
    [SerializeReference] public BlackboardVariable<PlayerState> NewState;

    protected override Status OnStart()
    {
        CurrentState.Value = NewState.Value;
        Debug.Log("currentstate is set to " + NewState.Value);
        return Status.Success;
    }
}

