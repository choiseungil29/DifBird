using UnityEngine;
using System.Collections;

public class HitOfGuard : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Animator : " + animator);
        Debug.Log(animator.GetBool("IsApproach"));
	}

    public void AnimHit()
    {
        if (animator)
        {
            Debug.Log("Success AnimHit");
            animator.SetBool("IsApproach", true);
        }
        else
        {
            Debug.Log("Failed AnimHit");
        }
    }

    public void StopAnimHit()
    {
        if (animator)
        {
            Debug.Log("Success StopAnimHit");
            animator.SetBool("IsApproach", false);
        }
        else
        {
            Debug.Log("Failed StopAnimHit");
        }
    }
}
