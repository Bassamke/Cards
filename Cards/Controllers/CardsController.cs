using AutoMapper;
using Cards.DTOs;
using Cards.Enums;
using Cards.Interfaces;
using Cards.Models;
using Cards.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CardsController : ControllerBase
{
    private readonly IRepository _repositoryService;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly ICardService _cardService;

    public CardsController(IRepository repositoryService, IAuthService authService, IMapper mapper, ICardService cardService)
    {
        _repositoryService = repositoryService;
        _authService = authService;
        _mapper = mapper;
        _cardService = cardService;
    }
    [HttpPost]
    [Route("AddCard")]
    public IActionResult CreateCard(CreateCardDTO card)
    {
        try
        {
            Card cardToAdd= _mapper.Map<Card>(card);
            cardToAdd.CreatedBy = _authService.GetUserIdFromToken(HttpContext.User);
            Card addedCard = _repositoryService.CreateCard(cardToAdd);
            return CreatedAtAction(nameof(GetCardById), new { id = addedCard.Id }, addedCard);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
        
    }
    [HttpGet]
    [Route("GetCardById/{id}")]
    public IActionResult GetCardById(int id)
    {
        try
        {
            string userId = _authService.GetUserIdFromToken(HttpContext.User);
            string role = _authService.GetRoleFromToken(HttpContext.User);
            if (role == Roles.Admin.ToString())
            {
                Card card = _repositoryService.GetCardById(id);
                return Ok(card);
            }
            else
            {
                Card? card = _repositoryService.GetCardByIdAndUser(id, userId);
                if (card != null)
                {
                    return Ok(card);
                }
            }
            return NoContent();
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet]
    [Route("GetAllCards")]
    public IActionResult GetAllCards(CardsRequest request)
    {
        try
        {
            List<Card> cards = [];
            string userId=_authService.GetUserIdFromToken(HttpContext.User);
            string role= _authService.GetRoleFromToken(HttpContext.User);
            if (role == Roles.Admin.ToString())
            {
                cards=_repositoryService.GetAllCards(request).ToList();
            }
            else
            {
                cards = _repositoryService.GetAllUserCards(userId, request).ToList();
            }
            return Ok(cards);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpDelete]
    [Route("DeleteCard/{id}")]
    public IActionResult DeleteCard(int id)
    {
        try
        {
            string userId = _authService.GetUserIdFromToken(HttpContext.User);
            string role = _authService.GetRoleFromToken(HttpContext.User);
            bool userHasAccessToCard=_cardService.UserHasAccessToCard(id, userId, role);
            if (userHasAccessToCard)
            {
                Card? card = _repositoryService.GetCardById(id);
                Card DeletedCard = _repositoryService.DeleteCard(card);
                return Ok("Succesfully Deleted Card");

            }
            else
            {
                return StatusCode(403, "Card Does not Exist or user Doesn't Have Enough Rights to Delete it");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPut]
    [Route("UpdateCard/{id}")]
    public IActionResult UpdateCard(UpdateCardDTO card, [FromRoute]int id)
    {
        try
        {
            string userId = _authService.GetUserIdFromToken(HttpContext.User);
            string role = _authService.GetRoleFromToken(HttpContext.User);
            card.Id = id;
            bool userHasAccessToCard = _cardService.UserHasAccessToCard(card.Id, userId, role);
            if (userHasAccessToCard)
            {
                Card UpDatedCard = _repositoryService.UpdateCard(card);
                return Ok(UpDatedCard);

            }
            else
            {
                return StatusCode(403, "Card Does not Exist or user Doesn't Have Enough Rights to Update it");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}
