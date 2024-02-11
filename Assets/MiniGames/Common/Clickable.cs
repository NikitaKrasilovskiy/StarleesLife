using System;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    public UnityEvent onClick;
    
    private Animator anim;

    public bool chosen;

    protected virtual void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClick.Invoke();
        }
    }

    public void Chosen(bool value)
    {
        anim.SetBool("Chosen", value);
        chosen = value;
    }
}
