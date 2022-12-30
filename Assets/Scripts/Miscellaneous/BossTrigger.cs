using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private Tile groundTile;
    [SerializeField] private Tilemap groundTilemap;
    Ogre ogre;
    GameManager GM;
    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GM = FindObjectOfType<GameManager>();
        ogre = FindObjectOfType<Ogre>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.isFightingBoss = true;
            ogre.fightMode = true;
            groundTilemap.SetTile(new Vector3Int(223,-6,0), groundTile);
            groundTilemap.SetTile(new Vector3Int(223,-5,0), groundTile);
            FindObjectOfType<SoundManager>().Stop("BackMusic");
            FindObjectOfType<SoundManager>().Play("BattleTheme");
            gameObject.SetActive(false);
        }
    }

    public void RefreshTile()
    {
        groundTilemap.SetTile(new Vector3Int(223,-6,0), null);
        groundTilemap.SetTile(new Vector3Int(223,-5,0), null);
    }
}
