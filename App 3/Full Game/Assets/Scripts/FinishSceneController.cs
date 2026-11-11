using System.Collections;
using UnityEngine;

public class FinishSceneController : MonoBehaviour
{
    [SerializeField] private float delay = 3f;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);

        SceneFader.Instance.FadeToScene("Scenes/Score");
    }
}
