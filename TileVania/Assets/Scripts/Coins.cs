using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSfx;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickUpSfx, gameObject.transform.position);
        FindObjectOfType<GameController>().Score(1);
        Destroy(gameObject);
    }
}
