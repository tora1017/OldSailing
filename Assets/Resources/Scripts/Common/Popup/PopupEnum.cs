/**********************************************************************************************/
/*@file   PopupEnum.cs
***********************************************************************************************
* @brief  PopupWindow関係のenum定義所
***********************************************************************************************
* @author     yuta takatsu
***********************************************************************************************
* Copyright © 2017 yuta takatsu All Rights Reserved.
**********************************************************************************************/

public enum EPopupState
{
    OpenBegin,
    Openning,
    OpenEnd,
    CloseBegin,
    Closing,
    CloseEnd
}

public enum EButtonId
{
    OK,
    Cancel
}

public enum EButtonSet
{
    SetNone,
    Set1,
    Set2
}

public enum EPopupType
{
    Tutorial,
    Stage,
    Matching
}