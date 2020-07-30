using System;

public enum EquipTypes
{
    Invalid = 0,
    Shoes = 107,
    Cape = 110,
    LongCoat = 105,
    Gloves = 108
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
        return EquipTypes.Invalid;
    }
}
