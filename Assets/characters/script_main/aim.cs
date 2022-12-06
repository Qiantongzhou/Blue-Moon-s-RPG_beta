using Unity.Mathematics;
using UnityEngine;

public class aim : MonoBehaviour
{
    //aim
    public Camera cam;
    public GameObject cam1;
    public GameObject cam2;
    public float maxlen;
    private Ray mouse;
    private Vector3 pos;
    private Vector3 direction;
    private quaternion rotation;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !GobalEvent.Pause_player_mouse_input)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            mouse = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(mouse.origin, mouse.direction, out hit, maxlen, 0))
            {
                rotatem(gameObject, hit.point);
            }
            else
            {
                var pos = mouse.GetPoint(maxlen);
                rotatem(gameObject, pos);
            }

        }
        else
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
    }
    void rotatem(GameObject x, Vector3 y)
    {
        direction = y - x.transform.position;
        rotation = Quaternion.LookRotation(direction);

        x.transform.localRotation = Quaternion.Lerp(x.transform.rotation, rotation, Time.deltaTime);
    }

}
