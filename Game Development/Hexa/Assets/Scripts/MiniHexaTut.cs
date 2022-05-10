using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniHexaTut : BaseController
{
	public delegate void CallBack();

	public GameObject Group;

	private int _hexaRotate;

	public bool CanRotate;

	public Image Target;

	public Image Triangle;

	public int TargetRotate;

	public MiniHexaTut.CallBack CallBackFinish;

	public MiniHexaTut.CallBack CallBackFail;

	public MiniHexaTut.CallBack CallBackClick;

	public MiniHexaTut.CallBack CallBackBeginDrag;

	private bool isDrag;

	private void Update()
	{
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
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener(delegate(BaseEventData data)
		{
			this.OnClick();
		});
		component.triggers.Add(entry);
	}

	public void BeginDrag()
	{
		if (this.CallBackBeginDrag != null)
		{
			this.CallBackBeginDrag();
		}
		this.isDrag = true;
		this.Group.transform.position = UnityEngine.Input.mousePosition;
		this.Group.transform.localPosition += new Vector3(0f, 120f, 0f);
		GameController.AudioController.PlayOneShot("Audios/Effect/beep");
	}

	public void OnDrag()
	{
		this.isDrag = true;
		this.Group.transform.position = UnityEngine.Input.mousePosition;
		this.Group.transform.localPosition += new Vector3(0f, 120f, 0f);
	}

	public void EndDrag()
	{
		Vector2 a = this.Triangle.transform.position * BaseController.GameController.CanvasScale;
		Vector2 b = this.Target.transform.position * BaseController.GameController.CanvasScale;
		Vector2 vector = a - b;
		float num = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
		if (num < 60f && this.IsUniform())
		{
			this.Group.transform.DOMove(this.Group.transform.position - (this.Triangle.transform.position - this.Target.transform.position), 0.2f, false).OnComplete(delegate
			{
				if (this.CallBackFinish != null)
				{
					this.CallBackFinish();
				}
			});
			GameController.AudioController.PlayOneShot("Audios/Effect/correct");
		}
		else
		{
			this.Group.transform.DOLocalMove(Vector3.zero, 0.2f, false).OnComplete(delegate
			{
				if (this.CallBackFail != null)
				{
					this.CallBackFail();
				}
			});
			GameController.AudioController.PlayOneShot("Audios/Effect/incorrect");
		}
	}

	public bool IsUniform()
	{
		int num = (int)Mathf.Round((float)(this._hexaRotate - this.TargetRotate) / 10f) * 10;
		return (num + 360) % 120 == 0;
	}

	public void OnClick()
	{
		if (!this.CanRotate)
		{
			return;
		}
		if (!this.isDrag)
		{
			this._hexaRotate += 60;
			if (this._hexaRotate == 360)
			{
				this._hexaRotate = 0;
			}
			this.Group.transform.DORotate(new Vector3(0f, 0f, (float)this._hexaRotate), 0.3f, RotateMode.Fast);
			if (this.CallBackClick != null)
			{
				this.CallBackClick();
			}
		}
		this.isDrag = false;
	}
}
