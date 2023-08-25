using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public class GameModeStaticDataService : IGameModeStaticDataService
    {
        string _classicModeStaticDataAddress = "ClassicModeStaticData";

        private IAssetsProvider _assetsProvider;

        private Dictionary<GameModeType, GameModeStaticData>
            _modes = new Dictionary<GameModeType, GameModeStaticData>();

        public void Initialize()
        {
            _assetsProvider = ServiceLocator.Get<IAssetsProvider>();
        }

        public async Task LoadModeData(GameModeType gameModeType)
        {
            switch (gameModeType)
            {
                case GameModeType.Classic:
                    GameModeStaticData modeStaticData = await _assetsProvider.Load<GameModeStaticData>(_classicModeStaticDataAddress);
                    _modes.Add(modeStaticData.GameModeType, modeStaticData);
                    break;
            }
        }

        public GameModeStaticData GetModeData(GameModeType modeType)
        {
            return _modes.TryGetValue(modeType, out GameModeStaticData gameModeData) ? gameModeData : null;
        }
    }
}