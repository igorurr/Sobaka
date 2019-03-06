using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

abstract class Node
{
    #region main atributes
    
    private Root         a_Root;
    private List<Node>   a_Childs;
    private Node         a_Parent;
    
    protected dobj       Props;
    protected dobj       State;
    
    #endregion
    
    #region life cycle

    public Node( Root _root, dobj _props = null )
    {
        a_Parent = null;
        a_Root   = _root;

        Initialize( _props );
    }

    public Node( Node _parrent, dobj _props = null )
    {
        a_Parent = _parrent;
        a_Root   = _parrent.a_Root;

        Initialize( _props ?? new dobj() );
    }

    // пользовательские действия в событии жизненного цикла
    protected virtual  void Init() {}
    protected abstract void Generate();
    protected virtual  void OnDestroy() {}

    // вызывается при инициализации компонента, когда она будет пока хз
    private void Initialize( dobj _props )
    {
        InitUpdateProps( _props );
        
        Init();
        Generate();
    }

    // вызывается откуда угодно для обновления
    // как это будет работать - хз, на данном этапе можно отдать свойства в Node
    public void Update( dobj _props )
    {
        InitUpdateProps( _props );
        
        Generate();
    }
    
    // вызывается откуда угодно, рекурсивно для всех детей вызывает Destroy
    public void Destroy()
    {
        DestroyChilds();
        
        OnDestroy();
    }
    
    #endregion
    
    #region life cycle helpers

    private void InitUpdateProps( dobj _props )
    {
        Props = _props;
    }

    private void DestroyChilds( params Node[] _childs )
    {
        ChildsAction( _child => _child.Destroy(), _childs );
    }
    
    #endregion
    
    #region show hide
    // у ноды есть свой цикл - исчезания появления элементов, работает вдоль по дереву:
    // есть текущая вершина (по дефолту - корень), все потомки текущей вершины считаются скрытыми,
    // все предки текущей вершины - видимыми
    // каждый вызов Show вызывает создание новой видимой вершины (их может быть несколько и её тоже
    // можно рассматривать как текущую), каждый вызов hide поднимает указатель текущей вершины в предка
    // там согласно реализованной логике ноды решается что делать дальше. Если текущая вершина изначально
    // скрыта - в dev режиме бросается варнинг, на проде и devе вызов игнорируется,
    // если текущая вершина показана и у неё имеются показываемые потомки - они все скрываются
    // НЕЛЬЗЯ включить ребёнка, предварительно не включив всех его родителей
    
    public bool Showed { get; private set; }
    private List<Node> a_ShowedChilds;

    private void InitShowHide()
    {
        a_ShowedChilds = new List<Node>();
        Showed = false;
    }
    
    // при появлении ноды ()
    public virtual void OnShow() 
    {}
    
    // при исчезании ноды
    public virtual void OnHide() 
    {}
    
    private void OnChildShow( Node _child )
    {
        #if DEV
        // все проверки в Show и ShowChild
        #endif

        a_ShowedChilds.Add( _child );
    }
    
    private void OnChildHide( Node _child )
    {
        #if DEV
        if( !a_ShowedChilds.Any() && !a_ShowedChilds.Contains( _child ) )
            Debug.LogWarning("попытка удалить hided или чужого ребёнка");
        #endif

        a_ShowedChilds.Remove( _child );
    }
    
    public void ShowChilds( params Node[] _childs )
    {
        ChildsAction( _child => _child.Show(), _childs );
    }

    public void Show()
    {
        #if DEV
        if( !a_Parent.Showed )
            Debug.LogError("попытка показать элемент у которого родитель скрыт");
        #endif
        
        if( !a_Parent.Showed )
            return;
        
        Showed = true;
        a_Parent.OnChildShow( this );
        OnShow();
    }

    public void Hide()
    {
        if ( a_ShowedChilds.Any() )
            foreach (var child in a_ShowedChilds.Copy()) // copy потому что исходный список меняется
            {
                child.Hide();
            }

        Showed = false;

        a_Parent.OnChildHide( this );
        OnHide();
    }
    
    #endregion

    #region helpers

    private void ChildsAction( Action<Node> _action, Node[] _childs )
    {
        // если _childs пуст - выполняется для всех потомков
        if ( _childs.Any() )
        {
            #if DEV
            if( a_ShowedChilds.Any() && a_ShowedChilds.Contains( _child ) )
                Debug.LogWarning("попытка добавить showed ребёнка");
            if( !a_Childs.Any() || !a_Childs.Contains( _child ) )
                Debug.LogWarning("попытка добавить чужого ребёнка");
            #endif
            
            foreach (var child in _childs)
                _action( child );
        }
        else
        {
            foreach (var child in a_Childs)
                _action( child );
        }
    }

    #endregion
    
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