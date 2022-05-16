using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderNet : MonoBehaviour
{
    [SerializeField]
    readonly private int INPUTS_MAX = 15;
    
    [SerializeField]
    private float       speedResistance;
    [SerializeField]
    private GameObject  explosion;
    private GameObject  player;
    private Vector2     previousInput;
    private Vector2[]   inputs;
    private int         inputsCount                 = 0;
    public  bool        onNet { get; private set; } = false;
    private Vector3     initialLocalPosition;
    private Vector3     initialAngle;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputs = new Vector2[INPUTS_MAX];
        initialLocalPosition = transform.position;
        initialAngle         = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        IntialTransform();
        if (onNet)
        {
            if (player.GetComponent<Player>().move != previousInput && player.GetComponent<Player>().move.magnitude != 0) // 前回の入力と同じなら無視
            {
                if(inputsCount < INPUTS_MAX)
                {
                    previousInput = inputs[inputsCount] = player.GetComponent<Player>().move;
                    ++inputsCount;
                }
                else // 入力が超えたんで破壊
                {
                    onNet = false;
                    Destroy();
                    player.GetComponent<Rigidbody2D>().drag = 0;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && 
            collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > speedResistance)
        {
            Destroy();
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            onNet = true;
            collision.gameObject.GetComponent<Rigidbody2D>().drag = 30;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().drag = 0;
            onNet = false;
        }
    }

    private void Destroy()
    {
        Instantiate(explosion, transform.position, new Quaternion());
        Destroy(gameObject);
    }

    private void IntialTransform()
    {
        transform.position      = initialLocalPosition;
        transform.eulerAngles   = initialAngle;
    }
}
