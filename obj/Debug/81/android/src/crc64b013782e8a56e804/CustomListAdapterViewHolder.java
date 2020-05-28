package crc64b013782e8a56e804;


public class CustomListAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("FantasyFootball.Adapters.CustomListAdapterViewHolder, FantasyFootball", CustomListAdapterViewHolder.class, __md_methods);
	}


	public CustomListAdapterViewHolder ()
	{
		super ();
		if (getClass () == CustomListAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("FantasyFootball.Adapters.CustomListAdapterViewHolder, FantasyFootball", "", this, new java.lang.Object[] {  });
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
