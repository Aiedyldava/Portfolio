using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SnowBackground : MonoBehaviour
{
	private sealed class _StartSnow_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal SnowBackground _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _StartSnow_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForEndOfFrame();
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				for (int i = 0; i < 50; i++)
				{
					this._this.CreateSnow(this._this.RectTransform.rect.y + UnityEngine.Random.Range(0f, this._this.RectTransform.rect.height));
				}
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _CreateSnow_c__AnonStorey1
	{
		internal Image image;

		internal SnowBackground _this;

		internal void __m__0()
		{
			TuNDPool.Despawn(this.image.gameObject);
			this._this.CreateSnow(this._this.RectTransform.rect.y + this._this.RectTransform.rect.height);
		}
	}

	public Image ImageSnow;

	public RectTransform RectTransform;

	private void Start()
	{
		TuNDPool.Preload(this.ImageSnow.gameObject, base.transform, 50);
		base.StartCoroutine(this.StartSnow());
	}

	private IEnumerator StartSnow()
	{
		SnowBackground._StartSnow_c__Iterator0 _StartSnow_c__Iterator = new SnowBackground._StartSnow_c__Iterator0();
		_StartSnow_c__Iterator._this = this;
		return _StartSnow_c__Iterator;
	}

	private void CreateSnow(float posY)
	{
		Image image = TuNDPool.Spawn(this.ImageSnow.gameObject, base.transform).GetComponent<Image>();
		image.transform.localPosition = new Vector2(this.RectTransform.rect.x + UnityEngine.Random.Range(0f, this.RectTransform.rect.width), posY);
		image.transform.localScale = UnityEngine.Random.Range(0.2f, 1f) * Vector2.one;
		image.color = new Color(1f, 1f, 1f, UnityEngine.Random.Range(0.5f, 1f));
		float duration = (image.transform.localPosition.y - this.RectTransform.rect.y) / (float)UnityEngine.Random.Range(100, 150);
		image.transform.DOLocalMoveY(this.RectTransform.rect.y, duration, false).OnComplete(delegate
		{
			TuNDPool.Despawn(image.gameObject);
			this.CreateSnow(this.RectTransform.rect.y + this.RectTransform.rect.height);
		});
	}

	private void Update()
	{
	}
}
