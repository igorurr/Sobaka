using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Helpers.Math;

class PathField : Field
{
    public Path Path;

    public PointPlace ebuchauafunctiua( v2f _point ){
        List<v2f> pointsPath = Points;
        List<SegmentLine> segmentsPath = new List<SegmentLine>( pointsPath.Count );

        for( int i=1; i < pointsPath.Count; i++ )
            segmentsPath[i-1] = new SegmentLine.From2Points( pointsPath[i], pointsPath[i-1] );

        segmentsPath[pointsPath.Count-1] = new SegmentLine.From2Points( pointsPath[0], pointsPath.Last() );

        // надо замутить контрольный тест когда луч из точки проходит через точку многоугольника, тогда по идее эта точка должна принадлежать двум его отрезкам
        // луч берём параллельный оси x в сторону +бесконечности
        LightWay dirrection = new LightWay( _point, new v2f( 1, 0 ) );

        int countCollisions = 0;

        foreach( var segmentPath in segmentsPath ){
            v2f? collision = dirrection.GetCollision( segmentPath );

            if( collision == null )
                continue;

            countCollisions++;

            if( segmentPath.PointBelong( collision.Value ) )
                return PointPlace.BORDER;
        }

        return ( countCollisions % 2 > 0 ) ? PointPlace.INSET : PointPlace.OUTSET;
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