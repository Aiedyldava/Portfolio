using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
	[HideInInspector]
	public List<Popup> ListDialogs = new List<Popup>();

	public GameObject ToastPrefabs;

	public GameObject GameOverPrefabs;

	public GameObject PausePrefabs;

	public GameObject ShopPrefab;

	public GameObject PopupSpinPrefab;

	public GameObject RewardPrefab;

	public GameObject PurchaseResultPrefab;

	public GameObject PopupThemesPrefab;

	private Toast _toast;

	[HideInInspector]
	public GameOver _gameOver;

	[HideInInspector]
	public PopupPause _popupPause;

	private PopupShop _popupShop;

	private PopupSpin _popupSpin;

	private PopupReward _popupReward;

	private PopupPurchaseResult _popupPurchaseResult;

	private DialogThemes _dialogThemes;

	public Toast Toast
	{
		get
		{
			return (!this._toast) ? (this._toast = (Toast)this.createDialog(this.ToastPrefabs)) : this._toast;
		}
	}

	public GameOver GameOver
	{
		get
		{
			return (!this._gameOver) ? (this._gameOver = (GameOver)this.createDialog(this.GameOverPrefabs)) : this._gameOver;
		}
	}

	public PopupPause PopupPause
	{
		get
		{
			return (!this._popupPause) ? (this._popupPause = (PopupPause)this.createDialog(this.PausePrefabs)) : this._popupPause;
		}
	}

	public PopupShop PopupShop
	{
		get
		{
			return (!this._popupShop) ? (this._popupShop = (PopupShop)this.createDialog(this.ShopPrefab)) : this._popupShop;
		}
	}

	public PopupSpin PopupSpin
	{
		get
		{
			return (!this._popupSpin) ? (this._popupSpin = (PopupSpin)this.createDialog(this.PopupSpinPrefab)) : this._popupSpin;
		}
	}

	public PopupReward PopupReward
	{
		get
		{
			return (!this._popupReward) ? (this._popupReward = (PopupReward)this.createDialog(this.RewardPrefab)) : this._popupReward;
		}
	}

	public PopupPurchaseResult PopupPurchaseResult
	{
		get
		{
			return (!this._popupPurchaseResult) ? (this._popupPurchaseResult = (PopupPurchaseResult)this.createDialog(this.PurchaseResultPrefab)) : this._popupPurchaseResult;
		}
	}

	public DialogThemes DialogThemes
	{
		get
		{
			return (!this._dialogThemes) ? (this._dialogThemes = (DialogThemes)this.createDialog(this.PopupThemesPrefab)) : this._dialogThemes;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void HideAllDialog()
	{
		foreach (Popup current in this.ListDialogs)
		{
			if (current.gameObject.activeSelf)
			{
				current.Hide();
			}
		}
	}

	private Popup createDialog(GameObject prefab)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
		gameObject.SetActive(false);
		gameObject.transform.SetParent(base.transform, false);
		Popup component = gameObject.GetComponent<Popup>();
		this.ListDialogs.Add(component);
		return component;
	}

	public int GetNumberActiveDialog()
	{
		int num = 0;
		foreach (Popup current in this.ListDialogs)
		{
			if (current.gameObject.activeSelf)
			{
				num++;
			}
		}
		return num;
	}
}
