using Android.Views;

namespace Audio_Player
{
    class SwipeManager
    {
        public enum SwipeDirection
        {
            Left,
            Right,
            Up,
            Down
        }

        public class SwipeEventArgs(SwipeDirection swipeDirection)
        {
            public SwipeDirection Direction { get; } = swipeDirection;
        }

        public class SwipeListener : GestureDetector.SimpleOnGestureListener
        {
            public event EventHandler<SwipeEventArgs>? Swipe;
            public float HorizontalSensivity { get; set; } = 50;
            public float VerticalSensivity { get; set; } = 200;
            public override bool OnFling(MotionEvent? e1, MotionEvent e2, float velocityX, float velocityY)
            {
                if(e1 == null)
                {
                    return true;
                }

                if ((e1.GetY() - e2.GetY()) > VerticalSensivity)
                {
                    Swipe?.Invoke(this, new SwipeEventArgs(SwipeDirection.Down));
                }
                else if ((e2.GetY() - e1.GetY()) > VerticalSensivity)
                {
                    Swipe?.Invoke(this, new SwipeEventArgs(SwipeDirection.Up));
                } 
                else if ((e1.GetX() - e2.GetX()) > HorizontalSensivity)
                {
                    Swipe?.Invoke(this, new SwipeEventArgs(SwipeDirection.Left));
                }
                else if ((e2.GetX() - e1.GetX()) > HorizontalSensivity)
                {
                    Swipe?.Invoke(this, new SwipeEventArgs(SwipeDirection.Right));
                }

                return true;
            }
        }
    }
}
