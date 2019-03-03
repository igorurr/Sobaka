
using System;
using UnityEngine;

public class v2i : v2<int>
{
	private Vector2Int a_Vector;
	
	public override int x
	{
		get { return a_Vector.x; }
		set { a_Vector.x = value; }
	}
	
	public override int y 
	{
		get { return a_Vector.y; }
		set { a_Vector.y = value; }
	}

	public v2i Abs()
	{
		return new v2i( Mathf.Abs(x), Mathf.Abs(y) );
	}
	
	public v2i( int _x, int _y )
		:base( _x, _y )
	{}
	
	public v2i TopPoint
	{
		get { return new v2i( x, y+1 ); }
	}

	public v2i RightPoint
	{
		get { return new v2i( x+1, y ); }
	}

	public v2i BottomPoint
	{
		get { return new v2i( x, y-1 ); }
	}

	public v2i LeftPoint
	{
		get { return new v2i( x-1, y ); }
	}
}