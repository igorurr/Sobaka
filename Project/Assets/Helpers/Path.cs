using System.Collections.Generic;
using UnityEngine;

struct Path
{
    public bool Cycle;
    public List<v2f> Points;

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