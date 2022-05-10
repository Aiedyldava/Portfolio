using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : BaseController
{
	public RectTransform Group;

	public Image BackgroundBlur;

	private Tweener _backgroundBlurTweener;

	private Color _blurColor;

	private float _timeOpen = 0.35f;

	private void Awake()
	{
		this._blurColor = this.BackgroundBlur.color;
	}

	private void Update()
	{
	}

	public virtual void Show()
	{
		base.transform.SetAsLastSibling();
		this.StopAllTweens();
		base.gameObject.SetActive(true);
		this.BackgroundBlur.color = new Color(this._blurColor.r, this._blurColor.g, this._blurColor.b, 0f);
		this._backgroundBlurTweener = this.BackgroundBlur.DOColor(this._blurColor, this._timeOpen).OnComplete(new TweenCallback(this.OnShowComplete));
		this.Group.anchoredPosition = new Vector2(0f, -base.GetComponent<RectTransform>().rect.height / 2f - this.Group.rect.height / 2f);
		this.Group.DOAnchorPosY(0f, this._timeOpen, false).SetEase(Ease.OutBack);
		GameController.AdsController.SetBannerShow(false);
	}

	public virtual void Hide()
	{
		this.StopAllTweens();
		this.Group.DOAnchorPosY(-base.GetComponent<RectTransform>().rect.height / 2f - this.Group.rect.height / 2f, this._timeOpen, false).SetEase(Ease.InBack).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
			if (GameController.ScreenManager.currentStage is PlayController && !Preference.Instance.DataGame.NoAds && GameController.DialogManager.GetNumberActiveDialog() == 0)
			{
				GameController.AdsController.SetBannerShow(true);
			}
		});
		this._backgroundBlurTweener = this.BackgroundBlur.DOColor(this._blurColor, this._timeOpen);
	}

	private void StopAllTweens()
	{
		if (this._backgroundBlurTweener != null)
		{
			this._backgroundBlurTweener.Kill(false);
		}
	}

	public virtual void OnShowComplete()
	{
	}
}
