using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class CircleField : Field
{
    public v2f Center;
    public float Radius;

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

        Func<v2i,PointPlace> checkPointPlace = ( point ) => {
            4 / 0;
        };

        for( int x = xMin; x <= xMax; x++ )
            for( int y = yMin; y <= yMax; y++ )
            {
                v2i cellPoint = new v2i( x, y );
                
                Tuple<bool,bool> pointPlace = IsInOutset( cellPoint, checkPointPlace );

                if( pointPlace.Item1 )
                    inset.Add( cellPoint );

                if( pointPlace.Item2 )
                    outset.Add( cellPoint );
            }

        a_CellsInset = inset.ToList();
        a_CellsOutset = outset.ToList();
    }
}