using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	private sealed class __PlayLoopSoundEffect_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string audioName;

		internal AudioClip _clip___0;

		internal AudioController _this;

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

		public __PlayLoopSoundEffect_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this.SoundEffectLoop.Contains(this.audioName))
				{
					this._clip___0 = this._this.AudioClips[this.audioName];
					this._this.AudioSource.PlayOneShot(this._clip___0);
					this._current = new WaitForSeconds(this._clip___0.length);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				break;
			case 1u:
				this._this.StartCoroutine(this._this._PlayLoopSoundEffect(this.audioName));
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

	private sealed class _PlayButtonClick_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal AudioController _this;

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

		public _PlayButtonClick_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.haveOtherSoundPlay = false;
				this._current = new WaitForSeconds(this._this.timeWaitClickButton);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (!this._this.haveOtherSoundPlay)
				{
					this._this.AudioSource.PlayOneShot(this._this.AudioClips["Audios/Effect/click_button"]);
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

	private sealed class _AfterOtherSoundPlay_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal AudioController _this;

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

		public _AfterOtherSoundPlay_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(this._this.timeWaitClickButton);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.haveOtherSoundPlay = false;
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

	private AudioSource _audioSource;

	[HideInInspector]
	public Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();

	[HideInInspector]
	public List<string> SoundEffectLoop = new List<string>();

	private bool isPlaying;

	private float timeWaitClickButton = 0.07f;

	private bool haveOtherSoundPlay;

	public AudioSource AudioSource
	{
		get
		{
			AudioSource arg_1C_0;
			if ((arg_1C_0 = this._audioSource) == null)
			{
				arg_1C_0 = (this._audioSource = base.GetComponent<AudioSource>());
			}
			return arg_1C_0;
		}
	}

	private void Start()
	{
	}

	public void PlayLoop(string audioName)
	{
		if (!this.CheckClipExist(audioName))
		{
			return;
		}
		this.AudioSource.clip = this.AudioClips[audioName];
		this.AudioSource.loop = true;
		this.isPlaying = true;
		this.AudioSource.Play();
	}

	public void StopAllLoopSoundEffect()
	{
		this.SoundEffectLoop.Clear();
	}

	public void StopBackgroundMusic(string audioName)
	{
		this.AudioSource.Stop();
		this.SoundEffectLoop.Remove(audioName);
	}

	public void StopLoopSoundEffect(string audioName)
	{
		this.SoundEffectLoop.Remove(audioName);
	}

	private IEnumerator _PlayLoopSoundEffect(string audioName)
	{
		AudioController.__PlayLoopSoundEffect_c__Iterator0 __PlayLoopSoundEffect_c__Iterator = new AudioController.__PlayLoopSoundEffect_c__Iterator0();
		__PlayLoopSoundEffect_c__Iterator.audioName = audioName;
		__PlayLoopSoundEffect_c__Iterator._this = this;
		return __PlayLoopSoundEffect_c__Iterator;
	}

	public void PlayOneShot(string audioName)
	{
		if (!Preference.Instance.DataGame.IsSound)
		{
			return;
		}
		if (!this.CheckClipExist(audioName))
		{
			return;
		}
		if (audioName == "Audios/Effect/click_button")
		{
			if (!this.haveOtherSoundPlay)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.PlayButtonClick());
			}
		}
		else
		{
			this.haveOtherSoundPlay = true;
			this.AudioSource.PlayOneShot(this.AudioClips[audioName]);
			base.StopAllCoroutines();
			base.StartCoroutine(this.AfterOtherSoundPlay());
		}
	}

	private IEnumerator PlayButtonClick()
	{
		AudioController._PlayButtonClick_c__Iterator1 _PlayButtonClick_c__Iterator = new AudioController._PlayButtonClick_c__Iterator1();
		_PlayButtonClick_c__Iterator._this = this;
		return _PlayButtonClick_c__Iterator;
	}

	private IEnumerator AfterOtherSoundPlay()
	{
		AudioController._AfterOtherSoundPlay_c__Iterator2 _AfterOtherSoundPlay_c__Iterator = new AudioController._AfterOtherSoundPlay_c__Iterator2();
		_AfterOtherSoundPlay_c__Iterator._this = this;
		return _AfterOtherSoundPlay_c__Iterator;
	}

	public bool CheckClipExist(string audioName)
	{
		if (this.AudioClips.ContainsKey(audioName))
		{
			return true;
		}
		AudioClip audioClip = Resources.Load<AudioClip>(audioName);
		if (audioClip == null)
		{
			return false;
		}
		this.AudioClips[audioName] = audioClip;
		return true;
	}
}
