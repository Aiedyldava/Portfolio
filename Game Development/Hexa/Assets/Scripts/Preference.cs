using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Preference
{
	public string DATA = "Hexa1010";

	public DataGame DataGame = new DataGame();

	private static Preference _instance;

	public static Preference Instance
	{
		get
		{
			if (Preference._instance == null)
			{
				Preference._instance = new Preference();
			}
			return Preference._instance;
		}
	}

	private Preference()
	{
		this.LoadData();
	}

	private void LoadData()
	{
		if (PlayerPrefs.HasKey(this.DATA))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(this.DataGame.GetType());
			StringReader textReader = new StringReader(PlayerPrefs.GetString(this.DATA));
			this.DataGame = (DataGame)xmlSerializer.Deserialize(textReader);
		}
		else
		{
			this.SaveData();
		}
	}

	public void SaveData()
	{
		XmlSerializer xmlSerializer = new XmlSerializer(this.DataGame.GetType());
		StringWriter stringWriter = new StringWriter();
		xmlSerializer.Serialize(stringWriter, this.DataGame);
		PlayerPrefs.SetString(this.DATA, stringWriter.ToString());
	}
}
