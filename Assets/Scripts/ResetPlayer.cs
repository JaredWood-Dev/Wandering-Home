using System;
using System.Collections;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public Vector2 checkPoint;
    public Fade overlay;

    public void ResetPlr()
    {
        transform.position = checkPoint;
        Camera.main.transform.position = new Vector3(checkPoint.x, checkPoint.y, Camera.main.transform.position.z);
    }
    
    public IEnumerator TriggerReset()
    {
        yield return new WaitForSeconds(1);
        print("starting fade");
        StartCoroutine(overlay.DoFade(0, 1, 1));
        yield return new WaitForSeconds(1);
        print("reset");
        ResetPlr();
        print("ending fade");
        yield return new WaitForSeconds(1);
        StartCoroutine(overlay.DoFade(1, 0, 1));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KillZone"))
        {
            StartCoroutine(TriggerReset());
        }
    }
}
