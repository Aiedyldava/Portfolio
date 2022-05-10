using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Triangle : BaseController
{
	public enum ConnectType
	{
		NONE,
		LEFT_UPPER,
		RIGHT_BELOW,
		LEFT_BELOW,
		RIGHT_UPPER,
		UPPER,
		BELOW
	}

	public List<Triangle> TrianglesConnect = new List<Triangle>();

	public Image Background;

	public Image ImageColor;

	public Image ImagePattern;

	public Text Text;

	public CanvasGroup CanvasGroup;

	public int Id;

	public int ColorId = -1;

	public RectTransform RectTransform;

	public bool IsDiamond;

	private List<Tween> _tween = new List<Tween>();

	public bool IsSet;

	public bool IsTemp;

	public bool IsDisable;

	public void SetDiamond(bool isDiamond)
	{
		this.IsDiamond = isDiamond;
		if (this.IsDiamond)
		{
			this.ImagePattern.gameObject.SetActive(false);
			this.ImageColor.sprite = Resources.Load<Sprite>("Images/GamePlay/triangle_diamond");
			this.ImageColor.color = Color.white;
		}
		else
		{
			this.ImageColor.sprite = Resources.Load<Sprite>("Images/GamePlay/triangle");
			if (this.ColorId >= 0)
			{
				this.ImageColor.color = GameController.ThemeManager.CurrentTheme.GameColors[this.ColorId];
			}
		}
	}

	public void Reset()
	{
		this.ImagePattern.gameObject.SetActive(false);
		this.IsDisable = false;
		this.SetColor(-1);
		foreach (Tween current in this._tween)
		{
			current.Kill(false);
		}
		this._tween.Clear();
		this.IsSet = false;
	}

	public void RePlayEffect()
	{
		if (base.transform.eulerAngles.z < 10f)
		{
			this.Background.DOFade(GameController.ThemeManager.CurrentTheme.TriangleColor.a - 0.1f, 0.3f).SetEase(Ease.Linear).SetDelay(0.3f).OnComplete(delegate
			{
				this.Background.DOFade(GameController.ThemeManager.CurrentTheme.TriangleColor.a, 0.3f).SetEase(Ease.Linear);
			});
		}
		else
		{
			this.Background.DOFade(GameController.ThemeManager.CurrentTheme.TriangleColor.a + 0.1f, 0.3f).SetEase(Ease.Linear).SetDelay(0.3f).OnComplete(delegate
			{
				this.Background.DOFade(GameController.ThemeManager.CurrentTheme.TriangleColor.a, 0.3f).SetEase(Ease.Linear);
			});
		}
	}

	private void Awake()
	{
		this.RectTransform = base.GetComponent<RectTransform>();
		this.Background.color = GameController.ThemeManager.CurrentTheme.TriangleColor;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetColor(int colorID)
	{
		this.ColorId = colorID;
		this.SetTemp(-1);
		this.ImagePattern.gameObject.SetActive(false);
		if (colorID == -1)
		{
			this.ImageColor.gameObject.SetActive(false);
			this.Background.color = GameController.ThemeManager.CurrentTheme.TriangleColor;
			this.SetDiamond(false);
			this.IsSet = false;
		}
		else
		{
			this.ImageColor.gameObject.SetActive(true);
			this.Background.color = new Color(0f, 0f, 0f, 0f);
			if (!this.IsDiamond)
			{
				this.ImageColor.color = GameController.ThemeManager.CurrentTheme.GameColors[colorID];
				if (GameController.ThemeManager.CurrentTheme.Patterns.Count > 0)
				{
					this.ImagePattern.gameObject.SetActive(true);
					this.ImagePattern.sprite = GameController.ThemeManager.CurrentTheme.RandomPattern();
				}
			}
			else
			{
				this.ImageColor.color = Color.white;
			}
			this.IsSet = true;
		}
	}

	public Triangle.ConnectType GetConnectTypeReal(Triangle triangle)
	{
		Triangle.ConnectType result = Triangle.ConnectType.NONE;
		if (triangle == this)
		{
			return Triangle.ConnectType.NONE;
		}
		Vector2 vector = base.transform.localPosition - triangle.transform.localPosition;
		float num = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
		if (num < 50f)
		{
			if (vector.x < 0f && vector.y < 0f)
			{
				result = Triangle.ConnectType.LEFT_BELOW;
			}
			else if (vector.x < 0f && vector.y > 0f)
			{
				result = Triangle.ConnectType.LEFT_UPPER;
			}
			else if (vector.x > 0f && vector.y < 0f)
			{
				result = Triangle.ConnectType.RIGHT_BELOW;
			}
			else
			{
				result = Triangle.ConnectType.RIGHT_UPPER;
			}
		}
		else if (num < 75f)
		{
			if (vector.y < 0f)
			{
				result = Triangle.ConnectType.BELOW;
			}
			else
			{
				result = Triangle.ConnectType.UPPER;
			}
		}
		return result;
	}

	public Triangle.ConnectType GetConnectType(Triangle triangle)
	{
		if (!this.IsSet || this.ColorId < 0 || triangle.ColorId < 0)
		{
			return Triangle.ConnectType.NONE;
		}
		return this.GetConnectTypeReal(triangle);
	}

	public void SetDisable(bool isdisable)
	{
		this.IsDisable = isdisable;
		this.Background.color = GameController.ThemeManager.CurrentTheme.TriangleColor;
		if (isdisable)
		{
			this.Background.color = new Color(this.Background.color.r, this.Background.color.g, this.Background.color.b, 0.0392156877f);
		}
	}

	public void SetTemp(int colorTemp)
	{
		if (this.IsSet)
		{
			return;
		}
		if (colorTemp == -1)
		{
			this.IsTemp = false;
			this.Background.color = GameController.ThemeManager.CurrentTheme.TriangleColor;
			if (this.IsDisable)
			{
				this.Background.color = new Color(this.Background.color.r, this.Background.color.g, this.Background.color.b, 0.0392156877f);
			}
		}
		else
		{
			this.IsTemp = true;
			this.Background.color = new Color(GameController.ThemeManager.CurrentTheme.GameColors[colorTemp].r, GameController.ThemeManager.CurrentTheme.GameColors[colorTemp].g, GameController.ThemeManager.CurrentTheme.GameColors[colorTemp].b, 0.470588237f);
		}
	}

	public void UpdateBackgroundColor()
	{
		if (this.IsSet)
		{
			this.Background.color = new Color(0f, 0f, 0f, 0f);
		}
		else
		{
			this.Background.color = GameController.ThemeManager.CurrentTheme.TriangleColor;
		}
	}
}
