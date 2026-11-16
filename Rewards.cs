namespace GuiEventsLab;

// Rewards: a subscriber that is also a publisher. It counts finished drinks
// for each customer and announces FreeDrinkEarned on every third one.
//
// Exercise 2. Write Connect and a private handler named OnOrderReady.
public class Rewards
{
    public const int DrinksPerReward = 3;

    private readonly Dictionary<string, int> drinks = new Dictionary<string, int>();

    // Given. Raise it with the same OrderEventArgs the bar sent you.
    public event EventHandler<OrderEventArgs>? FreeDrinkEarned;

    // Given.
    public int DrinksFor(string customer)
    {
        return drinks.GetValueOrDefault(customer);
    }

    // Subscribe the handler to the bar's OrderReady event.
    public void Connect(CoffeeBar bar)
    {
        bar.OrderReady += OnOrderReady;
    }

    // Counts finished drinks and announces each earned free drink.
    private void OnOrderReady(object? sender, OrderEventArgs e)
    {
        string name = e.Order.Customer;
        drinks[name] = drinks.GetValueOrDefault(name) + 1;

        if (drinks[name] % DrinksPerReward == 0)
        {
            FreeDrinkEarned?.Invoke(this, e);
        }
    }
}