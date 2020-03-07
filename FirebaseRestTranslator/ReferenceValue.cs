namespace FirebaseRestTranslator
{
    public class ReferenceValue
    {
        private readonly string _value;

        public ReferenceValue(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}