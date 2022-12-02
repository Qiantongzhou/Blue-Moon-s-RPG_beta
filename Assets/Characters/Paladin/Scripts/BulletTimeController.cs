using UnityEngine;
public class BulletTimeController : MonoBehaviour
{
    [SerializeField]
    private float TimeSlowFactor = 0.1f,
        TimeSlowDuration = 2f;
    [SerializeField]
    private GameObject TimeStopTriggerPrefab;
    private float timeSlowRemainingDuration = 0;
    private bool isOn;
    private Animator myAnimator;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        isOn = false;
    }
    public void StartBulletTime()
    {
        Time.timeScale = TimeSlowFactor;
        Time.fixedDeltaTime = 0.02f * TimeSlowFactor;
        timeSlowRemainingDuration = TimeSlowDuration;
        myAnimator.speed = 1 / TimeSlowFactor;
        isOn = true;
    }
    public void EndBulletTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        timeSlowRemainingDuration = 0;
        myAnimator.speed = 1;
        isOn = false;
    }
    private void InstantiateBulletTimeTrigger()
    {
        GameObject timeStopTrigger = Instantiate(TimeStopTriggerPrefab, transform.position, transform.rotation);
        timeStopTrigger.GetComponent<BulletTimeTrigger>().bulletTimeController = this;
    }
    private void Update()
    {
        if (isOn)
        {
            if (timeSlowRemainingDuration > 0)
            { timeSlowRemainingDuration -= Time.deltaTime / TimeSlowFactor; }
            else { EndBulletTime(); }
        }
    }
}