namespace StringCounter.StringProcessor
{
    public interface IStringProcessor<T>
    {
        T[] Process(string input, char[] delimiters);
    }
}
