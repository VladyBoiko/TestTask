using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    [Serializable]
    public class SettingsTabEntry
    {
        public SettingsTabType Type;
        public Button TabButton;
        public GameObject Panel;
    }
}