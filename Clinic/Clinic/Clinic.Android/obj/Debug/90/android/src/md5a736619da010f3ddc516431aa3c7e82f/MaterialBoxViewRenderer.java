package md5a736619da010f3ddc516431aa3c7e82f;


public class MaterialBoxViewRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.BoxRenderer
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnTouchListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTouch:(Landroid/view/View;Landroid/view/MotionEvent;)Z:GetOnTouch_Landroid_view_View_Landroid_view_MotionEvent_Handler:Android.Views.View/IOnTouchListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("XF.Material.Droid.Renderers.Internals.MaterialBoxViewRenderer, XF.Material.Droid", MaterialBoxViewRenderer.class, __md_methods);
	}


	public MaterialBoxViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == MaterialBoxViewRenderer.class)
			mono.android.TypeManager.Activate ("XF.Material.Droid.Renderers.Internals.MaterialBoxViewRenderer, XF.Material.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public MaterialBoxViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MaterialBoxViewRenderer.class)
			mono.android.TypeManager.Activate ("XF.Material.Droid.Renderers.Internals.MaterialBoxViewRenderer, XF.Material.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public MaterialBoxViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == MaterialBoxViewRenderer.class)
			mono.android.TypeManager.Activate ("XF.Material.Droid.Renderers.Internals.MaterialBoxViewRenderer, XF.Material.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public boolean onTouch (android.view.View p0, android.view.MotionEvent p1)
	{
		return n_onTouch (p0, p1);
	}

	private native boolean n_onTouch (android.view.View p0, android.view.MotionEvent p1);

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
