using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] GameObject text;
    void Start ( ) {
        text.SetActive (false);
    }
    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Player")
            text.SetActive (true);
    }

    void OnTriggerExit2D (Collider2D other) {
        if (other.gameObject.tag == "Player")
            text.SetActive (false);
    }
}
