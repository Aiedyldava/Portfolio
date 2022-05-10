using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Toast : Popup
{
	private sealed class __hide_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Toast _this;

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

		public __hide_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Hide();
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

	public Text Text;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public virtual void Show(string text)
	{
		this.Show();
		this.Text.text = text;
	}

	public override void OnShowComplete()
	{
		base.OnShowComplete();
		base.StartCoroutine(this._hide());
	}

	private IEnumerator _hide()
	{
		Toast.__hide_c__Iterator0 __hide_c__Iterator = new Toast.__hide_c__Iterator0();
		__hide_c__Iterator._this = this;
		return __hide_c__Iterator;
	}
}
