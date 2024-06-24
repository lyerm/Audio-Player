using Android.Views;
using Android.Views.Animations;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Navigation;

namespace Audio_Player
{
    class BottomNavigationViewItemAnimation(BottomNavigationView navigationView)
    {
        private IMenuItem? _prevItem = null;
        private readonly BottomNavigationView _navigationView = navigationView;
        public int Duration { get; set; } = 75;
        public float Scale { get; set; } = 1.25f;

        public void BindListeners()
        {
            _navigationView.ItemSelected += OnItemSelect;
        }
        public void UnbindListeners()
        {
            _navigationView.ItemSelected -= OnItemSelect;
        }

        private void OnItemSelect(object? sender, NavigationBarView.ItemSelectedEventArgs e)
        {
            if (sender is BottomNavigationView navView)
            {
                if(_prevItem != null)
                {
                    View? prevItemView = navView.FindViewById(_prevItem.ItemId);
                    prevItemView?.Animate()?.ScaleX(1.0f).ScaleY(1.0f).SetDuration(Duration).SetInterpolator(new LinearInterpolator());
                }

                View? itemView = navView.FindViewById(e.Item.ItemId);
                itemView?.Animate()?.ScaleX(Scale).ScaleY(Scale).SetDuration(Duration).SetInterpolator(new LinearInterpolator());
            }
            _prevItem = e.Item;
        }
    }
}
