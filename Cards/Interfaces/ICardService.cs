namespace Cards.Interfaces;
public interface ICardService
{
    public bool UserHasAccessToCard(int cardId, string userId, string Role);
}
