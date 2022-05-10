using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupReward : Popup
{
	public Button ButtonClaimX1;

	public Button ButtonClaimX2;

	private RewardItem[] _rewardItems;

	public Text CurrentDay;

	public Text TextValue;

	private int _currentDay;

	private void Start()
	{
		this.ButtonClaimX1.onClick.AddListener(delegate
		{
			this.Hide();
		});
		this.ButtonClaimX2.onClick.AddListener(delegate
		{
			GameController.AdsController.ShowAd(delegate
			{
				Preference.Instance.DataGame.Coin += this._rewardItems[this._currentDay].Value;
				if (GameController.ScreenManager.stateGame == ScreenManager.StateGame.PLAY)
				{
					GameController.ScreenManager.PlayController.SetTextCoin(Preference.Instance.DataGame.Coin);
				}
			});
			this.Hide();
		});
		if (this._rewardItems == null)
		{
			this._rewardItems = base.GetComponentsInChildren<RewardItem>();
		}
	}

	private void Update()
	{
	}

	public new void Show()
	{
		if (Preference.Instance.DataGame.LastDay != 0)
		{
			for (int i = Preference.Instance.DataGame.LastDay; i < DateTime.Now.DayOfYear - 1; i++)
			{
				Preference.Instance.DataGame.DailyRewardStatus.Add(false);
			}
		}
		Preference.Instance.DataGame.DailyRewardStatus.Add(true);
		Preference.Instance.DataGame.LastDay = DateTime.Now.DayOfYear;
		base.Show();
		this._currentDay = Preference.Instance.DataGame.DailyRewardStatus.Count - 1;
		if (this._rewardItems == null)
		{
			this._rewardItems = base.GetComponentsInChildren<RewardItem>();
		}
		//GameController.AnalyticsController.LogEvent(AnalyticsController.DAILY_REWARD, AnalyticsController.DAY, this._currentDay);
		for (int j = 0; j < this._rewardItems.Length; j++)
		{
			this._rewardItems[j].TextDay.text = string.Empty + (j + 1);
			this._rewardItems[j].TextDay.color = Color.cyan;
			if (j < 6)
			{
				this._rewardItems[j].SetValue(20);
			}
			else if (j == 6)
			{
				this._rewardItems[j].SetValue(100);
				this._rewardItems[j].Focus.gameObject.SetActive(true);
			}
			else if (j < 13)
			{
				this._rewardItems[j].SetValue(30);
			}
			else if (j == 13)
			{
				this._rewardItems[j].SetValue(150);
				this._rewardItems[j].Focus.gameObject.SetActive(true);
			}
			else if (j < 20)
			{
				this._rewardItems[j].SetValue(40);
			}
			else if (j == 20)
			{
				this._rewardItems[j].SetValue(200);
				this._rewardItems[j].Focus.gameObject.SetActive(true);
			}
			else if (j < 27)
			{
				this._rewardItems[j].SetValue(50);
			}
			else if (j == 27)
			{
				this._rewardItems[j].SetValue(250);
				this._rewardItems[j].Focus.gameObject.SetActive(true);
			}
			this._rewardItems[j].Check.SetActive(false);
			this._rewardItems[j].Uncheck.SetActive(false);
		}
		for (int k = 0; k < this._currentDay; k++)
		{
			this._rewardItems[k].Check.SetActive(Preference.Instance.DataGame.DailyRewardStatus[k]);
			this._rewardItems[k].Uncheck.SetActive(!Preference.Instance.DataGame.DailyRewardStatus[k]);
		}
		this.CurrentDay.text = "Day " + (this._currentDay + 1);
		this.TextValue.text = this._rewardItems[this._currentDay].Value + string.Empty;
		Preference.Instance.DataGame.Coin += this._rewardItems[this._currentDay].Value;
		if (GameController.ScreenManager.stateGame == ScreenManager.StateGame.PLAY)
		{
			GameController.ScreenManager.PlayController.SetTextCoin(Preference.Instance.DataGame.Coin);
		}
	}

	public override void OnShowComplete()
	{
		base.OnShowComplete();
		if (Preference.Instance.DataGame.DailyRewardStatus.Count >= 28)
		{
			Preference.Instance.DataGame.LastDay = 0;
			Preference.Instance.DataGame.DailyRewardStatus.Clear();
		}
	}
}
