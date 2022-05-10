using System;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
	public int Value;

	public Text TextDay;

	public Text TextValue;

	public GameObject Check;

	public GameObject Uncheck;

	public GameObject Focus;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetValue(int value)
	{
		this.Value = value;
		this.TextValue.text = value + string.Empty;
	}
}
