using System;
using System.Collections.Specialized;
using System.Linq;

using Android.Views;
using Android.Widget;

namespace SimplyMobile.Data
{
    /// <summary>
    /// Observable data source Android portion.
    /// </summary>
    public partial class ObservableDataSource<T> : Java.Lang.Object, IListAdapter, ISpinnerAdapter
    {
        /// <summary>
        /// The are all items enabled.
        /// </summary>
        /// <returns>
        /// <see cref="bool"/>.
        /// </returns>
        public bool AreAllItemsEnabled()
        {
            return true;
        }

        /// <summary>
        /// Return whether item is enabled.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsEnabled(int position)
        {
            return true;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get { return this.Data.Count; }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The <see cref="Object"/>.
        /// </returns>
        public Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public long GetItemId(int position)
        {
            return position;
        }

        public int GetItemViewType(int position)
        {
            return position;
        }

        public virtual View GetView(int position, View convertView, ViewGroup parent)
        {
			var item = this.Data [position];
			var cellProvider = parent as ITableCellProvider;
			if (cellProvider != null)
			{
				return cellProvider.GetView (item, convertView);
			}

            var v = convertView as TextView ?? new TextView(parent.Context);
            v.Text = item.ToString();
            return v;
        }

        public virtual bool HasStableIds
        {
            get { return true; }
        }

        public virtual bool IsEmpty
        {
            get { return !this.Data.Any(); }
        }

        public virtual void RegisterDataSetObserver(Android.Database.DataSetObserver observer)
        {
            //throw new NotImplementedException();
        }

        public virtual void UnregisterDataSetObserver(Android.Database.DataSetObserver observer)
        {
            //throw new NotImplementedException();
        }

        public virtual int ViewTypeCount
        {
            get { return 1; }
        }

        public virtual View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            return this.GetView(position, convertView, parent);
        }

        /// <summary>
        /// Handles the item selected.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void HandleItemClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            this.InvokeItemSelectedEvent(this.Data[e.Position]);
        }

        private void HandleItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.InvokeItemSelectedEvent(this.Data[e.Position]);
        }

        /// <summary>
        /// The collection changed partial method Android implementation.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// The notify collection changed event args.
        /// </param>
        partial void CollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            foreach (var listView in this.observers.OfType<ListView>())
            {
				listView.InvalidateViews();
            }
        }

        /// <summary>
        /// Observers changed partial method Android implementation.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// Changed observer information.
        /// </param>
        partial void ObserversChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
			if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (var newListView in notifyCollectionChangedEventArgs.NewItems.OfType<ListView>())
				{
					newListView.Adapter = this;
                    newListView.ItemClick -= HandleItemClicked;
                    newListView.ItemClick += HandleItemClicked;
				}

				foreach (var newSpinner in notifyCollectionChangedEventArgs.NewItems.OfType<Spinner>())
				{
					newSpinner.Adapter = this;
				}
//				foreach (var listView in this.observers.OfType<ListView>())
//				{
//					listView.Adapter = this;
//				}
			}
			else if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (var removedListView in notifyCollectionChangedEventArgs.OldItems.OfType<ListView>())
				{
					removedListView.Adapter = null;
				}
				foreach (var removedSpinner in notifyCollectionChangedEventArgs.OldItems.OfType<Spinner>())
				{
					removedSpinner.Adapter = null;
				}
			}
        }
    }
}