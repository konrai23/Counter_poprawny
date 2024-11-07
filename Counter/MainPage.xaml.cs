using System;
using Microsoft.Maui.Controls;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Dodaje nowy licznik na podstawie nazwy wpisanej przez użytkownika
        private void OnAddCounterClicked(object sender, EventArgs e)
        {
            string counterName = CounterNameEntry.Text?.Trim();
            if (string.IsNullOrEmpty(counterName))
            {
                DisplayAlert("Błąd", "Proszę wpisać nazwę dla licznika.", "OK");
                return;
            }

            var counterView = CreateCounterView(counterName);
            CountersContainer.Children.Add(counterView);
            CounterNameEntry.Text = string.Empty;
        }

        // Tworzy widok licznika z nazwą, etykietą stanu oraz przyciskami do zwiększania i zmniejszania wartości
        private StackLayout CreateCounterView(string counterName)
        {
            var counterLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10),
                BackgroundColor = Color.FromRgb(30, 30, 30),
                Margin = new Thickness(0, 10, 0, 0)
            };

            var label = new Label
            {
                Text = counterName,
                HeightRequest = 80,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.FromRgb(255, 105, 180)
            };

            var countLabel = new Label
            {
                Text = "0",
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 50,
                TextColor = Color.FromRgb(255, 255, 255)
            };

            var incrementButton = new Button
            {
                Text = "+",
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(0, 0, 255),
                TextColor = Color.FromRgb(255, 255, 255),
                WidthRequest = 50
            };
            incrementButton.Clicked += (s, e) => UpdateCounter(countLabel, 1);

            var decrementButton = new Button
            {
                Text = "-",
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(0, 0, 255),
                TextColor = Color.FromRgb(255, 255, 255),
                WidthRequest = 50
            };
            decrementButton.Clicked += (s, e) => UpdateCounter(countLabel, -1);

            counterLayout.Children.Add(label);
            counterLayout.Children.Add(decrementButton);
            counterLayout.Children.Add(countLabel);
            counterLayout.Children.Add(incrementButton);

            return counterLayout;
        }

        // Aktualizuje wartość licznika zgodnie z przekazaną zmianą
        private void UpdateCounter(Label countLabel, int change)
        {
            int currentCount = int.Parse(countLabel.Text);
            if (change > 0 && currentCount == int.MaxValue)
            {
                DisplayAlert("Limit osiągnięty", "Maksymalna wartość licznika została osiągnięta.", "OK");
                return;
            }
            currentCount += change;
            if (currentCount < int.MinValue)
            {
                DisplayAlert("Limit osiągnięty", "Minimalna wartość licznika została osiągnięta.", "OK");
                return;
            }
            else if (currentCount > int.MaxValue)
            {
                DisplayAlert("Limit osiągnięty", "Maksymalna wartość licznika została osiągnięta.", "OK");
                return;
            }
            countLabel.Text = currentCount.ToString();
        }
    }
}
