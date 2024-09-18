using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public void SFXPlay(string sfxName, AudioClip clip)
	{
		GameObject go = new GameObject(sfxName + "Sound");
		AudioSource audioSource = go.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.Play();
		Destroy(go, clip.length);
	}
}
