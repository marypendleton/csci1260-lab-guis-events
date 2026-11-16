namespace GuiEventsLab;

// OrderEventArgs: the details that travel with an order event.
// Every handler receives one of these as its e parameter.
//
// Part A. Write the constructor along with the Session 1 demo.
public class OrderEventArgs : EventArgs
{
    public Order Order { get; }

    // Stores the order that the event carries.
    public OrderEventArgs(Order order)
    {
        Order = order;
    }
}