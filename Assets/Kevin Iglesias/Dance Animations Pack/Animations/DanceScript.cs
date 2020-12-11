using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceScript : MonoBehaviour
{
    public GameObject DancingMan;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1Key"))
        {
            print("Dance monkey 1");
            DancingMan.GetComponent<Animator>().Play("Dance1");
        }
        if (Input.GetButtonDown("2Key"))
        {
            DancingMan.GetComponent<Animator>().Play("Dance3");
        }
        if (Input.GetButtonDown("3Key"))
        {
            DancingMan.GetComponent<Animator>().Play("Dance5");
        }

    }
}
