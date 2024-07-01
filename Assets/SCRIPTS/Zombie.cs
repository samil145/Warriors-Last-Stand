using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Animator animator;
    CharacterController charachterController;

    [SerializeField]
    GameObject soldier;

    ZombieGenerator zombieGenerator;

    float zombieSpeed;

    internal int health;

    // Start is called before the first frame update
    void Start()
    {
        zombieGenerator = ZombieGenerator.Instance;
        charachterController = GetComponent<CharacterController>();
        soldier = GameObject.Find("soldier");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieGenerator.wave == ZombieGenerator.Wave.wave1 || zombieGenerator.wave == ZombieGenerator.Wave.wave2)
        {
            zombieSpeed = 1;
            animator.SetBool("Run", false);
        }
        else
        {
            zombieSpeed = 4;
            animator.SetBool("Run", true);
        }

        if (!animator.GetBool("Die") && !animator.GetBool("Die2"))
        {
            transform.forward = soldier.transform.position - transform.position;
        }
        

        if (health == 0 && !(Soldier_Controller.health_soldier <= 0))
        {
            Soldier_Controller.score++;
            zombieGenerator.zombieCount++;
            charachterController.enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            gameObject.transform.position = new Vector3(transform.position.x, 0.05f, transform.position.z);

            var randomNumber = Random.Range(0,4);

            if (randomNumber == 0 || randomNumber == 1)
            {
                animator.SetBool("Die",true);
            } else
            {
                animator.SetBool("Die2", true);
            }
            health = -1;
            Invoke("setZombieFalse",3);
        }

  
        //Debug.Log(health);
    }

    private void FixedUpdate()
    {
        if (!animator.GetBool("Attack") && !animator.GetBool("Die") && !animator.GetBool("Die2"))
        {
            Vector3 move = transform.forward.normalized * Time.fixedDeltaTime * zombieSpeed / 2.5f;
            move.y -= 9.81f * Time.fixedDeltaTime;
            charachterController.Move(move);


            //transform.position += transform.forward.normalized * Time.fixedDeltaTime / 2.5f;
        }

        if ((transform.position - soldier.transform.position).magnitude < 1.5f)
        {
            animator.SetBool("Attack",true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    void setZombieFalse()
    {
        gameObject.SetActive(false);
    }

    public void SoldierDamage()
    {
        if ((transform.position - soldier.transform.position).magnitude < 1.5f && !soldier.GetComponent<Animator>().GetBool("Shield"))
        {
            Soldier_Controller.health_soldier--;
            UI_Manager.soldier_health_flag = true;
            soldier.GetComponent<Animator>().SetBool("Impact", true);
            Invoke("ImpactFalse", 0.2f);
        }
    }

    void ImpactFalse()
    {
        soldier.GetComponent<Animator>().SetBool("Impact", false);
    }
}


