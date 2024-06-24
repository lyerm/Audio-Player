using Google.Android.Material.BottomNavigation;
using AndroidX.AppCompat.App;
using Google.Android.Material.Navigation;
using Android.Views;
using Audio_Player;

namespace AndroidApp1
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        AndroidX.AppCompat.Widget.Toolbar? toolbar;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BottomNavigationView? navigationView = FindViewById<BottomNavigationView>(Resource.Id.navigation_view);
            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.main_toolbar);

            if (navigationView == null)
            {
                throw new Exception("NavigationView search rrror");
            }
            if(toolbar == null)
            {
                throw new Exception("Toolbar search rrror");
            }

            navigationView.ItemSelected += OnNavigationSelect;
            navigationView.SelectedItemId = Resource.Id.navigation_favourite;
            SetSupportActionBar(toolbar);
        }

        public override bool OnCreateOptionsMenu(IMenu? menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_toolbar_menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_menu:
                    return true;
                case Resource.Id.action_search:
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
            
        }
        private void OnNavigationSelect(object? sender, NavigationBarView.ItemSelectedEventArgs e)
        {
            AndroidX.Fragment.App.Fragment fragment;
            string title;

            switch (e.Item.ItemId)
            {
                case Resource.Id.navigation_favourite:
                    fragment = new FavouriteFragment();
                    title = GetString(Resource.String.favourite_title);
                    break;
                case Resource.Id.navigation_album:
                    fragment = new AlbumFragment();
                    title = GetString(Resource.String.album_title);
                    break;
                case Resource.Id.navigation_artist:
                    fragment = new ArtistFragment();
                    title = GetString(Resource.String.artist_title);
                    break;
                case Resource.Id.navigation_library:
                    fragment = new LibraryFragment();
                    title = GetString(Resource.String.library_title);
                    break;
                case Resource.Id.navigation_rec_added:
                    fragment = new RecentlyAddedFragment();
                    title = GetString(Resource.String.rec_added_title);
                    break;
                default:
                    throw new Exception("Error creating a fragment");
            }

            if(toolbar != null)
            {
                toolbar.Title = title;
            }
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment_container, fragment).AddToBackStack(null).Commit();
        }
    }
}