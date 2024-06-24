using Android.Animation;
using Android.Views;

namespace Audio_Player
{
    class SearchViewRevealAnimation
    {
        private readonly SearchView _searchView;
        public int Duration { set; get; } = 200;
        private readonly int _widthCollapsed, _heightCollapsed;
        private readonly int _widthExpanded, _heightExpanded;

        public SearchViewRevealAnimation(SearchView? searchView)
        {
            if(searchView == null)
            {
                throw new NullReferenceException("SearchViewReveal: parameter is null");
            }
            _searchView = searchView;
            
            if (_searchView.Iconified)
            {
                _searchView.Measure(View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified),
                    View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));

                _widthCollapsed = _searchView.MeasuredWidth;
                _heightCollapsed = _searchView.MeasuredHeight;
                _searchView.OnActionViewExpanded();
                _searchView.ClearFocus();

                _searchView.Measure(View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified),
                    View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));

                _widthExpanded = _searchView.MeasuredWidth;
                _heightExpanded = _searchView.MeasuredHeight;

                _searchView.OnActionViewCollapsed();
            } else
            {
                _searchView.Measure(View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified),
                    View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));

                _widthExpanded = _searchView.MeasuredWidth;
                _heightExpanded = _searchView.MeasuredHeight;
                _searchView.OnActionViewCollapsed();

                _searchView.Measure(View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified),
                    View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));

                _widthCollapsed = _searchView.MeasuredWidth;
                _heightCollapsed = _searchView.MeasuredHeight;
                
                _searchView.OnActionViewExpanded();
            }
        }

        public void BindListeners()
        {
            _searchView.SearchClick += SearchViewExpand;
            _searchView.Close += SearchViewCollapse;
        }
        public void UnbindListeners()
        {
            _searchView.SearchClick -= SearchViewExpand;
            _searchView.Close -= SearchViewCollapse;
        }

        private void SearchViewCollapse(object? sender, SearchView.CloseEventArgs e)
        {
            _searchView.ClearFocus();

            Animator? animator = ViewAnimationUtils.CreateCircularReveal(_searchView, _widthExpanded - _widthCollapsed / 2, _heightCollapsed / 2, _widthExpanded, 0) ?? throw new Exception("Animation created error");
            animator.AnimationEnd += (s, e) => {
                _searchView.OnActionViewCollapsed();
            };

            animator.SetDuration(Duration);
            animator.Start();
        }

        private void SearchViewExpand(object? sender, EventArgs e)
        {
            Animator? animator = ViewAnimationUtils.CreateCircularReveal(_searchView, _widthExpanded - _widthCollapsed / 2, _heightExpanded / 2, 0, _widthExpanded) ?? throw new Exception("Animation created error");
            animator.SetDuration(Duration);
            animator.Start();
        }
    }
}


//16909359 - search_plate
//16909355 - search_close_btn
//16909354 - search_button
//16909360 - search_src_text