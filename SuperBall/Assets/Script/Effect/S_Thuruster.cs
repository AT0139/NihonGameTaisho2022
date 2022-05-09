using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Thuruster : MonoBehaviour
{
    //  Shader 
    [SerializeField] float minSpeed = 30.0f;
    private MeshRenderer mesh;
    private float moveNoiseSpeed;

    //  Rotation
    private Rigidbody2D player;
    private Vector2 last, curr;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        player = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //  Shader
        SetShaderValues();

        SetThrusterRotate();
    }
    private void SetThrusterRotate()
    {
        curr = player.transform.position;

        float angleDir = GetAngle(curr, last);
        Debug.Log(angleDir);
        //Vector3 dir = new Vector3(Mathf.Cos(angleDir), Mathf.Sin(angleDir), 0.0f);

        //transform.localEulerAngles = dir;
        //transform.localRotation = Quaternion.Euler(dir);
        transform.localRotation = Quaternion.AngleAxis(angleDir, Vector3.forward);

        last = player.transform.position;
    }

    float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 dt = end - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;
        return degree;
    }


    private void SetShaderValues()
    {
        //  Shader 
        if (S_EffectBubble.movementSpeed < minSpeed)
        {
            moveNoiseSpeed *= 0.9f;
        }
        else
        {
            moveNoiseSpeed = S_EffectBubble.movementSpeed;
        }

        mesh.material.SetFloat("_Emission", moveNoiseSpeed);
        mesh.material.SetFloat("_BubbleSpeed", moveNoiseSpeed / 100.0f);
    }
}
