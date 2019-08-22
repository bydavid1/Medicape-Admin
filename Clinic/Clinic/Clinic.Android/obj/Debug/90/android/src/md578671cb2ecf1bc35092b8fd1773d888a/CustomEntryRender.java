package md578671cb2ecf1bc35092b8fd1773d888a;


public class CustomEntryRender
	extends md51558244f76c53b6aeda52c8a337f2c37.EntryRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Clinic.Droid.CustomEntryRender, Clinic.Android", CustomEntryRender.class, __md_methods);
	}


	public CustomEntryRender (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomEntryRender.class)
			mono.android.TypeManager.Activate ("Clinic.Droid.CustomEntryRender, Clinic.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CustomEntryRender (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomEntryRender.class)
			mono.android.TypeManager.Activate ("Clinic.Droid.CustomEntryRender, Clinic.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CustomEntryRender (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomEntryRender.class)
			mono.android.TypeManager.Activate ("Clinic.Droid.CustomEntryRender, Clinic.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
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
