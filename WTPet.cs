using wManager.Wow.Helpers;

namespace WholesomeToolbox
{
    /// <summary>
    /// Pets related methods
    /// </summary>
    public class WTPet
    {
        /// <summary>
        /// Returns the book index of the pet spell passed as argument.
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns>Pet spell index, 0 if not found</returns>
        public static int GetPetSpellIndex(string spellName) // incorrect
        {
            int spellindex = Lua.LuaDoString<int>($@"
            for i=1,10 do
                local name, _ = GetSpellName(i, ""pet"")
                if name == ""{spellName.EscapeLuaString()}"" then
                    return i
                end
            end");
            return spellindex;
        }

        /// <summary>
        /// Returns whether your pet knows the specified spell. Must be in action bar.
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns></returns>
        public static bool PetKnowsSpell(string spellName, int loops = 10) => GetPetSpellIndex(spellName) != 0;

        /// <summary>
        /// Returns the cooldown in milliseconds of the pet spell index passed as argument in seconds
        /// </summary>
        /// <param name="petSpellName"></param>
        /// <returns>Pet spell cooldown in milliseconds</returns>
        public static int PetSpellCooldown(int petSpellIndex)
        {
            return Lua.LuaDoString<int>($@"
                    local start, duration, enable = GetSpellCooldown({petSpellIndex}, 'pet') 
                    return (duration - (GetTime() - start)) * 1000
                ");
        }

        /// <summary>
        /// Returns whether a pet spell is available by index (off cooldown). Must be in action bar.
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns>true if the pet spell is ready</returns>
        public static bool PetSpellReady(int petSpellIndex) => PetSpellCooldown(petSpellIndex) < 0;

        /// <summary>
        /// Toggles Pet spell autocast (pass true as second argument to toggle on, or false to toggle off)
        /// </summary>
        /// <param name="spellName"></param>
        /// <param name="toggle"></param>
        public static void TogglePetSpellAuto(int petSpellIndex, bool toggle)
        {
            if (petSpellIndex <= 0) return;
            if ((toggle && !PetSpellIsAutocast(petSpellIndex)) || (!toggle && PetSpellIsAutocast(petSpellIndex)))
            {
                Lua.LuaDoString($@"ToggleSpellAutocast({petSpellIndex}, 'pet');");
            }
        }

        /// <summary>
        /// Returns whether te specified spell index is on auto cast
        /// </summary>
        /// <param name="petSpellIndex"></param>
        /// <returns>true if the pet spell is on auto cast</returns>
        public static bool PetSpellIsAutocast(int petSpellIndex)
        {
             return Lua.LuaDoString<bool>($@"
                    local _, autostate = GetSpellAutocast({petSpellIndex}, 'pet'); 
                    return autostate == 1;
                ");             
        }

        /// <summary>
        /// Returns the pet happiness (1 = unhappy, 2 = content, 3 = happy)
        /// </summary>
        public static int PetHappiness => Lua.LuaDoString<int>($@"
                local happiness, damagePercentage, loyaltyRate = GetPetHappiness();
                return happiness;
            ");

        /// <summary>
        /// Feeds your pet with the sepcified food
        /// </summary>
        /// <param name="foodName"></param>
        public static void TBCFeedPet(string foodName)
        {
            Lua.LuaDoString("CastSpellByName('Feed Pet');");
            Lua.LuaDoString($@"UseItemByName(""{foodName.EscapeLuaString()}"");");
        }
    }
}
