using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CircleListLayoutDemo
{
    public partial class LayoutItem : Frame
    {
        Random rnd = new Random();
        public LayoutItem()
        {
			Random rnd = new Random();
			Color randomColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
			BackgroundColor = randomColor;


            InitializeComponent();
            Padding = 0;
            Margin = 0;

            //Contact.Source = new UriImageSource()
            //{
            //    Uri = new Uri($"http://loremflickr.com/400/400/dogx?random={rnd.Next(256)}")
            //};
        }
    }
}
