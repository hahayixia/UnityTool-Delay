# UnityTool-Delay
untiy 的一个延迟支持的小工具<br>
支持 秒和帧的延迟<br>
在 MonoBehaviour 的 Update中进行驱动<br>
简单易用，下面是使用的例子：<br>
<br>
int oldtime = 0;<br>
float timertmp = Time.time;<br>
timerdc = Delay.Timer(5).OnComplete(() =><br>
{<br>
    Debug.LogError("Delay.Timer(5)");<br>
    timerdc = null;<br>
}).OnUpdate(() => {<br>
    int tmpv = (int)(Time.time - timertmp);<br>
    if (tmpv != oldtime)<br>
    {<br>
        oldtime = tmpv;<br>
        Debug.LogError("Delay.Timer(5):" + oldtime);<br>
    }<br>
});<br>
