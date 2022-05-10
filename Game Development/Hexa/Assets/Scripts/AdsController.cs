using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{
	public delegate void AdCallBack();

	private sealed class _StartReward_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal AdsController _this;

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

		public _StartReward_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (this._this.adCallBack != null)
				{
					this._this.adCallBack();
				}
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

	private BannerView bannerView;

	private BannerView bannerView2;

	public string placementId = "rewardedVideo";

	public string placementIdVideoNormal = "video";

	public bool testNoAdAvailable;

	private AdsController.AdCallBack adCallBack;

	private string gameId = "3736628";

	public bool IsShowBanner;

	public bool IsLoadBanner;

	private InterstitialAd interstitial;

	private RewardBasedVideoAd rewardBasedVideo;

	public bool IsShowInter;

	public bool IsShow
	{
		get
		{
			return Advertisement.isShowing;
		}
	}

	private void Start()
	{
		this.testNoAdAvailable = false;
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(this.gameId, false);
		}
		string appId = "ca-app-pub-3940256099942544~3347511713";
		MobileAds.Initialize(appId);
		this.RequestInterstitial();
		MobileAds.SetiOSAppPauseOnBackground(true);
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;
		this.rewardBasedVideo.OnAdLoaded += new EventHandler<EventArgs>(this.HandleRewardBasedVideoLoaded);
		this.rewardBasedVideo.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleRewardBasedVideoFailedToLoad);
		this.rewardBasedVideo.OnAdOpening += new EventHandler<EventArgs>(this.HandleRewardBasedVideoOpened);
		this.rewardBasedVideo.OnAdStarted += new EventHandler<EventArgs>(this.HandleRewardBasedVideoStarted);
		this.rewardBasedVideo.OnAdRewarded += new EventHandler<Reward>(this.HandleRewardBasedVideoRewarded);
		this.rewardBasedVideo.OnAdClosed += new EventHandler<EventArgs>(this.HandleRewardBasedVideoClosed);
		this.rewardBasedVideo.OnAdLeavingApplication += new EventHandler<EventArgs>(this.HandleRewardBasedVideoLeftApplication);
		this.RequestRewardAdmob();
		if (!Preference.Instance.DataGame.NoAds)
		{
			this.RequestBanner2();
		}
	}

	private void Update()
	{
	}

	public void ShowAd(AdsController.AdCallBack callback)
	{
		this.adCallBack = callback;
		if (UnityEngine.Random.Range(0, 2) == 1)
		{
			if (Advertisement.IsReady(this.placementId))
			{
				ShowOptions showOptions = new ShowOptions();
				showOptions.resultCallback = new Action<ShowResult>(this.HandleShowResult);
				this.IsShowInter = true;
				Advertisement.Show(this.placementId, showOptions);
				//GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Reward_Unity");
			}
			else if (this.rewardBasedVideo.IsLoaded())
			{
				this.IsShowInter = true;
				this.rewardBasedVideo.Show();
				//GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Reward_Admob");
			}
		}
		else if (this.rewardBasedVideo.IsLoaded())
		{
			this.IsShowInter = true;
			this.rewardBasedVideo.Show();
			//GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Reward_Admob");
		}
		else if (Advertisement.IsReady(this.placementId))
		{
			this.RequestRewardAdmob();
			ShowOptions showOptions2 = new ShowOptions();
			showOptions2.resultCallback = new Action<ShowResult>(this.HandleShowResult);
			this.IsShowInter = true;
			Advertisement.Show(this.placementId, showOptions2);
			//GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Reward_Unity");
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		if (result == ShowResult.Finished)
		{
			UnityEngine.Debug.Log("Video completed - Offer a reward to the player");
			base.StartCoroutine(this.StartReward());
		}
		else if (result == ShowResult.Skipped)
		{
			UnityEngine.Debug.LogWarning("Video was skipped - Do NOT reward the player");
		}
		else if (result == ShowResult.Failed)
		{
			UnityEngine.Debug.LogError("Video failed to show");
		}
	}

	public bool IsReady()
	{
		return !this.testNoAdAvailable && (Advertisement.IsReady(this.placementId) || (this.rewardBasedVideo != null && this.rewardBasedVideo.IsLoaded()));
	}

	public void RequestBanner()
	{
		this.DestroyBanner();
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		AdRequest request = new AdRequest.Builder().Build();
		this.bannerView.OnAdLoaded += new EventHandler<EventArgs>(this.HandleOnAdLoaded);
		this.bannerView.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleOnAdFailedToLoad);
		this.bannerView.OnAdOpening += new EventHandler<EventArgs>(this.HandleOnAdOpened);
		this.bannerView.OnAdClosed += new EventHandler<EventArgs>(this.HandleOnAdClosed);
		this.bannerView.OnAdLeavingApplication += new EventHandler<EventArgs>(this.HandleOnAdLeavingApplication);
		this.bannerView.LoadAd(request);
	}

	public void RequestBanner2()
	{
		if (this.bannerView2 != null)
		{
			this.bannerView2.Destroy();
		}
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		this.bannerView2 = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
		AdRequest request = new AdRequest.Builder().Build();
		this.bannerView2.LoadAd(request);
		this.bannerView2.OnAdLoaded += delegate
		{
			this.bannerView2.Hide();
		};
	}

	public void SetBanner2Show(bool isShow)
	{
		if (this.bannerView2 != null)
		{
			if (isShow && !Preference.Instance.DataGame.NoAds)
			{
				this.bannerView2.Show();
			}
			else
			{
				this.bannerView2.Hide();
			}
		}
	}

	public void DestroyBanner()
	{
		if (this.bannerView != null)
		{
			this.bannerView.Destroy();
		}
		this.IsShowBanner = false;
		this.IsLoadBanner = false;
	}

	public void SetBannerShow(bool isShow)
	{
		this.IsShowBanner = isShow;
		if (this.bannerView != null)
		{
			if (isShow)
			{
				this.bannerView.Show();
			}
			else
			{
				this.bannerView.Hide();
			}
		}
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		if (GameController.DialogManager.GetNumberActiveDialog() == 0)
		{
			this.bannerView.Show();
			this.IsShowBanner = true;
		}
		else
		{
			this.bannerView.Hide();
			this.IsShowBanner = true;
		}
		this.IsLoadBanner = true;
		UnityEngine.Debug.Log("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		UnityEngine.Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		UnityEngine.Debug.Log("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		UnityEngine.Debug.Log("HandleAdClosed event received");
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		UnityEngine.Debug.Log("HandleAdLeavingApplication event received");
	}

	private void RequestInterstitial()
	{
		string adUnitId = "ca-app-pub-3940256099942544/1033173712";
		this.interstitial = new InterstitialAd(adUnitId);
		this.interstitial.OnAdLoaded += new EventHandler<EventArgs>(this.HandleOnAdLoadedFull);
		this.interstitial.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleOnAdFailedToLoadFull);
		this.interstitial.OnAdOpening += new EventHandler<EventArgs>(this.HandleOnAdOpenedFull);
		this.interstitial.OnAdClosed += new EventHandler<EventArgs>(this.HandleOnAdClosedFull);
		this.interstitial.OnAdLeavingApplication += new EventHandler<EventArgs>(this.HandleOnAdLeavingApplicationFull);
		AdRequest request = new AdRequest.Builder().Build();
		this.interstitial.LoadAd(request);
	}

	public void HandleOnAdLoadedFull(object sender, EventArgs args)
	{
		UnityEngine.Debug.Log("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoadFull(object sender, AdFailedToLoadEventArgs args)
	{
		UnityEngine.Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleOnAdOpenedFull(object sender, EventArgs args)
	{
		UnityEngine.Debug.Log("HandleAdOpened event received");
	}

	public void HandleOnAdClosedFull(object sender, EventArgs args)
	{
		UnityEngine.Debug.Log("HandleAdClosed event received");
	}

	public void HandleOnAdLeavingApplicationFull(object sender, EventArgs args)
	{
		UnityEngine.Debug.Log("HandleAdLeavingApplication event received");
	}

	public void RequestRewardAdmob()
	{
		string adUnitId = "ca-app-pub-3940256099942544/5224354917";
		this.rewardBasedVideo.LoadAd(new AdRequest.Builder().Build(), adUnitId);
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		this.RequestRewardAdmob();
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		base.StartCoroutine(this.StartReward());
	}

	private IEnumerator StartReward()
	{
		AdsController._StartReward_c__Iterator0 _StartReward_c__Iterator = new AdsController._StartReward_c__Iterator0();
		_StartReward_c__Iterator._this = this;
		return _StartReward_c__Iterator;
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}

	public bool ShowFullAdmob()
	{
		if (this.interstitial.IsLoaded())
		{
			this.IsShowInter = true;
			this.interstitial.Show();
			this.RequestInterstitial();
			return true;
		}
		return false;
	}

	public bool ShowFullUnity()
	{
		if (Advertisement.IsReady())
		{
			this.IsShowInter = true;
			Advertisement.Show(this.placementIdVideoNormal);
			return true;
		}
		return false;
	}

	public void ShowVideoAd()
	{
		if (!Preference.Instance.DataGame.NoAds)
		{
			if (this.ShowFullAdmob())
			{
				//GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Interstitial_Admob");
			}
			else
			{
				this.ShowFullUnity();
				//GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Interstitial_Unity");
			}
		}
	}
}
