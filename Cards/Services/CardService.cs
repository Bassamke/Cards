using Cards.Enums;
using Cards.Interfaces;
using Cards.Models;
using Cards.Repositories;

namespace Cards.Services;
public class CardService :ICardService
{
    private readonly IRepository _repository;
    public CardService(IRepository repository)
    {
        _repository = repository;
    }
    public bool UserHasAccessToCard(int cardId, string userId, string role)
    {
        if (role == Roles.Admin.ToString())
        {
            Card card = _repository.GetCardById(cardId);
            if (card != null)
            {          
                return true;
            }
        }
        else
        {
            Card? card = _repository.GetCardByIdAndUser(cardId, userId);
            if (card != null)
            {
                return true;
            }
        }
        return false;
    }

}
