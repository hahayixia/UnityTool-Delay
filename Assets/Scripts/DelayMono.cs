using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    class DelayMono : MonoBehaviour
    {
        private void Update()
        {
            Delay.Update(Time.deltaTime);
        }
    }
public static class Delay
{
    static List<DelayCell_Time> _list_timer = new List<DelayCell_Time>(8);
    static List<DelayCell_Frame> _list_framer = new List<DelayCell_Frame>(8);
    private static bool _inited = false;
    static void Init()
    {
        if (_inited) return;
        _inited = true;
        GameObject go = new GameObject("DelayMono", typeof(DelayMono));
        go.hideFlags = HideFlags.HideInHierarchy;
        GameObject.DontDestroyOnLoad(go);
    }
    public static DelayCell Timer(float time)
    {
        Init();
        DelayCell_Time re = null;
        for (int i = 0; i < _list_timer.Count; i++)
        {
            re = _list_timer[i];
            if (re.ActiveState == 0)
            {
                re.SetData(time);
                return re;
            }
        }

        re = new DelayCell_Time();
        re.SetData(time);
        _list_timer.Add(re);
        return re;
    }
    public static DelayCell Framer(int frame)
    {
        Init();
        DelayCell_Frame re = null;
        for (int i = 0; i < _list_framer.Count; i++)
        {
            re = _list_framer[i];
            if (re.ActiveState == 0)
            {
                re.SetData(frame);
                return re;
            }
        }

        re = new DelayCell_Frame();
        re.SetData(frame);
        _list_framer.Add(re);
        return re;
    }
    public static void Update(float deltaTime)
    {
        for (int i = 0; i < _list_timer.Count; i++)
        {
            var item = _list_timer[i];
            if (item.ActiveState != 1) continue;
            item.Update(deltaTime);
        }
        for (int i = 0; i < _list_framer.Count; i++)
        {
            var item = _list_framer[i];
            if (item.ActiveState != 1) continue;
            item.Update(deltaTime);
        }
    }

    public static void KillAll()
    {
        for (int i = 0; i < _list_timer.Count; i++)
        {
            _list_timer[i].Stop();
        }
        for (int i = 0; i < _list_framer.Count; i++)
        {
            _list_framer[i].Stop();
        }
    }

    public abstract class DelayCell
    {
        public byte ActiveState = 0;
        protected System.Action action_complete = null;
        protected System.Action action_update = null;
        public DelayCell OnComplete(System.Action action)
        {
            action_complete = action;
            return this;
        }
        public DelayCell OnUpdate(System.Action action)
        {
            action_update = action;
            return this;
        }
        public DelayCell Play()
        {
            ActiveState = 1;
            return this;
        }
        public DelayCell Pause()
        {
            ActiveState = 2;
            return this;
        }
        public void Stop(bool docomplete = false)
        {
            var tmp = action_complete;
            action_complete = null;
            action_update = null;
            if (docomplete)
            {
                if (tmp != null) tmp();
            }
            ActiveState = 0;
        }
    }
    public class DelayCell_Time : DelayCell
    {
        float time;
        private float _timer;

        public void SetData(float time)
        {
            ActiveState = 1;
            this.time = time;
            _timer = 0;
        }

        public void Update(float deltaTime)
        {
            if (_timer >= time)
            {
                Stop(true);
            }
            else
            {
                _timer += deltaTime;
                if (action_update != null) action_update();
            }
        }
    }
    public class DelayCell_Frame : DelayCell
    {
        int frame;
        private int _frmaer;

        public void SetData(int frame)
        {
            ActiveState = 1;
            this.frame = frame;
            _frmaer = 0;
        }

        public void Update(float deltaTime)
        {
            if (_frmaer >= frame)
            {
                Stop(true);
            }
            else
            {
                _frmaer++;
                if (action_update != null) action_update();
            }
        }
    }

}