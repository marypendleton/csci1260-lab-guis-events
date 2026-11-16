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
    private readonly Rewards rewards = new Rewards();

    // Builds the window, connects listeners, and sets the initial buttons.
    public MainWindow()
    {
        InitializeComponent();

        bar.OrderPlaced += Bar_OrderPlaced;
        bar.OrderReady += Bar_OrderReady;
        bar.AllCaughtUp += Bar_AllCaughtUp;

        CustomerBox.TextChanged += Boxes_TextChanged;
        DrinkBox.TextChanged += Boxes_TextChanged;

        rewards.Connect(bar);
        rewards.FreeDrinkEarned += Rewards_FreeDrinkEarned;

        UpdateButtons();
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

    // Refreshes the waiting list and buttons when an order is placed.
    private void Bar_OrderPlaced(object? sender, OrderEventArgs e)
    {
        ShowWaiting();
        UpdateButtons();
    }

    // Shows the ready order and refreshes the waiting list and buttons.
    private void Bar_OrderReady(object? sender, OrderEventArgs e)
    {
        PickupList.Items.Add($"#{e.Order.Number} {e.Order.Customer}");
        ShowWaiting();
        UpdateButtons();
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

    // Updates the buttons whenever either input box changes.
    private void Boxes_TextChanged(object? sender, TextChangedEventArgs e)
    {
        UpdateButtons();
    }

    // Enables each button only when its action can work.
    private void UpdateButtons()
    {
        PlaceButton.IsEnabled = !string.IsNullOrWhiteSpace(CustomerBox.Text)
                                && !string.IsNullOrWhiteSpace(DrinkBox.Text);
        ReadyButton.IsEnabled = bar.WaitingCount > 0;
    }

    // Displays the customer who earned a free drink.
    private void Rewards_FreeDrinkEarned(object? sender, OrderEventArgs e)
    {
        RewardText.Text = $"Free drink earned: {e.Order.Customer}";
    }
}