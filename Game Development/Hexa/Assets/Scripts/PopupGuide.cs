using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupGuide : Popup
{
	public Button ButtonClose;

	public Image[] Dots;

	public SnapScrollRect ScrollRect;

	private Sprite _spriteDot;

	private Sprite _spriteDotLight;

	public Button ButtonOK;

	private int _index;

	private void Start()
	{
		this.ButtonClose.onClick.AddListener(new UnityAction(this.Hide));
		this.ButtonOK.onClick.AddListener(new UnityAction(this.Hide));
		this._spriteDot = Resources.Load<Sprite>("Images/Guide/dot");
		this._spriteDotLight = Resources.Load<Sprite>("Images/Guide/dot2");
		this.ScrollRect.SetEndCallBack(delegate
		{
			this.SetIndex(Mathf.Abs(this.ScrollRect.Index));
		});
	}

	private void Update()
	{
	}

	public void Show(int index)
	{
		this.Show();
		this.ScrollRect.SetIndex(index);
		this.ButtonOK.gameObject.SetActive(true);
		this._index = index;
	}

	public override void Show()
	{
		base.Show();
		this.ButtonOK.gameObject.SetActive(false);
		this._index = 0;
		this.ScrollRect.SetIndex(this._index);
	}

	public override void OnShowComplete()
	{
		base.OnShowComplete();
		this.ScrollRect.SetIndex(this._index);
	}

	private void SetIndex(int index)
	{
		for (int i = 0; i < this.Dots.Length; i++)
		{
			this.Dots[i].sprite = this._spriteDot;
		}
		this.Dots[index].sprite = this._spriteDotLight;
	}
}
