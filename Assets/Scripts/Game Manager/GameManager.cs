using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private Transform player;
    [SerializeField] private GameObject deathParticle1;
    [SerializeField] private GameObject deathParticle2;
    private float deathTime = 1;
    GameMaster gameMaster;
 
    public Vector2 lastCheckPointPos;

    GameObject[] diedEnemys;
    GameObject[] fallPlat;
    Ogre ogre;
    BossTrigger bossTrigger;
    GameObject[] potions;

    
    void Start()
    {
        potions = GameObject.FindGameObjectsWithTag("Potion");
        bossTrigger = FindObjectOfType<BossTrigger>();
        ogre = FindObjectOfType<Ogre>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        diedEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        fallPlat = GameObject.FindGameObjectsWithTag("FallPlat");
        gameMaster = FindObjectOfType<GameMaster>();
    }

    public void PlayerDie()
    {

        GameObject particle1 = Instantiate(deathParticle1, player.position, Quaternion.identity);
        particle1.transform.Rotate(-90,0,0);
        GameObject particle2 = Instantiate(deathParticle2, player.position, Quaternion.identity);
        particle2.transform.Rotate(-90,0,0);
        if(gameMaster.Dead())
        {
            EndGame();
        } else {
            Invoke(nameof(Respawn), deathTime);
        }

        foreach (GameObject potion in potions)
        {
            potion.gameObject.SetActive(true);
        }

        
        foreach (GameObject enemy in diedEnemys)
        {
            enemy.gameObject.SetActive(true);

            Poo poo = enemy.GetComponent<Poo>();
            if(poo != null)
            {
                poo.Respawn();
            }

            Goblin goblin = enemy.GetComponent<Goblin>();
            if(goblin != null)
            {
                goblin.Respawn();
            }

            OrcWarrior orcWarrior = enemy.GetComponent<OrcWarrior>();
            if(orcWarrior != null)
            {
                orcWarrior.Respawn();
            }

            OrcShaman orcShaman = enemy.GetComponent<OrcShaman>();
            if(orcShaman != null)
            {
                orcShaman.Respawn();
            }

            MaskedOrc maskedOrc = enemy.GetComponent<MaskedOrc>();
            if(maskedOrc != null)
            {
                maskedOrc.Respawn();
            }

            ogre.GetComponent<OgreStats>().Respawn();
            ogre.isCharging = false;
            bossTrigger.gameObject.SetActive(true);
            bossTrigger.RefreshTile();
        }

        foreach (GameObject plat in fallPlat)
        {
            plat.gameObject.SetActive(true);
            plat.GetComponent<FallGround>().Respawn();
        }
        
    }

    private void Respawn()
    {
        player.gameObject.SetActive(true);
        player.position = lastCheckPointPos;
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Finish()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(0);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
