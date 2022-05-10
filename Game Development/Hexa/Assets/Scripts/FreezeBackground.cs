using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FreezeBackground : MonoBehaviour
{
	private sealed class _StartSnow_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal FreezeBackground _this;

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
				for (int i = 0; i < 30; i++)
				{
					this._this.CreateSnow(this._this.RectTransform.rect.y + UnityEngine.Random.Range(0f, this._this.RectTransform.rect.height), this._this.ImageSnow.gameObject, false, 125f);
				}
				for (int j = 0; j < this._this.ImageSnowFlake.Length; j++)
				{
					for (int k = 0; k < 3; k++)
					{
						this._this.CreateSnow(this._this.RectTransform.rect.y + UnityEngine.Random.Range(0f, this._this.RectTransform.rect.height), this._this.ImageSnowFlake[j].gameObject, true, 100f);
					}
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

		internal GameObject prefab;

		internal bool rotate;

		internal float v;

		internal FreezeBackground _this;

		internal void __m__0()
		{
			TuNDPool.Despawn(this.image.gameObject);
			this._this.CreateSnow(this._this.RectTransform.rect.y + this._this.RectTransform.rect.height, this.prefab, this.rotate, this.v);
		}
	}

	public Image ImageSnow;

	public Image[] ImageSnowFlake;

	public RectTransform RectTransform;

	private void Start()
	{
		TuNDPool.Preload(this.ImageSnow.gameObject, base.transform, 30);
		base.StartCoroutine(this.StartSnow());
	}

	private IEnumerator StartSnow()
	{
		FreezeBackground._StartSnow_c__Iterator0 _StartSnow_c__Iterator = new FreezeBackground._StartSnow_c__Iterator0();
		_StartSnow_c__Iterator._this = this;
		return _StartSnow_c__Iterator;
	}

	private void CreateSnow(float posY, GameObject prefab, bool rotate = false, float v = 125f)
	{
		Image image = TuNDPool.Spawn(prefab, base.transform).GetComponent<Image>();
		image.transform.localPosition = new Vector2(this.RectTransform.rect.x + UnityEngine.Random.Range(0f, this.RectTransform.rect.width), posY);
		image.transform.localScale = UnityEngine.Random.Range(0.2f, 1f) * Vector2.one;
		image.color = new Color(1f, 1f, 1f, UnityEngine.Random.Range(0.5f, 1f));
		if (rotate)
		{
			image.transform.DORotate(new Vector3(0f, 0f, -360f), UnityEngine.Random.Range(5f, 8f), RotateMode.FastBeyond360).SetLoops(-1);
		}
		float duration = (image.transform.localPosition.y - this.RectTransform.rect.y) / UnityEngine.Random.Range(v - 25f, v + 25f);
		image.transform.DOLocalMoveY(this.RectTransform.rect.y - 30f, duration, false).OnComplete(delegate
		{
			TuNDPool.Despawn(image.gameObject);
			this.CreateSnow(this.RectTransform.rect.y + this.RectTransform.rect.height, prefab, rotate, v);
		});
	}
}
