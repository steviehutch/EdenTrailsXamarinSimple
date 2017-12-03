using System;
using CoreGraphics;

using Foundation;
using UIKit;
using CoreMedia;

using Wikitude.Architect;

namespace WikitudeSDKExample
{
	public partial class ExampleArchitectViewDelegate : WTArchitectViewDelegate 
	{
		public override void InvokedURL(WTArchitectView architectView, NSUrl url)
		{
			Console.WriteLine ("architect view invoked url: " + url);
		}

		public override void DidFinishLoadNavigation(WTArchitectView architectView, WTNavigation navigation)
		{
			Console.WriteLine ("architect view loaded navigation: " + navigation.OriginalURL);
		}

		public override void DidFailToLoadNavigation(WTArchitectView architectView, WTNavigation navigation, NSError error)
		{
			Console.WriteLine("architect view failed to load navigation. " + error.LocalizedDescription);
		}
	}

	public partial class WikitudeSDKExampleViewController : UIViewController
	{

		private WTArchitectView architectView;
		private WTAuthorizationRequestManager authorizationRequestManager = new WTAuthorizationRequestManager ();
		private ExampleArchitectViewDelegate architectViewDelegate = new ExampleArchitectViewDelegate ();

		private WTNavigation loadingArchitectWorldNavigation = null;

		private bool authorized = false;


		public WikitudeSDKExampleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{

            string WikiKey = "e9T8QNdR9to8mZXsh+NKsVpVyAIEfz58yOlPVEnQlRCCih4BQ0BetTTTmltJmgRWKfTdV2PiizecU4WjlpCLMApw23i17QELh4yElciFJZboITsYR6+OFIJjQSj6UOKDcLNM8MMfjaFeKplove23iz4EJxsGspNWKQRPHH+rCppTYWx0ZWRfX/ONUYPBt83/xwHZD6gZ+euFDQaOXUxYrWXCkuvtwIloIDBOCORD0eom6K4GCGj7ryLbX3Hdl6lG/8Oz2RsuE8N2/iQwHPnpJ5oWECtb/vTcuLEHyZinL6lJvU6I/ORraJuomTek6eyYnWxEdFDRFGimFHJKhGXoV8czzuzDwbDjZ16vrFJMjYj5QI+PX4t1MaCr0nTWUG6X2Kro17Vtm8j6+AMwj1IKDWGtiF+C3p2cf6UpBkjm9l83HasySxoWYwwPHdZpcOiSbS3179B6Wwh8eONtQSYh0uRgc51RjapRo1uydY9obUq1qPitdPCHgP+D5JlYfwh5ZqUDKU+dtet3tlX/l0zmEWLVlmYUtkBJ94N9bCKg6t4pxVX2qJHtgBW4eSUoHqkSLCXEt7RubDRg5dJ/e+PJ7SM1vYU8f/lMZY1XEtfyh8f+WgYSIlOBOTKFqX3VqZPhJLWA+dIpgWxd5P1/5R598lw0F+98HcmrVpXhxVOM4T8=";
    
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			this.architectView = new Wikitude.Architect.WTArchitectView ();
			this.architectView.Delegate = architectViewDelegate;
			this.architectView.ShouldAuthorizeRestrictedAPIs = false;
			this.View.AddSubview (this.architectView);
			this.architectView.TranslatesAutoresizingMaskIntoConstraints = false;

			NSDictionary views = new NSDictionary (new NSString ("architectView"), architectView);
			this.View.AddConstraints (NSLayoutConstraint.FromVisualFormat("|[architectView]|", 0, null, views));
			this.View.AddConstraints (NSLayoutConstraint.FromVisualFormat("V:|[architectView]|", 0, null, views));

            //architectView.SetLicenseKey ("NGBXvYi66YY4pT3CqeLvcv31N9xl4uasQSZwF5xPfJ5lcrI5leTkRrzzVihYTbEbRWRf9S9hWqkeCykxd1IgU/qbg5WJjwSK7dk9f/zHwlV1Qa/JYIB6l+sh2OjrdrXO7E9Qdqih1RYGF+3MDt7CC3BmMUrhkFanOvCf/eXMmX1TYWx0ZWRfX6PikQ4qQBjn7mRR7l4e36y3jrIqcuQfE4vdeKCDiD2pePwQ41U/FnA7HSShjqq9TcTpQaASuWQL+nnrKUU3ybpck+50zKokc0nK6tX0rjqAE3cKZJIXMV1VRszX2rUJFFzM80eMWNQ2FN6I3e0LlyY3gkAt05XUiTq4YaOVb62gRlytIPNvaxwFoj3Xvh5+vR4afdbKAgdAlxT4KLazRObTUBuYHWeKU9/cXR4RagzSDUt+mpYzEVpZTB8OjGFWKf+j+5kCRrQ/ra4gYIuf3KqYFy0JsuAeN2keaI5M34saqcTNSUV7Ng1V/ZjJg9Ac56TLC+D1FuMDdpZ6c3eWTsaccwc4tMmnyiA8Y60GqXIeFOClE1locWR0Fu/MXmOkoFSXGy/ldfzVOo756Mhb4xCSTvbN+PUKbyM9EYWrmj3Yu88wxglud9L+G8etmi+Mr1wO4SGCfIQdzu1Pt9go8QhZpIB7Nyk1IirWW99b10Kzh8rW1fj8ReVBddHb4SU5+r2/CmAUMrbohodJFBefpbagBhQ7EV8sg/1ylBYaNVXUi3bfCt437rcwNniWV6/Pm4thryejMMflAji9gp+TgioY4r1ex6LDzzzRrHGZ+Qypwrm41oxWpz5Gw4hbkeakbqhVT7AgTq2bvQ++Gkksaw5RO54rIzuh2QOu1Ad+XY81VHLYMnudcLzWuY3WcrQEqgQ/jRIGOH4ZywKTOqtXozF4Us85z5CWHfdESc2foe68ZbOem/pn7a9Hk+foWy9oFp8/0lEKjoVCZp264eeU7EsdQoHNnPDq8w3UZSfVGd3sOytAIQAkeFER/5GNQBXM+gsoCU3cd5Wrfvj9LHrC223CqdDENpyVc2uHc+RfyOAhXMZxtw0+Hi2etkJXkC+6y4ncxwKt8wlThhBtpDfgE0wB6wZ0sHLE+fmK7LOSUipj8qc7s6sD01la+xHL7yrcpWDJIJ46cgiycaVb7AWSW5c8wXpKK+i/JgI8IDVDPqMFZVfhFhfW8jg=");

            architectView.SetLicenseKey(WikiKey);

			NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, (notification) => {				
				StartAR();
			});

			NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.WillResignActiveNotification, (notification) => {
				StopAR();
			});
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (!authorizationRequestManager.RequestingRestrictedAppleiOSSDKAPIAuthorization)
			{
				NSOrderedSet<NSNumber> restrictedAppleiOSSKDAPIs = WTAuthorizationRequestManager.RestrictedAppleiOSSDKAPIAuthorizationsForRequiredFeatures(WTFeatures.WTFeature_ImageTracking);
				authorizationRequestManager.RequestRestrictedAppleiOSSDKAPIAuthorization(restrictedAppleiOSSKDAPIs, (bool success, NSError error) => {
					authorized = success;
					if (success)
					{
						StartAR();
					}
					else
					{
						handleAuthorizationError(error);
					}
				});
			}
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			StopAR ();
		}

		#endregion

		#region Rotation

		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate (toInterfaceOrientation, duration);

			architectView.SetShouldRotateToInterfaceOrientation (true, toInterfaceOrientation);
		}

		public override bool ShouldAutorotate()
		{
			return true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.All;
		}

		#endregion

		#region Segue
		[Action("UnwindToMainViewController")]
		public void UnwindToMainViewController()
		{ 
		}
		#endregion

		#region Private Methods
		private void handleAuthorizationError(NSError authorizationError)
		{
			NSDictionary unauthorizedAPIInfo = (NSDictionary)authorizationError.UserInfo.ObjectForKey(WTAuthorizationRequestManager.WTUnauthorizedAppleiOSSDKAPIsKey);

			NSMutableString detailedAuthorizationErrorLogMessage = (NSMutableString)new NSString("The following authorization states do not meet the requirements:").MutableCopy();
			NSMutableString missingAuthorizations = (NSMutableString)new NSString("In order to use the Wikitude SDK, please grant access to the following:").MutableCopy();
			foreach (NSString unauthorizedAPIKey in unauthorizedAPIInfo.Keys)
			{
				NSNumber unauthorizedAPIValue = (NSNumber)unauthorizedAPIInfo.ObjectForKey(unauthorizedAPIKey);
				detailedAuthorizationErrorLogMessage.Append(new NSString("\n"));
				detailedAuthorizationErrorLogMessage.Append(unauthorizedAPIKey);
				detailedAuthorizationErrorLogMessage.Append(new NSString(" = "));
				detailedAuthorizationErrorLogMessage.Append(WTAuthorizationRequestManager.StringFromAuthorizationStatusForUnauthorizedAppleiOSSDKAPI(unauthorizedAPIValue.Int32Value, unauthorizedAPIKey));

				missingAuthorizations.Append(new NSString("\n *"));
				missingAuthorizations.Append(WTAuthorizationRequestManager.HumanReadableDescriptionForUnauthorizedAppleiOSSDKAPI(unauthorizedAPIKey));
			}
			Console.WriteLine(detailedAuthorizationErrorLogMessage);

			UIAlertController settingsAlertController = UIAlertController.Create("Required API authorizations missing", missingAuthorizations, UIAlertControllerStyle.Alert);
			settingsAlertController.AddAction(UIAlertAction.Create("Open Settings", UIAlertActionStyle.Default, (UIAlertAction obj) =>
			{
				UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
			}));
			settingsAlertController.AddAction(UIAlertAction.Create("NO", UIAlertActionStyle.Destructive, (UIAlertAction obj) => { }));
			this.PresentViewController(settingsAlertController, true, null);
		}

		private void StartAR()
		{
			if (authorized)
			{
				if (!architectView.IsRunning)
				{
					architectView.Start((startupConfiguration) =>
					{
						// use startupConfiguration.CaptureDevicePosition = AVFoundation.AVCaptureDevicePosition.Front; to start the Wikitude SDK with an active front cam
						startupConfiguration.CaptureDevicePosition = AVFoundation.AVCaptureDevicePosition.Back;
						startupConfiguration.CaptureDeviceResolution = WTCaptureDeviceResolution.WTCaptureDeviceResolution_AUTO;
						startupConfiguration.TargetFrameRate = CMTime.PositiveInfinity; // resolves to WTMakeTargetFrameRateAuto();
					}, (bool isRunning, NSError startupError) => {
					   if (isRunning)
						{
							if (null == loadingArchitectWorldNavigation)
							{
								var path = NSBundle.MainBundle.BundleUrl.AbsoluteString + "1_ImageRecognition_1_ImageOnTarget/index.html";
								loadingArchitectWorldNavigation = architectView.LoadArchitectWorldFromURL(NSUrl.FromString(path), Wikitude.Architect.WTFeatures.WTFeature_ImageTracking);
							}

					   }
					   else
					   {
						   Console.WriteLine("Unable to start Wikitude SDK. Error (start ar): " + startupError.LocalizedDescription);
					   }
				   });
				}

				architectView.SetShouldRotateToInterfaceOrientation(true, UIApplication.SharedApplication.StatusBarOrientation);
			}
		}

		private void StopAR()
		{
			if (architectView.IsRunning)
			{
				architectView.Stop();
			}
		}
		#endregion
	}
}
