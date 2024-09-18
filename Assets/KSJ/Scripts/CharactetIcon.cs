using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharactetIcon : MonoBehaviour
{
	// Start is called before the first frame update
	public List<Image> skinIcons = new List<Image>();
	public Image skinIcon;
	// Update is called once per frame

	private void Start()
	{
		skinIcon = GetComponent<Image>();
	}
	void Update()
    {
		skinIcon.sprite = skinIcons[DataManager.Instance.skinID].sprite;

	}

	
}
