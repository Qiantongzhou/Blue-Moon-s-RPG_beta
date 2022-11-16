using UnityEngine;
public class Player : MonoBehaviour
{
    private static GameObject CurrentPlayer;

    private void Awake()
    {
        CurrentPlayer = this.gameObject;
    }
    public static GameObject GetCurrentPlayer()
    {
        return CurrentPlayer;
    }
}
