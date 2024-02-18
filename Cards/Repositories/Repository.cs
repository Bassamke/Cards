using Cards.Data;
using Cards.DTOs;
using Cards.Interfaces;
using Cards.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Cards.Repositories
{
    public class Repository : IRepository
    {
        private readonly CardDbContext _context;
        public Repository( CardDbContext context)
        {
            _context = context;
        }
        public Card CreateCard(Card card)
        {
            card.Status = "To Do";
            _context.cards.Add(card);
            _context.SaveChanges();
            return _context.Entry(card).Entity;
        }

        public Card DeleteCard(Card card)
        {
            _context.cards.Remove(card);
            _context.SaveChanges();
            return _context.Entry(card).Entity;
        }

        public IQueryable<Card> GetAllCards(CardsRequest request)
        {
            IQueryable<Card> cards = _context.cards; 

            if (request.SearchParameters != null)
            {
                if (!string.IsNullOrEmpty(request.SearchParameters.Name))
                {
                    cards = cards.Where(c => c.Name.ToLower() == request.SearchParameters.Name.ToLower());
                }

                if (!string.IsNullOrEmpty(request.SearchParameters.Color))
                {
                    cards = cards.Where(c => c.Color.ToLower() == request.SearchParameters.Color.ToLower());
                }

                if (!string.IsNullOrEmpty(request.SearchParameters.Status))
                {
                    cards = cards.Where(c => c.Status.ToLower() == request.SearchParameters.Status.ToLower());
                }

                if (request.SearchParameters.StartDate != null && request.SearchParameters.EndDate != null)
                {
                    cards = cards.Where(c => c.DateCreated >= request.SearchParameters.StartDate && c.DateCreated <= request.SearchParameters.EndDate);
                }
            }

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                switch (request.SortBy.ToLower())
                {
                    case "name":
                        cards = cards.OrderBy(c => c.Name);
                        break;
                    case "color":
                        cards = cards.OrderBy(c => c.Color);
                        break;
                    case "status":
                        cards = cards.OrderBy(c => c.Status);
                        break;
                    case "datecreated":
                        cards = cards.OrderBy(c => c.DateCreated);
                        break;
                    default:
                        break;
                }
            }

            if (request.PageSize > 0 && request.PageNumber > 0)
            {
                int cardsToSkip = (request.PageNumber - 1) * request.PageSize;
                cards = cards.Skip(cardsToSkip).Take(request.PageSize);
            }

            return cards;
        }

        public IQueryable<Card> GetAllUserCards(string userId, CardsRequest request)
        {
            return _context.cards.Where(card=>card.CreatedBy == userId);
        }

        public Card GetCardById(int id)
        {
            Card? card= _context.cards.Where(card=>card.Id==id).FirstOrDefault();
            return card;
        }
        public Card GetCardByIdAndUser(int id, string userId)
        {
            Card? card = _context.cards.Where(c => c.Id == id && c.CreatedBy==userId).FirstOrDefault();
            return card;
        }

        public Card? UpdateCard(UpdateCardDTO card)
        {
            Card? cardToUpdate = _context.cards.Where(c => c.Id == card.Id).FirstOrDefault();
            cardToUpdate.Name= card.Name;
            cardToUpdate.Description= card.Description;
            cardToUpdate.Color= card.Color;
            cardToUpdate.Status= card.Status;

            _context.SaveChanges();

           return _context.cards.Where(c => c.Id == card.Id).FirstOrDefault();
        }
    }
}
