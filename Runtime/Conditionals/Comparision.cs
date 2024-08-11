using System;
using TheKiwiCoder;
using UnityEngine;

namespace BigLazyET.BT
{
    public class Comparision<T> : ConditionNode where T : IComparable<T>
    {
        [Tooltip("The operation to perform")]
        public Operation operation;
        [Tooltip("The first op")]
        public NodeProperty<T> op1;
        [Tooltip("The second op")]
        public NodeProperty<T> op2;

        protected override bool CheckCondition()
        {
            var op1Value = op1.Value;
            var op2Value = op2.Value;

            switch (operation)
            {
                case Operation.LessThan:
                    return op1Value.CompareTo(op2Value) < 0 ? true : false;
                case Operation.LessThanOrEqualTo:
                    return op1Value.CompareTo(op2Value) <= 0 ? true : false;
                case Operation.EqualTo:
                    return op1Value.CompareTo(op2Value) == 0 ? true : false;
                case Operation.NotEqualTo:
                    return op1Value.CompareTo(op2Value) != 0 ? true : false;
                case Operation.GreaterThanOrEqualTo:
                    return op1Value.CompareTo(op2Value) >= 0 ? true : false;
                case Operation.GreaterThan:
                    return op1Value.CompareTo(op2Value) > 0 ? true : false;
            }
            return false;
        }
    }

    public enum Operation
    {
        LessThan,
        LessThanOrEqualTo,
        EqualTo,
        NotEqualTo,
        GreaterThanOrEqualTo,
        GreaterThan
    }
}
