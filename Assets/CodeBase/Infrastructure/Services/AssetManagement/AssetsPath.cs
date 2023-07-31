using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.TileLogic;

namespace CodeBase.Infrastructure.Services.AssetManagement
{
    public static class AssetsPath
    {
        public static string PathToTile(TileType type) => "Tiles/Tile_" + type;
        public static string PathToTileHighlight = "TileHighlights/TileHighlight";
        public static string PathToBattleUnit(TeamType teamType, UnitType unitType) => $"Units/{teamType}/{unitType}";
    }
}
