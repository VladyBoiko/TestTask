using System;
using UnityEngine;

namespace Menu
{
    [Serializable]
    public class SaveData
    {
        public string Id;
        public string Name;
        public DateTime Date;
        public string Description;
        public Sprite PreviewSprite;
    }
}