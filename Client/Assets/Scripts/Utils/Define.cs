using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Define
{
    public enum  AttRange
    {
        Knight = 1,
        Archer = 3,
        Mage = 3
    }

    public enum DefenderType
    {
        Knight=10,
        Archer=20,
        Mage=30
    }
    public enum MonsterType
    {
        Elf,
        Goblin,
        Skeleton
    }

    public enum State
    {
        Skill,
        Attack,
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}
