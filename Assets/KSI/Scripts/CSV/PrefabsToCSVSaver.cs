using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PrefabsToCSVSaver : MonoBehaviour
{
	public List<GameObject> prefabList;
	private static Dictionary<string, int> typeIds = new Dictionary<string, int>();

	private void Start()
	{
		string filePath = "Assets/Resources/prefabs.csv";
		List<string> lines = new List<string>();
		lines.Add("ItemID,ItemType,PositionX,PositionY,PositionZ");

		foreach (GameObject prefab in prefabList)
		{
			if (prefab.transform.parent == null)
			{
				ProcessChildGameObject(prefab, lines);
			}
		}

		File.WriteAllLines(filePath, lines);
	}

	private void ProcessChildGameObject(GameObject obj, List<string> lines)
	{
		foreach (Transform child in obj.transform)
		{
			ProcessGameObject(child.gameObject, lines);
			ProcessChildGameObject(child.gameObject, lines);
		}
	}

	private void ProcessGameObject(GameObject obj, List<string> lines)
	{
		string type = obj.name.Split('(')[0].Trim();
		if (!typeIds.ContainsKey(type))
		{
			typeIds[type] = typeIds.Count + 1;
		}

		lines.Add($"{typeIds[type]},{type},{obj.transform.position.x},{obj.transform.position.y},{obj.transform.position.z}");
	}
}