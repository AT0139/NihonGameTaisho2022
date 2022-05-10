using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject player { get; private set; }
    public GameObject explosion { get; private set; }
    public StateContext<Spider> m_StateContext { get; private set; }
    public Vector3 initialPosition { get; private set; }
    [SerializeField] private float movementSpeed;
    public float getSpeed{
        get {
            return movementSpeed;
        }

        private set { }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        if (!(player = GameObject.FindGameObjectWithTag("Player")))
            Debug.LogError("Playerタグを持ったオブジェクトが存在しません。");
        m_StateContext                = new StateContext<Spider>(this);
        m_StateContext.m_CurrentState = new Sleep_SpiderColonel();
        m_StateContext.m_CurrentState.Initialize(this);

        if (movementSpeed == 0) Debug.LogError("インスペクタでSpiderのmovementSpeedを設定してさい。");
    }

    // Update is called once per frame
    void Update()
    {
        m_StateContext.Update();
    }

    public void Destroy()
    {
        Instantiate(explosion, transform.position, new Quaternion());
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "SpiderNet")
        {
            //transform.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //transform.GetComponent<Rigidbody2D>().simulated = true;
    }
}
