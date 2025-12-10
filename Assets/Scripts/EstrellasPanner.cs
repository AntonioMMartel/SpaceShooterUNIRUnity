using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EstrellasPanner : MonoBehaviour
{
    [SerializeField] float scrollSpeedX = 0.5f;
    [SerializeField] float scrollSpeedY = 0.5f;
    public Texture starTexture; // The starfield texture (optional if already assigned)

    private Material material;
    private Vector2 startOffset;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        material = spriteRenderer.material;
        startOffset = material.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {
        // Create the panning effect by updating the texture's offset
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        material.mainTextureOffset = new Vector2(startOffset.x + offsetX, startOffset.y);
        material.mainTextureOffset = new Vector2(startOffset.x, startOffset.y + offsetY);
    }
}
