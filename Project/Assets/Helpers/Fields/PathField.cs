using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class PathField : Field
{
    public Path Path { get; }

    public PathField( List<v2f> _path )
        :this( new Path( _path, true ) )
    {
    }

    public PathField( Path _path )
    {
        Path = _path;
    }

    public override PointPlace GetPointPlace(v2f _point)
    {
        List<v2f> pathPoints = Path.Points;
        List<v2f> pathPaths = new List<v2f>( pathPoints.Count );

        for( int i=1; i<pathPoints.Count; i++ )
            pathPaths.Add( pathPoints[i] - pathPoints[i-1] );
        
        pathPaths.Add( pathPoints[0] - pathPoints.Last() );
        
        // начало отрезка pathPaths[i] лежит в pathPoints[i-1]
        
    }

    public override void CalculateCells()
    {
        // если бы реализация всегда оставалась такой - это можно было бы вынести в отдельную функцию в базовом классе и не печатать один и тот же код сто раз
        // однако когда мы это будем оптимизировать реализация у каждого дочернего класса от Field поменяется, поэтому код дублируется, его немного так шо пох

        int
            xMin = (int) Path.MinX(), 
            xMax = (int) Path.MaxX(), 
            yMin = (int) Path.MinY(),
            yMax = (int) Path.MaxY();

        HashSet<v2i> inset = new HashSet<v2i> ();
        HashSet<v2i> outset = new HashSet<v2i> ();

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