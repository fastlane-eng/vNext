namespace FastLane.UI.Controls
{
    /// <summary>
    /// Represents a simple panel component with a header.
    /// </summary>
    public class Panel
    {
        public string Header { get; set; }

        public Panel(string header)
        {
            Header = header;
        }
    }
}
