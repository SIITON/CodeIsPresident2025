using eWorldCup.Application;
using eWorldCup.Application.Services;
using eWorldCup.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eWorldCup.Presentation.Pages
{
    public class TournamentModel : PageModel
    {
        // Page now acts only as a static host for JS-driven API calls.
        public void OnGet() { }
    }
}