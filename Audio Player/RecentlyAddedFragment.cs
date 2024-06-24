using Android.Views;
using Audio_Player;

namespace AndroidApp1
{
    class RecentlyAddedFragment : AndroidX.Fragment.App.Fragment
    {
        public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            return inflater?.Inflate(Resource.Layout.rec_added_fragment, container, false);
        }
    }
}
