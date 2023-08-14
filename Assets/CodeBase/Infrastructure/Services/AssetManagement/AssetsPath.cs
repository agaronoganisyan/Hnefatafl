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
        public const string PathToTileHighlight = "TileHighlights/TileHighlight";

        public const string PathToClassicModeStaticData = "StaticData/ClassicModeStaticData";

        public const string PathToBoard = "Boards/Board";

        public const string PathToBoardHighlight = "BoardHighlights/BoardHighlight";
        
        public static string PathToBattleUnit(TeamType teamType, UnitType unitType) => $"Units/{teamType}/{unitType}";
    }
}
