using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Legend
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum Screens
    {
        Home,
        Intro,
        Continue,
        Level,
        GameOver
    }

    public enum PlayerState
    {
        Idle,
        Moving,
        Attacking,
        Interacting,
        CancelibleInteracting
    }

    public enum ItemOnGroundState
    {
        OnGround,
        GettingPickedUp,
        DoneAnimating
    }

    public enum PortalState
    {
        Spinning,
        Smaller,
        Bigger
    }

    public enum WeaponPower
    {
        no,
        poison,
        stun,
        slowing
    }

    public enum ItemType
    {
        Armour,
        Consumable,
        Weapon,
        Misc
    }

    public enum ToolTipObjType
    {
        Text,
        Key,
        Player
    }

    public enum KeyAnimationType
    {
        Inventory,
        Player,
        No
    }
}
