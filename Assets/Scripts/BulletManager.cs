using System.Collections.Generic;
using System.Linq;
using Tools;
using Units.Character;
using UnityEngine;
using Weapon;

namespace DefaultNamespace
{
    public class BulletManager : MonoBehaviour
    {
        private BulletPool _bulletsPool;
        private Dictionary<Bullet, Vector2> _shootingDirections = new Dictionary<Bullet, Vector2>();

        public void Init(Bullet bullet, Transform placeOfSpawn)
        {
            _bulletsPool = new BulletPool(bullet, placeOfSpawn);
            _bulletsPool.CreateObjectPool(4);
        }

        private void FixedUpdate()
        {
            foreach (var kvp in _shootingDirections.ToList())
            {
                Bullet bullet = kvp.Key;
                Vector2 shootingDirection = kvp.Value;
                // if (bullet.collisionCount > 0)

                if (bullet == null || !bullet.gameObject.activeSelf || bullet.collisionCount > 0)
                {
                    ReturnBulletToPool(bullet);
                }
                else
                {
                    MoveBullet(bullet, shootingDirection);

                    if (IsBulletExpired(bullet))
                    {
                        ReturnBulletToPool(bullet);
                    }
                }
            }
        }

        private void ReturnBulletToPool(Bullet bullet)
        {
            _bulletsPool.ReturnObjectInPool(bullet.gameObject);
            _shootingDirections.Remove(bullet);
        }

        private void MoveBullet(Bullet bullet, Vector2 shootingDirection)
        {
            Vector3 position = bullet.transform.position;
            position.x += shootingDirection.x * bullet.bulletStats.bulletSpeed * Time.fixedDeltaTime;
            position.y += shootingDirection.y * bullet.bulletStats.bulletSpeed * Time.fixedDeltaTime;

            bullet.transform1.position = position;
        }

        private bool IsBulletExpired(Bullet bullet)
        {
            return Time.time - bullet.bulletStats.bornTime > bullet.bulletStats.lifeTime;
        }

        public void AddBullet(BulletStats bulletStats, Vector3 dest = default)
        {
            Vector3 dest1 = dest == default ? PlayerController.mousePos : dest;
            Debug.Log("Set dest: " + dest1);

            GameObject obj = _bulletsPool.GetPoolObject(bulletStats.bulletSpawn.transform.position, dest1);
            Bullet bullet = obj.GetComponent<Bullet>();
            bullet.BulletStats(bulletStats, dest1);

            _shootingDirections[bullet] = new Vector2(dest1.x - bulletStats.bulletSpawn.transform.position.x, dest1.y - bulletStats.bulletSpawn.transform.position.y).normalized;
        }
    }
}