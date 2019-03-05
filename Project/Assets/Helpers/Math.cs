

namespace Helpers.Math
{
    // прямая
    public class Straight<T>
        where T: Straight
    {
        public v2f Origin { get; }

        public v2f Dirrection { get; }

        // Ax + By + C = 0
        public float A { get; }
        public float B { get; }
        public float C { get; }

        public Straight( v2f _origin, v2f _dirrection )
        {
            Origin = _origin;
            Dirrection = _dirrection;

            CalculateABC();
        }

        private void CalculateABC()
        {
            A = -Dirrection.y;
            B = Dirrection.x;
            C = Origin.x * Dirrection.y - Origin.y * Dirrection.x;
        }

        public static T From2Points( v2f a, v2f b )
        {
            return new T( a, b-a );
        }

        public virtual bool PointBelong( v2f _point )
        {
            return ( A * _point.x + B * _point.y + C ).Equal( 0 );
        }

        public virtual v2f? GetCollision( SegmentLine line )
        {
            float
                A1 = A,
                B1 = B,
                C1 = C,
                A2 = line.A,
                B2 = line.B,
                C2 = line.C;

            float delim = A2 * B1 - A1 * B2;

            // прямые либо совпадают либо параллельны
            if( delim.Equal( 0 ) )
            {
                // прямые равны
                if( C1.Equal(C2) )
                    return Origin;

                return null;
            }

            float y = ( A1 * C2 - A2 * C1 ) / delim;
            float x = - ( B1 * y + C1 ) / A1;

            return new v2f( x, y );
        }
    }

    // луч
    public class LightWay : Straight<LightWay>
    {
        public LightWay( v2f _origin, v2f _dirrection )
        :base( _origin, _dirrection )
        {
        }

        public bool WasCollision( SegmentLine line )
        {
            return GetCollision( line ) != null;
        }

        private bool Belong( v2f _point )
        {
            return v2f.InvLerp( Origin, Origin + Dirrection, resBase ).MoreEqual( 0 );
        }

        public override bool PointBelong( v2f _point )
        {
            bool resBase = base.PointBelong( _point );

            return resBase && Belong( _point );
        }

        public override v2f? GetCollision( SegmentLine line )
        {
            v2f? resBase = base.GetCollision( line );

            return Belong( resBase ) ? resBase : null;
        }
    }

    // отрезок
    public class SegmentLine : Straight<SegmentLine>
    {
        public SegmentLine( v2f _origin, v2f _dirrection )
        :base( _origin, _dirrection )
        {
        }

        private bool Belong( v2f _point )
        {
            float positionResBase = v2f.InvLerp( Origin, Origin + Dirrection, resBase );

            return positionResBase.MoreEqual( 0 ) && positionResBase.LessEqual( 1 );
        }

        public override bool PointBelong( v2f _point )
        {
            bool resBase = base.PointBelong( _point );

            return resBase && Belong( _point );
        }

        public override v2f? GetCollision( SegmentLine line )
        {
            return Belong( resBase ) ? resBase : null;
        }
    }
}