using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniHexa : BaseController
{
	private sealed class _EndDrag_c__AnonStorey0
	{
		internal int i1;

		internal MiniHexa _this;

		internal void __m__0()
		{
			this._this.trianglesTemp[this.i1].SetColor(this._this.ColorID);
			if (this.i1 == this._this.Triangles.Length - 1)
			{
				if (!this._this.PlayController.IsTutorial)
				{
					this._this.PlayController.AddScore(this._this.trianglesTemp.Count);
					this._this.PlayController.CheckAddGem(this._this.Group.transform.position);
				}
				this._this.PlayController.BigHexa.CheckBlock();
				this._this.trianglesTemp = null;
				this._this.Group.transform.localScale = Vector3.zero;
				this._this.Group.gameObject.SetActive(false);
				this._this.PlayController.CheckMiniHexa();
			}
		}
	}

	public Image[] Triangles;

	[HideInInspector]
	public int ColorID = -1;

	public int _hexaDegree;

	public GameObject Group;

	public PlayController PlayController;

	public int Id;

	[HideInInspector]
	public static float Scale = 0.75f;

	private Vector2 _destination;

	private float _speed = 40f;

	private bool isDrag;

	private Tween tweenScale;

	private List<Triangle> trianglesTemp;

	public bool _disable;

	public void RandomColor()
	{
		this.SetColor(UnityEngine.Random.Range(0, GameController.ThemeManager.CurrentTheme.GameColors.Length));
	}

	public void SetColor(int colorID)
	{
		this.ColorID = colorID;
		for (int i = 0; i < this.Triangles.Length; i++)
		{
			this.Triangles[i].color = GameController.ThemeManager.CurrentTheme.GameColors[this.ColorID];
		}
		this._hexaDegree = 60 * UnityEngine.Random.Range(0, 6);
		this.Group.transform.eulerAngles = new Vector3(0f, 0f, (float)this._hexaDegree);
	}

	public void SetColor2(int colorID)
	{
		this.ColorID = colorID;
		for (int i = 0; i < this.Triangles.Length; i++)
		{
			this.Triangles[i].color = GameController.ThemeManager.CurrentTheme.GameColors[this.ColorID];
		}
	}

	private void Start()
	{
		EventTrigger component = this.Group.GetComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.Drag;
		entry.callback.AddListener(delegate(BaseEventData data)
		{
			this.OnDrag();
		});
		component.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.BeginDrag;
		entry.callback.AddListener(delegate(BaseEventData data)
		{
			this.BeginDrag();
		});
		component.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerUp;
		entry.callback.AddListener(delegate(BaseEventData data)
		{
			this.EndDrag();
		});
		component.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener(delegate(BaseEventData data)
		{
			this.OnClick();
		});
		component.triggers.Add(entry);
	}

	private void Update()
	{
	}

	public void OnClick()
	{
		this._destination = UnityEngine.Input.mousePosition;
		this._destination += new Vector2(0f, (float)(Screen.height / 10));
		this.isDrag = true;
		this.Group.transform.localScale = Vector3.one;
	}

	public void BeginDrag()
	{
		if (this.tweenScale != null)
		{
			this.tweenScale.Kill(false);
			this.tweenScale = null;
		}
		this.isDrag = true;
		this.Group.transform.localScale = Vector3.one;
		this._destination = UnityEngine.Input.mousePosition;
		this._destination += new Vector2(0f, (float)(Screen.height / 10));
		GameController.AudioController.PlayOneShot("Audios/Effect/beep");
	}

	public void OnDrag()
	{
		this.Group.transform.localScale = Vector3.one;
		this.isDrag = true;
		this._destination = UnityEngine.Input.mousePosition;
		this._destination += new Vector2(0f, (float)(Screen.height / 10));
		this.CheckTriangleAvaiable();
	}

	public void EndDrag()
	{
		if (this.trianglesTemp != null)
		{
			this.Group.GetComponent<EventTrigger>().enabled = false;
			for (int i = 0; i < this.Triangles.Length; i++)
			{
				int i1 = i;
				this.Triangles[i].transform.DOMove(this.trianglesTemp[i].ImageColor.transform.position, 0.2f, false).SetEase(Ease.Linear).OnComplete(delegate
				{
					this.trianglesTemp[i1].SetColor(this.ColorID);
					if (i1 == this.Triangles.Length - 1)
					{
						if (!this.PlayController.IsTutorial)
						{
							this.PlayController.AddScore(this.trianglesTemp.Count);
							this.PlayController.CheckAddGem(this.Group.transform.position);
						}
						this.PlayController.BigHexa.CheckBlock();
						this.trianglesTemp = null;
						this.Group.transform.localScale = Vector3.zero;
						this.Group.gameObject.SetActive(false);
						this.PlayController.CheckMiniHexa();
					}
				});
			}
			GameController.AudioController.PlayOneShot("Audios/Effect/correct");
			if (this.PlayController.IsTutorial)
			{
				this.PlayController.TutorialController.Hand.gameObject.SetActive(false);
			}
		}
		else
		{
			this.Group.transform.DOLocalMove(Vector3.zero, 0.2f, false);
			this.Group.transform.DOScale(Vector2.one * MiniHexa.Scale, 0.2f);
			GameController.AudioController.PlayOneShot("Audios/Effect/incorrect");
		}
		this.isDrag = false;
	}

	private void CheckTriangleAvaiable()
	{
		this.trianglesTemp = this.GetTriangleAvaiable();
		Triangle[] triangles = this.PlayController.BigHexa.Triangles;
		for (int i = 0; i < triangles.Length; i++)
		{
			Triangle triangle = triangles[i];
			triangle.SetTemp(-1);
		}
		if (this.trianglesTemp != null)
		{
			Vector3 a = Vector3.zero;
			for (int j = 0; j < this.trianglesTemp.Count; j++)
			{
				this.trianglesTemp[j].SetTemp(this.ColorID);
				a += this.trianglesTemp[j].transform.position;
			}
		}
	}

	public List<Triangle> GetTriangleAvaiable()
	{
		List<Triangle> list = new List<Triangle>();
		for (int i = 0; i < this.Triangles.Length; i++)
		{
			Vector2 a = this.Triangles[i].transform.position * BaseController.GameController.CanvasScale;
			Triangle[] triangles = this.PlayController.BigHexa.Triangles;
			for (int j = 0; j < triangles.Length; j++)
			{
				Triangle triangle = triangles[j];
				Vector2 b = triangle.ImageColor.transform.position * BaseController.GameController.CanvasScale;
				Vector2 vector = a - b;
				float num = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
				if (!triangle.IsDisable && !triangle.IsSet && num < 40f * BaseController.GameController.HexaScale && this.isUniform(this.Triangles[i], triangle))
				{
					if (!list.Contains(triangle))
					{
						list.Add(triangle);
					}
					break;
				}
			}
		}
		return (list.Count != this.Triangles.Length) ? null : list;
	}

	public bool isUniform(Image image, Triangle triangle)
	{
		int num = (int)image.transform.rotation.eulerAngles.z - (int)triangle.transform.rotation.eulerAngles.z;
		num = (int)Mathf.Round((float)num / 10f) * 10;
		return (num + 360) % 120 == 0;
	}

	public void Reset()
	{
		this.isDrag = false;
		this._destination = Vector2.zero;
		this.Group.transform.eulerAngles = new Vector3(0f, 0f, (float)this._hexaDegree);
		this.Group.transform.localPosition = Vector3.zero;
		this.Group.transform.localScale = Vector3.one * MiniHexa.Scale;
		this._disable = false;
	}

	public void ResetPos()
	{
		this.isDrag = false;
		this._destination = Vector2.zero;
		this.Group.transform.eulerAngles = new Vector3(0f, 0f, (float)this._hexaDegree);
		this.Group.transform.localPosition = Vector3.zero;
		this.Group.transform.localScale = Vector3.one * MiniHexa.Scale;
	}

	public void SetDisable(bool isDisable)
	{
		this._disable = isDisable;
		if (isDisable)
		{
			for (int i = 0; i < this.Triangles.Length; i++)
			{
				this.Triangles[i].color = Color.gray;
			}
		}
		else
		{
			for (int j = 0; j < this.Triangles.Length; j++)
			{
				this.Triangles[j].color = GameController.ThemeManager.CurrentTheme.GameColors[this.ColorID];
			}
		}
	}

	private void FixedUpdate()
	{
		this.Movement();
	}

	private void Movement()
	{
		if (this.isDrag)
		{
			this.Group.transform.position = new Vector2(Mathf.Lerp(this.Group.transform.position.x, this._destination.x, this._speed * Time.deltaTime), Mathf.Lerp(this.Group.transform.position.y, this._destination.y, this._speed * Time.deltaTime));
		}
	}
}
