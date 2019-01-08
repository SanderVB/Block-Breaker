using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] int pointsPerBlock = 1;

    //cached reference
    Level level;
    GameStatus gameStatus;

    //state variables
    [SerializeField] int hitPoints; //only serialized for debugging

    private void Start()
    {
        hitPoints   = hitSprites.Length;
        level       = FindObjectOfType<Level>();
        gameStatus  = FindObjectOfType<GameStatus>();
        if (tag == "Breakable")
            level.BlockCounter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HitHandler();
        }
    }

    private void HitHandler()
    {
        hitPoints--;
        if (hitPoints <= 0)
            DestroyBlock();
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = hitPoints - 1;
        if (hitSprites[spriteIndex] != null)
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        else
            Debug.LogError("Sprite missing from array " + gameObject.name);
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        TriggerExplosion();
        Destroy(gameObject);
        level.BlockSubtracter();
        gameStatus.AddToScore(pointsPerBlock);
    }

    private void TriggerExplosion()
    {
        Instantiate(explosionVFX, gameObject.transform.position, gameObject.transform.rotation);
        //GameObject effect = Instantiate(explosionVFX, gameObject.transform.position, gameObject.transform.rotation);
        //Destroy(effect, 1f);
    }
}
