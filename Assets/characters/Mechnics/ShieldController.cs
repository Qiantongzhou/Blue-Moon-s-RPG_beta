using UnityEngine;
public class ShieldController : MonoBehaviour
{
    [SerializeField]
    private GameObject Shield;

    private Animator myAnimator;
    private const string AnimatorParameter_BlockHit = "Block Hit";
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void ShieldOn()
    {
        Shield.GetComponent<Collider>().enabled = true;
    }
    private void ShieldOff()
    {
        Shield.GetComponent<Collider>().enabled = false;
    }
    public void BlockHit()
    {
        myAnimator.SetTrigger(AnimatorParameter_BlockHit);
    }
}