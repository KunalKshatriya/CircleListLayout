using System;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
namespace CircleListLayoutDemo
{
    public class CircleListLayout : Layout<Frame>
    {
        int columns = 4;
        int spacing = 10;



        private static readonly BindableProperty IsOpenedProperty = BindableProperty.CreateAttached("IsOpened",
                                                                                            typeof(bool),
                                                                                            typeof(CircleListLayout),
                                                                                            false);
		public static void SetIsOpened(BindableObject bindable, bool isOpeded)
		{
			bindable.SetValue(IsOpenedProperty, isOpeded);
		}

		public static bool GetIsOpened(BindableObject bindable)
		{
			return (bool)bindable.GetValue(IsOpenedProperty);
		}


        bool isAnimating = false;
        public void Animate()
        {
            isAnimating = true;
            //LayoutChildren(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height);
            InvalidateLayout();
            isAnimating = false;
        }

        protected override void OnAdded(Frame view)
		{
			base.OnAdded(view);
			view.PropertyChanged += OnComponentPropertyChanged;
		}

        protected override void OnRemoved(Frame view)
		{
			base.OnRemoved(view);
			view.PropertyChanged -= OnComponentPropertyChanged;
		}

        public CircleListLayout()
        {
            
        }


        public void RandomColumns()
        {
            columns = new Random().Next(3, 10);
            InvalidateLayout();
        }

        int indexOpenedFrame = -1;

        public void Tapped(Frame frame)
        {
            Frame oldFrame = null;
            if(indexOpenedFrame > -1 ) // close the opened window
            {
                CircleListLayout.SetIsOpened(this.Children.Last(), false);
                oldFrame = this.Children.Last();
				Children.Remove(oldFrame);
                Children.Insert(indexOpenedFrame, oldFrame);
                indexOpenedFrame = -1;
            }

            if (oldFrame != frame)
            {
                CircleListLayout.SetIsOpened(frame, true);
                indexOpenedFrame = Children.IndexOf(frame);
                Children.Remove(frame);
                Children.Add(frame);
            }

        }

        void LayoutChild(Frame frame, Rectangle oldFrame, Rectangle newFrame)
        {


            var rnd = new Random();

			//change the corner radius
			frame.CornerRadius = (float)(newFrame.Width / 2);
            frame.IsClippedToBounds = true;

            if (oldFrame.IsEmpty || isAnimating)
            {
                frame.Layout(new Rectangle(newFrame.X + newFrame.Width / 2, newFrame.Y + newFrame.Height / 2, 0, 0));

                if (isAnimating)
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Delay(rnd.Next(100, 400));
                        await Task.WhenAny(
                        frame.LayoutTo(newFrame, 500, Easing.SpringOut)
                                );
                    });
                else
                    frame.LayoutTo(newFrame, 500, Easing.SpringOut);
            }
            else if (oldFrame.Center.ToString() != newFrame.Center.ToString())
                frame.LayoutTo(newFrame, 500, Easing.CubicInOut);
			//else
                //frame.Layout(newFrame);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
			float side = (float)((width - ((columns + 1) * spacing)) / columns);

            int currentRow = 0;
            int currentChild = 0;
                
            x += spacing;
            y += spacing;

            Frame shownFrame = null;

            foreach (Frame frame in Children)
            {
                x = spacing;


                if(GetIsOpened(frame))
                {
                    shownFrame = frame;
                }

                if (currentRow % 2 == 0)
                {
                    double newX = x + (currentChild * spacing) + (currentChild * side);
                    LayoutChild(frame, frame.Bounds, new Rectangle(newX, y, side, side));
                }
                else
                {
                    x += side / 2 + spacing / 2;
                    double newX = x + (currentChild * spacing) + (currentChild * side);
                    LayoutChild(frame, frame.Bounds, new Rectangle(newX, y, side, side));
                }

                currentChild++;


				//if current row and update the y 
				if((currentChild == columns && currentRow % 2 == 0) || (currentChild == columns - 1 && currentRow % 2 != 0))
                {
                    currentRow++;
                    y += side;
                    currentChild = 0;
                }
            }

            if (shownFrame != null)
            {
                var newwidth = 200;
                var newHeight = 200;

                LayoutChild(shownFrame, shownFrame.Bounds, new Rectangle(this.Bounds.Width/2 - newwidth / 2, shownFrame.Bounds.Center.Y - newHeight/2, newHeight, newwidth));
            }

        } 

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            double width = widthConstraint;
            float side = (float)((width - ((columns + 1) * spacing)) / columns);

            double height = Math.Ceiling((double)Children.Count / (columns + columns - 1)) * (2 * side) + (spacing * 3);
            
            return new SizeRequest(new Size(width, height), new Size(width, height));
        }

		void OnComponentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
		{
			//Check if it is the section
            if (args.PropertyName == IsOpenedProperty.PropertyName )
				InvalidateLayout();
		}
    }
}
