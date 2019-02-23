using System.Collections.Generic;
using UnityEngine;

namespace Sobaka.Maze.Texture
{
    abstract class TextureMaze : Maze
    {
        public bool       Inverse           { get; protected set; }
        public PathWidth  WidthPath         { get; protected set; }
        public PathWidth  WidthBackground   { get; protected set; }
        public PathRender RenderPath        { get; protected set; }
        public PathRender RenderBackground  { get; protected set; }
        
        // все пути лабиринта длиною меньше указанной будут сливаться не зависимо от настроек изолированности
        public float      MinPathLength     { get; protected set; }
    }
}