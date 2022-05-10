using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : Popup
{
	private sealed class _StartExploEffect_c__AnonStorey0
	{
		internal Image image;

		internal float time;

		internal float scale;

		internal float x;

		internal float y;

		internal void __m__0()
		{
			this.image.transform.DORotate(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), this.time, RotateMode.Fast).SetEase(Ease.Linear);
			this.image.transform.DOScale(Vector3.one * this.scale, this.time).SetEase(Ease.Linear);
			this.image.rectTransform.DOAnchorPos(new Vector2(this.x, this.y), this.time, false).SetEase(Ease.Linear);
		}

		internal void __m__1()
		{
			UnityEngine.Object.Destroy(this.image.gameObject);
		}
	}

	public GameObject HexagonPrefab;

	public Transform EndPosEffect;

	public GameObject Tut1;

	public RectTransform Hand;

	public Transform HexaPos;

	public MiniHexaTut HexaTut1;

	public Text TextTut1;

	public GameObject Tut2;

	public RectTransform Hand2;

	public MiniHexaTut HexaTut2;

	public Image ImageHand2;

	public Text TextTap;

	public Text TextRotate;

	public Text TextMove;

	public Transform HexaPos2;

	public Image Round;

	public GameObject Tut3;

	public RectTransform Hand3;

	public Transform HexaPos3;

	public Transform HexaPos32;

	public MiniHexaTut HexaTut3;

	public Text TextTut3;

	public TextFly TextFly;

	public GameObject EndTut;

	public Button ButtonPlay;

	private Tween _imageScale;

	private void Start()
	{
		this.ButtonPlay.onClick.AddListener(delegate
		{
			if (GameController.ScreenManager.stateGame != ScreenManager.StateGame.PLAY)
			{
				GameController.ScreenManager.OpenStage(ScreenManager.StateGame.PLAY, new ScreenManager.CallBack(this.Hide));
			}
			else
			{
				this.Hide();
			}
		});
	}

	public override void Show()
	{
		this.InitHelp1();
		base.transform.SetAsLastSibling();
		base.gameObject.SetActive(true);
	}

	public override void Hide()
	{
		base.gameObject.SetActive(false);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void InitHelp1()
	{
		this.Tut1.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).SetEase(Ease.Linear);
		this.Hand.transform.DOMove(this.HexaTut1.Target.transform.position, 1.2f, false).SetEase(Ease.Linear).SetLoops(-1);
		this.HexaTut1.CallBackFinish = delegate
		{
			this.Hand.gameObject.SetActive(false);
			DOTween.Sequence().AppendInterval(0.1f).AppendCallback(delegate
			{
				this.HexaPos.gameObject.SetActive(false);
				this.HexaTut1.gameObject.SetActive(false);
				Hexagon component = UnityEngine.Object.Instantiate<GameObject>(this.HexagonPrefab).GetComponent<Hexagon>();
				component.transform.SetParent(base.transform, false);
				component.StartEffect(this.HexaPos.position, this.EndPosEffect.position, 0f, new Color(0.858823538f, 0.3019608f, 0.3019608f, 1f));
				this.StartExploEffect(new Color(0.858823538f, 0.3019608f, 0.3019608f, 1f), this.HexaPos.position);
			}).AppendInterval(1.4f).AppendCallback(delegate
			{
				this.Tut1.gameObject.SetActive(false);
				this.InitHelp2();
			});
		};
		//GameController.AnalyticsController.LogEvent(AnalyticsController.START_TUTORIAL_STEP, AnalyticsController.STEP, 1);
	}

	private void InitHelp2()
	{
		this.Tut2.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).SetEase(Ease.Linear);
		this.Tut2.gameObject.SetActive(true);
		this._imageScale = this.ImageHand2.transform.DOScale(1.3f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
		this.TextTap.DOFade(0f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
		this.Round.transform.DORotate(new Vector3(0f, 0f, -360f), 5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
		DOTween.Sequence().AppendInterval(0.75f).Append(this.Round.DOFade(0.6f, 0.5f).SetEase(Ease.Linear)).Append(this.Round.DOFade(0f, 3f).SetEase(Ease.Linear)).AppendInterval(0.75f).SetLoops(-1);
		this.HexaTut2.CallBackClick = delegate
		{
			if (this.HexaTut2.IsUniform())
			{
				this.TextRotate.gameObject.SetActive(false);
				this.TextMove.gameObject.SetActive(true);
				this.TextTap.gameObject.SetActive(false);
				this.HexaTut2.CanRotate = false;
				this.Round.gameObject.SetActive(false);
				if (this._imageScale != null)
				{
					this._imageScale.Kill(false);
				}
				this.Hand2.transform.DOMove(this.HexaTut2.Target.transform.position, 1.2f, false).SetEase(Ease.Linear).SetLoops(-1).SetDelay(0.3f);
			}
		};
		this.HexaTut2.CallBackFinish = delegate
		{
			this.Hand2.gameObject.SetActive(false);
			DOTween.Sequence().AppendInterval(0.1f).AppendCallback(delegate
			{
				this.HexaPos2.gameObject.SetActive(false);
				this.HexaTut2.gameObject.SetActive(false);
				Hexagon component = UnityEngine.Object.Instantiate<GameObject>(this.HexagonPrefab).GetComponent<Hexagon>();
				component.transform.SetParent(base.transform, false);
				component.StartEffect(this.HexaPos2.position, this.EndPosEffect.position, 0f, new Color(0.858823538f, 0.3019608f, 0.3019608f, 1f));
				this.StartExploEffect(new Color(0.858823538f, 0.3019608f, 0.3019608f, 1f), this.HexaPos.position);
			}).AppendInterval(1.4f).AppendCallback(delegate
			{
				this.Tut2.gameObject.SetActive(false);
				this.InitHelp3();
			});
		};
		//GameController.AnalyticsController.LogEvent(AnalyticsController.START_TUTORIAL_STEP, AnalyticsController.STEP, 2);
	}

	private void InitHelp3()
	{
		this.Tut3.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).SetEase(Ease.Linear);
		this.Tut3.gameObject.SetActive(true);
		this.Hand3.transform.DOMove(this.HexaTut3.Target.transform.position, 1.2f, false).SetEase(Ease.Linear).SetLoops(-1);
		this.HexaTut3.CallBackFinish = delegate
		{
			this.Hand3.gameObject.SetActive(false);
			DOTween.Sequence().AppendInterval(0.1f).AppendCallback(delegate
			{
				this.HexaPos3.gameObject.SetActive(false);
				this.HexaTut3.gameObject.SetActive(false);
				Hexagon component = UnityEngine.Object.Instantiate<GameObject>(this.HexagonPrefab).GetComponent<Hexagon>();
				component.transform.SetParent(base.transform, false);
				component.StartEffect(this.HexaPos3.position, this.EndPosEffect.position, 0f, new Color(0.858823538f, 0.3019608f, 0.3019608f, 1f));
				Hexagon component2 = UnityEngine.Object.Instantiate<GameObject>(this.HexagonPrefab).GetComponent<Hexagon>();
				component2.transform.SetParent(base.transform, false);
				component2.StartEffect(this.HexaPos32.position, this.EndPosEffect.position, 0.3f, new Color(0.858823538f, 0.3019608f, 0.3019608f, 1f));
				this.StartExploEffect(new Color(0.858823538f, 0.3019608f, 0.3019608f, 1f), this.HexaPos.position);
				this.TextFly.gameObject.SetActive(true);
				this.TextFly.transform.SetAsLastSibling();
				this.TextFly.StartEffect(this.TextFly.transform.position, "Double", new Color(0.858823538f, 0.3019608f, 0.3019608f, 1f), 0.3f);
			}).AppendInterval(1.5f).AppendCallback(delegate
			{
				this.Tut3.gameObject.SetActive(false);
				this.InitEndTut();
			});
		};
		//GameController.AnalyticsController.LogEvent(AnalyticsController.START_TUTORIAL_STEP, AnalyticsController.STEP, 3);
	}

	private void InitEndTut()
	{
		this.EndTut.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).SetEase(Ease.Linear);
		this.EndTut.gameObject.SetActive(true);
		this.StartCongratEff();
		//GameController.AnalyticsController.LogEvent(AnalyticsController.START_TUTORIAL_STEP, AnalyticsController.STEP, 4);
	}

	public void StartCongratEff()
	{
	}

	private void Update()
	{
	}

	public void StartExploEffect(Color color, Vector3 pos)
	{
		for (int i = 0; i < 20; i++)
		{
			bool flag = UnityEngine.Random.Range(0, 2) != 0;
			string resource = (!flag) ? "Images/Effect/star" : "Images/GamePlay/triangle_shadow";
			Image image = base.CreateImage(resource, base.transform);
			if (flag)
			{
				image.color = color;
			}
			image.transform.eulerAngles = new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
			image.transform.localScale = Vector3.zero;
			image.transform.position = pos;
			float time = UnityEngine.Random.Range(0.4f, 0.6f);
			float num = 300f;
			float x = UnityEngine.Random.Range(-num, num);
			float y = (float)Math.Sqrt((double)(num * num - x * x)) * (float)((UnityEngine.Random.Range(-1, 1) >= 0) ? (-1) : 1);
			x += image.rectTransform.anchoredPosition.x;
			y += image.rectTransform.anchoredPosition.y;
			float scale = (!flag) ? UnityEngine.Random.Range(0.1f, 0.3f) : UnityEngine.Random.Range(0.6f, 1f);
			DOTween.Sequence().AppendInterval(0.3f).AppendCallback(delegate
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
}
