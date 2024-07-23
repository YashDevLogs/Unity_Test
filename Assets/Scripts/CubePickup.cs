using UnityEngine;

public class CubePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.CubeCollected();
            Destroy(gameObject);
        }
    }
}
