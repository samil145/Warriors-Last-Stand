using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            if (Soldier_Controller.health_soldier < 5)
            {
                Soldier_Controller.health_soldier++;
                UI_Manager.soldier_health_flag = true;
            }
            gameObject.SetActive(false);
        }
    }
}
