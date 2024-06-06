namespace advent_of_code.Helpers
{
    public class VariableWithPrevious<T>(T variable)
    {
        private T _value = variable;
        private VariableWithPrevious<T>? _previous = null;

        public T Value
        {
            get => _value;
            set
            {
                if (_previous == null)
                {
                    _previous = new(_value);
                }
                else
                {
                    _previous.Value = _value;
                }
                _value = value;
            }
        }

        public VariableWithPrevious<T> Previous
        {
            get
            {
                if (_previous == null)
                {
                    return new(_value);
                }
                return _previous;
            }
        }

        public void SetManual(T value, VariableWithPrevious<T>? previous)
        {
            _value = value;
            _previous = previous;
        }

        public List<T> Revert(int times = 1)
        {
            var backup = new List<T>();
            if (times < 1) return backup;
            VariableWithPrevious<T>? previous = null;
            var value = _value;
            while (times >= 1)
            {
                backup.Add(value);
                if (_previous == null) break;
                previous = _previous._previous;
                value = _previous.Value;
                times--;
            }
            _previous = previous;
            _value = value;
            return backup;
        }

        public List<T> GetHistory(bool includeCurrent = true)
        {
            var history = new List<T>();
            if (includeCurrent) history.Add(_value);
            var previous = _previous;
            while (previous != null)
            {
                history.Add(previous.Value);
                previous = previous._previous;
            }
            history.Reverse();
            return history;
        }

        public int GetDepth()
        {
            var i = 0;
            var previous = _previous;
            while (previous != null)
            {
                previous = previous._previous;
                i++;
            }
            return i;
        }

        public VariableWithPrevious<T> GetPrevious(int depth = 0)
        {
            var previous = _previous;
            if (previous is null) return this;
            for (var i = 0; i < depth; i++)
            {
                var previousT = previous._previous;
                if (previousT is null) return previous;
                previous = previousT;
            }
            return previous;
        }

        public static implicit operator T(VariableWithPrevious<T> t)
        {
            return t.Value;
        }

        public static implicit operator VariableWithPrevious<T>(T t)
        {
            return new(t);
        }

        public override string ToString()
        {
            if (Value is not null)
            {
                var retString = Value.ToString();
                if (retString is not null)
                {
                    return retString;
                }

            }
            return "null";
        }
    }
}