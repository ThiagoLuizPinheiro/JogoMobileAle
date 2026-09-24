using UnityEngine;
using UnityEngine.EventSystems;

public class SliderKick : MonoBehaviour, IPointerClickHandler
{
    public KickTiming kickTiming;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (kickTiming != null)
        {
            kickTiming.ChutarPeloSlider();
        }
    }
}