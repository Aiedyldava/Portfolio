using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmPopup : BaseController
{
	private sealed class _Confirm_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal ConfirmPopup _this;

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

		public _Confirm_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.Hide();
				if (Preference.Instance.DataGame.Coin >= Preference.Instance.DataGame.DestroyPrice)
				{
					GameController.ScreenManager.PlayController.SetTextCoin(Preference.Instance.DataGame.Coin - Preference.Instance.DataGame.DestroyPrice);
					this._current = new WaitForSeconds(0.15f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				GameController.DialogManager.PopupShop.Show();
				break;
			case 1u:
				if (this._this._miniHexa != null)
				{
					this._this._miniHexa.Group.gameObject.SetActive(false);
					GameController.EffectController.DestroyBlockEffect(this._this._miniHexa);
					GameController.AudioController.PlayOneShot("Audios/Effect/skill");
					this._current = new WaitForSeconds(0.3f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				}
				break;
			case 2u:
				if (this._this._miniHexa != null)
				{
					GameController.ScreenManager.PlayController.ReplaceMiniHexa(this._this._miniHexa);
					Preference.Instance.DataGame.DestroyPrice *= 2;
					//GameController.AnalyticsController.LogEvent(AnalyticsController.USE_BIN);
				}
				break;
			default:
				return false;
			}
			this._PC = -1;
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

	public Button Button;

	public Button ButtonCancel;

	public Button ButtonOk;

	public Text TextPrice;

	public GameObject Group;

	private Tween _tween;

	private MiniHexa _miniHexa;

	private void Start()
	{
		this.Button.onClick.AddListener(new UnityAction(this.Hide));
		this.ButtonCancel.onClick.AddListener(new UnityAction(this.Hide));
		this.ButtonOk.onClick.AddListener(delegate
		{
			BaseController.GameController.StartCoroutine(this.Confirm());
		});
	}

	private IEnumerator Confirm()
	{
		ConfirmPopup._Confirm_c__Iterator0 _Confirm_c__Iterator = new ConfirmPopup._Confirm_c__Iterator0();
		_Confirm_c__Iterator._this = this;
		return _Confirm_c__Iterator;
	}

	private void Update()
	{
	}

	public void Hide()
	{
		if (this._tween != null)
		{
			this._tween.Kill(false);
		}
		this.Group.transform.DOScale(Vector2.zero, 0.15f).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
		});
	}

	public void Show(MiniHexa miniHexa)
	{
		if (this._tween != null)
		{
			this._tween.Kill(false);
		}
		this.TextPrice.text = Preference.Instance.DataGame.DestroyPrice + string.Empty;
		this.Group.transform.localScale = Vector2.zero;
		this.Group.transform.DOScale(Vector2.one, 0.15f);
		this._miniHexa = miniHexa;
		base.gameObject.SetActive(true);
		this.Group.transform.position = new Vector2(miniHexa.transform.position.x, this.Group.transform.position.y);
		this.Group.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.TextPrice.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
	}
}
