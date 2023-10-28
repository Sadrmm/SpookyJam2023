using UnityEngine;

public class Forces : MonoBehaviour
{
    public static void PushObject(Collider collider, float force, Vector3 origin, float radius)
    {
        Rigidbody rb = collider.GetComponent<Rigidbody>();

        if (rb == null) {
            return;
        }

        rb.AddExplosionForce(force, origin, radius);
    }
}
