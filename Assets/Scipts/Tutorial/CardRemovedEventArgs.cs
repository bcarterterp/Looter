public class CardRemovedEventArgs : System.EventArgs
{
    public int CardIndex { get; private set; }

    public CardRemovedEventArgs(int cardIndex)
    {
        CardIndex = cardIndex;
    }
}