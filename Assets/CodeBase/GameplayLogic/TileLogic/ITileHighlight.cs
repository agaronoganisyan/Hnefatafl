using UnityEngine;

namespace CodeBase.GameplayLogic.TileLogic
{
    public interface ITileHighlight 
    {
        void Hide();
        void Show(ShowingType current);
        void Initialize(Vector3 pos, Color currentTileColor, Color availableTileColor);
    }
}