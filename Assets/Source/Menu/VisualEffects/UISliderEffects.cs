using UnityEngine.EventSystems;

namespace Menu
{
    public class UISliderEffects : UISelectableScalerBase,
        IPointerEnterHandler, IPointerExitHandler,
        ISelectHandler, IDeselectHandler
    {
        public void OnPointerEnter(PointerEventData eventData) => SetHoverState();
        public void OnPointerExit(PointerEventData eventData) => SetOriginalState();
        public void OnSelect(BaseEventData eventData) => SetHoverState();
        public void OnDeselect(BaseEventData eventData) => SetOriginalState();
    }
}