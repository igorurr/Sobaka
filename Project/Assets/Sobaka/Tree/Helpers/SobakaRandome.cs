using System.Linq;
using UnityEngine;

public class SobakaRandom : ISobakaRandome
{
    public float FloatValue => Random.value;

    public int IntValue => Random.Range(0, int.MaxValue);
}