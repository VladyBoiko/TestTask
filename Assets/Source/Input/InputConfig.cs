using UnityEngine;

namespace Input
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "Data/InputConfig", order = 0)]
    public class InputConfig : ScriptableObject
    {
        [SerializeField] private string _actionMapName = "UI";
        [SerializeField] private string _submitActionName = "Submit";
        [SerializeField] private string _cancelActionName = "Cancel";
        [SerializeField] private string _navigateActionName = "Navigate";
        
        public string ActionMapName => _actionMapName;
        public string SubmitActionName => _submitActionName;
        public string CancelActionName => _cancelActionName;
        public string NavigateActionName => _navigateActionName;
    }
}