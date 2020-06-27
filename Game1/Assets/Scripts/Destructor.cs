using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "PowerUp")
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Life")
        {
            Destroy(col.gameObject);
        }

    }
}
