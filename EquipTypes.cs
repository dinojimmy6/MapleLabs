using System;

public enum EquipTypes
{
    Misc = 0,
    Face = 1,
    Shoes = 107,
    Cape = 110,
    LongCoat = 105,
    Glove = 108,
    Weapon = 170
}
public enum WeaponTypes
{
    Invalid = 0,
    ShiningRod = 21,
    SoulShooter = 22,
    Desperado = 23,
    WhipBlade = 24,
    Sceptre = 25,
    PsyLimiter = 26,
    Chain = 27,
    LucentGauntlet = 28,
    RitualFan = 29,
    OneHandedSword = 30,
    OneHandedAxe = 31,
    OneHandedMace = 32,
    Dagger = 33,
    Katara = 34,
    Cane = 36,
    Wand = 37,
    Staff = 38,
    TwoHandedSword = 40,
    TwoHandedAxe = 41,
    TwoHandedMace = 42,
    Spear = 43,
    Polearm = 44,
    Bow = 45,
    Crosssbow = 46,
    Claw = 47,
    Knuckle = 48,
    Gun = 49,
    DualBowgun = 52,
    Cannon = 53,
    Katana = 54,
    Fan = 55,
    Lapis = 56,
    Lazuli = 57,
    HandCannon = 58,
    AncientBow = 59
}

public static class EquipTypesExtension
{

    public static EquipTypes GetEquipTypeFromId(string id)
    {
        int temp = Int32.Parse(id) / 10000;
        foreach(EquipTypes et in Enum.GetValues(typeof(EquipTypes)))
        {
            if(temp == (int) et)
            {
                return et;
            }
        }
        return EquipTypes.Misc;
    }
}
