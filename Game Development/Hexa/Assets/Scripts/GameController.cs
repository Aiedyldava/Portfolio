using System;
using UnityEngine;

public class GameController : BaseController
{
	[HideInInspector]
	public static float SCREEN_GRAPHIC_WIDTH = 720f;

	[HideInInspector]
	public static float SCREEN_GRAPHIC_HEIGHT = 1280f;

	[HideInInspector]
	public static DialogManager DialogManager;

	[HideInInspector]
	public static ScreenManager ScreenManager;

	[HideInInspector]
	public static ThemeManager ThemeManager;

	[HideInInspector]
	public static AudioController AudioController;

	[HideInInspector]
	public static AdsController AdsController;

	[HideInInspector]
    public static PurchaseController PurchaseController;

	public static EffectController EffectController;

	public float CanvasScale;

	public float HexaScale = 1f;

	public static GameController Instance
	{
		get
		{
			return BaseController.GameController;
		}
	}

	private void Awake()
	{
        Application.targetFrameRate = 60;
		this.CanvasScale = GameController.SCREEN_GRAPHIC_HEIGHT / (float)Screen.height;
		if ((float)Screen.height / (float)Screen.width <= 1.83333337f)
		{
			this.HexaScale = 0.95f;
		}
		else
		{
			this.HexaScale = 0.85f;
		}
		GameController.DialogManager = base.GetComponentInChildren<DialogManager>(true);
		GameController.ScreenManager = base.GetComponentInChildren<ScreenManager>(true);
		GameController.AudioController = base.GetComponentInChildren<AudioController>(true);
		GameController.AdsController = base.GetComponentInChildren<AdsController>(true);
		GameController.PurchaseController = base.GetComponentInChildren<PurchaseController>(true);
		//GameController.AnalyticsController = base.GetComponentInChildren<AnalyticsController>(true);
		GameController.EffectController = base.GetComponentInChildren<EffectController>(true);
		GameController.ThemeManager = base.GetComponent<ThemeManager>();
	}

	public void OnApplicationQuit()
	{
        Preference.Instance.SaveData();
	}

	public void OnApplicationPause(bool pause)
	{
		Preference.Instance.SaveData();
	}

	private void Start()
	{
		Screen.sleepTimeout = -1;
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	public string LinkGame()
	{
		return "https://apps.apple.com/";
	}

	public string LinkStore()
	{
		return "https://apps.apple.com/";
	}
}
