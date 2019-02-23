using System.Collections.Generic;
using UnityEngine;

class TestRandomeMapRoot : Root
{
    protected override Node InitRoot()
    {
        return new TestRandomeMapRootNode();
    }
}