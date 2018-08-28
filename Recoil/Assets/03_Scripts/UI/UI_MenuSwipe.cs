using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UI_MenuSwipe : MonoBehaviour {

    public Transform container;
    public float xSwipeThreshold;
    public float bound;

    [Header ("Direction")]
    public bool moveLeft;
    public float LeftPosition;
    public bool moveRight;
    public float RightPosition;

    Vector2 originTouchPosition;
    public bool isDraging;
    float xOffset;

    private void Update()
    {
        if (isDraging)
        {
            if (moveRight && container.position.x < LeftPosition + bound)
                MoveContainer(LeftPosition);
            else if (moveLeft && container.position.x > RightPosition - bound)
                MoveContainer(RightPosition);
        }
    }

    public void BeginSwipe()
    {
        isDraging = true;
        originTouchPosition = Input.mousePosition;
    }

    public void EndSwipe()
    {
        isDraging = false;

        if (moveRight)
        {
            if (container.position.x > LeftPosition - xSwipeThreshold)
            {
                container.DOKill();
                container.DOMoveX(LeftPosition, 0.1f);
            }
            else
            {
                ChangeContainer(RightPosition);
            }
        }
        
        if (moveLeft)
        {
            if (container.position.x < RightPosition + xSwipeThreshold)
            {
                container.DOKill();
                container.DOMoveX(RightPosition, 0.1f);
            }
            else
            {
                ChangeContainer(LeftPosition);
            }
        }
    }

    void MoveContainer(float xPosition)
    {
        xOffset = Input.mousePosition.x - originTouchPosition.x;
        container.position = new Vector3(xPosition + xOffset, container.position.y);
    }

    void ChangeContainer(float xPosition)
    {
        Debug.Log(this.gameObject.name);
        container.DOKill();
        container.DOMoveX(xPosition, 0.1f);
    }

}
