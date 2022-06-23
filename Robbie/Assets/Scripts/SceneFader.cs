using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    private Animator anim;
    private int fadeID;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        fadeID = Animator.StringToHash("Fade");
        GameManager.RegisterSceneFader(this);
    }

    public void FadeOut()
    {
        anim.SetTrigger(fadeID);
    }
    
}
