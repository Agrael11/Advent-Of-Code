namespace IntMachine
{
    public class Memory(int size)
    {
        private int[] Data = new int[size];

        public bool TryRead(int pointer, out int? result)
        {
            if (pointer < 0 || pointer >= Data.Length)
            {
                result = null;
                return false;
            }

            result = Data[pointer];
            return true;
        }

        public bool TryWrite(int pointer, int value)
        {
            if (pointer < 0 || pointer >= Data.Length)
            {
                return false;
            }

            Data[pointer] = value;
            return true;
        }

        public bool TryReadIndirect(int pointer, out int? result)
        {
            if (!TryRead(pointer, out var indirect) || indirect is null)
            {
                result = null;
                return false;
            }

            return TryRead(indirect.Value, out result);
        }

        public bool TryWriteIndirect(int pointer, int value)
        {
            if (!TryRead(pointer, out var indirect) || indirect is null)
            {
                return false;
            }

            return TryWrite(indirect.Value, value);
        }




        public static explicit operator Memory(int[] data)
        {
            var mem = new Memory(data.Length)
            {
                Data = (int[])data.Clone()
            };
            return mem;
        }

        public static explicit operator int[](Memory mem)
        {
            return mem.Data;
        }
    }
}
