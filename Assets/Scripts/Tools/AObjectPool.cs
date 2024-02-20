using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
	public abstract class AObjectPool
	{
		protected readonly GameObject _poolItem;
		protected readonly Transform _parentTransform;

		private Queue<GameObject> _objectsPool;

		protected AObjectPool(GameObject item, Transform parentTransform)
		{
			_poolItem = item;
			_parentTransform = parentTransform;
		}		

		protected abstract bool IsNullable();

		public GameObject GetPoolObject(Vector3 startPosition, Vector3 direction)
		{            
			if (_objectsPool.Count == 0)
				AddNewObjectInPool();

			GameObject obj = _objectsPool.Dequeue();
			obj.SetActive(true);
			obj.transform.SetParent(null);
			obj.transform.position = startPosition;
			// bullet.transform.rotation = Quaternion.LookRotation(direction);
			return obj;
		}

		public virtual void CreateObjectPool(int pullLength)
		{
			int queueLength =  Mathf.Min(pullLength, 200);
			_objectsPool = new Queue<GameObject>(queueLength);
			for (int i = 0; i < queueLength; i++)
				AddNewObjectInPool();
		}

		private void AddNewObjectInPool()
		{
			GameObject bullet = Object.Instantiate(_poolItem, _parentTransform);
			bullet.layer = 2; // ignore raycast
			bullet.SetActive(false);
			_objectsPool.Enqueue(bullet);
		}

		public void ReturnObjectInPool(GameObject gameObject, Vector3 scale = default)
		{
			gameObject.SetActive(false);
			if (IsNullable())
			{
				Object.Destroy(gameObject);
				return;
			}

			gameObject.transform.SetParent(_parentTransform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
			gameObject.transform.localScale = scale == default ? Vector3.one : scale;
			_objectsPool.Enqueue(gameObject);
		}
	}
}