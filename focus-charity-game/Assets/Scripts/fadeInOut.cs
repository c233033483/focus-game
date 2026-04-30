using System.Collections;
using UnityEngine;

public class fadeInOut : MonoBehaviour
{

    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void fadeIn()
    {
        StartCoroutine(fadeCount());
        
    }

    public void fadeOut()
    {
        
        anim.SetBool("fade", false);
    }

    IEnumerator fadeCount()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("fade", true);
    }
}
