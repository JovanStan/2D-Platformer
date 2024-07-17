using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	[SerializeField] private Image heart1, heart2, heart3;

	public TextMeshProUGUI gemsText;
	public Image fadeImage;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		fadeImage.DOFade(0, 3);
	}

	public void UpdateHealth()
	{
		switch (PlayerController.instance.currentHealth)
		{
			case 3:
				heart1.gameObject.SetActive(true);
				heart2.gameObject.SetActive(true);
				heart3.gameObject.SetActive(true);
				break;
			case 2:
				heart1.gameObject.SetActive(true);
				heart2.gameObject.SetActive(true);
				heart3.gameObject.SetActive(false);
				break;
			case 1:
				heart1.gameObject.SetActive(true);
				heart2.gameObject.SetActive(false);
				heart3.gameObject.SetActive(false);
				break;
			case 0:
				heart1.gameObject.SetActive(false);
				heart2.gameObject.SetActive(false);
				heart3.gameObject.SetActive(false);
				break;
		}
	}
}
