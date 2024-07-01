using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    Animator soldierAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        collision.gameObject.GetComponent<Zombie>().health--;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && soldierAnimator.GetBool("Attack") && !(other.gameObject.GetComponent<Animator>().GetBool("Die") || other.gameObject.GetComponent<Animator>().GetBool("Die2")))
        {
            if (UI_Manager.zombieOneAttack)
            {
                other.gameObject.GetComponent<Zombie>().health = 0;
            }
            else
            {
                other.gameObject.GetComponent<Zombie>().health--;
            }
        }
    }
}
