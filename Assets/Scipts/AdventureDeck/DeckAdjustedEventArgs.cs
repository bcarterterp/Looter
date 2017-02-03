public class DeckAdjustedEventArgs : System.EventArgs
{
    public int CardIndex { get; private set; }

    public bool CardRemoved { get; private set; }

    public DeckAdjustedEventArgs(int cardIndex, bool cardRemoved)
    {
        CardIndex = cardIndex;
        CardRemoved = cardRemoved;
    }
}
