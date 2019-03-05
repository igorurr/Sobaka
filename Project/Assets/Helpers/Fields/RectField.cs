using System;
using System.Collections.Generic;
using System.Linq;
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

    public override PointPlace GetPointPlace(v2f _point)
    {
        // небольшой хак: если сумма всех 4 углов, образованных 2 соседними вершинами прямоугольника и точкой _point
        // равна 360 градусов - точка находится внутри прямоугольника, иначе - снаружи
        // ВНИМАНИЕ - хак работает ТОЛЬКО при нахождении углов через скалярное произведение,
        // если искать иным путём, будет находиться развёрнутый угол, а не его противоположность
        // если один из углов равен 180 градусам - точка лежит на линии прямоугольника
        
        Vector2 
            pa = (Vector2)( a - _point ), 
            pb = (Vector2)( b - _point ), 
            pc = (Vector2)( c - _point ), 
            pd = (Vector2)( d - _point );

        float
            anglePA = Vector2.Angle( pa, pb ),
            anglePB = Vector2.Angle( pb, pc ),
            anglePC = Vector2.Angle( pc, pd ),
            anglePD = Vector2.Angle( pd, pa );

        if (
            anglePA.Equals(180) ||
            anglePB.Equals(180) ||
            anglePC.Equals(180) ||
            anglePD.Equals(180)
        )
            return PointPlace.BORDER;
        
        float angls =
            Vector2.Angle( pa, pb ) +
            Vector2.Angle( pb, pc ) +
            Vector2.Angle( pc, pd ) +
            Vector2.Angle( pd, pa );

        return ( angls.Equals(180) ) ? PointPlace.INSET : PointPlace.OUTSET;
    }

    public override void CalculateCells()
    {
        // если бы реализация всегда оставалась такой - это можно было бы вынести в отдельную функцию в базовом классе и не печатать один и тот же код сто раз
        // однако когда мы это будем оптимизировать реализация у каждого дочернего класса от Field поменяется, поэтому код дублируется, его немного так шо пох

        int
            xMin = (int)Mathf.Min( a.x, b.x, c.x, d.x ), 
            xMax = (int)Mathf.Max( a.x, b.x, c.x, d.x ), 
            yMin = (int)Mathf.Min( a.y, b.y, c.y, d.y ),
            yMax = (int)Mathf.Max( a.y, b.y, c.y, d.y );

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