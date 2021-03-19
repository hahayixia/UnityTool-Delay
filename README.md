# UnityTool-Delay
untiy 的一个延迟支持的小工具
支持 秒和帧的延迟
在 MonoBehaviour 的 Update中进行驱动
简单易用，下面是使用的例子：

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
