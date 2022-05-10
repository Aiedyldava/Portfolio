using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TextFly : MonoBehaviour
{
	public Text Text;

	public Outline Outline;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StartEffect(Vector3 position, string text, Color color, float timeDelay)
	{
		this.Text.text = text;
		base.gameObject.transform.position = position;
		this.Text.color = new Color(1f, 1f, 1f, 0f);
		base.gameObject.transform.localScale = Vector3.one * 0.7f;
		DOTween.Sequence().SetDelay(timeDelay).AppendCallback(delegate
		{
			base.gameObject.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
		}).Append(this.Text.DOFade(1f, 0.2f).SetEase(Ease.Linear)).AppendInterval(0.8f).Append(this.Text.DOFade(0f, 0.2f).SetEase(Ease.Linear));
	}
}
