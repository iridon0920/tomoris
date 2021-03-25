using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private float moveWaitSecond = 0.2f;
    private bool isWaitMove = false;

    private float inputHorizontal;
    private float inputVertical;

    readonly Vector2 ADD_X = new Vector2(1f, 0);
    readonly Vector2 ADD_Y = new Vector2(0, 1f);
    private Vector2 addPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     addPosition = -ADD_X;
        // }
        // else if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     addPosition = ADD_X;
        // }
        // else if (Input.GetKey(KeyCode.DownArrow))
        // {
        //     addPosition = -ADD_Y;
        // }
    }

    void FixedUpdate()
    {
        if (!isWaitMove & (inputHorizontal != 0 || inputVertical != 0))
        {
            StartCoroutine(MoveNewPosition());
        }
    }


    private IEnumerator MoveNewPosition()
    {
        isWaitMove = true;

        Vector2 movePosition = transform.position;
        movePosition.x += 1.0f * inputHorizontal;
        movePosition.y += 1.0f * inputVertical;
        transform.position = movePosition;

        yield return new WaitForSeconds(moveWaitSecond);
        isWaitMove = false;
    }
}
