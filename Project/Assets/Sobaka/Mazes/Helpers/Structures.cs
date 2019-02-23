using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sobaka.Maze
{
    struct ExactPercentage
    {
        public bool  IsPersentage;
        public float Value;

        public ExactPercentage( bool _isPersentage, float _value )
        {
            IsPersentage = _isPersentage;
            Value        = _isPersentage ? Mathf.Clamp01( _value ) : _value;
        }

        public int IntValue
        {
            get
            {
                // если дробная часть < 0.5 округляем в минус, иначе в +
                
                float fraction = Value - ( (int) Value );
                return fraction < 0.5 ? ( (int) Value ) : ( (int) Value + 1 );
            }
        }
    }

    struct Zone
    {
        public int Id;
        public int ParentId;
    }

    struct PathWidth
    {
        // вероятностное распределение выпадения значения от минимума до максимума 
        public Func<float, float> Probability;
        
        public float Min;
        public float Max;
    }

    struct PathRender
    {
        public Func<float, float> Smoothing;
    }
}