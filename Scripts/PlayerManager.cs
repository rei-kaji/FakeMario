using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] LayerMask blockLayer;
    public enum DIRECTION_TYPE
    {
        STOP,
        LEFT,
        RIGHT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    Rigidbody2D rigidbody2D;

    float speed;
    float jumpPower = 600;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");

        if (x == 0)
        {
            // Stop
            direction = DIRECTION_TYPE.STOP;
        }
        else if (x > 0)
        {
            // Move right
            direction = DIRECTION_TYPE.RIGHT;
        }
        else if (x < 0)
        {
            // Move left
            direction = DIRECTION_TYPE.LEFT;
        }

        // Jump by space key && IsGround
        if (Input.GetKeyDown("space") && IsGround())
        {
            Jump();
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
                speed = 3;
                transform.localScale = new Vector3(3, 3, 3);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -3;
                transform.localScale = new Vector3(-3, 3, 3);
                break;
        }

        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    void Jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpPower);
    }

    bool IsGround()
    {
        // Get start/end point
        Vector3 leftStartPoint = transform.position - Vector3.right * 0.2f;
        Vector3 rightStartPoint = transform.position + Vector3.right * 0.2f;
        Vector3 endStartPoint = transform.position - Vector3.up * 0.1f;
        //Debug.DrawLine(leftStartPoint, endStartPoint);
        //Debug.DrawLine(rightStartPoint, endStartPoint);

        return Physics2D.Linecast(leftStartPoint, endStartPoint,blockLayer)
            || Physics2D.Linecast(rightStartPoint, endStartPoint, blockLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            Debug.Log("Game Over...");
            gameManager.GameOver();
        }
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Clear!!");
            gameManager.GameClear();
        }
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("Get Item!!");
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy!!");
            EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();
            
            if (this.transform.position.y > enemy.transform.position.y)
            {
                // UpSide
                // Delete Enemy
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                Jump();
                enemy.DestroyEnemy();
            }
            else
            {
                // Side
                // GameOver
                Destroy(this.gameObject);
                gameManager.GameOver();
            }
        }
    }
}
