namespace Task13
{
    public class StringComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            return string.Compare(x, y);
        }
    }
}
