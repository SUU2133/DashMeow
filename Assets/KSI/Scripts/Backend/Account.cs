using BackEnd.Functions;
using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account : MonoBehaviour
{
	private static Account _inst;
	public static Account Inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new Account();
			}
			return _inst;
		}
	}

	public StageUnlockData stageUnlockData = null;

	List<BackendStoreManager> dataCollection = new List<BackendStoreManager>();

	public void UserDataGet()
	{
		var tableList = Backend.GameData.GetTableList();

		if (tableList.IsSuccess())
		{
			var tables = tableList.GetReturnValuetoJSON();
			var tableInfo = tables[1];

			for (int i = 0; i < tableInfo.Count; i++)
			{
				var dataKey = tableInfo[i][0].ToString();
				var bro = Backend.GameData.GetMyData(dataKey, new Where());

				if (bro.IsSuccess())
				{
					var data = bro.FlattenRows();
					switch (dataKey)
					{
						case "StageUnlockData":
							stageUnlockData = new StageUnlockData(dataKey, data);
							dataCollection.Add(stageUnlockData);
							break;
					}
				}
				else
				{
					Debug.LogError("Failed to retrieve data for table " + dataKey + ": " + bro);
				}
			}

			Debug.Log("All tables data retrieval completed.");
		}
		else
		{
			Debug.LogError("Failed to get table list: " + tableList);
		}

		UserDataCheck();
	}

	public void UserDataCheck()
	{
		if (stageUnlockData == null)
		{
			stageUnlockData = new StageUnlockData("StageUnlockData");
			dataCollection.Add(stageUnlockData);
		}
	}

	public void UserDataAllSave()
	{
		foreach (var data in dataCollection)
		{
			data.UpdateFlagSet();
		}
	}
}

