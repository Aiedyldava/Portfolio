using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Hexagon : BaseController
{
	private sealed class _StartEffect_c__AnonStorey0
	{
		internal float cellAmount;

		internal float timeMove;

		internal int score;

		internal Hexagon _this;

		internal void __m__0()
		{
			GameController.AudioController.PlayOneShot("Audios/Effect/double_hexa");
			this._this.Image.fillAmount = this.cellAmount;
			this._this.gameObject.SetActive(true);
		}

		internal void __m__1()
		{
			this._this.Image.fillAmount = this.cellAmount * 2f;
		}

		internal void __m__2()
		{
			this._this.Image.fillAmount = this.cellAmount * 3f;
		}

		internal void __m__3()
		{
			this._this.Image.fillAmount = this.cellAmount * 4f;
		}

		internal void __m__4()
		{
			this._this.Image.fillAmount = this.cellAmount * 5f;
		}

		internal void __m__5()
		{
			this._this.Image.fillAmount = 1f;
			this._this.Background.DOFade(1f, 0.3f);
		}

		internal void __m__6()
		{
			this._this.Image.fillAmount = 1f;
			this._this.Background.DOFade(1f, 0.1f);
			this._this.transform.DORotate(new Vector3(0f, 0f, -360f), this.timeMove, RotateMode.FastBeyond360).SetEase(Ease.Linear);
			this._this.transform.DOScale(Vector3.one * 0.4f, this.timeMove).SetEase(Ease.Linear);
			this._this.CanvasGroup.DOFade(0.5f, this.timeMove).SetEase(Ease.Linear);
			GameController.AudioController.PlayOneShot("Audios/Effect/skill");
		}

		internal void __m__7()
		{
			if (this._this.PlayController != null && this.score > 0)
			{
				this._this.PlayController.AddScore(this.score);
			}
			UnityEngine.Object.Destroy(this._this.gameObject);
		}
	}

	public Image Image;

	public Image Background;

	public CanvasGroup CanvasGroup;

	public PlayController PlayController;

	private void Start()
	{
		base.gameObject.gameObject.transform.localScale = Vector3.one * BaseController.GameController.HexaScale;
	}

	private void Update()
	{
	}

	public void StartEffect(Vector3 pos, Vector3 endPos, float delay, Color color)
	{
		base.transform.position = pos;
		this.Image.color = color;
		this.StartEffect(delay, 0, endPos);
	}

	private void StartEffect(float delay, int score, Vector3 endPos)
	{
		float cellAmount = 0.166666672f;
		float num = 0.06f;
		float timeMove = 0.4f + UnityEngine.Random.Range(0f, 0.2f);
		this.Background.color = new Color(this.Background.color.r, this.Background.color.g, this.Background.color.b, 0f);
		this.CanvasGroup.alpha = 1f;
		this.Image.fillAmount = 0f;
		base.transform.localScale = Vector3.one;
		base.gameObject.SetActive(false);
		DOTween.Sequence().AppendInterval(num + delay).AppendCallback(delegate
		{
			GameController.AudioController.PlayOneShot("Audios/Effect/double_hexa");
			this.Image.fillAmount = cellAmount;
			this.gameObject.SetActive(true);
		}).AppendInterval(num).AppendCallback(delegate
		{
			this.Image.fillAmount = cellAmount * 2f;
		}).AppendInterval(num).AppendCallback(delegate
		{
			this.Image.fillAmount = cellAmount * 3f;
		}).AppendInterval(num).AppendCallback(delegate
		{
			this.Image.fillAmount = cellAmount * 4f;
		}).AppendInterval(num).AppendCallback(delegate
		{
			this.Image.fillAmount = cellAmount * 5f;
		}).AppendInterval(num).AppendCallback(delegate
		{
			this.Image.fillAmount = 1f;
			this.Background.DOFade(1f, 0.3f);
		}).Append(base.transform.DOScale(Vector3.one * 1.2f, 0.25f)).Append(base.transform.DOScale(Vector3.one, 0.15f)).AppendCallback(delegate
		{
			this.Image.fillAmount = 1f;
			this.Background.DOFade(1f, 0.1f);
			this.transform.DORotate(new Vector3(0f, 0f, -360f), timeMove, RotateMode.FastBeyond360).SetEase(Ease.Linear);
			this.transform.DOScale(Vector3.one * 0.4f, timeMove).SetEase(Ease.Linear);
			this.CanvasGroup.DOFade(0.5f, timeMove).SetEase(Ease.Linear);
			GameController.AudioController.PlayOneShot("Audios/Effect/skill");
		}).Append(base.transform.DOLocalMove(new Vector2(base.transform.localPosition.x - 100f, base.transform.localPosition.y - 100f), 0.15f, false).SetEase(Ease.Linear)).Append(base.transform.DOMove(endPos, timeMove - 0.15f, false).SetEase(Ease.Linear)).AppendCallback(delegate
		{
			if (this.PlayController != null && score > 0)
			{
				this.PlayController.AddScore(score);
			}
			UnityEngine.Object.Destroy(this.gameObject);
		});
	}

	public void StartEffect(List<Triangle> triangles, float delay, int score)
	{
		base.gameObject.SetActive(true);
		base.transform.position = (triangles[0].transform.position + triangles[3].transform.position) / 2f;
		base.transform.localScale = Vector3.one;
		this.Image.color = GameController.ThemeManager.CurrentTheme.GameColors[triangles[0].ColorId];
		this.StartEffect(delay, score, this.PlayController.ScoreImage.transform.position);
	}
}
