using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaonCameraController : MonoBehaviour
{

    public float smooth = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool facingLeft;
    private Transform player;
    private int lastX;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        HeroFind(facingLeft);
    }

    public void HeroFind(bool playerfacingLeft)
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        if (playerfacingLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, transform.position.y + offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, transform.position.y + offset.y, transform.position.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) facingLeft = false; else if (currentX < lastX) facingLeft = true;
            lastX = Mathf.RoundToInt(player.position.x);

            Vector3 target;
            if (facingLeft)
            {
                target = new Vector3(player.position.x - offset.x, transform.position.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, smooth * Time.deltaTime);
            transform.position = currentPosition;
        }
    }

   
}
