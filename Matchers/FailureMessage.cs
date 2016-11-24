using System.Linq;

namespace NetSpec.Matchers
{
    public class FailureMessage
    {
        public string expected = "expected";
        public string actualValue = ""; // empty string -> use default; nil -> exclude
        public string to = "to";
        public string postfixMessage = "match";
        public string postfixActual = "";

        public string extendedMessage = null;
        public string userDescription = null;

        public string stringValue
        {
            get
            {
                var value = _stringValueOverride;

                if (value != null)
                {
                    return value;
                }
                else
                {
                    return computeStringValue();
                }
            }
            set
            {
                _stringValueOverride = value;
            }
        }

        internal string _stringValueOverride;

        public FailureMessage()
        {
        }

        public FailureMessage(string stringValue)
        {
            _stringValueOverride = stringValue;
        }

        internal string stripNewlines(string str)
        {
            return string.Join("", str.Split('\n').Select(x => x.Trim()));
        }

        internal string computeStringValue()
        {
            var value = $"{expected} {to} {postfixMessage}";

            if (actualValue != null)
            {
                value = $"{expected} {to} {postfixMessage}, got {actualValue}{postfixActual}";
            }
            value = stripNewlines(value);

            if (extendedMessage != null)
            {
                value += $"\n{stripNewlines(extendedMessage)}";
            }

            if (userDescription != null)
            {
                return $"{userDescription}\n{value}";
            }

            return value;
        }
    }
}