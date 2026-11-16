namespace GuiEventsLab;

// Buzzer: a subscriber that can also stop listening. It records the
// messages a customer's phone would show, like
// "Buzz: Ada, your latte is ready."
//
// Part B. Write Connect, Disconnect, and OnOrderReady along with the
// Session 1 demo.
public class Buzzer
{
    private readonly List<string> messages = new List<string>();

    // Given.
    public List<string> Messages()
    {
        return new List<string>(messages);
    }

    // Subscribes OnOrderReady to the bar's OrderReady event.
    public void Connect(CoffeeBar bar)
    {
        bar.OrderReady += OnOrderReady;
    }

    // Unsubscribes with the same method that subscribed.
    public void Disconnect(CoffeeBar bar)
    {
        bar.OrderReady -= OnOrderReady;
    }

    // Records a buzzer message whenever an order is ready.
    private void OnOrderReady(object? sender, OrderEventArgs e)
    {
        messages.Add($"Buzz: {e.Order.Customer}, your {e.Order.Drink} is ready.");
    }
}