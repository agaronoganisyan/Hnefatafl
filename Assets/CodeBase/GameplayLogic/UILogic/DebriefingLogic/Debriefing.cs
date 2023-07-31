using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.DebriefingLogic
{
    public enum DebriefingType
    {
        None,
        Win,
        Defeat
    }

    public class Debriefing : UICanvas
    {
        [SerializeField] DebriefingPanel _debriefingPanel;

        public override void Initialize()
        {
            base.Close();
        }

        public override void Open()
        {
            base.Open();


        }
    }
}