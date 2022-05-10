using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : BaseController
{
	private sealed class _StartHand_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal MiniHexa miniHexa;

		internal Triangle triangle;

		internal TutorialController _this;

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

		public _StartHand_c__Iterator0()
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
				this._this.Hand.gameObject.SetActive(true);
				this._this.Hand.transform.position = this.miniHexa.transform.position;
				this._this._tweenHand = DOTween.Sequence().Append(this._this.Hand.transform.DOMove(this.triangle.transform.position, 1f, false)).Append(this._this.Hand.transform.DOMove(this.miniHexa.transform.position, 0.3f, false)).SetLoops(-1);
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

	public int _step;

	public Image Hand;

	private Sequence _tweenHand;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void PlayTutorial()
	{
		if (this._step == 0)
		{
			this.InitTut0();
		}
		else if (this._step == 1)
		{
			this.InitTut1();
		}
		else if (this._step == 2)
		{
			this.InitTut2();
		}
		else if (this._step == 3)
		{
			this.FinishTut();
		}
		else if (this._step != 4)
		{
			if (this._step == 5)
			{
			}
		}
		this._step++;
	}

	private void InitTut0()
	{
		this.Hand = base.CreateImage("Images/Guide/hand", base.transform);
		this.Hand.raycastTarget = false;
		this.Hand.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
		this.Hand.gameObject.SetActive(false);
		GameController.ScreenManager.PlayController.Reset();
		int[] array = new int[]
		{
			40,
			55,
			39,
			56,
			41,
			54,
			57,
			53,
			42,
			38,
			58,
			52,
			43,
			37,
			36,
			60,
			50,
			45,
			34,
			61,
			49,
			46,
			33,
			47,
			48,
			62
		};
		MiniHexaInfo miniHexaInfo = new MiniHexaInfo();
		miniHexaInfo.ColorId = 4;
		miniHexaInfo.Id = 11;
		miniHexaInfo.Degree = 60;
		Triangle[] triangles = GameController.ScreenManager.PlayController.BigHexa.Triangles;
		for (int i = 0; i < triangles.Length; i++)
		{
			Triangle triangle = triangles[i];
			triangle.SetDisable(true);
		}
		GameController.ScreenManager.PlayController.BigHexa.Triangles[35].SetDisable(false);
		GameController.ScreenManager.PlayController.BigHexa.Triangles[44].SetDisable(false);
		GameController.ScreenManager.PlayController.BigHexa.Triangles[51].SetDisable(false);
		GameController.ScreenManager.PlayController.BigHexa.Triangles[59].SetDisable(false);
		for (int j = 0; j < array.Length; j++)
		{
			GameController.ScreenManager.PlayController.BigHexa.Triangles[array[j]].SetColor(6);
		}
		MiniHexa miniHexa = GameController.ScreenManager.PlayController.CreateHexaTemp(miniHexaInfo.Id, false);
		miniHexa.SetColor(miniHexaInfo.ColorId);
		miniHexa._hexaDegree = miniHexaInfo.Degree;
		miniHexa.Group.transform.eulerAngles = new Vector3(0f, 0f, (float)miniHexa._hexaDegree);
		base.StartCoroutine(this.StartHand(miniHexa, GameController.ScreenManager.PlayController.BigHexa.Triangles[51]));
	}

	private void InitTut1()
	{
		GameController.ScreenManager.PlayController.ResetHexa();
		int[] array = new int[]
		{
			55,
			40,
			39,
			13,
			4,
			3
		};
		Triangle[] triangles = GameController.ScreenManager.PlayController.BigHexa.Triangles;
		for (int i = 0; i < triangles.Length; i++)
		{
			Triangle triangle = triangles[i];
			triangle.SetDisable(true);
		}
		GameController.ScreenManager.PlayController.BigHexa.Triangles[14].SetDisable(false);
		GameController.ScreenManager.PlayController.BigHexa.Triangles[25].SetDisable(false);
		GameController.ScreenManager.PlayController.BigHexa.Triangles[26].SetDisable(false);
		MiniHexaInfo miniHexaInfo = new MiniHexaInfo();
		miniHexaInfo.ColorId = 5;
		miniHexaInfo.Id = 2;
		miniHexaInfo.Degree = 240;
		for (int j = 0; j < array.Length; j++)
		{
			GameController.ScreenManager.PlayController.BigHexa.Triangles[array[j]].SetColor(7);
		}
		MiniHexa miniHexa = GameController.ScreenManager.PlayController.CreateHexaTemp(miniHexaInfo.Id, false);
		miniHexa.SetColor(miniHexaInfo.ColorId);
		miniHexa._hexaDegree = miniHexaInfo.Degree;
		miniHexa.Group.transform.eulerAngles = new Vector3(0f, 0f, (float)miniHexa._hexaDegree);
		if (this._tweenHand != null)
		{
			this._tweenHand.Kill(false);
		}
		base.StartCoroutine(this.StartHand(miniHexa, GameController.ScreenManager.PlayController.BigHexa.Triangles[25]));
	}

	private IEnumerator StartHand(MiniHexa miniHexa, Triangle triangle)
	{
		TutorialController._StartHand_c__Iterator0 _StartHand_c__Iterator = new TutorialController._StartHand_c__Iterator0();
		_StartHand_c__Iterator.miniHexa = miniHexa;
		_StartHand_c__Iterator.triangle = triangle;
		_StartHand_c__Iterator._this = this;
		return _StartHand_c__Iterator;
	}

	private void InitTut2()
	{
		GameController.ScreenManager.PlayController.ResetHexa();
		int[] array = new int[]
		{
			40,
			39,
			41,
			38,
			42,
			37,
			35,
			45,
			34,
			46,
			33,
			47,
			91,
			82,
			80,
			71,
			67,
			58,
			52,
			22,
			17,
			10,
			7,
			0
		};
		MiniHexaInfo miniHexaInfo = new MiniHexaInfo();
		miniHexaInfo.ColorId = 5;
		miniHexaInfo.Id = 0;
		miniHexaInfo.Degree = 0;
		for (int i = 0; i < array.Length; i++)
		{
			GameController.ScreenManager.PlayController.BigHexa.Triangles[array[i]].SetColor(7);
		}
		MiniHexa miniHexa = GameController.ScreenManager.PlayController.CreateHexaTemp(miniHexaInfo.Id, false);
		miniHexa.SetColor(miniHexaInfo.ColorId);
		miniHexa._hexaDegree = miniHexaInfo.Degree;
		miniHexa.Group.transform.eulerAngles = new Vector3(0f, 0f, (float)miniHexa._hexaDegree);
		if (this._tweenHand != null)
		{
			this._tweenHand.Kill(false);
		}
		this.Hand.gameObject.SetActive(false);
		this.FinishTut();
	}

	private void InitTut3()
	{
		if (!GameController.ScreenManager.PlayController.MiniHexas[0].Group.activeSelf)
		{
			this.FinishTut();
			return;
		}
		this.Hand.gameObject.SetActive(false);
		DOTween.Sequence().AppendInterval(0.5f).AppendCallback(delegate
		{
			this.Hand.gameObject.SetActive(true);
			this.Hand.transform.position = GameController.ScreenManager.PlayController.MiniHexas[0].transform.position;
		});
	}

	private void InitTut4()
	{
	}

	public void FinishTut()
	{
		//GameController.AnalyticsController.LogEvent(AnalyticsController.FINISH_TUTORIAL);
		Preference.Instance.DataGame.FirstOpen = false;
		GameController.ScreenManager.PlayController.IsTutorial = false;
		Preference.Instance.DataGame.PlayData.IsPlay = true;
		UnityEngine.Object.Destroy(this.Hand.gameObject);
		if (!Preference.Instance.DataGame.NoAds && !GameController.AdsController.IsLoadBanner)
		{
			GameController.AdsController.RequestBanner();
		}
	}
}
