using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddForce : MonoBehaviour
{
    private Rigidbody2D rd;
    public float ForceAmountX = 0.0f;
    public float ForceAmountY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rd.AddForce(new Vector2(ForceAmountX, ForceAmountY));
    }
}
