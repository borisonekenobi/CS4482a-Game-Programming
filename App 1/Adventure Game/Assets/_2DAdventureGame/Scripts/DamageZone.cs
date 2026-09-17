using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        var controller = other.GetComponent<PlayerController>();

        if (controller != null) controller.ChangeHealth(-1);
    }
}
