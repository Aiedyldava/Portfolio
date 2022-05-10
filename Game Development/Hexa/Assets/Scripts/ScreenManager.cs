using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
	[HideInInspector]
	public delegate void CallBack();

	public enum StateGame
	{
		LOAD,
		MAIN,
		PLAY
	}

	private sealed class __OpenStage_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal ScreenManager.StateGame _stateGame;

		internal string _nameScene___0;

		internal AsyncOperation _asyncLoad___0;

		internal ScreenManager.CallBack callBack;

		internal ScreenManager _this;

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

		public __OpenStage_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this.currentStage != null)
				{
					this._this.currentStage.OnStageClose();
				}
				this._this.backState = this._this.stateGame;
				this._this.stateGame = this._stateGame;
				this._nameScene___0 = "Play";
				if (this._stateGame != ScreenManager.StateGame.PLAY)
				{
					if (this._stateGame == ScreenManager.StateGame.MAIN)
					{
						this._nameScene___0 = "Main";
					}
				}
				else
				{
					this._nameScene___0 = "Play";
				}
				this._asyncLoad___0 = SceneManager.LoadSceneAsync(this._nameScene___0);
				break;
			case 1u:
				break;
			case 2u:
				if (this._stateGame != ScreenManager.StateGame.PLAY)
				{
					if (this._stateGame == ScreenManager.StateGame.MAIN)
					{
						this._this.MainController = UnityEngine.Object.FindObjectOfType<MainController>();
						this._this.currentStage = this._this.MainController;
					}
				}
				else
				{
					this._this.PlayController = UnityEngine.Object.FindObjectOfType<PlayController>();
					this._this.currentStage = this._this.PlayController;
				}
				if (this.callBack != null)
				{
					this.callBack();
				}
				if (this._this.stateGame == this._stateGame)
				{
					ScreenManager.isLoadScreen = false;
					if (this._this.currentStage != null)
					{
						this._this.currentStage.OnStageOpen();
					}
				}
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._asyncLoad___0.isDone)
			{
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 2;
				}
			}
			else
			{
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
			}
			return true;
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

	[HideInInspector]
	public static bool isLoadScreen;

	[HideInInspector]
	public LoadController LoadController;

	[HideInInspector]
	public PlayController PlayController;

	[HideInInspector]
	public MainController MainController;

	[HideInInspector]
	public StageController currentStage;

	[HideInInspector]
	public ScreenManager.StateGame stateGame;

	[HideInInspector]
	public ScreenManager.StateGame backState;

	public void OpenStage(ScreenManager.StateGame stateGame)
	{
		this.OpenStage(stateGame, null);
	}

	public void OpenStage(ScreenManager.StateGame stateGame, ScreenManager.CallBack callBack)
	{
		ScreenManager.isLoadScreen = true;
		base.StopAllCoroutines();
		base.StartCoroutine(this._OpenStage(stateGame, callBack));
	}

	public IEnumerator _OpenStage(ScreenManager.StateGame _stateGame, ScreenManager.CallBack callBack)
	{
		ScreenManager.__OpenStage_c__Iterator0 __OpenStage_c__Iterator = new ScreenManager.__OpenStage_c__Iterator0();
		__OpenStage_c__Iterator._stateGame = _stateGame;
		__OpenStage_c__Iterator.callBack = callBack;
		__OpenStage_c__Iterator._this = this;
		return __OpenStage_c__Iterator;
	}
}
