using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GuiEventsLab;

// MainWindow: the code behind the window. It shows the coffee bar and asks
// it to do things. Every decision about orders stays in CoffeeBar.
//
// Part C and Part D are written along with the Session 2 demos.
// Exercise 3 and Exercise 4 are yours.
public partial class MainWindow : Window
{
    private readonly CoffeeBar bar = new CoffeeBar();

    // Builds the window and subscribes to coffee bar events.
    public MainWindow()
    {
        InitializeComponent();

        bar.OrderPlaced += Bar_OrderPlaced;
        bar.OrderReady += Bar_OrderReady;
        bar.AllCaughtUp += Bar_AllCaughtUp;

        // TODO: Part D, demo. Subscribe to TextChanged on both boxes,
        // then set the buttons for an empty window.

        // TODO: Exercise 4. Connect a Rewards object to the bar and
        // subscribe to its FreeDrinkEarned event.
    }

    // Places an order, clears successful input, or shows the refusal.
    private void PlaceButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bar.Place(CustomerBox.Text ?? "", DrinkBox.Text ?? "");
            CustomerBox.Text = "";
            DrinkBox.Text = "";
            StatusText.Text = "";
        }
        catch (ArgumentException ex)
        {
            StatusText.Text = ex.Message;
        }
    }

    // Asks the coffee bar to finish the oldest waiting order.
    private void ReadyButton_Click(object? sender, RoutedEventArgs e)
    {
        bar.FinishNext();
    }

    // Refreshes the waiting list when an order is placed.
    private void Bar_OrderPlaced(object? sender, OrderEventArgs e)
    {
        ShowWaiting();
    }

    // Adds the ready order to pickup and refreshes the waiting list.
    private void Bar_OrderReady(object? sender, OrderEventArgs e)
    {
        PickupList.Items.Add($"#{e.Order.Number} {e.Order.Customer}");
        ShowWaiting();
    }

    // Shows a status message when the last waiting order is finished.
    private void Bar_AllCaughtUp(object? sender, EventArgs e)
    {
        StatusText.Text = "All caught up.";
    }

    // Displays all waiting orders from oldest to newest.
    private void ShowWaiting()
    {
        WaitingList.Items.Clear();

        foreach (Order order in bar.Waiting())
        {
            WaitingList.Items.Add(order.ToString());
        }
    }
}