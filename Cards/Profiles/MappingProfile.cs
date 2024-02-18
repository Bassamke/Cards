using AutoMapper;
using Cards.DTOs;
using Cards.Interfaces;
using Cards.Models;
using Cards.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Cards.Profiles;
public class MappingProfile :Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationDTO, IdentityUser>();
        CreateMap<IdentityUser, RegistrationDTO>();
        CreateMap<CreateCardDTO, Card>().ForMember(dest => dest.DateCreated, opt => opt.MapFrom(_ => DateTime.Now.ToUniversalTime()));

    }
}

