//using Facebook.Unity;
//using Firebase.Analytics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnalyticsController : BaseController
{

	public static string START_TUTORIAL_STEP = "Start_tutorial";

	public static string STEP = "Step";

	public static string WATCH_ADS = "Watch_ads";

	public static string WATCH_ADS_TYPE = "Type";

	public static string GAME_OVER = "Game_over";

	public static string SCORE = "Score";

	public static string PURCHASE_REMOVE_ADS = "Purchase_remove_ads";

	public static string PURCHASE = "Purchase";

	public static string PACKAGE = "Package";

	public static string USE_THEME = "Use_theme";

	public static string NAME = "Name";

	public static string DAILY_REWARD = "Daily_Reward";

	public static string DAY = "Day";

	public static string FINISH_TUTORIAL = "Finish_tutorial";

	public static string SPIN = "Spin";

	public static string Gem = "Gem";

	public static string Gem2 = "GemX2";

	public static string START_GAME = "Start_game";

	public static string USE_BIN = "Use_bin";


	public void Start()
	{

	}

	public void LogEvent(string eventName)
	{
		try
		{
			
		}
		catch (Exception message)
		{
			UnityEngine.Debug.Log(message);
		}
	}

	public void LogEventPurchase(string productID, int value)
	{
		
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["productID"] = productID;
	}


}
