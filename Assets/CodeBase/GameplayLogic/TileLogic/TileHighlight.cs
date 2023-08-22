using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GameplayLogic.TileLogic
{
    public enum ShowingType
    {
        None,
        Current,
        Available
    }

    public class TileHighlight : MonoBehaviour, ITileHighlight
    {
        [SerializeField] SpriteRenderer _image;

        Color _currentTileColor;
        Color _availableTileColor;

        public void Initialize(Vector3 pos, Color currentTileColor, Color availableTileColor)
        {
            transform.position = pos;

            _currentTileColor = currentTileColor;
            _availableTileColor = availableTileColor;

            Hide();
        }

        public void Show(ShowingType showingType)
        {
            if (showingType == ShowingType.Current) _image.color = _currentTileColor;
            else if (showingType == ShowingType.Available) _image.color = _availableTileColor;

            _image.enabled = true;
        }

        public void Hide()
        {
            _image.enabled = false;
        }
    }
}