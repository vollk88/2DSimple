using System;
using Tools;
using Units;
using Units.Character;
using UnityEngine;
using UnityEngine.Serialization;
using Weapon;

namespace DefaultNamespace
{
    
    // [CreateAssetMenu(fileName = "Create Bullet", menuName = "Bullet")]
    public class Bullet : CustomBehaviour
    {
        private BulletStats _bulletStats;
        private Vector3 _destination;
        private Vector3 _direction;
        public int collisionCount;


        #region Properties
        public Vector3 destination
        {
            get => _destination;
            set => _destination = value;
        }
       
        public BulletStats bulletStats
        {
            get => _bulletStats;
            set => _bulletStats = value;
        }
        

        #endregion

        private void OnEnable()
        {
            collisionCount = 0;
            // transform.rotation = Quaternion.identity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            collisionCount += 1;
            AUnit target = other.gameObject. GetComponent<AUnit>();
            Debug.Log("Collisio");
            if (target != null)
            {
                Debug.Log("AUnit finded");
                target.health.TakeHit(_bulletStats.damageStats);
            }
        }

        public void BulletStats(BulletStats stats, Vector3 dest = default)
        {
            _bulletStats.damageStats = stats.damageStats;
            _bulletStats.bulletSpeed = stats.bulletSpeed;
            _bulletStats.lifeTime = stats.lifeTime;
            _bulletStats.bornTime = Time.time;
            destination = dest == default ? PlayerController.mousePos : dest;
        }
    }
}