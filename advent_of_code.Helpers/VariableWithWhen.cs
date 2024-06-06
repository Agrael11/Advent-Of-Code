namespace advent_of_code.Helpers
{
    public class VariableWithWhen<T>(T variable)
    {
        readonly private List<(Func<T, bool>? condition, Action action)> eventHandlers = new();

        public int When(Func<T, bool>? condition, Action action)
        {
            eventHandlers.Add((condition, action));
            if (condition == null || condition.Invoke(_value.Value))
            {
                action.Invoke();
            }
            return eventHandlers.Count - 1;
        }

        public void RemoveWhen(Func<T, bool>? condition, Action action)
        {
            eventHandlers.Remove((condition, action));
        }

        public void RemoveWhen(int index)
        {
            eventHandlers.RemoveAt(index);
        }

        private void Invoke()
        {
            // Check each event handler and invoke the associated action if the condition is met
            foreach (var (condition, action) in eventHandlers)
            {
                if (condition == null || condition.Invoke(_value.Value))
                {
                    action.Invoke();
                }
            }
        }

        private void UpdateVariable(T value)
        {
            // Update the variable
            _value.Value = value;

            Invoke();
        }

        readonly private VariableWithPrevious<T> _value = new(variable);
        public T Value
        {
            get => _value.Value;
            set
            {
                if (_value != null && _value.Value != null && !_value.Value.Equals(value))
                {
                    UpdateVariable(value);
                }
            }
        }

        public List<T> Revert(int times = 1)
        {
            var backup = _value.Revert(times);
            Invoke();
            return backup;
        }

        public List<T> GetHistory(bool includeCurrent = true)
        {
            return _value.GetHistory(includeCurrent);
        }
    }
}