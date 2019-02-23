using System.Collections.Generic;
using UnityEngine;

struct RectField : IField
{
    // можно хранить всего 2 точки одной диагонали прямоугольника и по ним строить остальные 2, но это долго
    public v2f a;
    public v2f b;
    public v2f c;
    public v2f d;
}