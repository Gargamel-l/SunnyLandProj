using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    float direction = 1;
    float minX, maxX;
    public bool facingRight = false;
    public float innedValue;

    void Start()
    {
        minX = transform.position.x;
        maxX = minX + innedValue;
    }

    void Update()
    {
        Vector3 currPos = transform.position;

        if (currPos.x >= maxX)
        {
            direction = -1;
        }
        if (currPos.x <= minX)
        {
            direction = 1;
        }
        transform.Translate(new Vector3(direction * 5f * Time.deltaTime, 0, 0));
        if ((direction < 0) && (facingRight))
        {
            Flip();
        }
        if ((direction > 0) && (!facingRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
