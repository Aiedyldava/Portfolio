using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupSpin : Popup
{
	private sealed class _Spin_c__AnonStorey0
	{
		internal int id;

		internal PopupSpin _this;

		internal void __m__0()
		{
			this._this.SetFocus(this.id % this._this.Focus.Length);
			this.id++;
			if (this.id == this._this.Focus.Length * 4 + this._this._result + 1)
			{
				GameController.AudioController.PlayOneShot("Audios/Effect/daily_bonus");
			}
		}

		internal void __m__1()
		{
			this._this.ButtonGetX1.gameObject.SetActive(true);
			this._this.ButtonGetX2.gameObject.SetActive(true);
		}
	}

	public Button ButtonCancel;

	public Button ButtonSpine;

	public Button ButtonGetX1;

	public Button ButtonGetX2;

	public GameObject[] Focus;

	private int[] _values = new int[]
	{
		50,
		25,
		75,
		500
	};

	public Text TextTitle;

	private Sequence _sequence;

	private int _result;

	private void Start()
	{
		this.ButtonCancel.onClick.AddListener(new UnityAction(this.Hide));
		this.ButtonSpine.onClick.AddListener(delegate
		{
			GameController.AdsController.ShowAd(new AdsController.AdCallBack(this.Spin));
		});
		this.ButtonGetX1.onClick.AddListener(delegate
		{
			GameController.AudioController.PlayOneShot("Audios/Effect/double_hexa");
			this.Hide();
			GameController.EffectController.AddGemEffect(base.transform.position, this._values[this._result], GameController.ScreenManager.PlayController.ImageDiamond, -1f);
			//GameController.AnalyticsController.LogEvent(AnalyticsController.SPIN, AnalyticsController.Gem, this._values[this._result]);
		});
		this.ButtonGetX2.onClick.AddListener(delegate
		{
			GameController.AdsController.ShowAd(delegate
			{
				GameController.AudioController.PlayOneShot("Audios/Effect/double_hexa");
				this.Hide();
				GameController.EffectController.AddGemEffect(base.transform.position, this._values[this._result] * 2, GameController.ScreenManager.PlayController.ImageDiamond, -1f);
				//GameController.AnalyticsController.LogEvent(AnalyticsController.SPIN, AnalyticsController.Gem2, this._values[this._result]);
			});
		});
	}

	private void Update()
	{
	}

	public void Spin()
	{
		this.TextTitle.text = "SPIN & WIN!";
		this.ButtonCancel.gameObject.SetActive(false);
		this.ButtonSpine.gameObject.SetActive(false);
		if (this._sequence != null)
		{
			this._sequence.Kill(false);
		}
		int num = UnityEngine.Random.Range(0, 100);
		if (num < 2)
		{
			this._result = 3;
		}
		else if (num < 20)
		{
			this._result = 2;
		}
		else if (num < 80)
		{
			this._result = 1;
		}
		else
		{
			this._result = 0;
		}
		int id = 0;
		GameController.AudioController.PlayOneShot("Audios/Effect/slot_spin");
		this._sequence = DOTween.Sequence().AppendCallback(delegate
		{
			this.SetFocus(id % this.Focus.Length);
			id++;
			if (id == this.Focus.Length * 4 + this._result + 1)
			{
				GameController.AudioController.PlayOneShot("Audios/Effect/daily_bonus");
			}
		}).AppendInterval(0.18f).SetLoops(this.Focus.Length * 4 + this._result + 1).OnComplete(delegate
		{
			this.ButtonGetX1.gameObject.SetActive(true);
			this.ButtonGetX2.gameObject.SetActive(true);
		});
	}

	public override void Show()
	{
		base.Show();
		this.SetFocus(0);
		this.ButtonCancel.gameObject.SetActive(true);
		this.ButtonSpine.gameObject.SetActive(true);
		this.ButtonGetX1.gameObject.SetActive(false);
		this.ButtonGetX2.gameObject.SetActive(false);
		this._result = 0;
		this.TextTitle.text = "GET A REWARD!";
	}

	private void SetFocus(int index)
	{
		for (int i = 0; i < this.Focus.Length; i++)
		{
			this.Focus[i].gameObject.SetActive(false);
		}
		this.Focus[index].gameObject.SetActive(true);
	}
}
