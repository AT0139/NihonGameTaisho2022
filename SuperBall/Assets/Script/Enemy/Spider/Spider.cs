using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public  GameObject              player          { get; private set; }
    public  GameObject              explosion       { get; private set; }
    public  StateContext<Spider>    m_StateContext  { get; private set; }
    public  Vector3                 initialPosition { get; private set; }
    [SerializeField] 
    private float                   movementSpeed;
    public  float                   getSpeed
    {
                get { return movementSpeed; }
        private set {                       }
    }
    List<GameObject> contactList;
    Rigidbody2D rb;
    int colcount = 0;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        if (!(player = GameObject.FindGameObjectWithTag("Player")))
            Debug.LogError("Playerタグを持ったオブジェクトが存在しません。");
        m_StateContext                = new StateContext<Spider>(this);
        m_StateContext.m_CurrentState = new Sleep_SpiderColonel();
        m_StateContext.m_CurrentState.Initialize(this);

        contactList = new List<GameObject>();
        rb = transform.GetComponent<Rigidbody2D>();

        if (movementSpeed == 0) Debug.LogError("インスペクタでSpiderのmovementSpeedを設定してさい。");
    }

    // Update is called once per frame
    void Update()
    {
        m_StateContext.Update();
    }

    private void FixedUpdate()
    {
        if (colcount == 0)
        {
            rb.gravityScale = 1;
            //Vector2 vel = new Vector2(1, 1);
            //vel += Physics2D.gravity * Time.fixedDeltaTime; // In PhysX, Acceleration ignores mass
            //float rigidbodyDrag = Mathf.Clamp01(1.0f - (0 * Time.fixedDeltaTime));
            //vel *= rigidbodyDrag;
            //transform.position += new Vector3(vel.x, vel.y, 0) * Time.fixedDeltaTime;
        }
        else
        {
            rb.gravityScale = 0;
        }
    }

    public void Destroy()
    {
        Instantiate(explosion, transform.position, new Quaternion());
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        colcount++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        colcount--;
    }
}
