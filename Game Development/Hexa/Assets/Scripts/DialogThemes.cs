using System;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogThemes : Popup
{
	public Button ButtonClose;

	public Button ButtonShop;

	public Text TextGem;

	public ThemeItem[] ThemeItems;

	private static UnityAction __f__am_cache0;

	private void Start()
	{
		this.ButtonClose.onClick.AddListener(new UnityAction(this.Hide));
		this.ButtonShop.onClick.AddListener(delegate
		{
			GameController.DialogManager.PopupShop.Show();
		});
	}

	private void Update()
	{
		this.TextGem.text = Preference.Instance.DataGame.Coin + string.Empty;
	}

	public override void Show()
	{
		base.Show();
		for (int i = 0; i < this.ThemeItems.Length; i++)
		{
			this.ThemeItems[i].InitInfo();
		}
	}
}
