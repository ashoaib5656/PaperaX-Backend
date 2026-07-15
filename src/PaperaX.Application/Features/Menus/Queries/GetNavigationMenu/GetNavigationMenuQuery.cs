using MediatR;
using System.Collections.Generic;
using PaperaX.Application.Features.Menus.DTOs;
using PaperaX.Domain.Enums;

namespace PaperaX.Application.Features.Menus.Queries.GetNavigationMenu
{
    public class GetNavigationMenuQuery : IRequest<List<MenuItemDto>>
    {
        public MenuPlacement Placement { get; set; }
        public string? RoleName { get; set; }
    }
}
