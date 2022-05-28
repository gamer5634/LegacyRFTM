namespace MucciArena.Entities.Tasks
{
    public class SecondCounter : IUpdatable
    {
        public float SecondsElapsed { get => _secondsSinceLastEvent; }
        public float SecondsCap { get => _secondsCap;set=> _secondsCap = value; }

        private float _secondsSinceLastEvent;
        private float _secondsCap;
        private bool _isCap;

        public SecondCounter()
        {
            _isCap = false;
            _secondsSinceLastEvent = 0;
        }

        public SecondCounter(float secondsCap)
        {
            _isCap = true;
            _secondsCap = secondsCap;
            _secondsSinceLastEvent = 0;
        }

        public bool IsCapReached()
        {
            return _isCap && _secondsSinceLastEvent >= _secondsCap;
        }

        public bool IsCapNotReached()
        {
            return _isCap && _secondsSinceLastEvent < _secondsCap;
        }

        public void ResetCounter()
        {
            _secondsSinceLastEvent = 0;
        }

        public float GetSecondsAfterCapReach()
        {
            return _secondsSinceLastEvent - _secondsCap;
        }

        public void Update(float delta)
        {
            _secondsSinceLastEvent += delta;
        }
    }
}
