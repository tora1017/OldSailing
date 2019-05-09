using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRender : BaseObject
{
    [SerializeField]
    private float updateInterval = 0.5f;

    private float accum;
    private int frames;
    private float timeleft;
    private float fps;
    private int allObj;

    public void Start()
    {
        allObj = 0;
    }

    public override void OnUpdate()
    {

        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        if (0 < timeleft) return;

        fps = accum / frames;
        timeleft = updateInterval;
        accum = 0;
        frames = 0;
        allObj = BaseObjectList.Count;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        allObj = 0;

    }

    private void OnGUI()
    {
        GUILayout.Label("FPS: " + fps.ToString("f2"));
        GUILayout.Label("Obj: " + allObj.ToString());
    }
}