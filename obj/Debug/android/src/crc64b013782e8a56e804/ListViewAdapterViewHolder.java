package crc64b013782e8a56e804;


public class ListViewAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("FantasyFootball.Adapters.ListViewAdapterViewHolder, FantasyFootball", ListViewAdapterViewHolder.class, __md_methods);
	}


	public ListViewAdapterViewHolder ()
	{
		super ();
		if (getClass () == ListViewAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("FantasyFootball.Adapters.ListViewAdapterViewHolder, FantasyFootball", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
