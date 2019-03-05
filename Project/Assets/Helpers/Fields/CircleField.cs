using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class CircleField : Field
{
    public v2f Center;
    public float Radius;

    public override PointPlace GetPointPlace(v2f _point)
    {
        float mathEqualRes = Mathf.Pow( ( _point.x - Center.x ) , 2 ) + Mathf.Pow( ( _point.y - Center.y ) , 2 );
        float rr = Mathf.Pow( Radius , 2 );

        return
            ( mathEqualRes.Less( rr ) ) ? PointPlace.INSET :
            ( mathEqualRes.More( rr ) ) ? PointPlace.OUTSET : PointPlace.BORDER;
    }

    public override void CalculateCells()
    {
        // если бы реализация всегда оставалась такой - это можно было бы вынести в отдельную функцию в базовом классе и не печатать один и тот же код сто раз
        // однако когда мы это будем оптимизировать реализация у каждого дочернего класса от Field поменяется, поэтому код дублируется, его немного так шо пох

        int
            xMin = (int) ( Center.x - Radius ), 
            xMax = (int) ( Center.x + Radius ), 
            yMin = (int) ( Center.y - Radius ),
            yMax = (int) ( Center.y + Radius );

        HashSet<v2i> inset = new HashSet<v2i>();
        HashSet<v2i> outset = new HashSet<v2i>();

        for( int x = xMin; x <= xMax; x++ )
            for( int y = yMin; y <= yMax; y++ )
            {
                v2i cellPoint = new v2i( x, y );
                
                Tuple<bool,bool> pointPlace = IsInOutset( cellPoint );

                if( pointPlace.Item1 )
                    inset.Add( cellPoint );

                if( pointPlace.Item2 )
                    outset.Add( cellPoint );
            }

        a_CellsInset = inset.ToList();
        a_CellsOutset = outset.ToList();
    }
    
    
}