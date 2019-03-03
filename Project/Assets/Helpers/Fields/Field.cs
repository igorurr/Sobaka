using System;
using System.Collections.Generic;
using UnityEngine;

abstract class Field
{
    protected List<v2i> a_CellsInset;
    protected List<v2i> a_CellsOutset;

    /*
        координаты точки - обычные декартовы координаты
        ккординаты клетки: имеем координаты точки a, клетка находящаяся справа сверху от этой точки имеет её координаты
    */
    public abstract void CalculateCells();

    public Field()
    {
        a_CellsInset = a_CellsOutset = null;
    }

    public List<v2i> CellsInset
    {
        get
        {
            if ( a_CellsInset == null )
                CalculateCells();
            
            return a_CellsInset.Copy();
        }
    }

    public List<v2i> CellsOutset
    {
        get
        {
            if ( a_CellsOutset == null )
                CalculateCells();
            
            return a_CellsOutset.Copy();
        }
    }

    #region Helpers

    protected enum PointPlace {
        INSET,
        BORDER,
        OUTSET
    }

    protected Tuple<bool,bool> IsInOutset( v2i _cellPoint, Func<v2i,PointPlace> _checkPointPlace )
    {
        // cellpoint (координаты ячейки) не то же самое что и координаты точки (point)

        PointPlace pointPlaceLeftBottom  = _checkPointPlace( _cellPoint );
        PointPlace pointPlaceLeftTop     = _checkPointPlace( new v2i( _cellPoint.x,   _cellPoint.y+1 ) );
        PointPlace pointPlaceRightTop    = _checkPointPlace( new v2i( _cellPoint.x+1, _cellPoint.y+1 ) );
        PointPlace pointPlaceRightBottom = _checkPointPlace( new v2i( _cellPoint.x+1, _cellPoint.y   ) );
        
        // inset
        bool inset = 
            pointPlaceLeftBottom != PointPlace.OUTSET && pointPlaceLeftTop != PointPlace.OUTSET && 
            pointPlaceRightTop != PointPlace.OUTSET && pointPlaceRightBottom != PointPlace.OUTSET;

        bool outset = 
            ( pointPlaceLeftBottom == PointPlace.INSET || pointPlaceLeftTop == PointPlace.INSET || 
            pointPlaceRightTop == PointPlace.INSET || pointPlaceRightBottom == PointPlace.INSET ) ||
            ( pointPlaceLeftBottom == PointPlace.BORDER && pointPlaceLeftTop == PointPlace.BORDER && 
            pointPlaceRightTop == PointPlace.BORDER && pointPlaceRightBottom == PointPlace.BORDER );

        return new Tuple<bool, bool>( inset, outset );
    }

    #endregion
}