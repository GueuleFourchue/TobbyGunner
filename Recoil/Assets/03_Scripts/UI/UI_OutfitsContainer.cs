using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OutfitsContainer : MonoBehaviour {

    [Header("Clamp")]
    public float leftClamp;
    public float rightClamp;

    [Header("Standard Positions")]
    public float leftStandardPosition;
    public float rightStandardPosition;

    [Header("Lerp")]
    public float timeOfTravel;
    float currentTime = 0;
    float normalizedValue;

    [Header("Change side")]
    public float leftChangeThreshold;
    public float rightChangeThreshold;

    [HideInInspector]
    public RectTransform container;
    [HideInInspector]
    public Vector3 originContainerPosition;
    [HideInInspector]
    public Vector2 originTouchPosition;
    [HideInInspector]
    public bool isDraging;
    float xOffset;
    IEnumerator coroutine;

    private void Start()
    {
        container = this.GetComponent<RectTransform>();
        originContainerPosition = container.anchoredPosition;
    }

    private void Update()
    {
        if (isDraging)
        {
            xOffset = Input.mousePosition.x - originTouchPosition.x;
            container.anchoredPosition = new Vector2(Mathf.Clamp(originContainerPosition.x + xOffset, leftClamp, rightClamp), container.anchoredPosition.y);
        }
    }

    public void BeginSwipe(Vector2 touchPosition)
    {
        //LerpMove
        if (coroutine != null)
            StopCoroutine(coroutine);
        currentTime = 0;

        //
        originTouchPosition = touchPosition;
        isDraging = true;
    }

    public void EndSwipe()
    {
        originContainerPosition = container.anchoredPosition;
        isDraging = false;

        if (container.anchoredPosition.x > leftStandardPosition)
        {
            coroutine = LerpMove(leftStandardPosition);
            StartCoroutine(coroutine);
        }
        else if (container.anchoredPosition.x < rightStandardPosition)
        {
            coroutine = LerpMove(rightStandardPosition);
            StartCoroutine(coroutine);
        }
        else if (container.anchoredPosition.x < leftChangeThreshold && container.anchoredPosition.x > rightChangeThreshold)
        {
            if(xOffset < 0)
                coroutine = LerpMove(rightStandardPosition);
            else
                coroutine = LerpMove(leftStandardPosition);
            StartCoroutine(coroutine);
        }
        else
        {
            if (container.anchoredPosition.x < 74.75f)
                coroutine = LerpMove(rightStandardPosition);
            else
                coroutine = LerpMove(leftStandardPosition);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator LerpMove(float xPosition)
    {
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 

            container.anchoredPosition = Vector2.Lerp(container.anchoredPosition, new Vector2(xPosition, container.anchoredPosition.y), normalizedValue);
            originContainerPosition = container.anchoredPosition;
            yield return null;
        }
        currentTime = 0;
        coroutine = null;
    }
}
