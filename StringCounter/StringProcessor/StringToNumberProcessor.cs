namespace StringCounter.StringProcessor
{
    /// <summary>
    /// Convert delimited integers within a string into an array of integers
    /// </summary>
    public class StringToNumberProcessor : BaseStringProcessor<int>
    {
        protected override int ParseEntry(string entry)
        {
            return int.Parse(entry);
        }
    }
}
