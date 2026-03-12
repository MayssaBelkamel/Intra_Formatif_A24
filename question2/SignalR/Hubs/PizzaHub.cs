using Microsoft.AspNetCore.SignalR;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;

        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }

        public override async Task OnConnectedAsync()
        {
            _pizzaManager.AddUser();
            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _pizzaManager.RemoveUser();
            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);
            await base.OnConnectedAsync();
        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            string groupName = _pizzaManager.GetGroupName(choice);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("money", _pizzaManager.Money[(int)choice]);

            await Clients.Group(groupName).SendAsync("nbPizzas", _pizzaManager.NbPizzas[(int)choice]);

            await Clients.Group(groupName).SendAsync("price", _pizzaManager.PIZZA_PRICES[(int)choice]);

           
        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
        }

        public async Task AddMoney(PizzaChoice choice)
        {
            _pizzaManager.IncreaseMoney(choice);
            await Clients.Group(_pizzaManager.GetGroupName(choice)).SendAsync("money", _pizzaManager.Money[(int)choice]);

        }

        public async Task BuyPizza(PizzaChoice choice)
        {
            _pizzaManager.BuyPizza(choice);
            await Clients.Group(_pizzaManager.GetGroupName(choice)).SendAsync("money", _pizzaManager.Money[(int)choice]);

            await Clients.Group(_pizzaManager.GetGroupName(choice)).SendAsync("nbPizzas", _pizzaManager.NbPizzas[(int)choice]);

        }
    }
}
