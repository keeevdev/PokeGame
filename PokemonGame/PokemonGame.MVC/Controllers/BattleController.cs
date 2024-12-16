using PokemonFame.MVC.Models;
using PokemonFame.MVC.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PokemonFame.MVC.Controllers
{
    public class BattleController : Controller
    {
        private readonly PokemonService _pokemonService;
        private static Pokemon _player1;
        private static Pokemon _player2;
        private static bool _player1Turn = true;

        public BattleController()
        {
            _pokemonService = new PokemonService();
        }

        public async Task<ActionResult> Index()
        {
            if (Session["NombreUsuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var pokemonList = await _pokemonService.GetAllPokemonAsync();
            ViewBag.PokemonList = pokemonList;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> StartBattle(string pokemon1Name, string pokemon2Name, bool isAI)
        {
            if (Session["NombreUsuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            _player1 = await _pokemonService.GetPokemonAsync(pokemon1Name);
            _player2 = await _pokemonService.GetPokemonAsync(pokemon2Name);

            if (_player1 == null || _player2 == null)
            {
                ViewBag.Error = "Uno o ambos Pokémon no se encontraron.";
                return View("Index");
            }

            _player1.Hp = 100;
            _player2.Hp = 100;
            _player2.IsAI = isAI;

            TempData["Log"] = $"¡La batalla comienza entre {_player1.Name} y {_player2.Name}!";
            return RedirectToAction("Battle");
        }

        public ActionResult Battle()
        {
            if (Session["NombreUsuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Log = TempData["Log"];
            ViewBag.IsCritical = TempData["IsCritical"];
            return View(new BattleViewModel
            {
                Player1 = _player1,
                Player2 = _player2,
                Player1Turn = _player1Turn
            });
        }

        [HttpPost]
        public ActionResult Attack(string moveName)
        {
            if (Session["NombreUsuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var attacker = _player1Turn ? _player1 : _player2;
            var defender = _player1Turn ? _player2 : _player1;

            if (!_player1Turn && _player2.IsAI)
            {
                var random = new System.Random();
                moveName = attacker.Moves[random.Next(attacker.Moves.Count)].Name;
            }

            var move = attacker.Moves.Find(m => m.Name == moveName);
            if (move == null) return RedirectToAction("Battle");

            var isCritical = new System.Random().Next(1, 101) <= 15;
            var damage = move.Power - (defender.Defense / 2);
            if (isCritical) damage *= 2;

            defender.Hp -= damage > 0 ? damage : 1;
            defender.Hp = defender.Hp < 0 ? 0 : defender.Hp;

            TempData["Log"] = $"{attacker.Name} usó {move.Name}. {(isCritical ? "¡Golpe crítico! " : "")}Causó {damage} de daño. {defender.Name} tiene {defender.Hp} HP restantes.";
            TempData["IsCritical"] = isCritical;

            if (defender.Hp <= 0)
            {
                TempData["Log"] += $"\n{defender.Name} ha sido derrotado. ¡{attacker.Name} es el ganador!";
                return RedirectToAction("EndBattle");
            }

            _player1Turn = !_player1Turn;

            if (!_player1Turn && _player2.IsAI)
            {
                return RedirectToAction("IAAttack");
            }

            return RedirectToAction("Battle");
        }

        public ActionResult IAAttack()
        {
            var attacker = _player2;
            var defender = _player1;

            var random = new System.Random();
            var move = attacker.Moves[random.Next(attacker.Moves.Count)];

            var isCritical = new System.Random().Next(1, 101) <= 15;
            var damage = move.Power - (defender.Defense / 2);
            if (isCritical) damage *= 2;

            defender.Hp -= damage > 0 ? damage : 1;
            defender.Hp = defender.Hp < 0 ? 0 : defender.Hp;

            TempData["Log"] = $"{attacker.Name} usó {move.Name}. {(isCritical ? "¡Golpe crítico! " : "")}Causó {damage} de daño. {defender.Name} tiene {defender.Hp} HP restantes.";
            TempData["IsCritical"] = isCritical;

            if (defender.Hp <= 0)
            {
                TempData["Log"] += $"\n{defender.Name} ha sido derrotado. ¡{attacker.Name} es el ganador!";
                return RedirectToAction("EndBattle");
            }

            _player1Turn = !_player1Turn;
            return RedirectToAction("Battle");
        }

        public ActionResult EndBattle()
        {
            if (Session["NombreUsuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(new BattleViewModel
            {
                Player1 = _player1,
                Player2 = _player2,
                Player1Turn = _player1Turn
            });
        }
    }
}









