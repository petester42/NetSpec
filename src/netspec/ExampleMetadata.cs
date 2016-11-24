namespace NetSpec
{
    public sealed class ExampleMetadata
    {

        public Example example { get; }

        public int exampleIndex { get; }

        internal ExampleMetadata(Example example, int exampleIndex)
        {
            this.example = example;
            this.exampleIndex = exampleIndex;
        }
    }
}
