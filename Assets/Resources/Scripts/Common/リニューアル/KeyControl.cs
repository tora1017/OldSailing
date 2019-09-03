using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour
{


    void Update()
    {
        shori();
    }

    void shori()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.transform.Rotate(0.0f, -10.0f, 0.0f);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.transform.Rotate(0.0f, 10.0f, 0.0f);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.transform.Rotate(0.0f, -1.0f, 0.0f);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.transform.Rotate(0.0f, 1.0f, 0.0f);
            }

        }
    }
}
