using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Extensions.Logging;

namespace VectorGraphicViewer.ViewHelpers.Behaviours
{
    public class ZoomInZoomOutBehavior : DependencyObject
    {

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
          "IsEnabled", typeof(bool), typeof(ZoomInZoomOutBehavior), new PropertyMetadata(default(bool), ZoomInZoomOutBehavior.OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject attachingElement, bool value) => attachingElement.SetValue(ZoomInZoomOutBehavior.IsEnabledProperty, value);

        public static bool GetIsEnabled(DependencyObject attachingElement) => (bool)attachingElement.GetValue(ZoomInZoomOutBehavior.IsEnabledProperty);

        public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.RegisterAttached(
          "ZoomFactor", typeof(double), typeof(ZoomInZoomOutBehavior), new PropertyMetadata(0.1));

        public static void SetZoomFactor(DependencyObject attachingElement, double value) => attachingElement.SetValue(ZoomInZoomOutBehavior.ZoomFactorProperty, value);

        public static double GetZoomFactor(DependencyObject attachingElement) => (double)attachingElement.GetValue(ZoomInZoomOutBehavior.ZoomFactorProperty);


        public static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.RegisterAttached(
          "ScrollViewer", typeof(ScrollViewer), typeof(ZoomInZoomOutBehavior), new PropertyMetadata(default(ScrollViewer)));

        public static void SetScrollViewer(DependencyObject attachingElement, ScrollViewer value) => attachingElement.SetValue(ZoomInZoomOutBehavior.ScrollViewerProperty, value);

        public static ScrollViewer GetScrollViewer(DependencyObject attachingElement) => (ScrollViewer)attachingElement.GetValue(ZoomInZoomOutBehavior.ScrollViewerProperty);

        private static void OnIsEnabledChanged(DependencyObject attachingElement, DependencyPropertyChangedEventArgs e)
        {
            if (!(attachingElement is FrameworkElement frameworkElement))
            {
                throw new ArgumentException("Attaching element must be of type FrameworkElement");
            }

            bool isEnabled = (bool)e.NewValue;
            if (isEnabled)
            {
                frameworkElement.PreviewMouseWheel += ZoomInZoomOutBehavior.Zoom_OnMouseWheel;
                if (ZoomInZoomOutBehavior.TryGetScaleTransform(frameworkElement, out _))
                {
                    return;
                }

                if (frameworkElement.LayoutTransform is TransformGroup transformGroup)
                {
                    transformGroup.Children.Add(new ScaleTransform());
                }
                else
                {
                    frameworkElement.LayoutTransform = new ScaleTransform();
                }
            }
            else
            {
                frameworkElement.PreviewMouseWheel -= ZoomInZoomOutBehavior.Zoom_OnMouseWheel;
            }
        }

        private static void Zoom_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var zoomTargetElement = sender as FrameworkElement;

            Point mouseCanvasPosition = e.GetPosition(zoomTargetElement);
            double scaleFactor = e.Delta > 0
              ? ZoomInZoomOutBehavior.GetZoomFactor(zoomTargetElement)
              : -1 * ZoomInZoomOutBehavior.GetZoomFactor(zoomTargetElement);

            ZoomInZoomOutBehavior.ApplyZoomToAttachedElement(mouseCanvasPosition, scaleFactor, zoomTargetElement);

            ZoomInZoomOutBehavior.AdjustScrollViewer(mouseCanvasPosition, scaleFactor, zoomTargetElement);
        }

        private static void ApplyZoomToAttachedElement(Point mouseCanvasPosition, double scaleFactor, FrameworkElement zoomTargetElement)
        {
            if (!ZoomInZoomOutBehavior.TryGetScaleTransform(zoomTargetElement, out ScaleTransform scaleTransform))
            {
                throw new InvalidOperationException("No ScaleTransform found");
            }

            scaleTransform.CenterX = mouseCanvasPosition.X;
            scaleTransform.CenterY = mouseCanvasPosition.Y;

            scaleTransform.ScaleX = Math.Max(0.1, scaleTransform.ScaleX + scaleFactor);
            scaleTransform.ScaleY = Math.Max(0.1, scaleTransform.ScaleY + scaleFactor);
        }

        private static void AdjustScrollViewer(Point mouseCanvasPosition, double scaleFactor, FrameworkElement zoomTargetElement)
        {
            ScrollViewer scrollViewer = ZoomInZoomOutBehavior.GetScrollViewer(zoomTargetElement);
            if (scrollViewer == null)
            {
                return;
            }

            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + mouseCanvasPosition.X * scaleFactor);
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + mouseCanvasPosition.Y * scaleFactor);
        }

        private static bool TryGetScaleTransform(FrameworkElement frameworkElement, out ScaleTransform scaleTransform)
        {

            scaleTransform = frameworkElement.LayoutTransform switch
            {
                TransformGroup transformGroup => transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault(),
                ScaleTransform transform => transform,
                _ => null
            };

            return scaleTransform != null;
        }
    }
}
