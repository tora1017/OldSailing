using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipControl : MonoBehaviour
{
    [SerializeField] private Vector3 touchStartPos;
    [SerializeField] private Vector3 touchEndPos;
    [SerializeField] private float kando = 30;
    [SerializeField] private string upDown = "none";
    [SerializeField] private string leftRight = "none";
    [SerializeField] private string onTouch = "none";

    void Update()
    {
        Flick();
    }

    void Flick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            onTouch = "yes!!";
        }
        else
        {
            onTouch = "none";
			touchStartPos = new Vector3(0.0f, 0.0f, 0.0f);
			touchEndPos = new Vector3(0.0f, 0.0f, 0.0f);
		}

        if (Input.GetKey(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            GetDirection();
            shori();
        }
    }
    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;

        if (kando < directionX)
        {
            //右向きにフリック
            leftRight = "right";
        }
        else if (-kando > directionX)
        {
            //左向きにフリック
            leftRight = "left";
        }

        if (kando < directionY)
        {
            //上向きにフリック
            upDown = "up";
        }
        else if (-kando > directionY)
        {
            //下向きのフリック
            upDown = "down";
        }
    }
    void shori()
    {
        if (leftRight == "left")
        {
            this.transform.Rotate(0.0f, -0.5f, 0.0f);
        }
        else if (leftRight == "right")
        {
            this.transform.Rotate(0.0f, 0.5f, 0.0f);
        }
    }

}
