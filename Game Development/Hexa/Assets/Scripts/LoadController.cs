using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LoadController : StageController
{
	private sealed class _StartGame_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
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

		public _StartGame_c__Iterator0()
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
				if (Preference.Instance.DataGame.FirstOpen)
				{
					GameController.ScreenManager.OpenStage(ScreenManager.StateGame.PLAY);
				}
				else
				{
					GameController.ScreenManager.OpenStage(ScreenManager.StateGame.MAIN);
				}
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

	private void Start()
	{
		base.StartCoroutine(this.StartGame());
	}

	private IEnumerator StartGame()
	{
		return new LoadController._StartGame_c__Iterator0();
	}

	private void Update()
	{
	}
}
