using System.Collections.Generic;
using UnityEngine;

class PathField : Field
{
    public Path Path;

    public override void CalculateCells()
    {
        // если бы реализация всегда оставалась такой - это можно было бы вынести в отдельную функцию в базовом классе и не печатать один и тот же код сто раз
        // однако когда мы это будем оптимизировать реализация у каждого дочернего класса от Field поменяется, поэтому код дублируется, его немного так шо пох

        int
            xMin = Path.MinX(), 
            xMax = Path.MaxX(), 
            yMin = Path.MinY(),
            yMax = Path.MaxY();

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