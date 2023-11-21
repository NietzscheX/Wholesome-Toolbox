using robotManager.Helpful;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using static wManager.Wow.Helpers.PathFinder;

namespace WholesomeToolbox
{

    public class FlyHelper
    {
        public static void FlyTo(Vector3 pos)
        {
            int processId = (int)wManager.Wow.Memory.WowMemory.Memory.GetProcess().Id;
            MemoryRobot.Memory memory = new MemoryRobot.Memory(processId);
            uint BaseAddress = (uint)memory.ReadInt32(0xCD87A8);
            BaseAddress = (uint)memory.ReadInt32(BaseAddress + 0x34);
            BaseAddress = (uint)memory.ReadInt32(BaseAddress + 0x24);
            //wManager.Wow.Memory.WowMemory.Memory.WriteByte(BaseAddress + 0x7CD,(byte)0x04);
            memory.WriteFloat(BaseAddress + 0x798, pos.X);
            memory.WriteFloat(BaseAddress + 0x79C, pos.Y);
            memory.WriteFloat(BaseAddress + 0x7A0, pos.Z);

            wManager.Wow.Helpers.Move.JumpOrAscend(wManager.Wow.Helpers.Move.MoveAction.PressKey, 100);

            Thread.Sleep(3000);
            //wManager.Wow.Memory.WowMemory.Memory.WriteByte(BaseAddress + 0x7CD, (byte)0x00);
        }


    }

}
