using System.Collections.Generic;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.CustomPoolLogic;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsFactory : IUnitsFactory
    {
        private ITeamsUnitsContainer _teamsUnitsContainer;
        public CustomPool<BattleUnit> WhiteWarriorsPool => _whiteWarriorsPool;
        CustomPool<BattleUnit> _whiteWarriorsPool;
        public CustomPool<BattleUnit> BlackWarriorsPool => _blackWarriorsPool;
        CustomPool<BattleUnit> _blackWarriorsPool;
        public CustomPool<BattleUnit> WhiteKingsPool  => _whiteKingsPool;
        CustomPool<BattleUnit> _whiteKingsPool;
        
        private int _whiteWarriorsAmount;
        private int _blackWarriorsAmount;
        
        public UnitsFactory(ITeamsUnitsContainer teamsUnitsContainer, GameModeStaticData gameModeStaticData)
        {
            _teamsUnitsContainer = teamsUnitsContainer;
            
            _whiteWarriorsAmount = gameModeStaticData.WhiteWarriorsAmount;
            _blackWarriorsAmount= gameModeStaticData.BlackWarriorsAmount;
        }

        public void Initialize()
        {
            _whiteWarriorsPool = new CustomPool<BattleUnit>(
                AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(TeamType.White, UnitType.Warrior)));
            _blackWarriorsPool = new CustomPool<BattleUnit>(
                AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(TeamType.Black, UnitType.Warrior)));
            _whiteKingsPool = new CustomPool<BattleUnit>(
                AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(TeamType.White, UnitType.King)));

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