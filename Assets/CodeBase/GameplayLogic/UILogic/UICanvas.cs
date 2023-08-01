using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic
{
    public abstract class UICanvas : MonoBehaviour, IService
    {
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected GraphicRaycaster graphicRaycaster;

        public virtual void Open()
        {
            canvas.enabled = true;
            graphicRaycaster.enabled = true;
        }

        public virtual void Close()
        {
            canvas.enabled = false;
            graphicRaycaster.enabled = false;
        }
    }
}