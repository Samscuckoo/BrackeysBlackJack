using UnityEngine;

public class PickupTripleShoot : MonoBehaviour
{
    public float duration = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shoot Shoot = other.GetComponent<Shoot>();
            if (Shoot != null)
            {
                Shoot.ActivateTripleShot(duration);
            }
            Destroy(gameObject); 
        }
    }
}
