using UnityEngine;

abstract class Root : StereoBehaviour
{
    private Node RootNode;

    protected abstract Node InitRoot();

    protected override void OnAwake()
    {
        base.OnAwake();

        RootNode = InitRoot();
        
        RootNode.Generate();
    }
}