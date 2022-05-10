using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BigHexa : BaseController
{
	private sealed class _InitHexa_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal BigHexa _this;

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

		public _InitHexa_c__Iterator0()
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
				this._this.VerticalLayoutGroup = this._this.GetComponent<VerticalLayoutGroup>();
				this._this.VerticalLayoutGroup.enabled = false;
				this._this.Triangles = this._this.GetComponentsInChildren<Triangle>();
				for (int i = 0; i < this._this.Triangles.Length; i++)
				{
					this._this.Triangles[i].Id = i;
					this._this.Triangles[i].Text.text = i + string.Empty;
					this._this.Triangles[i].Text.transform.eulerAngles = Vector3.zero;
					this._this.Triangles[i].transform.SetParent(this._this.transform);
					this._this.Triangles[i].Reset();
				}
				this._current = new WaitForEndOfFrame();
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
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

	public PlayController PlayController;

	public Triangle[] Triangles;

	private VerticalLayoutGroup VerticalLayoutGroup;

	private void Awake()
	{
	}

	private void Start()
	{
		base.StartCoroutine(this.InitHexa());
	}

	private IEnumerator InitHexa()
	{
		BigHexa._InitHexa_c__Iterator0 _InitHexa_c__Iterator = new BigHexa._InitHexa_c__Iterator0();
		_InitHexa_c__Iterator._this = this;
		return _InitHexa_c__Iterator;
	}

	public Triangle GetTriangleConnect(Triangle triangle, Triangle.ConnectType connectType)
	{
		for (int i = 0; i < this.Triangles.Length; i++)
		{
			if (triangle.GetConnectTypeReal(this.Triangles[i]) == connectType)
			{
				return this.Triangles[i];
			}
		}
		return null;
	}

	public void CheckBlock()
	{
		List<List<Triangle>> list = new List<List<Triangle>>();
		for (int i = 0; i < this.Triangles.Length; i++)
		{
			if (this.Triangles[i].IsSet)
			{
				if (this.GetTriangleConnect(this.Triangles[i], Triangle.ConnectType.RIGHT_BELOW) == null && this.Triangles[i].transform.eulerAngles.z < 10f)
				{
					List<Triangle> list2 = new List<Triangle>();
					Triangle.ConnectType connectType = Triangle.ConnectType.LEFT_BELOW;
					list2.Add(this.Triangles[i]);
					Triangle triangleConnect = this.GetTriangleConnect(this.Triangles[i], connectType);
					bool flag = true;
					while (triangleConnect != null)
					{
						if (!triangleConnect.IsSet)
						{
							flag = false;
							break;
						}
						list2.Add(triangleConnect);
						connectType = ((connectType != Triangle.ConnectType.LEFT_BELOW) ? Triangle.ConnectType.LEFT_BELOW : Triangle.ConnectType.LEFT_UPPER);
						triangleConnect = this.GetTriangleConnect(triangleConnect, connectType);
					}
					if (flag)
					{
						list.Add(list2);
					}
					List<Triangle> list3 = new List<Triangle>();
					Triangle.ConnectType connectType2 = Triangle.ConnectType.UPPER;
					list3.Add(this.Triangles[i]);
					Triangle triangleConnect2 = this.GetTriangleConnect(this.Triangles[i], connectType2);
					bool flag2 = true;
					while (triangleConnect2 != null)
					{
						if (!triangleConnect2.IsSet)
						{
							flag2 = false;
							break;
						}
						list3.Add(triangleConnect2);
						connectType2 = ((connectType2 != Triangle.ConnectType.UPPER) ? Triangle.ConnectType.UPPER : Triangle.ConnectType.LEFT_UPPER);
						triangleConnect2 = this.GetTriangleConnect(triangleConnect2, connectType2);
					}
					if (flag2)
					{
						list.Add(list3);
					}
				}
				if (this.GetTriangleConnect(this.Triangles[i], Triangle.ConnectType.RIGHT_UPPER) == null && this.Triangles[i].transform.eulerAngles.z > 10f)
				{
					List<Triangle> list4 = new List<Triangle>();
					Triangle.ConnectType connectType3 = Triangle.ConnectType.LEFT_UPPER;
					list4.Add(this.Triangles[i]);
					Triangle triangleConnect3 = this.GetTriangleConnect(this.Triangles[i], connectType3);
					bool flag3 = true;
					while (triangleConnect3 != null)
					{
						if (!triangleConnect3.IsSet)
						{
							flag3 = false;
							break;
						}
						list4.Add(triangleConnect3);
						connectType3 = ((connectType3 != Triangle.ConnectType.LEFT_UPPER) ? Triangle.ConnectType.LEFT_UPPER : Triangle.ConnectType.LEFT_BELOW);
						triangleConnect3 = this.GetTriangleConnect(triangleConnect3, connectType3);
					}
					if (flag3)
					{
						list.Add(list4);
					}
					List<Triangle> list5 = new List<Triangle>();
					Triangle.ConnectType connectType4 = Triangle.ConnectType.BELOW;
					list5.Add(this.Triangles[i]);
					Triangle triangleConnect4 = this.GetTriangleConnect(this.Triangles[i], connectType4);
					bool flag4 = true;
					while (triangleConnect4 != null)
					{
						if (!triangleConnect4.IsSet)
						{
							flag4 = false;
							break;
						}
						list5.Add(triangleConnect4);
						connectType4 = ((connectType4 != Triangle.ConnectType.BELOW) ? Triangle.ConnectType.BELOW : Triangle.ConnectType.LEFT_BELOW);
						triangleConnect4 = this.GetTriangleConnect(triangleConnect4, connectType4);
					}
					if (flag4)
					{
						list.Add(list5);
					}
				}
				if (this.GetTriangleConnect(this.Triangles[i], Triangle.ConnectType.BELOW) == null && this.Triangles[i].transform.eulerAngles.z > 10f)
				{
					List<Triangle> list6 = new List<Triangle>();
					Triangle.ConnectType connectType5 = Triangle.ConnectType.LEFT_UPPER;
					list6.Add(this.Triangles[i]);
					Triangle triangleConnect5 = this.GetTriangleConnect(this.Triangles[i], connectType5);
					bool flag5 = true;
					while (triangleConnect5 != null)
					{
						if (!triangleConnect5.IsSet)
						{
							flag5 = false;
							break;
						}
						list6.Add(triangleConnect5);
						connectType5 = ((connectType5 != Triangle.ConnectType.LEFT_UPPER) ? Triangle.ConnectType.LEFT_UPPER : Triangle.ConnectType.UPPER);
						triangleConnect5 = this.GetTriangleConnect(triangleConnect5, connectType5);
					}
					if (flag5)
					{
						list.Add(list6);
					}
				}
				if (this.GetTriangleConnect(this.Triangles[i], Triangle.ConnectType.UPPER) == null && this.Triangles[i].transform.eulerAngles.z < 10f)
				{
					List<Triangle> list7 = new List<Triangle>();
					Triangle.ConnectType connectType6 = Triangle.ConnectType.LEFT_BELOW;
					list7.Add(this.Triangles[i]);
					Triangle triangleConnect6 = this.GetTriangleConnect(this.Triangles[i], connectType6);
					bool flag6 = true;
					while (triangleConnect6 != null)
					{
						if (!triangleConnect6.IsSet)
						{
							flag6 = false;
							break;
						}
						list7.Add(triangleConnect6);
						connectType6 = ((connectType6 != Triangle.ConnectType.LEFT_BELOW) ? Triangle.ConnectType.LEFT_BELOW : Triangle.ConnectType.BELOW);
						triangleConnect6 = this.GetTriangleConnect(triangleConnect6, connectType6);
					}
					if (flag6)
					{
						list.Add(list7);
					}
				}
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			for (int k = 0; k < list[j].Count; k++)
			{
				list[j][k].IsSet = false;
				GameController.EffectController.StartRemoveEffect(list[j][k], (float)k * 0.06f);
			}
		}
		if (list.Count >= 5)
		{
			Preference.Instance.DataGame.Combo5Line++;
		}
		if (list.Count > 0)
		{
			GameController.AudioController.PlayOneShot("Audios/Effect/double_hexa");
			int num = 10 * list.Count * list.Count;
			string text = string.Empty;
			if (list.Count == 2)
			{
				text = "Double";
			}
			if (list.Count == 3)
			{
				text = "Triple";
			}
			if (list.Count >= 4)
			{
				text = "Quadruple";
			}
			Vector2 vector = list[0][list[0].Count / 2].transform.position;
			GameController.EffectController.StartTextFly(vector, "+" + num, Color.cyan, 0f);
			if (list.Count > 1)
			{
				if (!this.PlayController.IsTutorial)
				{
					this.PlayController.PerfectEffect.StartEffect();
				}
				vector = list[1][list[1].Count * 2 / 3].transform.position;
				GameController.EffectController.StartTextFly(vector, text, Color.yellow, 0f);
			}
			if (!this.PlayController.IsTutorial)
			{
				this.PlayController.AddScore(num);
				this.PlayController.CheckAddGem(vector);
			}
			else
			{
				GameController.EffectController.AddGemEffect(vector, 1, this.PlayController.ImageDiamond, -1f);
			}
		}
		if (this.PlayController.IsTutorial)
		{
			float interval = 2f;
			if (this.PlayController.TutorialController._step == 5)
			{
				interval = 0f;
			}
			DOTween.Sequence().AppendInterval(interval).AppendCallback(delegate
			{
				this.PlayController.TutorialController.PlayTutorial();
			});
		}
	}

	private void Update()
	{
	}

	public void Reset()
	{
		for (int i = 0; i < this.Triangles.Length; i++)
		{
			this.Triangles[i].Reset();
		}
	}

	public void RePlayEff()
	{
		for (int i = 0; i < this.Triangles.Length; i++)
		{
			this.Triangles[i].RePlayEffect();
		}
	}

	public void UpdateBackgroundColor()
	{
		for (int i = 0; i < this.Triangles.Length; i++)
		{
			this.Triangles[i].UpdateBackgroundColor();
		}
	}
}
