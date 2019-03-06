using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sobaka.Maze.Tree
{
    class AldousBroderBelyakov : TreeMaze
    {
        #region child classes
        
        class AldousBroderBelyakovCell : TreeMazeCell
        {
            public bool WasVisited;

            public AldousBroderBelyakovCell( TreeMaze _maze, v2i _coord )
                :base( _maze, _coord )
            {
                WasVisited = false;
            }
        }

        // гуляка генерирует проходы в лабиринте
        class Walker
        {
            private AldousBroderBelyakov a_Maze;

            private v2i a_Coord;

            private int a_ZoneId;

            public Walker( AldousBroderBelyakov _maze, int _zoneId, v2i _coord )
            {
                a_Maze = _maze;
                a_Coord = _coord;
                a_ZoneId = _zoneId;
            }

            public void Go()
            {
                int to = a_Maze.RandomeIntRange( 0, 3 );

                if ( to == 0 )
                    GoTop();
                else if ( to == 1 )
                    GoRight();
                else if ( to == 2 )
                    GoBottom();
                else
                    GoLeft();
            }

            public void GoTop()
            {
                CheckMove( a_Maze.GetCellFromPoint( a_Coord.TopPoint ) );
                a_Coord = a_Coord.TopPoint;
            }
            
            public void GoRight()
            {
                CheckMove( a_Maze.GetCellFromPoint( a_Coord.RightPoint ) );
                a_Coord = a_Coord.RightPoint;
            }

            public void GoBottom()
            {
                CheckMove( a_Maze.GetCellFromPoint( a_Coord.BottomPoint ) );
                a_Coord = a_Coord.BottomPoint;
            }
            
            public void GoLeft()
            {
                CheckMove( a_Maze.GetCellFromPoint( a_Coord.LeftPoint ) );
                a_Coord = a_Coord.LeftPoint;
            }

            private void Destroy()
            {
                a_Maze.Destroy( this );
            }

            private void CheckMove( AldousBroderBelyakovCell newPoint )
            {
                if ( newPoint.WasVisited )
                {
                    Destroy();
                }

                newPoint.WasVisited = true;
                newPoint.ZoneId = a_ZoneId;
            }
        }
        
        #endregion

        #region atributes

        // количество walkerов на старте алгоритма
        private int a_StartWalkers;
        
        // за весь жизненный цикл алгоритма некоторые walkerы будут разрушаться, в этой переменной
        // хранится их количество
        private int a_WasWalkers;

        // список действующих walkerов
        private List<Walker> a_Walkers;
        
        // список непосещённых точек
        private List<v2i> a_NoVisitedMazeCells;
        
        #endregion

        #region public methods
        
        public AldousBroderBelyakov( Node _parent, dobj _props )
            :base( _parent, _props )
        {}
        
        protected override void Generate()
        {
            a_Zones      = new List<Zone>();
            a_CellsZones = new Dictionary<v2i, int>();

            CycleResult     = 0;
            IsolationResult = 0;
            
            a_AllCells = CutingBorderCells ? Field.CellsInset : Field.CellsOutset;

            GenerateMazeCells();
            
            a_Walls = GetAllWalls( a_AllCells );

            a_StartWalkers = (int)Mathf.Max( 1, a_AllCells.Count / 100f );
            a_WasWalkers   = a_StartWalkers;

            a_NoVisitedMazeCells = a_AllCells.Copy();



            for(
                int curWalker = 0;
                a_NoVisitedMazeCells.Any();
                curWalker++
            )
            {
                // тут надо проверять был ли разрушен волкер, если да - генерим новый
                a_Walkers[curWalker].Go();
            }
        }
        
        #endregion

        #region protected methods

        protected override TreeMazeCell CreateCellFromPoint( v2i _cell )
        {
            return new AldousBroderBelyakovCell( this, _cell );
        }
        
        #endregion

        #region private methods

        private AldousBroderBelyakovCell GetCellFromPoint( v2i _cell )
        {
            return a_MazeCells[_cell] as AldousBroderBelyakovCell;
        }

        private void Destroy( Walker _walker )
        {
            
        }

        private void CreateWalker()
        {
            
        }

        private AldousBroderBelyakovCell GetRandomNoVisitedPoint()
        {
            
        }

        private void InitWalkers()
        {
            a_Walkers = new List<Walker>();
            
            for( int i=0; i< a_StartWalkers; i++ )
                a_Walkers.Add( new Walker( this, i, ) );
        }
        
        #endregion
    }
}