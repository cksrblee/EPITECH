using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{

    public GameObject ClockTik;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TikRotationAnim());
    }


    IEnumerator TikRotationAnim()
    {
        var time = 0.0f;

        ClockTik.transform.eulerAngles = Vector3.zero;
        while (true)
        {
            time += Time.deltaTime;
            ClockTik.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 1.3f);
            yield return null;
            //Debug.Log(time);
            if (time > 10)
            {
                break;
            }
        }

        yield return new WaitForSeconds(0.5f);

    }
}
