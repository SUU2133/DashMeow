using UnityEngine;
using UnityEngine.UI;
public class OverlayButtonController : MonoBehaviour
{
	public Button OverlayButton;

	private void Start()
	{
		OverlayButton = GameObject.Find("/Canvas/Overlay Button").GetComponent<Button>();


	}

	public void OnClickOverlay()
	{
		Debug.Log("오버레이클릭");
		transform.localScale = new Vector3(0, 0, 0);
		OverlayButton.transform.localScale = new Vector3(0, 0, 0);
	}
}
