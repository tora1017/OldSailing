using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


/// <summary>
/// 解像度を変更するコンポーネント
/// </summary>
[RequireComponent(typeof(Camera))]
public class ResolutionConverter : BaseObject
{
    /// <summary>
    /// 解像度
    /// </summary>
    public enum Resolution
    {
        None,
        HD,
        FULLHD
    }

    /// <summary>
    /// 解像度設定
    /// </summary>
    [SerializeField] private Resolution m_Resolution = Resolution.None;

    /// <summary>
    /// ターゲットカメラ
    /// </summary>
    private Camera m_Camera;
    /// <summary>
    /// フレームバッファ
    /// </summary>
    private RenderTexture m_FrameBuffer;
    /// <summary>
    /// コマンドバッファ
    /// </summary>
    private CommandBuffer m_CommandBuffer;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        m_Camera = GetComponent<Camera>();

        Apply(m_Resolution);
    }

    /// <summary>
    /// 適用する
    /// </summary>
    public void Apply(Resolution resolution)
    {
        m_Resolution = resolution;

        if (resolution == Resolution.None)
        {
            return;
        }

        var size = GetResolutionSize(resolution);
        size = FitScreenAspect(Screen.width, Screen.height, size.x, size.y);
        UpdateFrameBuffer(size.x, size.y, 24);
        UpdateCameraTarget();
        AddCommand();
    }


    /// <summary>
    /// フレームバッファの更新
    /// </summary>
    private void UpdateFrameBuffer(int width, int height, int depth, RenderTextureFormat format = RenderTextureFormat.Default)
    {
        if (m_FrameBuffer != null)
        {
            m_FrameBuffer.Release();
            Destroy(m_FrameBuffer);
        }

        m_FrameBuffer = new RenderTexture(width, height, depth, format);
        m_FrameBuffer.useMipMap = false;
        m_FrameBuffer.Create();
    }

    /// <summary>
    /// カメラの描画先を更新
    /// </summary>
    private void UpdateCameraTarget()
    {
        if (m_FrameBuffer != null)
        {
            m_Camera.SetTargetBuffers(m_FrameBuffer.colorBuffer, m_FrameBuffer.depthBuffer);
        }
        else
        {
            m_Camera.SetTargetBuffers(Display.main.colorBuffer, Display.main.depthBuffer);
        }
    }


    /// <summary>
    /// 解像度の実サイズを取得
    /// </summary>
    private Vector2Int GetResolutionSize(Resolution resolution)
    {
        bool isPortrait = Screen.height > Screen.width;

        switch (resolution)
        {
            case Resolution.HD:
                if (isPortrait)
                {
                    return new Vector2Int(720, 1280);
                }
                return new Vector2Int(1280, 720);
            case Resolution.FULLHD:
                if (isPortrait)
                {
                    return new Vector2Int(1080, 1920);
                }
                return new Vector2Int(1920, 1080);
        }

        return new Vector2Int(Screen.width, Screen.height);
    }


    /// <summary>
    /// 解像度の計算
    /// </summary>
    private static Vector2Int FitScreenAspect(int width, int height, int maxWidth, int maxHeight)
    {
        // 解像度以下なら何もしない
        if (width <= maxWidth && height <= maxHeight)
        {
            return new Vector2Int(width, height);
        }

        if (width > height)
        {
            float aspect = height / (float)width;
            int w = Mathf.Min(width, maxWidth);
            int h = Mathf.RoundToInt(w * aspect);

            return new Vector2Int(w, h);
        }

        {
            float aspect = width / (float)height;
            int h = Mathf.Min(height, maxHeight);
            int w = Mathf.RoundToInt(height * aspect);

            return new Vector2Int(w, h);
        }
    }

    /// <summary>
    /// コマンドを追加する
    /// </summary>
    private void AddCommand()
    {
        RemoveCommand();

        // カラーバッファをバックバッファ(画面)に描きこむコマンド
        {
            m_CommandBuffer = new CommandBuffer();
            m_CommandBuffer.name = "blit to Back buffer";

            m_CommandBuffer.SetRenderTarget(-1);
            m_CommandBuffer.Blit(m_FrameBuffer, BuiltinRenderTextureType.CurrentActive);

            m_Camera.AddCommandBuffer(CameraEvent.AfterEverything, m_CommandBuffer);
        }
    }
    /// <summary>
    /// コマンドを破棄する
    /// </summary>
    private void RemoveCommand()
    {
        if (m_CommandBuffer == null)
        {
            return;
        }
        if (m_Camera == null)
        {
            return;
        }

        m_Camera.RemoveCommandBuffer(CameraEvent.AfterEverything, m_CommandBuffer);
        m_CommandBuffer = null;
    }
}