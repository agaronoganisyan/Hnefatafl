using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.CustomPoolLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsFactory : IUnitsFactory
    {
        private ITeamsUnitsContainer _teamsUnitsContainer;
        private IAssetsProvider _assetsProvider;
        public CustomPool<BattleUnit> WhiteWarriorsPool => _whiteWarriorsPool;
        CustomPool<BattleUnit> _whiteWarriorsPool;
        public CustomPool<BattleUnit> BlackWarriorsPool => _blackWarriorsPool;
        CustomPool<BattleUnit> _blackWarriorsPool;
        public CustomPool<BattleUnit> WhiteKingsPool  => _whiteKingsPool;
        CustomPool<BattleUnit> _whiteKingsPool;
        
        private int _whiteWarriorsAmount;
        private int _blackWarriorsAmount;

        private string BattleUnitAddress(TeamType teamType, UnitType unitType) => $"{teamType}_{unitType}";

        public void Initialize()
        {
            GameModeStaticData currentModeData =
                ServiceLocator.Get<IGameModeStaticDataService>().GetModeData(GameModeType.Classic);
            
            _teamsUnitsContainer = ServiceLocator.Get<ITeamsUnitsContainer>();
            _assetsProvider  = ServiceLocator.Get<IAssetsProvider>();
            
            _whiteWarriorsAmount = currentModeData.WhiteWarriorsAmount;
            _blackWarriorsAmount = currentModeData.BlackWarriorsAmount;
        }

        public async Task InitializePool()
        {
            GameObject whiteWarriorPrefab = await _assetsProvider.Load<GameObject>(BattleUnitAddress(TeamType.White, UnitType.Warrior));
            _whiteWarriorsPool = new CustomPool<BattleUnit>(whiteWarriorPrefab.GetComponent<BattleUnit>());

            GameObject blackWarriorPrefab = await _assetsProvider.Load<GameObject>(BattleUnitAddress(TeamType.Black, UnitType.Warrior));
            _blackWarriorsPool = new CustomPool<BattleUnit>(blackWarriorPrefab.GetComponent<BattleUnit>());

            GameObject whiteKingPrefab = await _assetsProvider.Load<GameObject>(BattleUnitAddress(TeamType.White, UnitType.King));
            _whiteKingsPool = new CustomPool<BattleUnit>(whiteKingPrefab.GetComponent<BattleUnit>());

            BattleUnit intsUnit;

            for (int i = 0; i < _whiteWarriorsAmount; i++)
            {
                intsUnit = _whiteWarriorsPool.Get();

                InitSingleUnit(intsUnit, TeamType.White);
            }

            for (int i = 0; i < _blackWarriorsAmount; i++)
            {
                intsUnit = _blackWarriorsPool.Get();

                InitSingleUnit(intsUnit, TeamType.Black);
            }

            intsUnit = _whiteKingsPool.Get();
            InitSingleUnit(intsUnit, TeamType.White);
        }
        
        void InitSingleUnit(BattleUnit unit, TeamType teamType)
        {
            if (teamType == TeamType.White)
            {
                _teamsUnitsContainer.AddWhiteUnit(unit);
            }
            else if (teamType == TeamType.Black)
            {
                _teamsUnitsContainer.AddBlackUnit(unit);
            }
        }
    }
}