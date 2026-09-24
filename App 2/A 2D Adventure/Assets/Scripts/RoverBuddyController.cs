using UnityEngine;

public class RoverBuddyController : MonoBehaviour
{
    [SerializeField] private StopwatchController stopwatchController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        stopwatchController.StopStopwatch();
        SceneChanger.Instance.MoveToScene("Leaderboard");
    }
}
