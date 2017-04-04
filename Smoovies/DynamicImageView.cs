using Android.Content;
using Android.Widget;
using Android.Util;

namespace Smoovies
{
    /// <summary>
    /// DynamicImageView is a helper extension that overrides OnMeasure in order to scale the said image
    /// to fit the entire width/or height of the parent container. 
    /// </summary>
    public class DynamicImageView : ImageView
    {
        public DynamicImageView(Context context, IAttributeSet attributeSet) : base(context, attributeSet)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int width = MeasureSpec.GetSize(widthMeasureSpec);
            int height = width * Drawable.IntrinsicHeight / Drawable.IntrinsicWidth;
            SetMeasuredDimension(width, height);
        }

    }
}