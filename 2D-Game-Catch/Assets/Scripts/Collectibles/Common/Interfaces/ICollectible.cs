using UnityEngine;

namespace Collectibles.Common.Interfaces
{
    public interface ICollectible
    {
        int PointValue { get; }
        string Tag { get; }
        void Collect();
        void SetPlayerTransform(Transform player);
    }
}