using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchAnimation : MonoBehaviour
{
    public Sprite[] Frames;
    public float FrameLife = 1; // in second;
    private float currentFrameLife;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentFrameLife += Time.deltaTime;
        if(currentFrameLife > FrameLife)
        {
            currentFrameLife = 0;
            int frameCount = Frames.Length;
            int nextFrameIndex = (int)(Random.value * frameCount);
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Frames[nextFrameIndex];
            spriteRenderer.flipX = Random.value > 0.5f;
            spriteRenderer.flipY = Random.value > 0.5f;
        }
    }
}
