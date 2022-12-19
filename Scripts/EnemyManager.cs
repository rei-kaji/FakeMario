using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    public enum DIRECTION_TYPE
    {
        STOP,
        LEFT,
        RIGHT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.RIGHT;

    Rigidbody2D enemyRigidbody2D;

    float speed;

    private void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        // Move right
        direction = DIRECTION_TYPE.LEFT;
    }

    bool IsGround()
    {
        Vector3 startVec = transform.position + transform.right * 0.8f * transform.localScale.x;
        Vector3 endVec = startVec - transform.up * 0.8f;
        //Debug.Log(Physics2D.Linecast(startVec, endVec, blockLayer));
        return Physics2D.Linecast(startVec,endVec, blockLayer);
    }

    void ChangeDirection()
    {
        if(direction == DIRECTION_TYPE.RIGHT)
        {
            direction = DIRECTION_TYPE.LEFT;
        }
        else if (direction == DIRECTION_TYPE.LEFT)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
    }

    private void Update()
    {
        if (!IsGround())
        {
            ChangeDirection();
        }

    }


    private void FixedUpdate()
    {
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 2;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -2;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

}
