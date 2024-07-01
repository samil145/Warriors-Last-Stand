using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_Controller : MonoBehaviour
{
    Animator animator;
    float directionX, directionZ;
    CharacterController charachterController;
    Vector3 moveDirection = Vector3.zero;
    float speed;

    static internal int score;

    static internal int health_soldier = 5;

    float angle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        charachterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxis("Horizontal");
        directionZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(directionX, 0, directionZ);
        animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"),directionX,Time.deltaTime * 10));
        //animator.SetFloat("Vertical", directionZ);
        animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), directionZ, Time.deltaTime * 20));

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Attack",true);           
        }

        if (Input.GetKey(KeyCode.M) && UI_Manager.shieldFlag)
        {
            animator.SetBool("Shield", true);
        }
        else
        {
            animator.SetBool("Shield", false);
        }


    }

    private void FixedUpdate()
    {
        if (!UI_Manager.soldierFreeze)
        {
            if (!animator.GetBool("Shield") && !animator.GetBool("Attack"))
            {
                Vector3 move = (transform.right.normalized * directionX + transform.forward.normalized * directionZ) * Time.fixedDeltaTime * 2.5f;
                move.y -= 9.81f * Time.fixedDeltaTime;
                charachterController.Move(move);
            }

            var mousepos = Input.mousePosition;
            var capsulePos = Camera.main.WorldToScreenPoint(transform.position);
            var dir = capsulePos - mousepos;

            angle = Mathf.Lerp(angle, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, Time.fixedDeltaTime * 7);
            //angle = Mathf.Lerp(angle, Time.fixedDeltaTime);

            transform.rotation = Quaternion.AngleAxis(-angle - 90, Vector3.up);
        }
    }

    public void AttackFalse()
    {
        animator.SetBool("Attack", false);
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.tag == "HealthBooster")
    //    {
    //        hit.gameObject.SetActive(false);
    //        if (health_soldier < 5)
    //        {
    //            health_soldier++;
    //            UI_Manager.soldier_health_flag = true;
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "HealthBooster")
    //    {
    //        other.gameObject.SetActive(false);
    //        if (health_soldier < 5)
    //        {
    //            health_soldier++;
    //            UI_Manager.soldier_health_flag = true;
    //        }
    //    }
    //}

    //public void SoldierDamage()
    //{
    //    animator.SetBool("Impact",true);
    //    Invoke("ImpactFalse",0.2f);
    //}

    //void ImpactFalse()
    //{
    //    animator.SetBool("Impact", false);
    //}
}
