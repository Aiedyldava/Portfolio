using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
	private sealed class _TweenNumber_c__AnonStorey0
	{
		internal int from;

		internal Text text;

		internal int __m__0()
		{
			return this.from;
		}

		internal void __m__1(int x)
		{
			this.from = x;
		}

		internal void __m__2()
		{
			this.text.text = this.from + string.Empty;
		}
	}

	private sealed class _TweenNumber_c__AnonStorey1
	{
		internal int from;

		internal Text text;

		internal int __m__0()
		{
			return this.from;
		}

		internal void __m__1(int x)
		{
			this.from = x;
		}

		internal void __m__2()
		{
			this.text.text = this.from + string.Empty;
		}
	}

	private sealed class _TweenNumber_c__AnonStorey2
	{
		internal int from;

		internal Text text;

		internal int __m__0()
		{
			return this.from;
		}

		internal void __m__1(int x)
		{
			this.from = x;
		}

		internal void __m__2()
		{
			this.text.text = this.from + string.Empty;
		}
	}

	[HideInInspector]
	public Transform CurrentParrent;

	private static GameController _gameController;

	public static GameController GameController
	{
		get
		{
			if (BaseController._gameController == null)
			{
				BaseController._gameController = UnityEngine.Object.FindObjectOfType<GameController>();
			}
			return BaseController._gameController;
		}
	}

	private void Start()
	{
		this.CurrentParrent = base.transform.parent;
	}

	public void ResetParrent()
	{
		if (this.CurrentParrent != null)
		{
			base.transform.SetParent(this.CurrentParrent);
		}
	}

	private void Update()
	{
	}

	public T InstantiatePrefab<T>(string resources)
	{
		return BaseController.InstantiatePrefab(resources).GetComponent<T>();
	}

	public static GameObject InstantiatePrefab(string resources)
	{
		return UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>(resources));
	}

	public Image CreateImage(string resource, Transform parent)
	{
		return this.CreateImage(Resources.Load<Sprite>(resource), parent);
	}

	public Image CreateImage(Sprite sprite, Transform parent)
	{
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<Image>();
		gameObject.transform.SetParent(parent, false);
		Image component = gameObject.GetComponent<Image>();
		component.sprite = sprite;
		component.SetNativeSize();
		component.transform.localScale = Vector3.one;
		return component;
	}

	public static void TweenNumber(int to, Text text)
	{
		int from = int.Parse(text.text);
		DOTween.To(() => from, delegate(int x)
		{
			from = x;
		}, to, 0.5f).OnUpdate(delegate
		{
			text.text = from + string.Empty;
		}).SetEase(Ease.Linear);
	}

	public static void TweenNumber(int from, int to, Text text)
	{
		DOTween.To(() => from, delegate(int x)
		{
			from = x;
		}, to, 0.5f).OnUpdate(delegate
		{
			text.text = from + string.Empty;
		}).SetEase(Ease.Linear);
	}

	public static void TweenNumber(int from, int to, Text text, float time)
	{
		DOTween.To(() => from, delegate(int x)
		{
			from = x;
		}, to, time).OnUpdate(delegate
		{
			text.text = from + string.Empty;
		}).SetEase(Ease.Linear);
	}
}
