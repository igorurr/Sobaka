using UnityEngine;

abstract class Root : StereoBehaviour
{
    private Node RootNode;

    #region init methods
    
    protected override void OnAwake()
    {
        base.OnAwake();

        DefaulInit();
    }

    private void DefaulInit()
    {
        Randome = InitRandom();
        
        RootNode = InitRoot();
        
        RootNode.Generate();
    }

    protected abstract Node InitRoot();

    protected virtual ISobakaRandome InitRandom()
    {
        return new SobakaRandom();
    }
    
    #endregion
    
    #region randome

    private ISobakaRandome Randome;
    
    public float RandomeFloatRange( float _a, float _b )
    {
        float value = Randome.FloatValue;
        return _a + value * ( _b - _a );
    }
    
    public float RandomeFloatRange( v2f _ab )
    {
        return RandomeFloatRange( _ab.x, _ab.y );
    }
    
    public int RandomeIntRange( v2i _ab )
    {
        return RandomeIntRange( _ab.x, _ab.y );
    }
    
    public int RandomeIntRange( int _a, int _b )
    {
        int value = Randome.IntValue;
        return _a + value % ( _b - _a );
    }
    
    #endregion
}