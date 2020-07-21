using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTaunt : MonoBehaviour
{
    // Start is called before the first frame update
    int frameCount;
    bool flipped;
    void Start()
    {
        frameCount = 0;
        flipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("I'm taunting");
        flipSprite();
    }

    void flipSprite()
    {
        frameCount = (frameCount + 1) % 10;
        if(frameCount == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = flipped;
            flipped = !flipped;
        }
    }

    public void StopTaunt()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            flipped = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
