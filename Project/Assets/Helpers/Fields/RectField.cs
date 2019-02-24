using System.Collections.Generic;
using UnityEngine;

class RectField : Field
{
    // можно хранить всего 2 точки одной диагонали прямоугольника и по ним строить остальные 2, но это долго
    public v2f a { get; }
    public v2f b { get; }
    public v2f c { get; }
    public v2f d { get; }

    private List<v2i> a_CellsInset;
    private List<v2i> a_CellsOutset;

    public RectField( v2f a, v2f c )
        :this( a, new v2f( a.x, c.y ), c, new v2f( c.x, a.y ) )
    {
        
    }

    public RectField( v2f a, v2f b, v2f c, v2f d )
    {
        // тут нужны проверки, одной формулы со скалярным произведением недостаточно, ибо получается арккосинус,
        // надо чекать синус наверное

        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
    }

    public override void CalculateCells()
    {
        
    }
}