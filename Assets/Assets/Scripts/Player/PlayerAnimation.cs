using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //handle to animator
    private Animator _anim;
    private Animator _swordAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //assign handle to animator
        _anim = GetComponentInChildren<Animator>();
        _swordAnimator = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        //anim set float Move, move
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnimator.SetTrigger("SwordAnimation");
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
    }
}
