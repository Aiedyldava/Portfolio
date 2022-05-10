using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdditionButton : BaseController
{
	public enum EFFECT_TYPE
	{
		NORMAL,
		CIRCLE,
		RECTANGLE,
		NOEFF
	}

	private EventTrigger trigger;

	public AdditionButton.EFFECT_TYPE EffectType;

	private static UnityAction __f__am_cache0;

	private void Awake()
	{
		if (this.EffectType != AdditionButton.EFFECT_TYPE.NOEFF)
		{
			this.trigger = base.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				this.EffectOnDown();
			});
			this.trigger.triggers.Add(entry);
			EventTrigger.Entry entry2 = new EventTrigger.Entry();
			entry2.eventID = EventTriggerType.PointerUp;
			entry2.callback.AddListener(delegate(BaseEventData e)
			{
				this.EffectOnExit();
			});
			this.trigger.triggers.Add(entry2);
		}
		else
		{
			base.GetComponent<Button>().onClick.AddListener(delegate
			{
				GameController.AudioController.PlayOneShot("Audios/Effect/click_button");
			});
		}
	}

	private void EffectOnDown()
	{
		GameController.AudioController.PlayOneShot("Audios/Effect/click_button");
		base.gameObject.transform.DOScale(Vector3.one * 0.95f, 0.2f);
		if (this.EffectType == AdditionButton.EFFECT_TYPE.CIRCLE)
		{
			EffectButton component = BaseController.InstantiatePrefab("Prefabs/Effect/CircleButtonEff").GetComponent<EffectButton>();
			component.transform.SetParent(base.gameObject.transform, false);
			component.StartEff(base.GetComponent<Image>());
		}
		if (this.EffectType == AdditionButton.EFFECT_TYPE.RECTANGLE)
		{
			EffectButton component2 = BaseController.InstantiatePrefab("Prefabs/Effect/RecButtonEff").GetComponent<EffectButton>();
			component2.transform.SetParent(base.gameObject.transform, false);
			component2.StartEff(base.GetComponent<Image>());
		}
	}

	private void EffectOnExit()
	{
		base.gameObject.transform.DOScale(Vector3.one, 0.2f);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
