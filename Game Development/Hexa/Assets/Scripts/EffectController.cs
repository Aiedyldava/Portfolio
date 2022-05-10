using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EffectController : BaseController
{
	private sealed class _StartRemoveEffect_c__AnonStorey1
	{
		internal Triangle triangle;
	}

	private sealed class _StartRemoveEffect_c__AnonStorey0
	{
		internal Image image;

		internal float time;

		internal float scale;

		internal float x;

		internal float y;

		internal EffectController._StartRemoveEffect_c__AnonStorey1 __f__ref_1;

		internal void __m__0()
		{
			this.image.gameObject.SetActive(true);
			this.__f__ref_1.triangle.Reset();
		}

		internal void __m__1()
		{
			this.image.transform.DORotate(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), this.time, RotateMode.Fast).SetEase(Ease.Linear);
			this.image.transform.DOScale(Vector3.one * this.scale, this.time).SetEase(Ease.Linear);
			this.image.rectTransform.DOAnchorPos(new Vector2(this.x, this.y), this.time, false).SetEase(Ease.Linear);
		}

		internal void __m__2()
		{
			UnityEngine.Object.Destroy(this.image.gameObject);
		}
	}

	private sealed class _DestroyBlockEffect_c__AnonStorey2
	{
		internal Image image;

		internal void __m__0()
		{
			UnityEngine.Object.Destroy(this.image.gameObject);
		}
	}

	private sealed class _AddGemEffect_c__AnonStorey3
	{
		private sealed class _AddGemEffect_c__AnonStorey4
		{
			internal Image image2;

			internal EffectController._AddGemEffect_c__AnonStorey3 __f__ref_3;

			internal void __m__0()
			{
				this.image2.DOFade(0f, 0.3f).OnComplete(delegate
				{
					UnityEngine.Object.Destroy(this.image2.gameObject);
				});
			}

			internal void __m__1()
			{
				UnityEngine.Object.Destroy(this.image2.gameObject);
			}
		}

		internal Image image;

		internal Image diamond;

		internal float time;

		internal int gem;

		internal EffectController _this;

		internal void __m__0()
		{
			this.image.transform.DORotate(this.diamond.transform.eulerAngles, this.time, RotateMode.Fast);
		}

		internal void __m__1()
		{
			UnityEngine.Object.Destroy(this.image.gameObject);
			Preference.Instance.DataGame.Coin += this.gem;
			if (GameController.ScreenManager.PlayController != null)
			{
				GameController.ScreenManager.PlayController.SetTextCoin(Preference.Instance.DataGame.Coin);
			}
			for (int i = 0; i < 12; i++)
			{
				Image image2 = this._this.CreateImage("Images/UI/diamond", this._this.transform);
				image2.rectTransform.sizeDelta = this.diamond.rectTransform.sizeDelta * 0.5f;
				image2.transform.position = this.diamond.transform.position;
				int num = UnityEngine.Random.Range(30, 50);
				int num2 = UnityEngine.Random.Range(0, 360);
				image2.transform.DOLocalMove(new Vector3((float)num * Mathf.Cos(0.0174532924f * (float)(num2 - 90)), (float)num * Mathf.Sin(0.0174532924f * (float)(num2 - 90))) + image2.transform.localPosition, UnityEngine.Random.Range(0.2f, 0.3f), false).OnComplete(delegate
				{
					image2.DOFade(0f, 0.3f).OnComplete(delegate
					{
						UnityEngine.Object.Destroy(image2.gameObject);
					});
				});
			}
		}
	}

	public GameObject TextFlyPrefab;

	public GameObject HexagonPrefab;

	public RectTransform RectTransform;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StartTextFly(Vector3 position, string text, Color color, float timeDelay)
	{
		TextFly component = UnityEngine.Object.Instantiate<GameObject>(this.TextFlyPrefab).GetComponent<TextFly>();
		component.transform.SetParent(base.transform, false);
		component.StartEffect(position, text, color, timeDelay);
	}

	public void StartWinEff(List<Triangle> triangles, float delay, int score)
	{
		Hexagon component = UnityEngine.Object.Instantiate<GameObject>(this.HexagonPrefab).GetComponent<Hexagon>();
		component.PlayController = GameController.ScreenManager.PlayController;
		component.transform.SetParent(base.transform, false);
		component.StartEffect(triangles, delay, score);
	}

	public void StartRemoveEffect(Triangle triangle, float delay)
	{
		for (int i = 0; i < 4; i++)
		{
			bool flag = UnityEngine.Random.Range(0, 2) != 0;
			string resource = (!flag) ? "Images/Effect/star" : "Images/GamePlay/triangle";
			Image image = base.CreateImage(resource, base.transform);
			if (flag)
			{
				image.color = GameController.ThemeManager.CurrentTheme.GameColors[triangle.ColorId];
			}
			image.transform.eulerAngles = new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
			image.transform.localScale = Vector3.zero;
			image.transform.position = triangle.ImageColor.transform.position;
			float time = UnityEngine.Random.Range(0.3f, 0.5f);
			float num = 50f;
			float x = UnityEngine.Random.Range(-num, num);
			float y = (float)Math.Sqrt((double)(num * num - x * x)) * (float)((UnityEngine.Random.Range(-1, 1) >= 0) ? (-1) : 1);
			x += image.rectTransform.anchoredPosition.x;
			y += image.rectTransform.anchoredPosition.y;
			float scale = (!flag) ? UnityEngine.Random.Range(0.1f, 0.3f) : UnityEngine.Random.Range(0.6f, 0.9f);
			image.gameObject.SetActive(false);
			DOTween.Sequence().AppendInterval(delay).AppendCallback(delegate
			{
				image.gameObject.SetActive(true);
				triangle.Reset();
			}).AppendCallback(delegate
			{
				image.transform.DORotate(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), time, RotateMode.Fast).SetEase(Ease.Linear);
				image.transform.DOScale(Vector3.one * scale, time).SetEase(Ease.Linear);
				image.rectTransform.DOAnchorPos(new Vector2(x, y), time, false).SetEase(Ease.Linear);
			}).AppendInterval(time * 2f / 3f).Append(image.DOFade(0f, 0.2f).SetEase(Ease.Linear)).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(image.gameObject);
			});
		}
	}

	public void DestroyBlockEffect(MiniHexa miniHexa)
	{
		for (int i = 0; i < miniHexa.Triangles.Length; i++)
		{
			Image image = base.CreateImage(miniHexa.Triangles[i].sprite, base.transform);
			image.rectTransform.sizeDelta = miniHexa.Triangles[i].rectTransform.sizeDelta;
			image.transform.eulerAngles = miniHexa.Triangles[i].transform.eulerAngles;
			image.transform.position = miniHexa.Triangles[i].transform.position;
			image.transform.localScale = miniHexa.Group.transform.localScale;
			image.color = miniHexa.Triangles[i].color;
			image.gameObject.SetActive(true);
			float delay = (float)(miniHexa.Triangles.Length - i + 1) * 0.01f;
			float duration = UnityEngine.Random.Range(0.4f, 0.6f);
			image.rectTransform.DOAnchorPos(new Vector2(image.rectTransform.anchoredPosition.x + UnityEngine.Random.Range(-100f, 100f), image.rectTransform.anchoredPosition.y + UnityEngine.Random.Range(-100f, 100f)), duration, false).SetDelay(delay).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(image.gameObject);
			});
			image.DOFade(0f, duration).SetDelay(delay);
		}
	}

	public void GameOverEffect(Triangle[] Triangles)
	{
		for (int i = 0; i < Triangles.Length; i++)
		{
			if (Triangles[i].IsSet)
			{
				Triangles[i].ImageColor.DOColor(Color.gray, 1f);
			}
			else
			{
				Triangles[i].Reset();
			}
		}
	}

	public void AddGemEffect(Vector3 position, int gem, Image diamond, float times = -1f)
	{
		Image image = base.CreateImage("Images/UI/diamond", base.transform);
		image.transform.eulerAngles = new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
		image.rectTransform.sizeDelta = diamond.rectTransform.sizeDelta;
		image.transform.position = position;
		int num = UnityEngine.Random.Range(-40, 40);
		float time = times;
		if (time <= 0f)
		{
			time = 0.8f;
		}
		else
		{
			time += UnityEngine.Random.Range(-0.1f, 0.1f);
		}
		DOTween.Sequence().Append(image.transform.DOLocalMove(new Vector2(image.transform.localPosition.x + (float)num, image.transform.localPosition.y + (float)num), 0.12f, false)).AppendCallback(delegate
		{
			image.transform.DORotate(diamond.transform.eulerAngles, time, RotateMode.Fast);
		}).Append(image.transform.DOMove(diamond.transform.position, time, false)).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(image.gameObject);
			Preference.Instance.DataGame.Coin += gem;
			if (GameController.ScreenManager.PlayController != null)
			{
				GameController.ScreenManager.PlayController.SetTextCoin(Preference.Instance.DataGame.Coin);
			}
			for (int i = 0; i < 12; i++)
			{
				Image image2 = this.CreateImage("Images/UI/diamond", this.transform);
				image2.rectTransform.sizeDelta = diamond.rectTransform.sizeDelta * 0.5f;
				image2.transform.position = diamond.transform.position;
				int num2 = UnityEngine.Random.Range(30, 50);
				int num3 = UnityEngine.Random.Range(0, 360);
				image2.transform.DOLocalMove(new Vector3((float)num2 * Mathf.Cos(0.0174532924f * (float)(num3 - 90)), (float)num2 * Mathf.Sin(0.0174532924f * (float)(num3 - 90))) + image2.transform.localPosition, UnityEngine.Random.Range(0.2f, 0.3f), false).OnComplete(delegate
				{
					image2.DOFade(0f, 0.3f).OnComplete(delegate
					{
						UnityEngine.Object.Destroy(image2.gameObject);
					});
				});
			}
		});
	}
}
