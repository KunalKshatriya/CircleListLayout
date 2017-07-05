using System;
using Xamarin.Forms;
using System.Linq;

namespace CircleListLayoutDemo
{
    public partial class CircleListLayoutDemoPage : ContentPage
    {
        public CircleListLayoutDemoPage()
        {
            InitializeComponent();


            Frame addButton = new Frame()
            {
                Margin = 0,
                Padding = 0,
                BackgroundColor = Color.Green,
                WidthRequest = 40,
                HeightRequest = 40,
                CornerRadius = 20,
                Content = new Label()
                {
                    Text = "+",
                    TextColor = Color.White,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    InputTransparent = true
                },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            ButtonStack.Children.Add(addButton);
            addButton.GestureRecognizers.Add(new TapGestureRecognizer((obj) =>
            {  var item = new LayoutItem();
				  item.GestureRecognizers.Add(new TapGestureRecognizer((obj1) =>
				  {
					  CircleLayout.Tapped(item);
				  }));

				  CircleLayout.Children.Add(item);
			}));


			Frame removeButton = new Frame()
			{
				Margin = 0,
				Padding = 0,
				BackgroundColor = Color.Red,
				WidthRequest = 40,
				HeightRequest = 40,
				CornerRadius = 20,
				Content = new Label()
				{
					Text = "-",
					TextColor = Color.White,
					FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalTextAlignment = TextAlignment.Center,
					HorizontalTextAlignment = TextAlignment.Center
				},
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

            ButtonStack.Children.Add(removeButton);
			removeButton.GestureRecognizers.Add(new TapGestureRecognizer((obj) =>
			{
				var rnd = new Random();
 				CircleLayout.Children.Remove(CircleLayout.Children.ElementAt(rnd.Next(CircleLayout.Children.Count() - 1)));
			}));

			Frame AnimateButton = new Frame()
			{
				Margin = 0,
				Padding = 0,
                BackgroundColor = Color.Blue,
				WidthRequest = 40,
				HeightRequest = 40,
				CornerRadius = 20,
				Content = new Label()
				{
					Text = "A",
					TextColor = Color.White,
					FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalTextAlignment = TextAlignment.Center,
					HorizontalTextAlignment = TextAlignment.Center
				},
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

            ButtonStack.Children.Add(AnimateButton);
			AnimateButton.GestureRecognizers.Add(new TapGestureRecognizer((obj) =>
			{
				CircleLayout.Animate();
			}));


			Frame changeColumnsButton = new Frame()
			{
				Margin = 0,
				Padding = 0,
				BackgroundColor = Color.DarkOrange,
				WidthRequest = 40,
				HeightRequest = 40,
				CornerRadius = 20,
				Content = new Label()
				{
					Text = "C",
					TextColor = Color.White,
					FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalTextAlignment = TextAlignment.Center,
					HorizontalTextAlignment = TextAlignment.Center
				},
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			ButtonStack.Children.Add(changeColumnsButton);
			changeColumnsButton.GestureRecognizers.Add(new TapGestureRecognizer((obj) =>
			{
                CircleLayout.RandomColumns();
			}));

		}

      
		void OnAnimate(object sender, EventArgs e)
		{
           
		}
    }
}
