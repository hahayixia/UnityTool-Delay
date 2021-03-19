using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTest : MonoBehaviour
{
    Delay.DelayCell timerdc = null;
    Delay.DelayCell framedc = null;
    void Start()
    {
        int oldtime = 0;
        float timertmp = Time.time;
        timerdc = Delay.Timer(5).OnComplete(() =>
        {
            Debug.LogError("Delay.Timer(5)");
            timerdc = null;
        }).OnUpdate(() => {
            int tmpv = (int)(Time.time - timertmp);
            if (tmpv != oldtime)
            {
                oldtime = tmpv;
                Debug.LogError("Delay.Timer(5):" + oldtime);
            }
        });

        framedc = Delay.Framer(100).OnComplete(() =>
        {
            Debug.LogError("Delay.Framer(100)");
            framedc = null;
        }).OnUpdate(()=> {
            
        });
    }
    private void OnDestroy()
    {
        if (timerdc != null) timerdc.Stop();
        if (framedc != null) framedc.Stop();
    }
}
