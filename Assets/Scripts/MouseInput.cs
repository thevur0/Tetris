using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput  {

    public enum ClickType
    {
        None,
        SingleType,
        LongPress,
        Padding,
        MoveLeft,
        MoveRight,
    }
    bool isGetBeginPos = false;
    bool isCheckCollider = false;
    bool isLongPress = false;
    bool isSingleClick = false;
    bool isPadding = false;
    Vector3 beginPosition = Vector3.zero;
    Vector3 curPosition = Vector3.zero;
    float timer = 0.0f;

    ClickType clickType = ClickType.None;
    public void GetClickType()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isGetBeginPos)
            {
                beginPosition = Input.mousePosition;
                isGetBeginPos = false;
            }
            if (isCheckCollider)
            {
                //检测碰撞体

                isCheckCollider = false;
            }
            Debug.Log("GetMouseButtonDown");
        }

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            curPosition = Input.mousePosition;
            float offsetX = Mathf.Abs(curPosition.x - beginPosition.x);
            float offectY = Mathf.Abs(curPosition.y - beginPosition.y);
            float offectZ = Mathf.Abs(curPosition.z - beginPosition.z);
            if (offsetX < 0.05f && offectY < 0.05f && offectZ < 0.05f)
            {
                //以1S为点击和长按的判定条件
                if (timer > 1)
                {
                    isLongPress = true;
                }
                else
                {
                    isSingleClick = true;
                }
            }
            else
            {
                isPadding = true;
            }
            Debug.Log("GetMouseButton");
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isSingleClick)
            {
                clickType = ClickType.SingleType;
                isSingleClick = false;
            }
            else if (isLongPress)
            {
                clickType = ClickType.LongPress;
                isLongPress = false;
            }
            else if (isPadding)
            {
                clickType = ClickType.Padding;
                isPadding = false;
            }
            Debug.Log("GetMouseButtonUp");
        }
    }
}
