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
}