using System;
using System.Collections.Generic;
using UnityEngine;

public class CSVMapRoader : MonoBehaviour
{
	private void Awake()
	{
		List<Dictionary<string, object>> data = CSVReader.Read("Item");

		for (var i = 0; i < data.Count; i++)
		{
			float positionX = Convert.ToSingle(data[i]["PositionX"]);
			float positionY = Convert.ToSingle(data[i]["PositionY"]);
			float positionZ = Convert.ToSingle(data[i]["PositionZ"]);

			Vector3 position = new Vector3(positionX, positionY, positionZ);

			string itemType = data[i]["ItemType"].ToString();
			GameObject itemPrefab = Resources.Load<GameObject>(itemType);

			if (itemPrefab != null)
			{
				Instantiate(itemPrefab, position, Quaternion.identity);
			}
			else
			{
				Debug.LogError("Resources에 " + itemType + "이 없음");
			}

			//Debug.Log("ItemID " + data[i]["ItemID"] + " " +
			//		"ItemType " + data[i]["ItemType"] + " " +
			//		"Value " + data[i]["Value"] + " " +
			//		"PositionX " + data[i]["PositionX"] + " " +
			//		"PositionY " + data[i]["PositionY"] + " " +
			//		"PositionZ " + data[i]["PositionZ"]);
		}
	}
}