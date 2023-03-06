using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform newPosition;
    public Animator doorFXAnimator;
    public Animator gateFXAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            StartCoroutine(TeleportToNewLocation(collision));
    }

    IEnumerator TeleportToNewLocation(Collider2D collision)
    {
        doorFXAnimator.Play("Teleport");
        collision.GetComponent<SpriteRenderer>().enabled = false;
        collision.GetComponent<PlayerBehaviour>().enabled = false;
        collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        yield return new WaitForSeconds(2);

        collision.transform.position = newPosition.position;
        gateFXAnimator.Play("Teleport");
        
        yield return new WaitForSeconds(1);
        collision.GetComponent<SpriteRenderer>().enabled = true;
        collision.GetComponent<PlayerBehaviour>().enabled = true;
    }
}
