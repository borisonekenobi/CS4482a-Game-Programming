using System.Collections;
using Borisonekenobi.Localization;
using TMPro;
using UnityEngine;

public class FinishSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text finishText;
    [SerializeField] private float delay = 3f;

    private void Start()
    {
        finishText.text = Localization.Get("finish_release");
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);

        SceneFader.Instance.FadeToScene("Scenes/Score");
    }
}
