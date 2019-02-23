using System.Collections.Generic;
using UnityEngine;

namespace Sobaka.Maze.Tree
{
    abstract class TreeMaze : Maze
    {
        // все пути лабиринта длиною меньше указанной будут сливаться не зависимо от настроек изолированности
        public int MinPathSize { get; protected set; }
        
        public Texture.TextureMaze ToTexture()
        {
            return new Texture.TreeToTexture( this );
        }
    }
}