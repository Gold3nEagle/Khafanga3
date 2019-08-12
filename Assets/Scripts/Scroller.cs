using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed;

    // Update is called once per frame
    void Update()
    {
         transform.Translate(new Vector2(scrollSpeed * Time.deltaTime, 0));
    }
}
