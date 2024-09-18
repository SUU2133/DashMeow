using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
	Button button;
    void Start()
    {
		button = GetComponent<Button>();
		button.onClick.AddListener(PlaySound);
	}

    public void PlaySound(){
		KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Positive);
	}
}
