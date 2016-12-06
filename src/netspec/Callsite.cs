using System;

namespace NetSpec
{
    public sealed class Callsite : IEquatable<Callsite>
    {
        public string file { get; }

        public uint line { get; }

        internal Callsite(string file, uint line)
        {
            this.file = file;
            this.line = line;
        }

        public bool Equals(Callsite other)
        {
            return string.Equals(file, other.file, StringComparison.Ordinal) && line == other.line;
        }

        public override bool Equals(Object other)
        {
            var castedOther = other as Callsite;
            if (castedOther == null)
            {
                return false;
            }
            return Equals(castedOther);
        }

        public override int GetHashCode()
        {
            return file.GetHashCode() + line.GetHashCode();
        }

        public static bool operator ==(Callsite leftOperand, Callsite rightOperand)
        {
            if (ReferenceEquals(null, leftOperand))
            {
                return ReferenceEquals(null, rightOperand);
            }

            return leftOperand.Equals(rightOperand);
        }

        public static bool operator !=(Callsite leftOperand, Callsite rightOperand)
        {
            return !(leftOperand == rightOperand);
        }
    }
}
