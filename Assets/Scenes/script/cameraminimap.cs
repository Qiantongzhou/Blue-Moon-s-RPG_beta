using UnityEngine;

public class cameraminimap : MonoBehaviour
{
    public float targetsize;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (player.GetComponent<character>() is not null)
        {
            if (player.GetComponent<character>().isrunning())
            { targetsize = 40; }
            else
            { targetsize = 20; }
        }

        if (transform.GetComponent<Camera>().orthographicSize > targetsize + 0.5)
        {
            transform.GetComponent<Camera>().orthographicSize -= targetsize * Time.deltaTime;
        }
        else if (transform.GetComponent<Camera>().orthographicSize < targetsize - 0.5)
        {
            transform.GetComponent<Camera>().orthographicSize += targetsize * Time.deltaTime;
        }
    }
}
