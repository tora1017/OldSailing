using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseObject
{

    private bool _activeMode = false;
    private GameObject GameObj;

    [SerializeField]
    private float m_updateInterval = 0.5f;

    private float m_accum;
    private int m_frames;
    private float m_timeleft;
    private float m_fps;

    private void FPS()
    {
        m_timeleft -= Time.deltaTime;
        m_accum += Time.timeScale / Time.deltaTime;
        m_frames++;

        if (0 < m_timeleft) return;

        m_fps = m_accum / m_frames;
        m_timeleft = m_updateInterval;
        m_accum = 0;
        m_frames = 0;
    }

    private void OnGUI()
    {
        GUILayout.Label("FPS: " + m_fps.ToString("f2"));
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        RemoveObjectToList(this);
        AppendManagerObjectToList(this);
    }

	public override void OnUpdate()
	{
		base.OnUpdate();
		FPS();
	}

	public bool ActiveMode
    {
        get{ return _activeMode; }
        set { _activeMode = value; }
    }

    // ゲームオブジェクトを止めるかの判断を行う
    void GameState()
    {
        // 動いている状態
        if (_activeMode)
        {
            Time.timeScale = 1;
        }
        // 止めている状態
        else
        {
            Time.timeScale = 0;
        }
        _activeMode = !_activeMode;
    }
}
