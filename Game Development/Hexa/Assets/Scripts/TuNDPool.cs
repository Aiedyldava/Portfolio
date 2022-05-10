using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TuNDPool
{
	private class Pool
	{
		private int nextId = 1;

		private Stack<GameObject> inactive;

		private GameObject prefab;

		public Pool(GameObject prefab, int initialQty)
		{
			this.prefab = prefab;
			this.inactive = new Stack<GameObject>(initialQty);
		}

		public GameObject Spawn(Transform parent)
		{
			GameObject gameObject;
			if (this.inactive.Count == 0)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
				gameObject.name = string.Concat(new object[]
				{
					this.prefab.name,
					" (",
					this.nextId++,
					")"
				});
				gameObject.AddComponent<TuNDPool.PoolMember>().myPool = this;
			}
			else
			{
				gameObject = this.inactive.Pop();
				if (gameObject == null)
				{
					return this.Spawn(parent);
				}
			}
			gameObject.SetActive(true);
			gameObject.transform.SetParent(parent, false);
			return gameObject;
		}

		public void Despawn(GameObject obj)
		{
			obj.SetActive(false);
			DOTween.Kill(obj, false);
			this.inactive.Push(obj);
		}
	}

	private class PoolMember : MonoBehaviour
	{
		public TuNDPool.Pool myPool;
	}

	private const int DEFAULT_POOL_SIZE = 3;

	private static Dictionary<GameObject, TuNDPool.Pool> pools;

	private static void Init(GameObject prefab = null, int qty = 3)
	{
		if (TuNDPool.pools == null)
		{
			TuNDPool.pools = new Dictionary<GameObject, TuNDPool.Pool>();
		}
		if (prefab != null && !TuNDPool.pools.ContainsKey(prefab))
		{
			TuNDPool.pools[prefab] = new TuNDPool.Pool(prefab, qty);
		}
	}

	public static void Preload(GameObject prefab, Transform parent, int qty = 1)
	{
		TuNDPool.Init(prefab, qty);
		GameObject[] array = new GameObject[qty];
		for (int i = 0; i < qty; i++)
		{
			array[i] = TuNDPool.Spawn(prefab, parent);
		}
		for (int j = 0; j < qty; j++)
		{
			TuNDPool.Despawn(array[j]);
		}
	}

	public static GameObject Spawn(GameObject prefab, Transform parent)
	{
		TuNDPool.Init(prefab, 3);
		return TuNDPool.pools[prefab].Spawn(parent);
	}

	public static void Despawn(GameObject obj)
	{
		TuNDPool.PoolMember component = obj.GetComponent<TuNDPool.PoolMember>();
		if (component == null)
		{
			UnityEngine.Object.Destroy(obj);
		}
		else
		{
			component.myPool.Despawn(obj);
		}
	}
}
