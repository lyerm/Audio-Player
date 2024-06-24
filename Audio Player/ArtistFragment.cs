using Android.Views;
using Audio_Player;

namespace AndroidApp1
{
    class ArtistFragment : AndroidX.Fragment.App.Fragment
    {
        public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            return inflater?.Inflate(Resource.Layout.artist_fragment, container, false);
        }
    }
}
