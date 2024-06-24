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
        private AndroidX.AppCompat.Widget.Toolbar? _toolbar;
        private BottomNavigationView? _navigationView;
        private BottomNavigationViewItemAnimation? _bnvItemAnimation;
        private SearchViewRevealAnimation? _svRevealAnimation;
        private GestureDetector? _gestureDetector;
        SwipeManager.SwipeListener? _swipeListener;
        private int _currentTabId;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _navigationView = FindViewById<BottomNavigationView>(Resource.Id.navigation_view);
            _toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.main_toolbar);

            if (_navigationView == null)
            {
                throw new Exception("NavigationView search error");
            }
            if (_toolbar == null)
            {
                throw new Exception("Toolbar search error");
            }

            _swipeListener = new SwipeManager.SwipeListener();
            _gestureDetector = new GestureDetector(this, _swipeListener);
            _bnvItemAnimation = new BottomNavigationViewItemAnimation(_navigationView);
            _currentTabId = Resource.Id.navigation_favourite;

            SetSupportActionBar(_toolbar);            
        }

        protected override void OnResume()
        {   
            _bnvItemAnimation?.BindListeners();
            _svRevealAnimation?.BindListeners();
            if (_navigationView != null)
            {
                _navigationView.ItemSelected += OnNavigationSelect;
                _navigationView.SelectedItemId = _currentTabId;
            }
            if (_swipeListener != null)
            {
                _swipeListener.Swipe += OnSwipe;
            }
            base.OnResume();
        }

        protected override void OnPause()
        {
            _bnvItemAnimation?.UnbindListeners();
            _svRevealAnimation?.UnbindListeners();
            if (_navigationView != null)
            {
                _navigationView.ItemSelected -= OnNavigationSelect;
            }
            if (_swipeListener != null)
            {
                _swipeListener.Swipe -= OnSwipe;
            }
            base.OnPause();
        }

        private void OnSwipe(object? sender, SwipeManager.SwipeEventArgs e)
        {
            int currentTabIndex = -1;
            for (int i = 0; i < _navigationView?.Menu.Size(); i++)
            {
                int? itemId = _navigationView?.Menu?.GetItem(i)?.ItemId;
                if (itemId != null && itemId == _currentTabId)
                {
                    currentTabIndex = i;
                    break;
                }
            }

            if(currentTabIndex == -1)
            {
                throw new Exception("Navigation error");
            }

            switch (e.Direction)
            { 
                case SwipeManager.SwipeDirection.Right:
                    if(currentTabIndex > 0)
                    {
                        currentTabIndex--;
                    }
                    break;
                case SwipeManager.SwipeDirection.Left:
                    if (currentTabIndex < _navigationView?.Menu.Size() - 1)
                    {
                        currentTabIndex++;
                    }
                    break;
            }

            if (_navigationView != null)
            {
                _navigationView.SelectedItemId = _navigationView?.Menu?.GetItem(currentTabIndex)?.ItemId ?? _currentTabId;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu? menu)
        {
            if (menu == null)
            {
                throw new Exception("Create options menu error");
            }
            MenuInflater.Inflate(Resource.Menu.main_toolbar_menu, menu);

            SearchView? searchView = menu.FindItem(Resource.Id.action_search)?.ActionView as SearchView ?? throw new Exception("SearchView search error");
            View? searchButton = searchView.FindViewById(16909354);
            if (searchButton != null)
            {
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                {
                    #pragma warning disable CA1416 // Проверка совместимости платформы
                    searchButton.TooltipText = string.Empty;
                    #pragma warning restore CA1416 // Проверка совместимости платформы
                }
            }

            View? searchPlate = searchView.FindViewById(16909359);
            searchPlate?.SetBackgroundResource(Resource.Drawable.searchview_background);

            AutoCompleteTextView? searchSrcText = searchView.FindViewById<AutoCompleteTextView>(16909360);
            searchSrcText?.SetHint(Resource.String.searchview_query_hint);

            _svRevealAnimation = new SearchViewRevealAnimation(searchView);
            _svRevealAnimation.BindListeners();
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_menu:
                    Toast.MakeText(this, "В разработке...", ToastLength.Short)?.Show();
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

            if(e.Item.ItemId == _currentTabId && SupportFragmentManager.Fragments.Count > 0)
            {
                return;
            } else
            {
                _currentTabId = e.Item.ItemId;
            }

            if(_toolbar != null)
            {
                _toolbar.Title = title;
            }
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment_container, fragment).AddToBackStack(null).Commit();
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (_gestureDetector != null && e != null)
            {
                return _gestureDetector.OnTouchEvent(e);
            }
            else
            {
                return base.OnTouchEvent(e);
            }
        }   
    }
}