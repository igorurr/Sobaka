
using UnityEngine;
using UnityEngine.Analytics;

public class v2f : v2<float>
{
	private Vector2 a_Vector;
	
	public override float x
	{
		get { return a_Vector.x; }
		set { a_Vector.x = value; }
	}
	
	public override float y 
	{
		get { return a_Vector.y; }
		set { a_Vector.y = value; }
	}

	public Vector2 ToVector2()
	{
		return a_Vector;
	}

	public v2i Floor()
	{
		return new v2i( (int)x, (int)y );
	}

	public static bool operator == ( v2f a, v2f b )
	{
		return a.x.Equals( b.x ) && a.y.Equals( b.y );
	}

	public static bool operator != ( v2f a, v2f b )
	{
		return !( a == b );
	}

	public static v2f operator + ( v2f a, v2f b )
	{
		return new v2f( a.x + a.y, b.x + b.y );
	}

	public static v2f operator - ( v2f a, v2f b )
	{
		return new v2f( a.x - a.y, b.x - b.y );
	}
	
	public v2f( float _x, float _y )
		:base( _x, _y )
	{}

	public static v2f Lerp( v2f a, v2f b, float t )
	{
		return (v2f)Vector2.Lerp( a, b, t );
	}
	public static float InvLerp( v2f a, v2f b, v2f c ){
		// подразумевается, что точки лежат на одной прямой, c лежит между b и a, отрезок, если он направленный, начинается в a и заканчивается в b
		if( Mathf.Abs( a.x - b.x ) > Mathf( a.y - b.y ) )
			return ( c.x - a.x ) / ( b.x - a.x );
		else
			return ( c.y - a.y ) / ( b.y - a.y );
	}
}