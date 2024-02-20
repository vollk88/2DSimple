using System;
using Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapon
{
    [Serializable]
    public struct BulletStats
    {
        public DamageStats damageStats;
        public GameObject bulletSpawn;
        public float bulletSpeed;
        public float lifeTime;
        public float bornTime;
    }
}