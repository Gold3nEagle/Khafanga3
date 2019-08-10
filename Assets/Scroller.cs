using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{

    
      
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(-0.4f * Time.deltaTime, 0));
    }
}
