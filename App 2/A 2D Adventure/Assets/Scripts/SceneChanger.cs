using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
	public static SceneChanger Instance;

	[SerializeField] private Image fadeImage;
	[SerializeField] private float fadeDuration = 1.0f;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			StartCoroutine(FadeIn());
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void MoveToScene(string sceneName)
	{
		StartCoroutine(TransitionSequence(sceneName));
	}

	private IEnumerator TransitionSequence(string sceneName)
	{
		yield return StartCoroutine(FadeOut());

		var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		while (asyncLoad?.isDone == false) yield return null;

		yield return StartCoroutine(FadeIn());
	}

	private IEnumerator FadeOut()
	{
		if (fadeImage == null) yield break;

		var timer = 0f;
		var color = fadeImage.color;

		while (timer < fadeDuration)
		{
			timer += Time.deltaTime;
			color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
			fadeImage.color = color;
			yield return null;
		}
	}

	private IEnumerator FadeIn()
	{
		if (fadeImage == null) yield break;

		var timer = 0f;
		var color = fadeImage.color;

		while (timer < fadeDuration)
		{
			timer += Time.deltaTime;
			color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
			fadeImage.color = color;
			yield return null;
		}
	}
}
