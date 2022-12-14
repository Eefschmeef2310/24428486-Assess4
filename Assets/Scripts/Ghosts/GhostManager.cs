using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostManager : MonoBehaviour
{
    public TextMeshProUGUI ghostTimer;
    public AudioSource backgroundMusic;
    public bool pelletMode = false;
    void Update()
    {
        
    }
    public void powerPellet()
    {
        pelletMode = true;
        StopAllCoroutines();
        StartCoroutine(scaredGhosts());
    }

    IEnumerator scaredGhosts()
    {
        ghostTimer.gameObject.SetActive(true);
        backgroundMusic.clip = backgroundMusic.GetComponent<AudioPlayer>().clips[2];
        backgroundMusic.Play();

        foreach(Transform child in transform)
        {
            child.GetComponent<ScareManager>().scaredState();
        }

        for(int i = 10; i > 3; i--)
        {
            ghostTimer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        foreach(Transform child in transform)
        {
            if(child.GetComponent<ScareManager>().scared)
            {
                Animator animator = child.GetComponent<Animator>();
                animator.enabled = true;
                animator.Play("BackToNormal");
            }   
        }
        for(int i = 3; i > 0; i--)
        {
            ghostTimer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        exitPelletMode();
    }

    public void exitPelletMode()
    {
        foreach(Transform child in transform)
        {
            if(child.GetComponent<ScareManager>().scared)
            {
                child.GetComponent<ScareManager>().scared = false;
                Animator animator = child.GetComponent<Animator>();
                animator.enabled = true;
                animator.Play("TurnOnTrails");
            }
        }
        ghostTimer.gameObject.SetActive(false);
        pelletMode = false;
        
        backgroundMusic.loop = true;
        backgroundMusic.clip = backgroundMusic.GetComponent<AudioPlayer>().clips[1];
        backgroundMusic.Play();
    }
}
