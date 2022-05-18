using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Redzone : MonoBehaviour
{
    [SerializeField]
    private PlayerLife playerLife;

    RawImage zoneSprite;

    private float zoneAlfa;
    private bool Alfaswitch;

    public float AlfaSpeed;
    public float AlfaMax;

    void Start()
    {
        zoneSprite = GetComponent<RawImage>();

        zoneSprite.color = new Color(1, 1, 1, 0);

        zoneAlfa = 0;

        Alfaswitch = false;
    }

    void Update()
    {
        if(playerLife.GetLife() == 1)
        {
            if (Alfaswitch)
            {
                zoneAlfa += AlfaSpeed;
            }
            else
            {
                zoneAlfa -= AlfaSpeed;
            }

            zoneSprite.color = new Color(1, 1, 1, zoneAlfa);

            if (zoneAlfa > AlfaMax)
            {
                Alfaswitch = false;
            }
            else if (zoneAlfa < 0)
            {
                Alfaswitch = true;
            }
        }
        else
        {
            zoneSprite.color = new Color(1, 1, 1, 0);

            zoneAlfa = 0;
        }
    }
}
