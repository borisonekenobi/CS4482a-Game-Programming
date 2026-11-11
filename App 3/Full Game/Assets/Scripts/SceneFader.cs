using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private bool _isFading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeToScene(string sceneName)
    {
        if (!_isFading) StartCoroutine(TransitionRoutine(sceneName));
    }

    private IEnumerator TransitionRoutine(string sceneName)
    {
        _isFading = true;
        fadeCanvasGroup.blocksRaycasts = true;

        yield return StartCoroutine(Fade(1f));

        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (asyncLoad is { isDone: false }) yield return null;

        yield return null;

        yield return StartCoroutine(Fade(0f));

        fadeCanvasGroup.blocksRaycasts = false;
        _isFading = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        var startAlpha = fadeCanvasGroup.alpha;
        var elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            var safeDelta = Mathf.Min(Time.unscaledDeltaTime, 0.05f);
            elapsedTime += safeDelta;

            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
