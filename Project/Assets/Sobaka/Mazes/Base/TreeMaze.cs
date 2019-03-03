using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sobaka.Maze.Tree
{
    abstract class TreeMaze : Maze
    {
        #region child classes
        
        protected class TreeMazeCell
        {
            private TreeMaze a_Maze;
            
            public v2i Coord;
            
            public int ZoneId;

            public TreeMazeCell( TreeMaze _maze, v2i _coord )
            {
                a_Maze = _maze;
                Coord = _coord;
            }

            public bool TopBorderOpened
            {
                get
                {
                    return GetPointOpened( Coord.TopPoint );
                }
                set
                {
                    SetPointOpened( Coord.TopPoint, value );
                }
            }
            
            public bool RightBorderOpened
            {
                get
                {
                    return GetPointOpened( Coord.RightPoint );
                }
                set
                {
                    SetPointOpened( Coord.RightPoint, value );
                }
            }
            
            public bool BottomBorderOpened
            {
                get
                {
                    return GetPointOpened( Coord.BottomPoint );
                }
                set
                {
                    SetPointOpened( Coord.BottomPoint, value );
                }
            }
            
            public bool LeftBorderOpened
            {
                get
                {
                    return GetPointOpened( Coord.LeftPoint );
                }
                set
                {
                    SetPointOpened( Coord.LeftPoint, value );
                }
            }

            private bool GetPointOpened( v2i point )
            {
                return (
                    a_Maze.a_Walls.Contains( new Tuple<v2i, v2i>( Coord, point ) ) ||
                    a_Maze.a_Walls.Contains( new Tuple<v2i, v2i>( point, Coord ) )
                );
            }

            private void SetPointOpened( v2i point, bool value )
            {
                if ( value )
                {
                    if ( GetPointOpened( point ) )
                        return;
                        
                    a_Maze.a_Walls.Add( new Tuple<v2i, v2i>( Coord, point ) );
                }
                else
                {
                    a_Maze.a_Walls.Remove( new Tuple<v2i, v2i>( Coord, point ) );
                    a_Maze.a_Walls.Remove( new Tuple<v2i, v2i>( point, Coord ) );
                }
            }
        }

        #endregion

        #region Properties
        
        // все пути лабиринта длиною меньше указанной будут сливаться не зависимо от настроек изолированности
        public int MinPathSize { get; protected set; }

        protected List<Tuple<v2i, v2i>>         a_Walls;
        protected List<v2i>                     a_AllCells;
        protected Dictionary<v2i,TreeMazeCell>  a_MazeCells;
        
        #endregion

        #region Public Methods
        
        public TreeMaze( Field _field )
            :base(_field)
        {}
        
        public Texture.TextureMaze ToTexture()
        {
            return new Texture.TreeToTexture( this );
        }

        #endregion

        #region Protected Methods

        protected List<Tuple<v2i,v2i>> GetAllWalls( List<v2i> cells )
        {
            List<Tuple<v2i, v2i>> ret = new List<Tuple<v2i, v2i>>();

            foreach (var cell in cells)
            {
                // проверяем добавлены ли уже 4 стены вокруг каждой ячейки
                
                v2i topPoint = new v2i( cell.x, cell.y+1 );
                
                if( 
                    !ret.Contains( new Tuple<v2i, v2i>( cell,     topPoint ) ) &&
                    !ret.Contains( new Tuple<v2i, v2i>( topPoint, cell ) )
                )
                    ret.Add( new Tuple<v2i, v2i>( cell, topPoint ) );
                
                v2i rightPoint = new v2i( cell.x+1, cell.y );
                
                if( 
                    !ret.Contains( new Tuple<v2i, v2i>( cell,       rightPoint ) ) &&
                    !ret.Contains( new Tuple<v2i, v2i>( rightPoint, cell ) )
                )
                    ret.Add( new Tuple<v2i, v2i>( cell, rightPoint ) );
                
                v2i bottomPoint = new v2i( cell.x, cell.y-1 );
                
                if( 
                    !ret.Contains( new Tuple<v2i, v2i>( cell,        bottomPoint ) ) &&
                    !ret.Contains( new Tuple<v2i, v2i>( bottomPoint, cell ) )
                )
                    ret.Add( new Tuple<v2i, v2i>( cell, bottomPoint ) );
                
                v2i leftPoint = new v2i( cell.x-1, cell.y );
                
                if( 
                    !ret.Contains( new Tuple<v2i, v2i>( cell,      leftPoint ) ) &&
                    !ret.Contains( new Tuple<v2i, v2i>( leftPoint, cell ) )
                )
                    ret.Add( new Tuple<v2i, v2i>( cell, leftPoint ) );
            }

            return ret;
        }
        
        /*protected override void InitDefault()
        {
            base.InitDefault();
            
            List<v2i> allCells = CutingBorderCells ? Field.CellsInset : Field.CellsOutset;
            
            List<Tuple<v2i,v2i>> walls = GetAllWalls( allCells );
        }*/

        protected abstract TreeMazeCell CreateCellFromPoint( v2i _cell );

        #endregion

        #region Private Methods
        
        // временно protected
        protected void GenerateMazeCells()
        {
            a_MazeCells = new Dictionary<v2i,TreeMazeCell>();

            foreach (var cell in a_AllCells)
            {
                a_MazeCells[cell] = CreateCellFromPoint( cell );
            }
        }

        #endregion
    }
}