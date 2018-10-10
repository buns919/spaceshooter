using UnityEngine;

public class LaserBoundary : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(collision.gameObject);
    }
}
