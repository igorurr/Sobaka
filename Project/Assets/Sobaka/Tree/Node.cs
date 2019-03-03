using System.Collections.Generic;
using UnityEngine;

abstract class Node
{
    private Root a_Root;
    
    private List<Node> a_Childs;
    
    public abstract void Generate();
    
    #region randome

    public float RandomeFloatRange( float _a, float _b ) 
        => a_Root.RandomeFloatRange( _a, _b );
    
    public float RandomeFloatRange( v2f _ab )
        => a_Root.RandomeFloatRange( _ab );
    
    public int RandomeIntRange( v2i _ab )
        => a_Root.RandomeIntRange( _ab );
    
    public int RandomeIntRange( int _a, int _b )
        => a_Root.RandomeIntRange( _a, _b );
    
    #endregion
}