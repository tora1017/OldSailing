using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSound : BaseObject
{



    [SerializeField]
    private float fadeTime = 1.0f;

    private bool callOnce = false;

    void Start()
    {
        //fadeTime = 1.0f;
        Singleton<SoundPlayer>.Instance.PlayBGM("Sea", fadeTime, true);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Singleton<SoundPlayer>.Instance.Update();

    }

    public void OnTap()
    {
        if (!callOnce)
        {
            Singleton<SoundPlayer>.Instance.PlaySE("TitleButton");
            callOnce = !callOnce;
        }
    }

}