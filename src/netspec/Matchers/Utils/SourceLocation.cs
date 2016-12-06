namespace NetSpec.Matchers
{

    public sealed class SourceLocation
    {
        public string file;
        public uint line;

        private SourceLocation()
        {
            file = "Unknown File";
            line = 0;
        }

        public SourceLocation(string file, uint line)
        {
            this.file = file;
            this.line = line;
        }

        public override string ToString()
        {
            return $"{file}:{line}";
        }
    }
}