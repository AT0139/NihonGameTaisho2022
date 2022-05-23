using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
    private Image image;
    private Animator animator;


    public void ChangeSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void ChangeMaterial(Material material)
    {
        image.material = material;
    }


    public void SelectBehavior()
    {
        animator.SetBool("IsSelect", true);
        image.material.SetFloat("_IsBlinking", 0);
    }

    public void DeselectBehavior()
    {
        animator.SetBool("IsSelect", false);
        image.material.SetFloat("_IsBlinking", 1);
    }


    private void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
