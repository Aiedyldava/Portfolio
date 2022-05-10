using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FlashEfffect : MonoBehaviour
{
	private sealed class _StartFlashEffect_c__AnonStorey0
	{
		internal Image image;

		internal FlashEfffect _this;

		internal void __m__0()
		{
			this._this.StartFlashEffect(this.image);
		}
	}

	public Image Image;

	public Image Flash;

	public float time = 1.5f;

	public float timeDelay = 1.5f;

	private void Start()
	{
		this.StartFlashEffect(this.Flash);
	}

	private void Update()
	{
	}

	private void StartFlashEffect(Image image)
	{
		image.rectTransform.localPosition = new Vector3(this.Image.rectTransform.sizeDelta.x, 0f, 0f);
		image.rectTransform.DOLocalMoveX(-this.Image.rectTransform.sizeDelta.x, this.time, false).OnComplete(delegate
		{
			this.StartFlashEffect(image);
		}).SetDelay(this.timeDelay).SetEase(Ease.Linear);
	}
}
