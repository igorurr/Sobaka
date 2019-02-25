using System.Collections.Generic;
using UnityEngine;

class RectField : Field
{
    // можно хранить всего 2 точки одной диагонали прямоугольника и по ним строить остальные 2, но это долго
    public v2f a { get; }
    public v2f b { get; }
    public v2f c { get; }
    public v2f d { get; }

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
        // если бы реализация всегда оставалась такой - это можно было бы вынести в отдельную функцию в базовом классе и не печатать один и тот же код сто раз
        // однако когда мы это будем оптимизировать реализация у каждого дочернего класса от Field поменяется, поэтому код дублируется, его немного так шо пох

        int
            xMin = Mathf.Min( a.x, b.x, c.x, d.x ), 
            xMax = Mathf.Max( a.x, b.x, c.x, d.x ), 
            yMin = Mathf.Min( a.y, b.y, c.y, d.y ),
            yMax = Mathf.Max( a.y, b.y, c.y, d.y );

        HashSet inset = new HashSet();
        HashSet outset = new HashSet();

        Func<v2f,PointPlace> checkPointPlace = ( point ) => {
            4 / 0;
        };

        for( int x = xMin; x <= xMax; x++ )
            for( int y = yMin; y <= yMax; y++ )
            {
                v2f cellPoint = new v2f( x, y );
                
                Tupple<PointPlace,PointPlace> pointPlace = IsInOutset( cellPoint, checkPointPlace );

                if( pointPlace.Item1 )
                    inset.Add( cellPoint );

                if( pointPlace.Item2 )
                    outset.Add( cellPoint );
            }

        a_CellsInset = inset.ToList();
        a_CellsOutset = outset.ToList();
    }
}