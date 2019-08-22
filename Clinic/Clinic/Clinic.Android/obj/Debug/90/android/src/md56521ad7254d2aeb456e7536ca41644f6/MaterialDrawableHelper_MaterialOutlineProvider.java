package md56521ad7254d2aeb456e7536ca41644f6;


public class MaterialDrawableHelper_MaterialOutlineProvider
	extends android.view.ViewOutlineProvider
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getOutline:(Landroid/view/View;Landroid/graphics/Outline;)V:GetGetOutline_Landroid_view_View_Landroid_graphics_Outline_Handler\n" +
			"";
		mono.android.Runtime.register ("XF.Material.Droid.Renderers.MaterialDrawableHelper+MaterialOutlineProvider, XF.Material.Droid", MaterialDrawableHelper_MaterialOutlineProvider.class, __md_methods);
	}


	public MaterialDrawableHelper_MaterialOutlineProvider ()
	{
		super ();
		if (getClass () == MaterialDrawableHelper_MaterialOutlineProvider.class)
			mono.android.TypeManager.Activate ("XF.Material.Droid.Renderers.MaterialDrawableHelper+MaterialOutlineProvider, XF.Material.Droid", "", this, new java.lang.Object[] {  });
	}

	public MaterialDrawableHelper_MaterialOutlineProvider (int p0)
	{
		super ();
		if (getClass () == MaterialDrawableHelper_MaterialOutlineProvider.class)
			mono.android.TypeManager.Activate ("XF.Material.Droid.Renderers.MaterialDrawableHelper+MaterialOutlineProvider, XF.Material.Droid", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void getOutline (android.view.View p0, android.graphics.Outline p1)
	{
		n_getOutline (p0, p1);
	}

	private native void n_getOutline (android.view.View p0, android.graphics.Outline p1);

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
