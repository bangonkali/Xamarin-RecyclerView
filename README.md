# 2 Dimensional RecyclerViewer

This sample is inspired by the original work by Mark McLemore on RecyclerViewer.
On this version, we get a 2 dimensional recyclerview whereby the Vertical Axis
is composed of multiple Horizontal recyclerviews which the user can swipe
horizontally from left to right. The user can still swipe from top to bottom
by moving through multiple rows of reyclerviews.

Each row of recyclerviews is independent from the other rows.

## Modified by
Gil Michael 'Bangon Kali' Regalado

# Original RecyclerViewer

This sample app accompanies the articles
[CardView](http://developer.xamarin.com/guides/android/user_interface/cardview) and
[RecyclerView](http://developer.xamarin.com/guides/android/user_interface/recyclerview).
You can use this sample to learn how to use the new `CardView` and `RecyclerView` widgets
introduced with Android 5.0 Lollipop.

This app is a simple "Photo Album Viewer" that lets the user scroll up
and down to view a collection of photos. Each photo, which consists of
an image with caption text, is displayed as a row item in the
`RecyclerView`. The <span class="uiitem">Random Pick</span> button
randomly swaps a photo in the collection with the first photo to
demonstrate how `RecyclerView` is updated when the data set changes.
When the user taps a photo, a toast appears to display the number of
the photo within the collection &ndash; this demonstrates how item view
click handlers work.

Each image/caption (photo) row item is displayed in a `CardView`
layout, and the app uses a single `RecyclerView` layout to hold and
display over thirty unique row items. An *adapter* is used to connect
the `RecyclerView` with a simple "Photo Album" database, a
*view-holder* is used to cache view references, and a linear *layout
manager* positions the `CardView` row items within the `RecyclerView`.


## Author
Mark McLemore
