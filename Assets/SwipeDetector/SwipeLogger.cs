using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    private GameObject player;
    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
    }
}