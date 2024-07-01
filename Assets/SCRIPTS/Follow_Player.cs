using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    public Transform player;

    Vector3 startCameraDistance = Vector3.zero;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + new Vector3(0, 0.36f, -0.34f) * player.transform.forward.normalized;
        transform.position = (player.transform.position);
        //transform.position = Vector3.Lerp(transform.position,player.transform.position,Time.deltaTime/100);

        if (Soldier_Controller.health_soldier <= 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(50, player.transform.rotation.eulerAngles.y, transform.rotation.z), Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(16.68f, player.transform.rotation.eulerAngles.y, transform.rotation.z);
        }
    }
}

