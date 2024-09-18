using UnityEngine;

public class KSJSoundManager : MonoBehaviour
{
	public static KSJSoundManager Instance; // 어디에서나 쓸 수 있게 정적 메모리에 담음

	[Header("BGM")]
	public AudioClip bgmClip;
	public float bgmVolume;
	AudioSource bgmPlayer;
	AudioHighPassFilter bgmEffect;

	[Header("SFX")]
	public AudioClip[] sfxClips;
	public float sfxVolume;
	public int channels;//동시 다발적으로 많은 사운드를 내기 위해
	AudioSource[] sfxPlayers;
	int channelIndex;//현재 재생중인 채널 인덱스

	public enum Sfx //레벨업을 3으로 지정하면 다음에 나오는 lose는 4가 된다. 마지막 요소의 수가 랭스 -1 이면 맞음

	{
		Positive,
		Negative, 
		Hit,
		Jump,
		Slide, 
		Glide,
		Destroy,
		Booster,
		Invincible,
		Shield,
		Heal,
		Firfly
	}

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // 씬 전환 시 객체 유지
		}
		else
		{
			Destroy(gameObject); // 중복된 인스턴스 제거
		}
		Init();

	}

	public void Init()
	{
		// 배경음 플레이어 초기화
		GameObject bgmObject = new GameObject("BgmPlayer");
		bgmObject.transform.parent = transform;
		bgmPlayer = bgmObject.AddComponent<AudioSource>();
		bgmPlayer.playOnAwake = true;
		bgmPlayer.loop = true;
		bgmPlayer.volume = bgmVolume;
		bgmPlayer.clip = bgmClip;
		bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();


		// 효과음 플레이어 초기화
		GameObject sfxObject = new GameObject("SfxPlayer");
		sfxObject.transform.parent = transform;
		sfxPlayers = new AudioSource[channels];
		for (int index = 0; index < sfxPlayers.Length; index++)
		{
			sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
			sfxPlayers[index].playOnAwake = false;
			sfxPlayers[index].bypassListenerEffects = true;
			sfxPlayers[index].volume = sfxVolume;
		}
		bgmPlayer.Play();
	}

	public void PlaySfx(Sfx sfx)
	{
		for (int index = 0; index < sfxPlayers.Length; index++)
		{
			
			int loopIndex = (index + channelIndex) % sfxPlayers.Length;


			channelIndex = loopIndex;
			sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
			sfxPlayers[loopIndex].Play();
			break;
		}

	}
	public void StopBGM()
	{
		if (bgmPlayer != null && bgmPlayer.isPlaying)
		{
			bgmPlayer.Stop();
		}
	}
}
