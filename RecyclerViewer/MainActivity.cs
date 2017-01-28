using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Util;
using Context = Android.Content.Context;

namespace RecyclerViewer
{
	[Activity (Label = "RecyclerViewer", MainLauncher = true, Icon = "@drawable/icon", 
               Theme = "@android:style/Theme.Material.Light.DarkActionBar")]
	public class MainActivity : Activity
	{
	    public static MainActivity ContextActivity;

        // RecyclerView instance that displays the photo album:
        RecyclerView mRecyclerView;

        // Layout manager that lays out each card in the RecyclerView:
		RecyclerView.LayoutManager mLayoutManager;

        // Adapter that accesses the data set (a photo album):
		AlbumAdapter mAdapter;

        // Album album that is managed by the adapter:
        Library MLibrary;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Instantiate the photo album:
            MLibrary = new Library();

			// Set our view from the "main" layout resource:
			SetContentView (Resource.Layout.Main);

            // Get our RecyclerView layout:
			mRecyclerView = FindViewById<RecyclerView> (Resource.Id.recyclerView);

            //............................................................
            // Layout Manager Setup:

            // Use the built-in linear layout manager:
			mLayoutManager = new LinearLayoutManager (this);
            ContextActivity = this;

            // Or use the built-in grid layout manager (two horizontal rows):
            // mLayoutManager = new GridLayoutManager
            //        (this, 2, GridLayoutManager.Horizontal, false);

            // Plug the layout manager into the RecyclerView:
            mRecyclerView.SetLayoutManager (mLayoutManager);

            //............................................................
            // Adapter Setup:

            // Create an adapter for the RecyclerView, and pass it the
            // data set (the photo album) to manage:
			mAdapter = new AlbumAdapter (MLibrary);

            // Register the item click handler (below) with the adapter:
            mAdapter.ItemClick += OnItemClick;

            // Plug the adapter into the RecyclerView:
			mRecyclerView.SetAdapter (mAdapter);
            
		}

        // Handler for the item click event:
        void OnItemClick (object sender, int position)
        {
            // Display a toast that briefly shows the enumeration of the selected photo:
            int albumNum = position + 1;
            Toast.MakeText(this, "This is album number " + albumNum, ToastLength.Short).Show();
        }
	}

    // Adapter to connect the data set (photo album) to the RecyclerView: 
    public class PhotoAdapter : RecyclerView.Adapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        // Underlying data set (a photo album):
        public List<int> _photos;

        // Load the adapter with the data set (photo album) at construction time:
        public PhotoAdapter(List<int> photos)
        {
            _photos = photos;
        }

        // Create a new photo CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ImageCellView, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick);
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder vh = holder as PhotoViewHolder;

            if (vh != null)
            {
                vh.Image.SetImageResource(_photos[position]);
            }
        }

        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return _photos.Count; }
        }

        // Raise an event when the item-click takes place:
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }

    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Text { get; private set; }

        // Get references to the views defined in the CardView layout.
        public PhotoViewHolder(View itemView, Action<int> listener)
            : base(itemView)
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageCellView);
            Text = itemView.FindViewById<TextView>(Resource.Id.imageCellViewText);

            // Detect user clicks on the item view and report which item
            // was clicked (by position) to the listener:
            itemView.Click += (sender, e) => listener(base.Position);
        }
    }

    //----------------------------------------------------------------------
    // VIEW HOLDER

    // Implement the ViewHolder pattern: each ViewHolder holds references
    // to the UI components (ImageView and TextView) within the CardView 
    // that is displayed in a row of the RecyclerView:
    public class AlbumViewHolder : RecyclerView.ViewHolder
    {
        // public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }
        public RecyclerView SubRecyclerView { get; private set; }

        // Get references to the views defined in the CardView layout.
        public AlbumViewHolder (View itemView, Action<int> listener) 
            : base (itemView)
        {
            // Locate and cache view references:
            SubRecyclerView = itemView.FindViewById<RecyclerView> (Resource.Id.subRecyclerView);

            var mLayoutManager = new LinearLayoutManager(itemView.Context)
            {
                Orientation = (int)Orientation.Horizontal
            };

            SubRecyclerView.SetLayoutManager(mLayoutManager);
            

            Caption = itemView.FindViewById<TextView> (Resource.Id.textView);
            
            // Or use the built-in grid layout manager (two horizontal rows):
            // mLayoutManager = new GridLayoutManager
            //        (this, 2, GridLayoutManager.Horizontal, false);

            // Plug the layout manager into the RecyclerView:
            // SubRecyclerView.SetLayoutManager(itemView.LayoutParameters);

            // Detect user clicks on the item view and report which item
            // was clicked (by position) to the listener:
            itemView.Click += (sender, e) => listener (base.Position);
        }
    }

    //----------------------------------------------------------------------
    // ADAPTER

    // Adapter to connect the data set (photo album) to the RecyclerView: 
    public class AlbumAdapter : RecyclerView.Adapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        // Underlying data set (a photo album):
        public Library MLibrary;
        
        // Load the adapter with the data set (photo album) at construction time:
        public AlbumAdapter (Library library)
        {
            MLibrary = library;
        }

        // Create a new photo CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From (parent.Context).
                Inflate (Resource.Layout.PhotoCardView, parent, false);

            AlbumViewHolder vh = new AlbumViewHolder (itemView, OnClick); 
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override void  OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
        {
            AlbumViewHolder vh = holder as AlbumViewHolder;
            var mAdapter = new PhotoAdapter(MLibrary.GetItems(position));
            vh.SubRecyclerView.SetAdapter(mAdapter);
            vh.Caption.Text = MLibrary[position].Caption;
        }

        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return MLibrary.NumAlbums; }
        }

        // Raise an event when the item-click takes place:
        void OnClick (int position)
        {
            if (ItemClick != null)
                ItemClick (this, position);
        }
    }
}
