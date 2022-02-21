using UnityEngine;

namespace Bomberman.Level
{
    public struct GridPoint
    {
        public int index;
        public Vector3 position;

        public GridPoint(int index, Vector3 pos)
        {
            this.index = index;
            this.position = pos;
        }

    }
    
}
