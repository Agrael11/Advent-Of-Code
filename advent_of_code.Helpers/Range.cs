namespace advent_of_code.Helpers
{
    public class Range(long start, long end)
    {
        private long _start = start;
        private long _end = end;
        public long Start
        {
            get => _start;
            set
            {
                if (value > _end)
                {
                    _start = _end;
                    _end = value;
                }
                else
                {
                    _start = value;
                }
            }
        }
        public long End
        {
            get => _end;
            set
            {
                if (value < _start)
                {
                    _end = _start;
                    _start = value;
                }
                else
                {
                    _end = value;
                }
            }
        }
        public long Length => _end - _start + 1;

        public bool InRange(long value)
        {
            return Start <= value && value <= End;
        }

        public bool InRange(Range range)
        {
            return Start <= range.Start && range.End <= End;
        }

        public bool Overlaps(Range range)
        {
            return (range.Start <= Start && range.End >= End) || (range.Start <= End && range.End >= Start);
        }

        private bool NearOverlaps(Range range)
        {
            return ((range.Start + 1) <= Start && (range.End + 1) >= End) || ((range.Start - 1) <= End && (range.End - 1) >= Start);
        }

        public void Join(Range other)
        {
            Start = long.Min(Start, other.Start);
            End = long.Max(End, other.End);
        }

        public static Range Join(Range range1, Range range2)
        {
            return new Range(long.Min(range1.Start, range2.Start), long.Max(range1.End, range2.End));
        }

        public static List<Range> TryCombine(List<Range> list)
        {
            var newList = list.OrderBy(t => t.Start).ToList();

            for (var i = 0; i < newList.Count; i++)
            {
                for (var j = i+1; j < newList.Count; j++)
                {
                    if (newList[i].NearOverlaps(newList[j]))
                    {
                        newList[i].Join(newList[j]);
                        newList.RemoveAt(j);
                        j--;
                    }
                    else
                    {
                        if (newList[j].Start > newList[i].End) break;
                    }
                }
            }

            return newList;
        }

        public override string ToString()
        {
            return $"{{{Start} - {End}}}";
        }
    }
}
