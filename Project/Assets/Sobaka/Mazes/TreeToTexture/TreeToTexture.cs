using System.Collections.Generic;
using UnityEngine;

namespace Sobaka.Maze.Texture
{
    class TreeToTexture : TextureMaze
    {
        // "подземелья" (разная ширина путей, дающая комнаты) появляется только здесь и одному богу ведомо как
        // есть такая идея: когда генерятся утолщения - происходит растяжение по горизонтали или по вертикали
        // на значение максимальной отрендереной толщины утолщения.
        
        private Tree.TreeMaze a_TreeMaze;
        
        public TreeToTexture( Tree.TreeMaze _treeMaze )
        {
            a_TreeMaze = _treeMaze;
        }
    }
}