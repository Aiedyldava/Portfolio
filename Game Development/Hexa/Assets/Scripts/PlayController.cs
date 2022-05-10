using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayController : StageController
{
	private sealed class _GameOver_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal List<MiniHexa>.Enumerator _locvar0;

		internal int _score___0;

		internal PlayController _this;

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

		public _GameOver_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._locvar0 = this._this.MiniHexas.GetEnumerator();
				try
				{
					while (this._locvar0.MoveNext())
					{
						MiniHexa current = this._locvar0.Current;
						current.Reset();
					}
				}
				finally
				{
					((IDisposable)this._locvar0).Dispose();
				}
				this._this.BackgroundButton.gameObject.SetActive(false);
				Preference.Instance.DataGame.PlayData.IsPlay = false;
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				GameController.AudioController.PlayOneShot("Audios/Effect/game_over");
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				GameController.EffectController.GameOverEffect(this._this.BigHexa.Triangles);
				this._score___0 = this._this._score;
				this._current = new WaitForSeconds(1.5f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				Preference.Instance.DataGame.HighScore = Mathf.Max(this._score___0, Preference.Instance.DataGame.HighScore);
				this._this.SetTextCoin(Preference.Instance.DataGame.Coin);
				this._this.SettextHighScore(Preference.Instance.DataGame.HighScore);
				GameController.DialogManager.GameOver.Show(this._score___0);
				if (this._this.DailyRewardAvailable)
				{
					GameController.DialogManager.PopupReward.Show();
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

	private sealed class _LoadPlayData_c__AnonStorey1
	{
		internal int i1;

		internal PlayController _this;

		internal void __m__0()
		{
			if (this.i1 == this._this.MiniHexas.Count - 1)
			{
				this._this.BigHexa.CheckBlock();
				this._this.CheckGameOver();
			}
		}
	}

	public ConfirmPopup ConfirmPopup;

	public TutorialController TutorialController;

	public bool IsTutorial;

	public GameObject[] HexaPrefabs;

	public Transform[] HexaTransforms;

	public BigHexa BigHexa;

	public Text TextScore;

	public Button ButtonWatchAd;

	public Image ImageGift;

	public GameObject MiniHexaParent;

	public GameObject ScoreImage;

	[HideInInspector]
	public List<MiniHexa> MiniHexas = new List<MiniHexa>();

	public GameObject Background;

	public Button BackgroundButton;

	public Transform TransformBack;

	public Transform TransformFront;

	public Image ImageDiamond;

	public Text TextCoin;

	public Text TextHighScore;

	public Text TextBest;

	[HideInInspector]
	private int _score;

	public Button ButtonPause;

	public Image PauseIcon;

	public Button ButtonGem;

	private Vector2 _origilPosAdsButton;

	public PerfectEffect PerfectEffect;

	private int _countFree;

	private int _countHelp;

	private int _nextScore100 = 100;

	private float _timeShowAds;

	private GameObject _background;

	public bool DailyRewardAvailable
	{
		get
		{
			return Preference.Instance.DataGame.LastDay == 0 || Preference.Instance.DataGame.LastDay != DateTime.Now.Date.DayOfYear;
		}
	}

	private void Start()
	{
		this.TextScore.text = "0";
		this.ButtonPause.onClick.AddListener(delegate
		{
			if (!this.IsTutorial)
			{
				GameController.DialogManager.PopupPause.Show();
			}
		});
		this.BackgroundButton.onClick.AddListener(new UnityAction(this.ClickBackground));
		this.ButtonGem.onClick.AddListener(delegate
		{
			if (!this.IsTutorial)
			{
				GameController.DialogManager.PopupShop.Show();
			}
		});
		this.ScoreImage.transform.DORotate(new Vector3(0f, 0f, -360f), 10f, RotateMode.FastBeyond360).SetLoops(-1);
		this.ButtonWatchAd.onClick.AddListener(delegate
		{
			this.ButtonWatchAd.gameObject.SetActive(false);
			GameController.DialogManager.PopupSpin.Show();
			this._countFree += 2;
		});
		this._origilPosAdsButton = this.ButtonWatchAd.transform.localPosition;
		this.ButtonWatchAd.gameObject.SetActive(false);
		this.ButtonWatchAd.transform.localPosition = new Vector2(this._origilPosAdsButton.x + 120f, this._origilPosAdsButton.y);
		this.BigHexa.gameObject.transform.localScale = Vector3.one * BaseController.GameController.HexaScale;
		this.MiniHexaParent.gameObject.transform.localScale = Vector3.one * BaseController.GameController.HexaScale;
		this.ImageGift.transform.eulerAngles = new Vector3(0f, 0f, 10f);
		this.ImageGift.transform.DORotate(new Vector3(0f, 0f, -10f), 0.3f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);
		DOTween.Sequence().Append(this.ImageGift.transform.DOLocalMoveY(20f, 0.2f, false)).Append(this.ImageGift.transform.DOLocalMoveY(0f, 0.2f, false)).AppendInterval(0.5f).SetLoops(-1);
		this.InitTheme();
	}

	public void BringGameObjectToTop(BaseController baseController)
	{
		baseController.CurrentParrent = baseController.gameObject.transform.parent;
		baseController.gameObject.transform.SetParent(this.TransformFront);
	}

	private void ClickBackground()
	{
		this.BackgroundButton.gameObject.SetActive(false);
	}

	public void SettextHighScore(int highScore)
	{
		Preference.Instance.DataGame.HighScore = highScore;
		this.TextHighScore.text = highScore + string.Empty;
	}

	public void SetTextCoin(int coin)
	{
		BaseController.TweenNumber(coin, this.TextCoin);
		Preference.Instance.DataGame.Coin = coin;
	}

	public void AddScore(int score)
	{
		BaseController.TweenNumber(this._score, this._score + score, this.TextScore);
		this._score += score;
		Preference.Instance.DataGame.HighScore = Math.Max(Preference.Instance.DataGame.HighScore, this._score);
		this.SettextHighScore(Preference.Instance.DataGame.HighScore);
	}

	public void CheckAddGem(Vector2 position)
	{
		if (this._score >= this._nextScore100)
		{
			int nextScore = this._nextScore100;
			this._nextScore100 = this._score / 100 * 100 + 100;
			GameController.EffectController.AddGemEffect(position, (this._nextScore100 - nextScore) / 100, this.ImageDiamond, -1f);
		}
	}

	public MiniHexa CreateHexaTemp(int id, bool randomColor = true)
	{
		MiniHexa component = UnityEngine.Object.Instantiate<GameObject>(this.HexaPrefabs[id]).GetComponent<MiniHexa>();
		component.Reset();
		if (randomColor)
		{
			component.RandomColor();
		}
		component.PlayController = this;
		component.transform.SetParent(this.MiniHexaParent.transform, false);
		component.Id = id;
		this.MiniHexas.Add(component);
		return component;
	}

	public void RemoveMiniHexa(MiniHexa miniHexa)
	{
		UnityEngine.Object.Destroy(miniHexa.gameObject);
		this.MiniHexas.Remove(miniHexa);
	}

	public MiniHexa CreateHexaTemp()
	{
		int id = UnityEngine.Random.Range(0, this.HexaPrefabs.Length);
		return this.CreateHexaTemp(id, true);
	}

	public void ReplaceMiniHexa(MiniHexa miniHexa)
	{
		miniHexa.Group.SetActive(false);
		this.CheckMiniHexa();
	}

	public int RandomDiff(int diff)
	{
		return UnityEngine.Random.Range(2, 5);
	}

	public void CheckMiniHexa()
	{
		if (this.IsTutorial && this.TutorialController._step != 3 && this.TutorialController._step != 4)
		{
			return;
		}
		for (int i = 0; i < this.MiniHexas.Count; i++)
		{
			if (this.MiniHexas[i].Group.activeSelf)
			{
				this.CheckGameOver();
				return;
			}
		}
		for (int j = 0; j < this.MiniHexas.Count; j++)
		{
			GameObject gameObject = this.MiniHexas[j].gameObject;
			UnityEngine.Object.Destroy(gameObject.gameObject);
		}
		GameController.AudioController.PlayOneShot("Audios/Effect/beep2");
		this.MiniHexas.Clear();
		this.CreateHexaTemp();
		this.CreateHexaTemp();
		this.CreateHexaTemp();
		if (!this.IsTutorial && UnityEngine.Random.Range(0, 7) == 0 && GameController.AdsController.IsReady() && !this.ButtonWatchAd.gameObject.activeSelf && this._countFree <= 2)
		{
			this._countFree++;
			this.ButtonWatchAd.gameObject.SetActive(true);
			this.ButtonWatchAd.gameObject.transform.DOLocalMoveX(this._origilPosAdsButton.x, 0.2f, false).OnComplete(delegate
			{
				DOTween.Sequence().AppendInterval(6f).Append(this.ButtonWatchAd.gameObject.transform.DOLocalMoveX(this._origilPosAdsButton.x + 130f, 0.2f, false)).OnComplete(delegate
				{
					this.ButtonWatchAd.gameObject.SetActive(false);
				});
			}).SetDelay(1f);
		}
		if (this.IsTutorial)
		{
			this.TutorialController.FinishTut();
		}
		this.MiniHexaParent.transform.localPosition = new Vector2(this.MiniHexaParent.GetComponent<RectTransform>().rect.width, this.MiniHexaParent.transform.localPosition.y);
		this.MiniHexaParent.transform.DOLocalMoveX(0f, 0.2f, false).OnComplete(new TweenCallback(this.CheckGameOver));
	}

	public void RePlay()
	{
		this._nextScore100 = 100;
		GameController.AudioController.PlayOneShot("Audios/Effect/daily_bonus");
		this.Reset();
		this.CreateHexaTemp();
		this.CreateHexaTemp();
		this.CreateHexaTemp();
		for (int i = 0; i < this.MiniHexas.Count; i++)
		{
			this.MiniHexas[i].Group.transform.localScale = Vector3.zero;
			this.MiniHexas[i].Group.transform.DOScale(Vector3.one * MiniHexa.Scale, 0.2f + (float)i * 0.15f);
		}
		this.BigHexa.RePlayEff();
		Preference.Instance.DataGame.PlayData.IsPlay = true;
		GameController.ScreenManager.PlayController.IsTutorial = false;
		Preference.Instance.DataGame.FirstOpen = false;
		Preference.Instance.DataGame.DestroyPrice = 10;
		//GameController.AnalyticsController.LogEvent(AnalyticsController.START_GAME, AnalyticsController.Gem, Preference.Instance.DataGame.Coin);
	}

	public void Reset()
	{
		this._countFree = 0;
		this._countHelp = 0;
		this.SetTextCoin(Preference.Instance.DataGame.Coin);
		this.SettextHighScore(Preference.Instance.DataGame.HighScore);
		this.ResetHexa();
		this._score = 0;
		this.TextScore.text = string.Empty + this._score;
		Preference.Instance.DataGame.PlayData.IsPlay = false;
	}

	public void ResetHexa()
	{
		this.BigHexa.Reset();
		foreach (MiniHexa current in this.MiniHexas)
		{
			UnityEngine.Object.Destroy(current.gameObject);
		}
		this.MiniHexas.Clear();
	}

	public void CheckGameOver()
	{
		if (!this.CheckAlive())
		{
			GameController.DialogManager.Toast.Show("Out of move!");
			base.StartCoroutine(this.GameOver());
		}
	}

	public IEnumerator GameOver()
	{
		PlayController._GameOver_c__Iterator0 _GameOver_c__Iterator = new PlayController._GameOver_c__Iterator0();
		_GameOver_c__Iterator._this = this;
		return _GameOver_c__Iterator;
	}

	public bool CheckAlive()
	{
		bool result = false;
		foreach (MiniHexa current in this.MiniHexas)
		{
			if (current.Group.activeSelf)
			{
				bool flag = false;
				for (int i = 0; i < this.BigHexa.Triangles.Length; i++)
				{
					if (!this.BigHexa.Triangles[i].IsSet)
					{
						current.Group.transform.localScale = Vector3.one;
						Vector3 b = current.Triangles[0].transform.position - current.Group.transform.position;
						current.Group.transform.position = this.BigHexa.Triangles[i].transform.position - b;
						if (current.GetTriangleAvaiable() != null)
						{
							current.ResetPos();
							result = true;
							flag = true;
							break;
						}
					}
				}
				current.ResetPos();
				if (!flag)
				{
					current.SetDisable(true);
				}
				else
				{
					current.SetDisable(false);
				}
			}
		}
		return result;
	}

	public override void OnStageOpen()
	{
		base.OnStageOpen();
		if (Preference.Instance.DataGame.FirstOpen)
		{
			this.IsTutorial = true;
		}
		if (this.IsTutorial)
		{
			this.TutorialController.PlayTutorial();
		}
		else
		{
			this.LoadPlayData();
		}
		if (!Preference.Instance.DataGame.NoAds && !GameController.AdsController.IsLoadBanner && !this.IsTutorial)
		{
			GameController.AdsController.RequestBanner();
		}
		else if (!Preference.Instance.DataGame.NoAds && GameController.DialogManager.GetNumberActiveDialog() == 0)
		{
			GameController.AdsController.SetBannerShow(true);
		}
	}

	public override void OnStageClose()
	{
		this.SavePlayData();
		base.OnStageClose();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		this.SavePlayData();
		if (!Preference.Instance.DataGame.NoAds && !this.IsTutorial && GameController.DialogManager.GetNumberActiveDialog() == 0)
		{
			GameController.AdsController.SetBannerShow(!pauseStatus);
		}
		if (!pauseStatus && !GameController.AdsController.IsShowInter)
		{
			GameController.AdsController.ShowVideoAd();
		}
		else if (!pauseStatus)
		{
			GameController.AdsController.IsShowInter = false;
		}
	}

	private void OnApplicationQuit()
	{
		this.SavePlayData();
	}

	public void SavePlayData()
	{
		Preference.Instance.DataGame.PlayData.Reset();
		if (Preference.Instance.DataGame.PlayData.IsPlay)
		{
			Preference.Instance.DataGame.PlayData.CurrentScore = this._score;
			Triangle[] triangles = this.BigHexa.Triangles;
			for (int i = 0; i < triangles.Length; i++)
			{
				Triangle triangle = triangles[i];
				TriangleInfo triangleInfo = new TriangleInfo();
				triangleInfo.ColorId = triangle.ColorId;
				triangleInfo.IsDiamond = triangle.IsDiamond;
				Preference.Instance.DataGame.PlayData.ListTriangle.Add(triangleInfo);
			}
			foreach (MiniHexa current in this.MiniHexas)
			{
				MiniHexaInfo miniHexaInfo = new MiniHexaInfo();
				miniHexaInfo.ColorId = current.ColorID;
				miniHexaInfo.Id = current.Id;
				miniHexaInfo.Visible = current.Group.activeSelf;
				miniHexaInfo.Degree = current._hexaDegree;
				Preference.Instance.DataGame.PlayData.ListMiniHexa.Add(miniHexaInfo);
			}
			Preference.Instance.SaveData();
		}
	}

	public void LoadPlayData()
	{
		if (Preference.Instance.DataGame.PlayData.NeedLoad())
		{
			this._score = 0;
			this._score += Preference.Instance.DataGame.PlayData.CurrentScore;
			this.TextScore.text = this._score + string.Empty;
			this.TextScore.text = this._score + string.Empty;
			this.SetTextCoin(Preference.Instance.DataGame.Coin);
			this.SettextHighScore(Preference.Instance.DataGame.HighScore);
			this._nextScore100 = this._score / 100 * 100 + 100;
			for (int i = 0; i < Preference.Instance.DataGame.PlayData.ListTriangle.Count; i++)
			{
				this.BigHexa.Triangles[i].SetColor(Preference.Instance.DataGame.PlayData.ListTriangle[i].ColorId);
				this.BigHexa.Triangles[i].SetDiamond(Preference.Instance.DataGame.PlayData.ListTriangle[i].IsDiamond);
			}
			for (int j = 0; j < Preference.Instance.DataGame.PlayData.ListMiniHexa.Count; j++)
			{
				MiniHexa miniHexa = this.CreateHexaTemp(Preference.Instance.DataGame.PlayData.ListMiniHexa[j].Id, false);
				miniHexa.SetColor(Preference.Instance.DataGame.PlayData.ListMiniHexa[j].ColorId);
				miniHexa.Group.SetActive(Preference.Instance.DataGame.PlayData.ListMiniHexa[j].Visible);
				miniHexa._hexaDegree = Preference.Instance.DataGame.PlayData.ListMiniHexa[j].Degree;
				miniHexa.Group.transform.eulerAngles = new Vector3(0f, 0f, (float)miniHexa._hexaDegree);
			}
			for (int k = 0; k < this.MiniHexas.Count; k++)
			{
				this.MiniHexas[k].Group.transform.localScale = Vector3.zero;
				int i1 = k;
				this.MiniHexas[k].Group.transform.DOScale(Vector3.one * MiniHexa.Scale, 0.2f + (float)k * 0.15f).OnComplete(delegate
				{
					if (i1 == this.MiniHexas.Count - 1)
					{
						this.BigHexa.CheckBlock();
						this.CheckGameOver();
					}
				});
			}
		}
		else
		{
			this.RePlay();
		}
	}

	public void InitTheme()
	{
		for (int i = 0; i < this.BigHexa.Triangles.Length; i++)
		{
			this.BigHexa.Triangles[i].SetColor(this.BigHexa.Triangles[i].ColorId);
		}
		for (int j = 0; j < this.MiniHexas.Count; j++)
		{
			this.MiniHexas[j].SetColor2(this.MiniHexas[j].ColorID);
		}
		if (this._background != null)
		{
			UnityEngine.Object.Destroy(this._background);
		}
		this._background = BaseController.InstantiatePrefab(GameController.ThemeManager.CurrentTheme.BackgroundPrefab);
		this._background.transform.SetParent(this.Background.transform, false);
		this.TextBest.color = GameController.ThemeManager.CurrentTheme.TextBestColor;
		this.TextHighScore.color = GameController.ThemeManager.CurrentTheme.TextBestScoreColor;
		this.TextScore.color = GameController.ThemeManager.CurrentTheme.TextScoreColor;
		this.PauseIcon.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
		this.ButtonGem.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.ButtonPause.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.TextCoin.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
	}
}
