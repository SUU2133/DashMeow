using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
	public Slider hpSlider;
	public Player player;
    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
		hpSlider.value = player.health / player.maxHealth;
    }
}
