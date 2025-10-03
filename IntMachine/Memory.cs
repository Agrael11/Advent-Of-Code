namespace IntMachine
{
    public class Memory()
    {
        private Dictionary<long, long> Data = [];

        public Memory Clone()
        {
            return new Memory()
            {
                Data = Data.ToDictionary(t => t.Key, t => t.Value)
            };
        }

        public bool TryRead(long pointer, out long? result)
        {
            if (pointer < 0)
            {
                result = null;
                return false;
            }
            if (!Data.TryGetValue(pointer, out var tryresult))
            {
                result = 0;
                return true;
            }

            result = tryresult;
            return true;
        }

        public bool TryWrite(long pointer, long value)
        {
            if (pointer < 0)
            {
                return false;
            }

            Data[pointer] = value;
            return true;
        }

        public bool TryReadIndirect(long pointer, out long? result)
        {
            if (!TryRead(pointer, out var indirect) || indirect is null)
            {
                result = null;
                return false;
            }

            return TryRead(indirect.Value, out result);
        }

        public bool TryWriteIndirect(long pointer, long value)
        {
            if (!TryRead(pointer, out var indirect) || indirect is null)
            {
                return false;
            }

            return TryWrite(indirect.Value, value);
        }




        public static explicit operator Memory(long[] data)
        {
            var mem = new Memory()
            {
                Data = data.Select((v, i) => (v, i)).ToDictionary(t => (long)t.i, t => t.v)
            };
            return mem;
        }
    }
}
