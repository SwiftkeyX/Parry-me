using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "InitiateSelfForSubTree", story: "Initiate [Self] in SubTree equal to [BlackBoardSelf]", category: "Action", id: "b4be9618df80c6e4f2a753dcc983bdbb")]
public partial class InitiateSelfForSubTreeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> BlackBoardSelf;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

