using System.Collections.Generic;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.CustomPoolLogic;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsFactory : IUnitsFactory
    {  
        public CustomPool<BattleUnit> WhiteWarriorsPool => _whiteWarriorsPool;
        CustomPool<BattleUnit> _whiteWarriorsPool;
        public CustomPool<BattleUnit> BlackWarriorsPool => _blackWarriorsPool;
        CustomPool<BattleUnit> _blackWarriorsPool;
        public CustomPool<BattleUnit> WhiteKingsPool  => _whiteKingsPool;
        CustomPool<BattleUnit> _whiteKingsPool;

        
        List<BattleUnit> _allWhiteUnits = new List<BattleUnit>();
        public IReadOnlyList<BattleUnit> AllWhiteUnits => _allWhiteUnits;
        List<BattleUnit> _allBlackUnits = new List<BattleUnit>();
        public IReadOnlyList<BattleUnit> AllBlackUnits => _allBlackUnits;
        
        private int _whiteWarriorsAmount;
        private int _blackWarriorsAmount;
        
        public UnitsFactory(GameModeStaticData gameModeStaticData)
        {
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
        
        public void DisableAllUnits()
        {
            int allWhiteUnitsAmount = _allWhiteUnits.Count;
            for (int i = 0; i < allWhiteUnitsAmount; i++)
            {
                _allWhiteUnits[i].SetActiveStatus(false);
            }

            int allBlackUnitsAmount = _allBlackUnits.Count;
            for (int i = 0; i < allBlackUnitsAmount; i++)
            {
                _allBlackUnits[i].SetActiveStatus(false);
            }
        }
        
        void InitSingleUnit(BattleUnit unit, TeamType teamType)
        {
            if (teamType == TeamType.White) _allWhiteUnits.Add(unit);
            else _allBlackUnits.Add(unit);
        }
    }
}