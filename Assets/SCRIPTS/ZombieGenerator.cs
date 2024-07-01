using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    private static ZombieGenerator _instance;

    public static ZombieGenerator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ZombieGenerator>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("ZombieGenerator");
                    _instance = obj.AddComponent<ZombieGenerator>();
                }
            }
            return _instance;
        }
    }

    [SerializeField]
    GameObject[] portals;

    [SerializeField]
    GameObject healthBooster;

    GameObject[] zombies = new GameObject[4]; 

    internal int zombieCount;

    int zombiePar;

    internal bool gameOver = false;

    internal bool waveChanged = false;
    internal bool zombieFlag = false;

    internal enum Wave
    {
        wave1,
        wave2,
        wave3,
        wave4
    }

    [SerializeField]
    internal GameObject soldier;

    [SerializeField]
    GameObject zombiePrefab;

    internal Wave wave;

    // Start is called before the first frame update
    void Start()
    {
        healthBooster.SetActive(false);
        StartCoroutine(waiter());
        waveChanged = true;
        wave = Wave.wave1;
        zombiePar = 2;
        Invoke("ZombieCreator",2f);
    }

    private void Update()
    {
        if (!UI_Manager.soldierFreeze)
        {
            if (wave == Wave.wave1 || wave == Wave.wave3)
            {
                for (int i = 0; i < 2; i++)
                {
                    portals[i].SetActive(true);
                }
                for (int i = 2; i < 4; i++)
                {
                    portals[i].SetActive(false);
                }
            }

            if (wave == Wave.wave2 || wave == Wave.wave4)
            {
                for (int i = 2; i < 4; i++)
                {
                    portals[i].SetActive(true);
                }
            }

            if (zombieFlag)
            {
                Invoke("ZombieCreator",1.5f);
                zombieFlag = false;
            }

            if (zombieCount == 2 && wave == Wave.wave1 || zombieCount == 2 && wave == Wave.wave3)
            {
                zombieCount = 0;
                zombiePar = 4;
                //Invoke("ZombieCreator", 3.5f);
                wave++;
                waveChanged = true;
                zombieFlag = true;
            }

            if (zombieCount == 4 && wave == Wave.wave2 || zombieCount == 4 && wave == Wave.wave4)
            {
                zombieCount = 0;
                if (wave == Wave.wave4)
                {
                    gameOver = true;
                }
                else
                {
                    zombiePar = 2;
                    //Invoke("ZombieCreator", 3.5f);
                    wave++;
                    waveChanged = true;
                    zombieFlag = true;
                }
            }
        }
    }

    void ZombieCreator()
    {
        for (int i = 0; i < zombiePar; i++)
        {
            if (zombies[i] != null)
            {
                var zombieForHealth = zombies[i].GetComponent<Zombie>();
                zombieForHealth.health = 2;
                zombies[i].transform.position = new Vector3(portals[i].transform.position.x, 0.05f, portals[i].transform.position.z);
                zombies[i].GetComponent<CharacterController>().enabled = true;
                zombies[i].GetComponent<CapsuleCollider>().enabled = false;
                zombies[i].SetActive(true);
                zombies[i].GetComponent<Animator>().Rebind();
                zombies[i].GetComponent<Animator>().Update(0f);
                zombies[i].GetComponent<Animator>().SetBool("Die", false);
                zombies[i].GetComponent<Animator>().SetBool("Die2", false);
                zombies[i].GetComponent<Animator>().SetBool("Attack", false);
            } else
            {
                zombies[i] = Instantiate(zombiePrefab, new Vector3(portals[i].transform.position.x, 0.05f, portals[i].transform.position.z), Quaternion.identity);
                var zombieForHealth = zombies[i].GetComponent<Zombie>();
                zombieForHealth.health = 2;
            }

            //if (wave == Wave.wave1 || wave == Wave.wave3)
            //{
            //    zombies[i].GetComponent<Zombie>().health = 2;
            //} else if (wave == Wave.wave2 || wave == Wave.wave4)
            //{
            //    zombies[i].GetComponent<Zombie>().health = 3;
            //}
        }
    }

    IEnumerator waiter()
    {
        var waitingSeconds = Random.Range(25,40);

        yield return new WaitForSeconds(waitingSeconds);
        healthBooster.transform.position = new Vector3(Random.Range(-9,9),1,Random.Range(-9,9));
        healthBooster.SetActive(true);
    }
}
