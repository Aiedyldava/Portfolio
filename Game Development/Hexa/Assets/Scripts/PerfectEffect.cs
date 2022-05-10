using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PerfectEffect : MonoBehaviour
{
	public Image Background;

	public Text Text;

	private string[] _strings = new string[]
	{
		"PERFECT",
		"GREAT",
		"AWESOME",
		"GOOD"
	};

	private Sequence _sequence;

	private static TweenCallback __f__am_cache0;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StartEffect()
	{
		this.StartEffect(this._strings[UnityEngine.Random.Range(0, this._strings.Length)]);
	}

	public void StartEffect(string text)
	{
		base.transform.SetAsLastSibling();
		base.gameObject.SetActive(true);
		this.Reset();
		this.Text.text = text;
		this.Background.DOFade(0.7f, 0.5f).SetEase(Ease.Linear).SetDelay(1f);
		this._sequence = DOTween.Sequence().AppendInterval(1f).Append(this.Text.rectTransform.DOAnchorPosX(0f, 0.5f, false).SetEase(Ease.OutBack)).AppendCallback(delegate
		{
			if (Preference.Instance.DataGame.FirstMulti)
			{
				Preference.Instance.DataGame.FirstMulti = false;
			}
		}).AppendInterval(0.5f).Append(this.Text.rectTransform.DOAnchorPosX(-this.Background.rectTransform.sizeDelta.x / 2f - this.Text.rectTransform.sizeDelta.x / 2f, 0.5f, false).SetEase(Ease.InBack)).Append(this.Background.DOFade(0f, 0.2f).SetEase(Ease.Linear)).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
		});
	}

	public void Reset()
	{
		if (this._sequence != null)
		{
			this._sequence.Kill(false);
		}
		this.Background.color = new Color(this.Background.color.r, this.Background.color.g, this.Background.color.b, 0f);
		this.Text.rectTransform.anchoredPosition = new Vector2(this.Background.rectTransform.sizeDelta.x / 2f + this.Text.rectTransform.sizeDelta.x / 2f, 0f);
	}
}
