using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ThemeItem : BaseController
{
	private sealed class _InitSize_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal ThemeItem _this;

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

		public _InitSize_c__Iterator0()
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
				this._this.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(this._this.GetComponent<Image>().rectTransform.sizeDelta.x, this._this.GetComponent<Image>().rectTransform.sizeDelta.x * 359f / 720f);
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

	public Button ButtonUse;

	public Button ButtonBuy;

	public Button ButtonWatchAds;

	public Text TextName;

	public Text NumAds;

	public Text Requir;

	public int Id;

	public int Price;

	public int RequirAds;

	public RectTransform Background;

	public ThemeStatus ThemeStatus
	{
		get
		{
			return Preference.Instance.DataGame.ThemeStatus[this.Id];
		}
	}

	private void Start()
	{
		this.ButtonUse.onClick.AddListener(delegate
		{
			Preference.Instance.DataGame.ThemeId = this.Id;
			GameController.ThemeManager.InitTheme();
			GameController.DialogManager.DialogThemes.Hide();
			//GameController.AnalyticsController.LogEvent(AnalyticsController.USE_THEME, AnalyticsController.NAME, this.TextName.text);
		});
		base.StartCoroutine(this.InitSize());
		this.ButtonBuy.onClick.AddListener(delegate
		{
			if (Preference.Instance.DataGame.Coin < this.Price)
			{
				GameController.DialogManager.PopupShop.Show();
			}
			else
			{
				Preference.Instance.DataGame.Coin -= this.Price;
				this.SetOpen();
				if (GameController.ScreenManager.PlayController != null)
				{
					GameController.ScreenManager.PlayController.SetTextCoin(Preference.Instance.DataGame.Coin);
				}
			}
		});
		this.ButtonWatchAds.onClick.AddListener(delegate
		{
			GameController.AdsController.ShowAd(delegate
			{
				this.ThemeStatus.NumAds++;
				this.InitInfo();
			});
		});
	}

	private IEnumerator InitSize()
	{
		ThemeItem._InitSize_c__Iterator0 _InitSize_c__Iterator = new ThemeItem._InitSize_c__Iterator0();
		_InitSize_c__Iterator._this = this;
		return _InitSize_c__Iterator;
	}

	private void Update()
	{
	}

	private void SetOpen()
	{
		this.ThemeStatus.IsOpen = true;
		this.ButtonUse.gameObject.SetActive(true);
		this.ButtonBuy.gameObject.SetActive(false);
		this.ButtonWatchAds.gameObject.SetActive(false);
		this.Requir.gameObject.SetActive(false);
	}

	public void InitInfo()
	{
		if (this.ThemeStatus.IsOpen)
		{
			this.SetOpen();
		}
		else
		{
			this.ButtonUse.gameObject.SetActive(false);
			this.ButtonBuy.gameObject.SetActive(true);
			this.ButtonWatchAds.gameObject.SetActive(true);
			this.Requir.gameObject.SetActive(false);
			if (this.Id == 7)
			{
				if (Preference.Instance.DataGame.HighScore >= 600)
				{
					this.SetOpen();
				}
				else
				{
					this.ButtonWatchAds.gameObject.SetActive(false);
					this.Requir.gameObject.SetActive(true);
					this.Requir.text = "Or rearch score 600 (" + Preference.Instance.DataGame.HighScore + "/600)";
				}
			}
			else if (this.Id == 8)
			{
				if (Preference.Instance.DataGame.NumPlay >= 50)
				{
					this.SetOpen();
				}
				else
				{
					this.ButtonWatchAds.gameObject.SetActive(false);
					this.Requir.gameObject.SetActive(true);
					this.Requir.text = "Or play 50 times (" + Preference.Instance.DataGame.NumPlay + "/50)";
				}
			}
			else if (this.ThemeStatus.NumAds >= this.RequirAds)
			{
				this.SetOpen();
			}
			else
			{
				this.NumAds.text = "x" + (this.RequirAds - this.ThemeStatus.NumAds);
				this.ButtonWatchAds.gameObject.SetActive(true);
			}
		}
	}
}
