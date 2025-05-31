using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hitsToDestroy = 2;
    private int currentHits = 0;

    public void ReceiveHit()
    {
        currentHits++;
        if (currentHits >= hitsToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
