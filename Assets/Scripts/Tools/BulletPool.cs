using DefaultNamespace;
using UnityEngine;

namespace Tools
{
    public class BulletPool : AObjectPool
    {
        private Bullet _bullet;
        private float _lifetime;
        public BulletPool(Bullet item, Transform parentTransform) : base(item.gameObject, parentTransform)
        {
            _bullet = item;
            _lifetime = item.bulletStats.lifeTime;
        }

        protected override bool IsNullable()
        {
            return false;
        }
    }
}