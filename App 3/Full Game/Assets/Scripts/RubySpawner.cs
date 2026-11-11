using UnityEngine;

public class RubySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light sun;
    [SerializeField] private Ruby ruby;

    [Header("Settings")]
    [SerializeField] private float nightThreshold = 0.05f;

    private void Update()
    {
        if (!ruby)
        {
            Destroy(gameObject);
            return;
        }

        ruby.gameObject.SetActive(sun.transform.forward.y > nightThreshold);
    }
}
