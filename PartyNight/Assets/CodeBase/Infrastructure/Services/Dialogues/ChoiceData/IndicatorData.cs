using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Stats;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Dialogues.ChoiceData
{
    [Serializable]
    public class IndicatorData : IIndicatorData
    {
        private const int MinValue = 0;
        private const int MaxValue = 10;
        public event Action<OutOfRangeType> ValueIsOutOfRange;

        [SerializeField] private int _housekeeping;
        [SerializeField] private int _fun;
        [SerializeField] private int _drunkenness;
        [SerializeField] private int _loudness;

        public List<int> ValuesList => new List<int>()
        {
            Housekeeping,
            Fun,
            Drunkenness,
            Loudness
        };

        public int Housekeeping
        {
            get => _housekeeping;
            private set
            {
                _housekeeping = value;
                if (_housekeeping >= MaxValue) ValueIsOutOfRange?.Invoke(OutOfRangeType.MaxHousekeeping);
                if (_housekeeping <= MinValue) ValueIsOutOfRange?.Invoke(OutOfRangeType.MinHousekeeping);
            }
        }

        public int Fun
        {
            get => _fun;
            private set
            {
                _fun = value;
                if (_fun >= MaxValue) ValueIsOutOfRange?.Invoke(OutOfRangeType.MaxFun);
                if (_fun <= MinValue) ValueIsOutOfRange?.Invoke(OutOfRangeType.MinFun);
            }
        }

        public int Drunkenness
        {
            get => _drunkenness;
            private set
            {
                _drunkenness = value;
                if (_drunkenness >= MaxValue) ValueIsOutOfRange?.Invoke(OutOfRangeType.MaxDrunkenness);
                if (_drunkenness <= MinValue) ValueIsOutOfRange?.Invoke(OutOfRangeType.MinDrunkenness);
            }
        }

        public int Loudness
        {
            get => _loudness;
            private set
            {
                _loudness = value;
                if (_loudness >= MaxValue) ValueIsOutOfRange?.Invoke(OutOfRangeType.MaxLoudness);
                if (_loudness <= MinValue) ValueIsOutOfRange?.Invoke(OutOfRangeType.MinLoudness);
            }
        }

        public IndicatorData(int housekeeping, int fun, int drunkenness, int loudness)
        {
            InitData(housekeeping, fun, drunkenness, loudness);
        }

        public IndicatorData(List<int> values)
        {
            InitData(values[0], values[1], values[2], values[3]);
        }

        private void InitData(int housekeeping, int fun, int drunkenness, int loudness)
        {
            Housekeeping = housekeeping;
            Fun = fun;
            Drunkenness = drunkenness;
            Loudness = loudness;
        }

        public void UpdateData(IndicatorUpdateData impact)
        {
            Drunkenness += impact.Drunkenness;
            Fun += impact.Fun;
            Housekeeping += impact.Housekeeping;
            Loudness += impact.Loudness;
        }
    }
}