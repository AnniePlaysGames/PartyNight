using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Dialogues.ChoiceData
{
    [Serializable]
    public class IndicatorUpdateData : IIndicatorData
    {
        private const int MaxImpact = 10;

        public List<int> ValuesList => new List<int>()
        {
            Housekeeping,
            Fun,
            Drunkenness,
            Loudness
        };

        [field: SerializeField] public int Housekeeping { get; private set; }
        [field: SerializeField] public int Fun { get; private set; }
        [field: SerializeField] public int Drunkenness { get; private set; }
        [field: SerializeField] public int Loudness { get; private set; }

        public IndicatorUpdateData(int housekeeping = 0, int fun = 0, int drunkenness = 0, int loudness = 0)
        {
            InitData(housekeeping, fun, drunkenness, loudness);
        }

        public IndicatorUpdateData(List<int> values)
        {
            InitData(values[0], values[1], values[2], values[3]);
        }

        private void InitData(int housekeeping, int fun, int drunkenness, int loudness)
        {
            Housekeeping = SetLimits(to: housekeeping);
            Fun = SetLimits(to: fun);
            Drunkenness = SetLimits(to: drunkenness);
            Loudness = SetLimits(to: loudness);
        }

        private int AddToValuesList(int value)
        {
            ValuesList.Add(value);
            return value;
        }

        private static int SetLimits(int to)
            => math.clamp(to, -MaxImpact, MaxImpact);
    }
}