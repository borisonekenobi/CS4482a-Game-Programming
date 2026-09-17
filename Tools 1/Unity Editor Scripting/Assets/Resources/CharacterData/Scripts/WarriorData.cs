using Types;
using UnityEngine;

[CreateAssetMenu(fileName="New Warrior Data", menuName="Character Data/Warrior")]
public class WarriorData : CharacterData
{
    public WarriorClassType classType;
    public WarriorWpnType wpnType;
}
