using System.Collections.Generic;
using Sobaka.Maze.Tree;
using UnityEngine;

namespace Sobaka.Maze.Texture
{
    class TreeToTexture : TextureMaze
    {
        // "подземелья" (разная ширина путей, дающая комнаты) появляется только здесь и одному богу ведомо как
        // есть такая идея: когда генерятся утолщения - происходит растяжение по горизонтали или по вертикали
        // на значение максимальной отрендереной толщины утолщения.
        
        private Tree.TreeMaze a_TreeMaze;

        protected override void Init()
        {
            base.Init();

            a_TreeMaze = Props["treeMaze"] as TreeMaze;
        }
    }
}