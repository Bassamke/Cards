using Cards.DTOs;
using Cards.Models;

namespace Cards.Repositories;
public interface IRepository
{
    Card CreateCard(Card card);
    Card UpdateCard(UpdateCardDTO card);
    Card DeleteCard(Card card);
    Card GetCardById(int id);
    Card GetCardByIdAndUser(int id, string userId);
    IQueryable<Card> GetAllCards(CardsRequest request);
    IQueryable<Card> GetAllUserCards(string userId, CardsRequest request);

}
