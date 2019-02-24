using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sobaka.Maze
{
    abstract class Maze : Node
    {
        #region atributes - property

        protected ExactPercentage      a_Cycle;
        protected ExactPercentage      a_Isolation;
        
        protected List<Zone>           a_Zones;
        protected Dictionary<v2i, int> a_CellsZones;
    
        public Field  Field           { get; protected set; }
        public bool   SmoothingField  { get; protected set; }
        public float  CycleResult     { get; protected set; }
        public float  IsolationResult { get; protected set; }

        public ExactPercentage Cycle
        {
            get { return a_Cycle; }
            protected set { a_Cycle = value; }
        }

        public ExactPercentage Isolation
        {
            get { return a_Isolation; }
            protected set { a_Isolation = value; }
        }

        public Maze( Field _field )
        {
            a_Cycle     = new ExactPercentage( false, 0 );
            a_Isolation = new ExactPercentage( false, 1 );

            Field          = _field;
            SmoothingField = true;
            
            InitCells();
        }

        private void InitDefaultZones()
        {
            a_Zones = new List<Zone>( a_CellsZones.Count );
            
            int i = 0;
            foreach (var cell in a_CellsZones)
            {
                a_Zones[i] = new Zone( i );
                
                a_CellsZones[cell.Key] = i;
                i++;
            }
        }

        private void InitCells()
        {
            List<v2i> cells = Field.CellsInset;
            
            a_CellsZones = cells.ToDictionary( cell => cell, cell => 0 );
        }
    
        #endregion
    }
}