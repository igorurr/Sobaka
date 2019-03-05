using System;
using System.Collections.Generic;
using UnityEngine;

struct Path
{
    public bool Cycle { get; }
    public List<v2f> Points { get; }

    public Path( List<v2f> _points, bool _cycle = false )
    {
        if( _points.Count < 3 )
            throw new Exception("Слишком мало углов в контуре");
        
        Points = _points;
        Cycle = _cycle;
    }

    public float MinX()
    {
        float min = Points[0].x;
        
        foreach (var point in Points)
        {
            if ( point.x < min )
                min = point.x;
        }

        return min;
    }

    public float MinY()
    {
        float min = Points[0].y;
        
        foreach (var point in Points)
        {
            if ( point.y < min )
                min = point.y;
        }

        return min;
    }

    public float MaxX()
    {
        float max = Points[0].x;
        
        foreach (var point in Points)
        {
            if ( point.x > max )
                max = point.x;
        }

        return max;
    }

    public float MaxY()
    {
        float max = Points[0].y;
        
        foreach (var point in Points)
        {
            if ( point.y > max )
                max = point.y;
        }

        return max;
    }
}