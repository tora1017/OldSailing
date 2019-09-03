using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSound : BaseObject {

    private string audioClipName = "";

    BGMPlayer player;

    void Start()
    {
        Singleton<SoundPlayer>.Instance.PlayBGM("Water", 0.0f, true);
        Singleton<SoundPlayer>.Instance.PlayBGM("Wind", 0.0f, true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Singleton<SoundPlayer>.Instance.Update();
    }

}
