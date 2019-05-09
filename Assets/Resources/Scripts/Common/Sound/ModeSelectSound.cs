using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectSound : BaseObject
{
    private string audioClipName = "";
    [SerializeField]
    private float fadeTime = 0.0f;


    private bool callOnce;


    void Start()
    {
        callOnce = false;

        Singleton<SoundPlayer>.Instance.PlayBGM("ModeSelect", fadeTime, true);
    }

    public void OnTap()
    {
        if (!callOnce)
        {
            Singleton<SoundPlayer>.Instance.PlaySE("TitleButton");
            callOnce = !callOnce;
        }
    }
    public void OnTapBack()
    {
        if (!callOnce)
        {
            Singleton<SoundPlayer>.Instance.PlaySE("0");
            callOnce = !callOnce;
        }
    }
}
