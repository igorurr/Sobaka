using System.Collections.Generic;
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
    
        public IField Field           { get; protected set; }
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
    
        #endregion
    }
}